using System;

using Microsoft.Xna.Framework;

namespace KingdomRising {
	public class RoadModel {
		public int x;
		public int y;
		public float rotation;
		public float length;

		public RoadModel (int a, int b) {
			Point locationA = KingdomRising.country.locations[a].center;
			Point locationB = KingdomRising.country.locations [b].center;
			this.trig (locationA, locationB);
			this.x = locationA.X;
			this.y = locationA.Y;
		}

		public void trig(Point a, Point b) {
			float deltaY = (float)(a.Y - b.Y);
			float deltaX = (float)(a.X - b.X);
			this.rotation = (float) Math.Atan(deltaY / deltaX);
			this.rotation += (deltaX >= 0) ? (float) Math.PI : 0;
			this.length = (float) Math.Sqrt (Math.Pow (deltaX, 2) + Math.Pow (deltaY, 2));
		}
	}
}

