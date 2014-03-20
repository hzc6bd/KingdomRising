using System;
using Size = System.Drawing.Size;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingdomRising {
	public abstract class ModelBasedSprite {
		protected Texture2D texture;
		public Point drawPoint;

		public ModelBasedSprite(Texture2D texture) {
			SetTexture (texture);
		}

		protected void SetDrawPoint(Point center, Size size) {
			this.drawPoint = UpperLeftCorner (center, size);
		}

		protected static Point UpperLeftCorner(Point center, Size size) {
			return new Point(center.X - size.Width / 2, center.Y - size.Height / 2);
		}

		protected void SetTexture(Texture2D texture) {
			this.texture = texture;
		}

		public abstract void Draw (SpriteBatch batch);
	}
}
