namespace Sticks
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuDebug = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuInputDevice = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuOutputDevice = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuDrumMap = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ButtonMidi = new System.Windows.Forms.ToolStripButton();
			this.ButtonStartStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.TempoBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.FadeSpeed = new System.Windows.Forms.ToolStripComboBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Status = new System.Windows.Forms.ToolStripStatusLabel();
			this.DrawTimer = new System.Windows.Forms.Timer(this.components);
			this.Notation = new Sticks.Core.DrumNotation();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(820, 24);
			this.menuStrip1.TabIndex = 8;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuDebug,
            this.MenuExit});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// MenuDebug
			// 
			this.MenuDebug.Name = "MenuDebug";
			this.MenuDebug.Size = new System.Drawing.Size(116, 22);
			this.MenuDebug.Text = "Debug";
			this.MenuDebug.Click += new System.EventHandler(this.ButtonDebug_Click);
			// 
			// MenuExit
			// 
			this.MenuExit.Name = "MenuExit";
			this.MenuExit.Size = new System.Drawing.Size(116, 22);
			this.MenuExit.Text = "E&xit";
			this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuInputDevice,
            this.MenuOutputDevice,
            this.MenuDrumMap});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// MenuInputDevice
			// 
			this.MenuInputDevice.Name = "MenuInputDevice";
			this.MenuInputDevice.Size = new System.Drawing.Size(180, 22);
			this.MenuInputDevice.Text = "MIDI Input Device";
			this.MenuInputDevice.Click += new System.EventHandler(this.MenuInputDevice_Click);
			// 
			// MenuOutputDevice
			// 
			this.MenuOutputDevice.Name = "MenuOutputDevice";
			this.MenuOutputDevice.Size = new System.Drawing.Size(180, 22);
			this.MenuOutputDevice.Text = "MIDI Output Device";
			this.MenuOutputDevice.Click += new System.EventHandler(this.MenuOutputDevice_Click);
			// 
			// MenuDrumMap
			// 
			this.MenuDrumMap.Name = "MenuDrumMap";
			this.MenuDrumMap.Size = new System.Drawing.Size(180, 22);
			this.MenuDrumMap.Text = "MIDI Drum Map";
			this.MenuDrumMap.Click += new System.EventHandler(this.MenuDrumMap_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuHelpAbout});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// MenuHelpAbout
			// 
			this.MenuHelpAbout.Name = "MenuHelpAbout";
			this.MenuHelpAbout.Size = new System.Drawing.Size(114, 22);
			this.MenuHelpAbout.Text = "&About";
			this.MenuHelpAbout.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonMidi,
            this.ButtonStartStop,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.TempoBox,
            this.toolStripLabel2,
            this.FadeSpeed});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(820, 25);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// ButtonMidi
			// 
			this.ButtonMidi.CheckOnClick = true;
			this.ButtonMidi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ButtonMidi.Image = ((System.Drawing.Image)(resources.GetObject("ButtonMidi.Image")));
			this.ButtonMidi.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonMidi.Name = "ButtonMidi";
			this.ButtonMidi.Size = new System.Drawing.Size(23, 22);
			this.ButtonMidi.Text = "Activate/Deactivate MIDI";
			this.ButtonMidi.CheckStateChanged += new System.EventHandler(this.ButtonMidi_CheckStateChanged);
			// 
			// ButtonStartStop
			// 
			this.ButtonStartStop.CheckOnClick = true;
			this.ButtonStartStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ButtonStartStop.Image = ((System.Drawing.Image)(resources.GetObject("ButtonStartStop.Image")));
			this.ButtonStartStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonStartStop.Name = "ButtonStartStop";
			this.ButtonStartStop.Size = new System.Drawing.Size(23, 22);
			this.ButtonStartStop.Text = "Start / Stop";
			this.ButtonStartStop.Click += new System.EventHandler(this.ButtonStartStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
			this.toolStripLabel1.Text = "Tempo";
			// 
			// TempoBox
			// 
			this.TempoBox.Items.AddRange(new object[] {
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100",
            "110",
            "120"});
			this.TempoBox.Name = "TempoBox";
			this.TempoBox.Size = new System.Drawing.Size(75, 25);
			this.TempoBox.Text = "60";
			this.TempoBox.ToolTipText = "Tempo";
			this.TempoBox.TextChanged += new System.EventHandler(this.TempoBox_TextChanged);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(31, 22);
			this.toolStripLabel2.Text = "Fade";
			// 
			// FadeSpeed
			// 
			this.FadeSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FadeSpeed.Items.AddRange(new object[] {
            "Very Slow",
            "Slow",
            "Medium",
            "Fast",
            "Very Fast"});
			this.FadeSpeed.Name = "FadeSpeed";
			this.FadeSpeed.Size = new System.Drawing.Size(75, 25);
			this.FadeSpeed.SelectedIndexChanged += new System.EventHandler(this.FadeSpeed_SelectedIndexChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status});
			this.statusStrip1.Location = new System.Drawing.Point(0, 162);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(820, 22);
			this.statusStrip1.TabIndex = 10;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// Status
			// 
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(38, 17);
			this.Status.Text = "Ready";
			// 
			// DrawTimer
			// 
			this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
			// 
			// Notation
			// 
			this.Notation.BackColor = System.Drawing.Color.White;
			this.Notation.BeatsPerBar = 4;
			this.Notation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Notation.Location = new System.Drawing.Point(0, 49);
			this.Notation.MaxNotes = 32;
			this.Notation.Name = "Notation";
			this.Notation.Size = new System.Drawing.Size(820, 113);
			this.Notation.TabIndex = 11;
			this.Notation.Tempo = 60;
			this.Notation.Text = "drumNotation1";
			this.Notation.TimeIndicator = ((long)(0));
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(820, 184);
			this.Controls.Add(this.Notation);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "vSticks";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MenuExit;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MenuHelpAbout;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton ButtonMidi;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MenuDrumMap;
		private System.Windows.Forms.ToolStripMenuItem MenuInputDevice;
		private System.Windows.Forms.ToolStripMenuItem MenuOutputDevice;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel Status;
		private Sticks.Core.DrumNotation Notation;
		private System.Windows.Forms.ToolStripMenuItem MenuDebug;
		private System.Windows.Forms.Timer DrawTimer;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox TempoBox;
		private System.Windows.Forms.ToolStripButton ButtonStartStop;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripComboBox FadeSpeed;

	}
}

