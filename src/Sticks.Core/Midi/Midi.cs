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
using System.Threading;
using System.Diagnostics;
using Sanford.Multimedia;
using Sanford.Multimedia.Midi;

namespace Sticks.Core
{
	/// <summary>
	/// An abstraction layer to ensure we're independent of whatever MIDI library we're using.
	/// Provides two basic functions, the ability to read incoming MIDI notes and the ability
	/// to send notes to the output MIDI device.
	/// </summary>
 	public class Midi
	{
		// Highest MIDI note number
		public const int MaxMidiNote = 127;

		// We run in a separate thread
		private Thread _thread;
		// ...and require thread synchronisation
		private object _lock = new object();
		// Our (Windows) input/output devices
		private InputDevice _inputDevice = null;
		private OutputDevice _outputDevice = null;
		// Flags so we know if we're connected or not
		private bool _inputConnected = false;
		private bool _outputConnected = false;
		// High resolution (hopefully) timer
		private Stopwatch _timer = new Stopwatch();
		// Errors
		private string _errorMessage = "";

		/// <summary>
		/// Enable/Disable MIDI output device
		/// </summary>
		public bool MidiOutEnabled
		{
			get
			{
				lock (_lock)
				{
					return _midiOutEnabled;
				}
			}
			set
			{
				lock (_lock)
				{
					_midiOutEnabled = value;
				}
			}
		}
		private bool _midiOutEnabled;

		/// <summary>
		/// Enable/Disable MIDI input device
		/// </summary>
		public bool MidiInEnabled
		{
			get
			{
				lock (_lock)
				{
					return _midiInEnabled;
				}
			}
			set
			{
				lock (_lock)
				{
					_midiInEnabled = value;
				}
			}
		}
		private bool _midiInEnabled;

		/// <summary>
		/// If True, MIDI messages recieved are forwarded to the MIDI output device.
		/// </summary>
		public bool MidiThrough
		{
			get { return _midiThrough; }
			set {
				lock (_lock)
				{
					_midiThrough = value; 					
				}
			}
		}
		private bool _midiThrough = true;

		/// <summary>
		/// Which MIDI channel should we send/receive on?
		/// </summary>
		public int MidiChannel
		{
			get { return _midiChannel; }
			set {
				lock (_lock)
				{
					_midiChannel = value;
				}
			}
		}
		private int _midiChannel = 9;

		/// <summary>
		/// MIDI input device number. We don't allow changing the device if it's already
		/// connected.
		/// </summary>
		public int MidiInDeviceId
		{
			get
			{
				lock (_lock)
				{
					return _midiInDeviceId;
				}
			}
			set
			{
				lock (_lock)
				{
					if(!_inputConnected)
						_midiInDeviceId = value;
				}
			}

		}
		private int _midiInDeviceId;

		/// <summary>
		/// MIDI ouptut device number. We don't allow changing the device if it's already
		/// connected.
		/// </summary>
		public int MidiOutDeviceId
		{
			get
			{
				lock (_lock)
				{
					return _midiOutDeviceId;
				}
			}
			set
			{
				lock (_lock)
				{
					if(!_outputConnected)
						_midiOutDeviceId = value;
				}
			}

		}
		private int _midiOutDeviceId;

		/// <summary>
		/// Get the current timestamp
		/// </summary> 
		public long Timestamp
		{
			get
			{
				lock (_lock)
				{
					return _timer.ElapsedTicks;
				}
			}
		}

		/// <summary>
		/// Indicates how many note messages are waiting in the received queue
		/// </summary>
		public int NotesReceived
		{
			get
			{
				lock (_lock)
				{
					return _notesReceived.Count;
				}
			}
		}
		private Queue<DrumNote> _notesReceived = new Queue<DrumNote>(20);

		/// <summary>
		/// True if we have a problem.
		/// </summary>
		public bool ErrorOccurred
		{
			get
			{
				lock (_lock)
				{
					return _errorMessage != string.Empty;
				}
			}
		}

		/// <summary>
		/// Description of the catastrophe which has just occurred
		/// </summary>
		public string ErrorMessage
		{
			get { return _errorMessage; }
		}

		/// <summary>
		/// Create a new MIDI handler
		/// </summary>
		public Midi()
		{
			_timer.Start();
			// Start our thread
			_thread = new Thread(MidiThread);
			_thread.IsBackground = true;
			_thread.Start();
		}

		/// <summary>
		/// A error has occured, shutdown everything and return us to an idle state.
		/// </summary>
		private void Error(string Message)
		{
			DisconnectOutput();
			DisconnectInput();
			lock (_lock)
			{
				_errorMessage = Message;
			}
		}

		/// <summary>
		/// Connect to the given MIDI output device
		/// </summary>
		private void ConnectOutput(int DeviceNumber)
		{
			try
			{
				_outputDevice = new OutputDevice(DeviceNumber);
				_outputConnected = true;
				MidiOutEnabled = true;
			}
			catch (Exception e)
			{
				Error(e.Message);
			}
		}

		/// <summary>
		/// Connect to the given MIDI input device
		/// </summary>
		private void ConnectInput(int DeviceNumber)
		{
			try
			{
				_inputDevice = new InputDevice(DeviceNumber);
				_inputDevice.ChannelMessageReceived += HandleChannelMessageReceived;
				_inputDevice.Error += new EventHandler<ErrorEventArgs>(HandleInputDeviceError);
				_inputDevice.SysCommonMessageReceived += HandleSysCommonMessageReceived;
				_inputDevice.SysExMessageReceived += HandleSysExMessageReceived;
				_inputDevice.SysRealtimeMessageReceived += HandleSysRealtimeMessageReceived;
				_inputConnected = true;
				MidiInEnabled = true;
				_inputDevice.StartRecording();
			}
			catch (Exception e)
			{
				Error(e.Message);
			}
		}

