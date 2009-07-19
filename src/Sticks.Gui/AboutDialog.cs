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
using System.Reflection;

namespace Sticks
{
	public partial class AboutDialog : Form
	{
		public AboutDialog()
		{
			const string ReleaseDate = "1st Aug 2009";

			InitializeComponent();
			int major = Assembly.GetExecutingAssembly().GetName().Version.Major;
			int minor = Assembly.GetExecutingAssembly().GetName().Version.Minor;
			int build = Assembly.GetExecutingAssembly().GetName().Version.Build;
			Version.Text = String.Format("Version {0}.{1}.{2} ({3})", major, minor, build,ReleaseDate);
		}
	}
}
