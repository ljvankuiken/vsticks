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

namespace Sticks.Core
{
	/// <summary>
	/// Helper for various configuration tasks.
	/// </summary>
	public class Config
	{
		private const string BaseDir = "vSticks";

		/// <summary>
		/// Return the path to where we should be saving our config. Create if needed.
		/// </summary>
		public static string GetSettingsFolder(string Filename)
		{
			// We assume we don't want user specific settings
			string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			path = Path.Combine(path,BaseDir);
			Directory.CreateDirectory(path);
			return path+Path.DirectorySeparatorChar+Filename;
		}

	}
}