		/// <summary>
		/// Disconnect from the current output device.
		/// </summary>
		private void DisconnectOutput()
		{
			if (_outputConnected)
			{
				try
				{
					_outputDevice.Reset();
					_outputDevice.Close();
				}
				finally
				{
					_outputDevice = null;
					_outputConnected = false;
					MidiOutEnabled = false;
				}
			}
		}

		/// <summary>
		/// Disconnect from the current input device
		/// </summary> 
		private void DisconnectInput()
		{
			if (_inputConnected)
			{
				try
				{
					_inputDevice.StopRecording();
					_inputDevice.Reset();
					_inputDevice.Close();
				}
				finally
				{
					_inputDevice = null;
					_inputConnected = false;
					MidiInEnabled = false;
				}
			}
		}


		/// <summary>
		/// Handles errors from the MIDI input device
		/// </summary>
		private void HandleInputDeviceError(object sender, ErrorEventArgs e)
		{
			Error(e.Error.Message);
		}

		/// <summary>
		/// Handles MIDI channel messages, note on, note off, etc
		/// </summary>
		private void HandleChannelMessageReceived(object sender, ChannelMessageEventArgs e)
		{
			long time = Timestamp;

			// Pass on anything we receive?
			if (_midiThrough)
			{
				if(_outputDevice != null)
					_outputDevice.Send(e.Message);
			}
			
			// Only listen on our configured channel
			if (e.Message.MidiChannel == MidiChannel)
			{
				// Messages we're listening for
				// XXX we're ignoring aftertouch
				if (e.Message.Command == ChannelCommand.NoteOn)
				{
					DrumNote note = new DrumNote(e.Message.Data1, e.Message.Data2, time);
					lock(_lock)
					{
						_notesReceived.Enqueue(note);
					}
				}
			}
		}

		/// <summary>
		/// Handles MIDI Sys messages
		/// </summary>
		private void HandleSysCommonMessageReceived(object sender, SysCommonMessageEventArgs e)
		{
			// Pass on anything we receive?
			if (_midiThrough)
			{
				if (_outputDevice != null && _outputConnected)
					_outputDevice.Send(e.Message);
			}
		}
		
		/// <summary>
		/// Handles MIDI SysEx messages
		/// </summary>
		private void HandleSysExMessageReceived(object sender, SysExMessageEventArgs e)
		{
			// Pass on anything we receive?
			if (_midiThrough)
			{
				if (_outputDevice != null && _outputConnected)
					_outputDevice.Send(e.Message);
			}
		}

		/// <summary>
		/// Handles MIDI realtime messages
		/// </summary>
		private void HandleSysRealtimeMessageReceived(object sender, SysRealtimeMessageEventArgs e)
		{
			// Pass on anything we receive?
			if (_midiThrough)
			{
				if (_outputDevice != null && _outputConnected)
					_outputDevice.Send(e.Message);
			}
		}

		/// <summary>
		/// Play a note (or at least try to send one to the output device if connected).
		/// </summary>
		/// <param name="MidiNote">MIDI note number</param>
		/// <param name="Velocity">Velocity</param>
		public void PlayNote(int MidiNote, int Velocity)
		{
			if (_outputConnected)
			{
				// NoteOn
				ChannelMessage msg = 
					new ChannelMessage(ChannelCommand.NoteOn, MidiChannel, MidiNote, Velocity);
				_outputDevice.Send(msg);
				// NoteOff
				msg = new ChannelMessage(ChannelCommand.NoteOff, MidiChannel, MidiNote);
				_outputDevice.Send(msg);
			}
		}

		/// <summary>
		/// Returns a note from the notes received queue or null if no notes in the queue.
		/// Notes coming from the queue have their MidiNote property set but not their 
		/// Voice property, i.e. they require converting from MIDI to our internal drum voices
		/// via a DrumMap.
		/// </summary>
		public DrumNote GetNextNote()
		{
			lock (_lock)
			{
				if (_notesReceived.Count > 0)
					return _notesReceived.Dequeue();
			}
			return null;
		}

		/// <summary>
		/// Clears any error condition we have.
		/// </summary>
		public void ClearError()
		{
			_errorMessage = string.Empty;
		}

		/// <summary>
		/// Loop forever, receiving MIDI events and connecting/disconnecting as required.
		/// </summary>
		private void MidiThread()
		{
			bool shutdown = false;

			// Handle MIDI until we're shutdown
			while (!shutdown)
			{
				Thread.Sleep(1);
				if (ErrorOccurred) continue;

				// If not connected to input device but we should be, try to connect
				if (!_inputConnected && MidiInEnabled)
				{
					ConnectInput(MidiInDeviceId);
				}

				// If not connected to output device but we should be, try to connect
				if (!_outputConnected && MidiOutEnabled)
				{
					ConnectOutput(MidiOutDeviceId);
				}

				// If we are connected to input but should not be, disconnect
				if (_inputConnected && !MidiInEnabled)
				{
					DisconnectInput();
				}

				// If we are connected to output but should not be, disconnect
				if (_outputConnected && !MidiOutEnabled)
				{
					DisconnectOutput();
				}

			}
		}


	}


}
