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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sticks.Core
{

	/// <summary>
	/// Graphical control to display a bar of drum notation.
	/// </summary>
	public class DrumNotation : Control
	{
		// Stave position for each voice
		private int[] _drumToStave;

		/// <summary>
		/// Our note information - a bar
		/// </summary>
		public Bar Bar
		{
			get { return _bar; }
		}
		private Bar _bar = new Bar();

		/// <summary>
		/// Our overlay note data
		/// </summary>
		public Bar BarOverlay
		{
			get { return _barOverlay; }
		}
		private Bar _barOverlay = new Bar();

		/// <summary>
		/// Tempo
		/// </summary>
		public int Tempo
		{
			get { return _tempo; }
			set
			{
				if (value > 0)
				{
					_tempo = value;
					UpdateTimeSignature();
				}
			}
		}
		private int _tempo;

		/// <summary>
		/// Time Signature - Beats per bar
		/// </summary>
		public int BeatsPerBar
		{
			get { return _beatsPerBar; }
			set { 
				_beatsPerBar = value;
				UpdateTimeSignature();
			}
		}
		private int _beatsPerBar;

		/// <summary>
		/// Number of notes to display in the overlay before fading them
		/// </summary>
		public int MaxNotes
		{
			get { return _maxNotes; }
			set { _maxNotes = value; }
		}
		private int _maxNotes;

		/// <summary>
		/// Microseconds Per Bar
		/// </summary>
		public long MicrosecondsPerBar
		{
			get { return _uSecPerBar; }
		}
		private long _uSecPerBar;

		/// <summary>
		/// Ticks (of the high resolution timer) per bar
		/// </summary>
		public long TicksPerBar
		{
			get { return _ticksPerBar; }
		}
		private long _ticksPerBar;

		/// <summary>
		/// Current time indicator in ticks
		/// </summary> 
		public long TimeIndicator
		{
			get { return _timeIndicator; }
			set {
				_timeIndicator = value % _ticksPerBar;
				this.Invalidate();
			}
		}
		private long _timeIndicator;

		public DrumNotation()
		{
			InitialiseComponent();
		}

		/// <summary>
		/// Setup our control
		/// </summary>
		private void InitialiseComponent()
		{
			// Redraw on resize	and double buffer
			this.SetStyle(
			  ControlStyles.ResizeRedraw |
			  ControlStyles.AllPaintingInWmPaint |
			  ControlStyles.UserPaint |
			  ControlStyles.DoubleBuffer, true);
			// Default Beats per bar
			BeatsPerBar = 4;
			// Default tempo
			Tempo = 60;
			// Default history
			MaxNotes = 32;

			// Drum to stave position
			// We number our stave positions from 0 to 14. Zero being the top space.
			// XXX We should allow customisation of this, there are so many differing ways
			// XXX of representing drum notation.
			_drumToStave = new int[Drums.GetValues(typeof(Drums)).Length];
			_drumToStave[(int)Drums.Bass] = 10;
			_drumToStave[(int)Drums.Crash] = 1;
			_drumToStave[(int)Drums.CrashRim] = 1;
			_drumToStave[(int)Drums.HiHatClosed] = 2;
			_drumToStave[(int)Drums.HiHatFoot] = 12;
			_drumToStave[(int)Drums.HiHatOpen] = 2;
			_drumToStave[(int)Drums.Ride] = 3;
			_drumToStave[(int)Drums.RideRim] = 3;
			_drumToStave[(int)Drums.Snare] = 6;
			_drumToStave[(int)Drums.SnareRim] = 6;
			_drumToStave[(int)Drums.Tom1] = 4;
			_drumToStave[(int)Drums.Tom1Rim] = 4;
			_drumToStave[(int)Drums.Tom2] = 5;
			_drumToStave[(int)Drums.Tom2Rim] = 5;
			_drumToStave[(int)Drums.Tom3] = 8;
			_drumToStave[(int)Drums.Tom3Rim] = 8;
			
		}

		/// <summary>
		/// Render the control
		/// </summary>
		protected override void OnPaint(PaintEventArgs pe)
		{
			// Draw in black
			System.Drawing.Pen pen = new System.Drawing.Pen(Color.Black);
			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.Black);
			// Time is highlighted
			System.Drawing.Pen penTime = new System.Drawing.Pen(Color.Red);
			penTime.Width = 2;

			// Where are we drawing?
			Graphics g = pe.Graphics;

			// We divide the display into eight stripes
			int stripes = 8;
			int lineHeight = (this.Height - 1) / stripes;

			// Render the notation lines
			for (int i = 2; i < stripes-1; i++)
			{
				g.DrawLine(pen, 0, i * lineHeight, this.Width - 1, i * lineHeight);
			}
			
			// Render bar lines	if there are any
			g.DrawLine(pen, 0, 2 * lineHeight, 0, 6 * lineHeight);
			g.DrawLine(pen, this.Width - 1, 2 * lineHeight, this.Width - 1, 6 * lineHeight);
			int barWidth = this.Width - 1;

			// Change to smooth rendering
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

			// Render background notes
			for (int i = 0; i < Bar.Count; i++)
			{
				DrumNote n = Bar[i];
				int noteHeight = (int)(lineHeight) - 1;
				int noteWidth = (int)(noteHeight * 1.4);
				int noteX = (int)(((double)(n.Timestamp + (MicrosecondsPerBar / 32)) / MicrosecondsPerBar) * barWidth);
				int noteY = (lineHeight * 4) + 1;
				noteX = (int)(noteX - (noteWidth / 2)); // adjust for centre
				g.FillEllipse(brush, noteX, noteY, noteWidth, noteHeight);
			}

			// Render overlay notes
			for (int i = 0; i < BarOverlay.Count; i++)
			{
				DrumNote n = BarOverlay[i];
				System.Drawing.SolidBrush brushOverlay = new System.Drawing.SolidBrush(Color.FromArgb(n.Velocity * 2, 0, 0, 127));
				int noteHeight = (int)(lineHeight) - 1;
				int noteWidth = (int)(noteHeight * 1.4);
				int noteX = (int)(((double)n.Timestamp / _ticksPerBar) * barWidth);
				int noteY = (int)((((double)lineHeight / 2) * _drumToStave[(int)n.Voice])) + 1;
				noteX = (int)(noteX - (noteWidth / 2)); // adjust for centre
				g.FillEllipse(brushOverlay, noteX, noteY, noteWidth, noteHeight);
				brushOverlay.Dispose();
			}

			// Render time indicator
			int timeX = (int)(((double)TimeIndicator / _ticksPerBar) * barWidth);
			g.DrawLine(penTime, timeX, 1 * lineHeight, timeX, 7 * lineHeight);

			// Free our resources
			pen.Dispose();
			penTime.Dispose();
			brush.Dispose();
		}

		/// <summary>
		/// Add a note to the overlay notes
		/// </summary>
		public void AddOverlayNote(DrumNote note)
		{
			FadeOverlayNotes();
			note.Timestamp = (long)(note.Timestamp % _ticksPerBar);
			BarOverlay.Add(note);
			this.Invalidate();
		}

		/// <summary>
		/// Fade out overlay notes, remove them once invisible.
		/// </summary>
		private void FadeOverlayNotes()
		{
			int notes = BarOverlay.Count;
			if (notes > MaxNotes)
			{
				int fade = (notes - MaxNotes);
				int i = 0;
				while (i < BarOverlay.Count && i <= fade)
				{
					DrumNote n = BarOverlay[i];
					n.Velocity -= 4;
					if (n.Velocity < 0)
					{
						BarOverlay.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
		}

		/// <summary>
		/// Clear all overlay notes
		/// </summary>
		public void ClearOverlayNotes()
		{
			BarOverlay.Clear();
		}

		/// <summary>
		/// If we changed parameters which effect time, we need to recalculate some things.
		/// </summary>
		private void UpdateTimeSignature()
		{
			// How many microseconds in a bar?
			double barsPerSecond = ((double)_tempo / 60) / BeatsPerBar;
			_uSecPerBar = (long)(1000000 / barsPerSecond);
			_ticksPerBar = (long)((double)Stopwatch.Frequency / barsPerSecond);
		}

	}
}
