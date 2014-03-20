using System;
using System.Drawing;

using Microsoft.Xna.Framework;
using Point = Microsoft.Xna.Framework.Point;

namespace KingdomRising {
	public class LocationModel {
		public Point center;
		public Size size;
		public int id;

		public LocationModel (int id, int x, int y) {
			this.center = new Point (x, y);
			this.size = new Size (Dimensions.LOCATION_WIDTH, 
				Dimensions.LOCATION_HEIGHT);
			this.id = id;
		}

		public bool contains(Point point) {
			return point.X > center.X - size.Width / 2 &&
				point.X < center.X + size.Width / 2 &&
				point.Y > center.Y - size.Height / 2 &&
				point.Y < center.Y + size.Height / 2;
		}
	}
}

