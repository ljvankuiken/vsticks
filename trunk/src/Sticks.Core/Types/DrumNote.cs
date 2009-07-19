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

namespace Sticks.Core
{
	/// <summary>
	/// Encapsulation of a note
	/// </summary>
	public class DrumNote
	{
		/// <summary>
		/// The drum voice of this note. If "Invalid" we probably have a MidiNote
		/// rather than a Voice.
		/// </summary>
		public Drums Voice
		{
			get { return _voice; }
			set { _voice = value; }
		}
		private Drums _voice;

		/// <summary>
		/// The MIDI velocity of this note.
		/// </summary>
		public int Velocity
		{
			get { return _velocity; }
			set { _velocity = value; }
		}
		private int _velocity;

		/// <summary>
		/// The timestamp for this note.
		/// </summary>		
		public long Timestamp
		{
			get { return _timestamp; }
			set { _timestamp = value; }
		}
		private long _timestamp;

		/// <summary>
		/// The MIDI note number of this note
		/// </summary>
		public int MidiNote
		{
			get { return _midiNote; }
			set { _midiNote = value; }
		}
		private int _midiNote;

		/// <summary>
		/// Create a note using our internal drum voices
		/// </summary>
		/// <param name="Voice">The drum voice</param>
		/// <param name="Velocity">Midi velocity</param>
		/// <param name="Timestamp">Note timestamp</param>
		public DrumNote(Drums Voice, int Velocity, long Timestamp)
		{
			this.Voice = Voice;
			this.Velocity = Velocity;
			this.Timestamp = Timestamp;
			this.MidiNote = 0; // XXX ...or something better
		}

		/// <summary>
		/// Create a note but with a MIDI note number
		/// </summary>
		/// <param name="Voice">The MIDI note number</param>
		/// <param name="Velocity">Midi velocity</param>
		/// <param name="Timestamp">Note timestamp</param>
		public DrumNote(int MidiNote, int Velocity, long Timestamp)
		{
			this.Voice = Drums.Invalid;
			this.MidiNote = MidiNote;
			this.Velocity = Velocity;
			this.Timestamp = Timestamp;
		}

	}


}
