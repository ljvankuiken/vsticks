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
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sticks.Core
{
	/// <summary>
	/// A Bar, a collection of notes
	/// </summary>
	public class Bar : CollectionBase
	{
		public virtual void Add(DrumNote NewNote)
		{
			this.List.Add(NewNote);
		}
		public virtual DrumNote this[int Index]
		{
			get
			{
				return (DrumNote)this.List[Index];
			}
		}
	}
}
