using System;
using Point = Microsoft.Xna.Framework.Point;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace KingdomRising {
	public class CounterSprite {
		private NumberAtlas atlas;
		public Point referencePoint;
		private Point drawPoint;
		int digits;
		int count;
		
		public CounterSprite (Point reference) {
			this.referencePoint = reference;
			this.atlas = new NumberAtlas ();
			this.digits = 1;
			this.digits = 0;
		}

		public void increment() {
			this.setCount (count + 1);
		}

		public void setCount(int count) {
			int oldDigits = this.count.ToString().Length;
			int newDigits = count.ToString().Length;
			this.count = count;

			if (oldDigits != newDigits) {
				digits = newDigits;
				int drawX = (int)(referencePoint.X - Dimensions.NUMBER_WIDTH * (double) digits / 2);
				int drawY = (int)(referencePoint.Y - Dimensions.NUMBER_HEIGHT / 2);
				drawPoint = new Point (drawX, drawY);
			}
		}

		public void Draw(SpriteBatch batch) {
			for(int i = 0; i < digits; i++) {
				int digit = (int)Char.GetNumericValue (count.ToString ().ToCharArray () [i]);
				Rectangle draw = new Rectangle (drawPoint.X + i * Dimensions.NUMBER_WIDTH, drawPoint.Y , Dimensions.NUMBER_WIDTH, Dimensions.NUMBER_HEIGHT);
				Rectangle source = atlas.getSource (digit);
				batch.Draw (atlas.texture, draw, source,
					Color.Black,
						0f,
						new Vector2(0, 0),
						SpriteEffects.None,
					LayerOrder.NUMBER_LAYER);
			}
		}
	}
}

