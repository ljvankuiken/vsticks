/* vSticks - Virtual Drum Practice
 * Copyright (c) 2009, Graham R King.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;

namespace Sticks.Core
{	
	/// <summary>
	/// Translate Midi note numbers to our internal drum voices (and visa-versa) 
	/// allowing custom MIDI mapping to support a variety of devices.
	/// 
	/// We want to provide support for these maps to be saved and easily shared.
	/// </summary>
	public class DrumMap
	{
		private XmlDocument _config;

		private int[] _drumToMidi;
		private int[] _midiToDrum;

		// Default file for settings
		private const string DefaultMapFile = "DrumMap.xml";

		public DrumMap()
		{
			// Load default map
			Load();
		}

		/// <summary>
		/// Load a drum map from the given file. If no filename is given use the default
		/// </summary>
		public void Load(string Filename)
		{
			if (Filename == string.Empty)
			{
				Filename = DefaultMapFile;
			}
			if (!Path.IsPathRooted(Filename))
			{
				Filename = Config.GetSettingsFolder(Path.GetFileName(Filename));
			}
			FileStream file;
			try
			{
				file = new FileStream(Filename, FileMode.Open, FileAccess.Read);
			}
			catch (FileNotFoundException)
			{
				CreateDefault();
				return;
			}
			_config = new XmlDocument();
			XmlTextReader reader = new XmlTextReader(file);
			_config.Load(reader);
			reader.Close();
			file.Close();
			CalculateDrumMap();
		}
		
		/// <summary>
		/// Load the default drum map
		/// </summary>
		public void Load()
		{
			Load("");
		}

		/// <summary>
		/// Save the current drum map to the given file. Save as default if no filename
		/// given.
		/// </summary>
		public void Save(string Filename)
		{
			if (Filename == string.Empty)
			{
				Filename = DefaultMapFile;
			}
			if (!Path.IsPathRooted(Filename))
			{
				Filename = Config.GetSettingsFolder(Path.GetFileName(Filename));
			}
			FileStream file = new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write);
			file.SetLength(0);
			XmlTextWriter writer = new XmlTextWriter(file, new UnicodeEncoding());
			_config.Save(writer);
			writer.Close();
			file.Close();
		}
		
		/// <summary>
		/// Save the current drum map as default
		/// </summary>
		public void Save()
		{
			Save("");
		}

		/// <summary>
		/// Convert the given drum voice into a Midi note number
		/// </summary>
		public int DrumToMidi(Drums Drum)
		{
			return _drumToMidi[(int)Drum];
		}

		/// <summary>
		/// Convert the given midi note number into a drum voice
		/// </summary>
		public Drums MidiToDrum(int MidiNote)
		{
			return (Drums)_midiToDrum[MidiNote];
		}

		/// <summary>
		/// Create a new mapping between the given drum voice and the given Midi note.
		/// </summary>
		public void MidiMap(Drums Drum, int MidiNote)
		{
			SetMidi(Drum, MidiNote);
		}

		/// <summary>
		/// Update our internal drum map based on the currently loaded config file.
		/// </summary>
		private void CalculateDrumMap()
		{
			// Populate our map - drum to midi
			_drumToMidi = new int[Drums.GetValues(typeof(Drums)).Length];
			foreach (Drums drum in Drums.GetValues(typeof(Drums)))
			{
				_drumToMidi[(int)drum] = GetMidi(drum);
			}

			// Populate the inverse, midi to drum
			_midiToDrum = new int[Midi.MaxMidiNote+1]; 
			for (int i = 0; i < _drumToMidi.Length; i++)
			{
				_midiToDrum[_drumToMidi[i]] = i;
			}
		}

		/// <summary>
		/// Return the MIDI note number for the given drum from our config.
		/// </summary>
		private int GetMidi(Drums Drum)
		{
			// Sanity check
			if (!Enum.IsDefined(typeof(Drums), Drum))
			{
				throw new Exception("Invalid drum voice");
			}

			if (_config != null)
			{
				XmlNodeList nodes = _config.SelectNodes(@"/DrumMap/Map");
				foreach (XmlNode node in nodes)
				{
					if (node.Attributes["Voice"].Value.Equals(Drum.ToString()))
					{
						int midi;
						if (int.TryParse(node.Attributes["Midi"].Value, out midi))
						{
							return midi;
						}
					}
				}
			}
			return 0; // XXX Or something better
		}

		/// <summary>
		/// Set the given drum to point to the given Midi note number in our map.
		/// </summary>
		private void SetMidi(Drums Drum, int MidiNote)
		{
			// Create a new config if we haven't loaded one
			if (_config == null)
			{
				CreateDefault();
			}

			XmlNodeList nodes = _config.SelectNodes(@"/DrumMap/Map");
			// Update setting if we already have it
			bool updated = false;
			foreach (XmlNode node in nodes)
			{
				if (node.Attributes["Voice"].Value.Equals(Drum.ToString()))
				{
					node.Attributes["Midi"].Value = MidiNote.ToString();
					updated = true;
				}
			}
			// Create new setting if requried
			if (!updated)
			{
				XmlNode parentNode = _config.SelectSingleNode(@"/DrumMap");
				XmlNode setting = _config.CreateElement("Map");
				setting.Attributes.Append(_config.CreateAttribute("Voice"));
				setting.Attributes.Append(_config.CreateAttribute("Midi"));
				setting.Attributes["Voice"].Value = Drum.ToString();
				setting.Attributes["Midi"].Value = MidiNote.ToString();
				parentNode.AppendChild(setting);
			}
			// Update the drum map
			CalculateDrumMap();
		}

		/// <summary>
		/// Create a default, empty drum map.
		/// </summary>
		private void CreateDefault()
		{
			_config = new XmlDocument();
			_config.CreateXmlDeclaration("1.0", null, "yes");
			XmlElement root = _config.CreateElement("DrumMap");
			_config.AppendChild(root);
			CalculateDrumMap();
		}
	}
}
