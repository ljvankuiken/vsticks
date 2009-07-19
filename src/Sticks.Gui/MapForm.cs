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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sticks.Core;

namespace Sticks
{
	public partial class MapForm : Form
	{
		// Our drum map
		private DrumMap _drumMap;
		// Indicates which drum we are waiting for
		private int _mapInput = -1;
		// Map of regions to drum voices
		private int[] _visualMap;
		// Our Midi layer
		private Midi _midi;

		/// <summary>
		/// Allow editing of a drum map
		/// </summary>
		public MapForm(Midi Midi)
		{
			_midi = Midi;
			// Load the default map
			_drumMap = new DrumMap();
			_drumMap.Load();
			_visualMap = new int[Drums.GetValues(typeof(Drums)).Length];
			InitializeComponent();
			AddRegion(Drums.Snare,"Snare Drum", 185, 96, 185 + 57, 96 + 38);
			AddRegion(Drums.Tom1,"High Tom", 205, 48, 205 + 47, 48 + 40);
			AddRegion(Drums.Tom2, "Mid Tom", 258, 50, 258 + 50, 50 + 36);
			AddRegion(Drums.Tom3,"Low Tom", 289, 96, 289 + 55, 96 + 36);
			AddRegion(Drums.Bass, "Bass Drum", 237, 137, 237 + 37, 137 + 96);
			AddRegion(Drums.HiHatClosed, "Hi-hat", 116, 58, 116 + 67, 58 + 43);
			AddRegion(Drums.HiHatFoot, "Hi-hat Foot", 141, 222, 141 + 65, 222 + 43);
			AddRegion(Drums.Crash, "Crash", 155, 12, 155 + 60, 12 + 36);
			AddRegion(Drums.Ride, "Ride", 295, 10, 295 + 57, 10 + 32);
			timer.Enabled = true;
		}

		private void AddRegion(Drums Drum, string Hover, int x, int y, int x2, int y2)
		{
			int index = Map.AddRectangle(Hover, x, y, x2, y2);
			_visualMap[index] = (int)Drum;
		}						  

		private void Map_RegionClick(int index, string key)
		{
			Status.Text = key + " selected, hit pad on drum kit...";
			_mapInput = index;
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			_drumMap.Save();
			Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			// If we have incoming notes
			if (_midi.NotesReceived > 0)
			{
				// are we expecting incoming notes?
				if (_mapInput >= 0)
				{
					// yep, so map this input
					DrumNote note = _midi.GetNextNote();
					Status.Text = "OK, got mapping for " + ((Drums)_visualMap[_mapInput]) + " (" + note.MidiNote + ")";
					_drumMap.MidiMap((Drums)_visualMap[_mapInput], note.MidiNote);
					_mapInput = -1;
				}
				else
				{
					// discard notes from incoming note queue
					while (_midi.NotesReceived > 0)
						_midi.GetNextNote();
				}

			}
		}


	}
}
