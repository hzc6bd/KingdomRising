using System;

using Point = Microsoft.Xna.Framework.Point;

namespace KingdomRising {
	public static class SpriteArtist {
		public static Point UpperLeftCorner(Point center, int width, int height) {
			return new Point(center.X - width / 2, center.Y - height / 2);
		}
	}
}

