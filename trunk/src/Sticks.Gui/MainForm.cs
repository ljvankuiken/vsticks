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
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using Sanford.Multimedia.Midi.UI;
using Sticks.Properties;
using Sticks.Core;

namespace Sticks
{
	public partial class MainForm : Form
	{
		// Our Midi layer
		public Midi _midi;
		private int _in = Settings.Default.MidiInputDevice;
		private int _out = Settings.Default.MidiOutputDevice;
		// Our drum map
		private DrumMap _drumMap = new DrumMap();

		public MainForm()
		{
			InitializeComponent();
			_midi = new Midi();
			FadeSpeed.SelectedIndex = 3;
		}

		/// <summary>
		/// Activate/Deactive MIDI
		/// </summary>
		private void ButtonMidi_CheckStateChanged(object sender, EventArgs e)
		{
			if (ButtonMidi.Checked)
			{
				if (_in == -1)
				{
					throw new Exception("No input device selected");
				}
				if (_out == -1)
				{
					throw new Exception("No output device selected");
				}

				_midi.MidiInDeviceId = _in;
				_midi.MidiOutDeviceId = _out;
				_midi.MidiInEnabled = true;
				_midi.MidiOutEnabled = true;
			}
			else
			{
				_midi.MidiInEnabled = false;
				_midi.MidiOutEnabled = false;
			}

		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			// Activate MIDI immediately if already configured
			if (_in != -1 && _out != -1)
			{
				ButtonMidi.Checked = true;
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Deactivate MIDI if required
			if (ButtonMidi.Checked)
			{
				ButtonMidi.Checked = false;
			}
		}

		private void AddTestNotes()
		{
			for (int j = 0; j < 16; j++)
			{
				Notation.Bar.Add(new DrumNote(Drums.Snare, 127, j * (Notation.MicrosecondsPerBar / 16)));
			}
		}

		private void ButtonDebug_Click(object sender, EventArgs e)
		{
			AddTestNotes();
			this.Text = Stopwatch.Frequency.ToString()+" "+Stopwatch.IsHighResolution.ToString();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutDialog about = new AboutDialog();
			about.ShowDialog();
		}

		private void TempoBox_TextChanged(object sender, EventArgs e)
		{
			int tempo;
			if (int.TryParse(TempoBox.Text, out tempo))
			{
				Notation.Tempo = tempo;
			}
		}

		// Count in then commence...
		private void Start()
		{
			// Clear overlays
			Notation.ClearOverlayNotes();

			// Count in

			// Start
			DrawTimer.Enabled = true;
		}

		private void Stop()
		{
			DrawTimer.Stop();
		}

		private void ButtonStartStop_Click(object sender, EventArgs e)
		{
			if (ButtonStartStop.Checked)
			{
				Start();
			}
			else
			{
				Stop();
			}
		}

		private void FadeSpeed_SelectedIndexChanged(object sender, EventArgs e)
		{
			Notation.MaxNotes = 100 - (FadeSpeed.SelectedIndex * 24);
		}

		private void MenuExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void MenuDrumMap_Click(object sender, EventArgs e)
		{
			// Stop processing midi notes ourself
			DrawTimer.Enabled = false;
			MapForm map = new MapForm(_midi);
			if (map.ShowDialog() == DialogResult.OK)
			{
				_drumMap.Load();
			}
			DrawTimer.Enabled = true;
		}

		private void DrawTimer_Tick(object sender, EventArgs e)
		{
			Notation.TimeIndicator = _midi.Timestamp;
			// Maybe we have an error?
			if (_midi.ErrorOccurred)
			{
				MessageBox.Show(_midi.ErrorMessage);
				_midi.ClearError();
			}
			// Process any notes we have received
			while (_midi.NotesReceived > 0)
			{
				DrumNote note = _midi.GetNextNote();
				note.Voice = _drumMap.MidiToDrum(note.MidiNote);
				Notation.AddOverlayNote(note);
			}
		}

		private void MenuInputDevice_Click(object sender, EventArgs e)
		{
			InputDeviceDialog input = new InputDeviceDialog();
			if (input.ShowDialog() == DialogResult.OK)
			{
				_in = input.InputDeviceID;
				Settings.Default.MidiInputDevice = _in;
				Settings.Default.Save();
			}
		}

		private void MenuOutputDevice_Click(object sender, EventArgs e)
		{
			OutputDeviceDialog output = new OutputDeviceDialog();
			if (output.ShowDialog() == DialogResult.OK)
			{
				_out = output.OutputDeviceID;
				Settings.Default.MidiOutputDevice = _out;
				Settings.Default.Save();
			}
		}

	}
}
