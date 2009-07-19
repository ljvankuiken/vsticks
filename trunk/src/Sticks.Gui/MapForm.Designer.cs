namespace Sticks
{
	partial class MapForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
			this.ButtonSave = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.Status = new System.Windows.Forms.Label();
			this.Map = new Sticks.Core.ImageMap();
			this.timer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
			this.SuspendLayout();
			// 
			// ButtonSave
			// 
			this.ButtonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ButtonSave.Location = new System.Drawing.Point(358, 327);
			this.ButtonSave.Name = "ButtonSave";
			this.ButtonSave.Size = new System.Drawing.Size(75, 23);
			this.ButtonSave.TabIndex = 0;
			this.ButtonSave.Text = "Save";
			this.ButtonSave.UseVisualStyleBackColor = true;
			this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(439, 327);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 1;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(372, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Click on a pad in the picture below, then strike the actual pad on the drum kit.";
			// 
			// Status
			// 
			this.Status.AutoSize = true;
			this.Status.Location = new System.Drawing.Point(12, 327);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(0, 13);
			this.Status.TabIndex = 5;
			// 
			// Map
			// 
			this.Map.Image = global::Sticks.Properties.Resources.MappingImage;
			this.Map.Location = new System.Drawing.Point(15, 36);
			this.Map.Name = "Map";
			this.Map.Size = new System.Drawing.Size(499, 275);
			this.Map.TabIndex = 4;
			this.Map.TabStop = false;
			this.Map.RegionClick += new Sticks.Core.ImageMap.RegionClickDelegate(this.Map_RegionClick);
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// MapForm
			// 
			this.AcceptButton = this.ButtonSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(526, 362);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.Map);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonSave);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MapForm";
			this.Text = "Mapping";
			((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ButtonSave;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.Label label1;
		private Sticks.Core.ImageMap Map;
		private System.Windows.Forms.Label Status;
		private System.Windows.Forms.Timer timer;
	}
}