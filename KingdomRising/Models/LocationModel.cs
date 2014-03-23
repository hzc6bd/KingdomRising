using System;
using System.Drawing;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Point = Microsoft.Xna.Framework.Point;

namespace KingdomRising {
	public class LocationModel {
		// Identifier
		public int ID;

		// Physical Information
		public Point center;
		public Size size;

		// Game Information
		public int population;
		public int rate;
		public int playerID;
		public HashSet<int[]> routes;

		public LocationModel (int ID, int x, int y) {
			this.center = new Point (x, y);
			this.size = new Size (Dimensions.LOCATION_WIDTH, 
				Dimensions.LOCATION_HEIGHT);
			this.ID = ID;
			this.population = 10;
			this.routes = new HashSet<int[]>();
		}

		public bool contains(Point point) {
			return point.X > center.X - size.Width / 2 &&
				point.X < center.X + size.Width / 2 &&
				point.Y > center.Y - size.Height / 2 &&
				point.Y < center.Y + size.Height / 2;
		}
	}
}

