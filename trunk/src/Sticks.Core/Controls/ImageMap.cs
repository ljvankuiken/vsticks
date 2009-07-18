using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sticks.Core
{
	/// <summary>
	/// Summary description for ImageMap.
	/// </summary>
	[ToolboxBitmap(typeof(ImageMap))]
	public class ImageMap : PictureBox
	{
		private GraphicsPath _pathData;
		private int _activeIndex = -1;
		private ArrayList _pathsArray;
		private ToolTip _toolTip;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components;

		public delegate void RegionClickDelegate(int index, string key);

		[Category("Action")]
		public event RegionClickDelegate RegionClick;

		public ImageMap()
		{
			_pathsArray = new ArrayList();
			_pathData = new GraphicsPath();
			_pathData.FillMode = FillMode.Winding;

			components = new Container();
			_toolTip = new ToolTip(components);
			_toolTip.AutoPopDelay = 5000;
			_toolTip.InitialDelay = 1000;
			_toolTip.ReshowDelay = 500;
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		public int ActiveIndex
		{
			get { return _activeIndex; }
		}

		public int AddElipse(string key, Point center, int radius)
		{
			return AddElipse(key, center.X, center.Y, radius);
		}

		public int AddElipse(string key, int x, int y, int radius)
		{
			if (_pathsArray.Count > 0)
				_pathData.SetMarkers();
			_pathData.AddEllipse(x - radius, y - radius, radius * 2, radius * 2);
			return _pathsArray.Add(key);
		}

		public int AddRectangle(string key, int x1, int y1, int x2, int y2)
		{
			return AddRectangle(key, new Rectangle(x1, y1, (x2 - x1), (y2 - y1)));
		}

		public int AddRectangle(string key, Rectangle rectangle)
		{
			if (_pathsArray.Count > 0)
				_pathData.SetMarkers();
			_pathData.AddRectangle(rectangle);
			return _pathsArray.Add(key);
		}

		public int AddPolygon(string key, Point[] points)
		{
			if (_pathsArray.Count > 0)
				_pathData.SetMarkers();
			_pathData.AddPolygon(points);
			return _pathsArray.Add(key);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			int newIndex = GetActiveIndexAtPoint(new Point(e.X, e.Y));
			if (newIndex > -1)
			{
				Cursor = Cursors.Hand;
				if (_activeIndex != newIndex)
					_toolTip.SetToolTip(this, _pathsArray[newIndex].ToString());
			}
			else
			{
				Cursor = Cursors.Default;
				_toolTip.RemoveAll();
			}
			_activeIndex = newIndex;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			_activeIndex = -1;
			Cursor = Cursors.Default;
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);

			Point p = PointToClient(Cursor.Position);
			if (_activeIndex == -1)
				GetActiveIndexAtPoint(p);
			if (_activeIndex > -1 && RegionClick != null)
				RegionClick(_activeIndex, _pathsArray[_activeIndex].ToString());
		}


		public GraphicsPath GetPath(int index)
		{
			var path = new GraphicsPath();
			var iterator = new GraphicsPathIterator(_pathData);
			iterator.Rewind();
			for (int current = 0; current < iterator.SubpathCount; current++)
			{
				iterator.NextMarker(path);
				if (current == index)
					return path;
			}
			return null;
		}

		[Browsable(false)]
		public override Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}


		private int GetActiveIndexAtPoint(Point point)
		{
			var path = new GraphicsPath();
			var iterator = new GraphicsPathIterator(_pathData);
			iterator.Rewind();
			for (int current = 0; current < iterator.SubpathCount; current++)
			{
				iterator.NextMarker(path);
				if (path.IsVisible(point, CreateGraphics()))
					return current;
			}
			return -1;
		}
	}
}