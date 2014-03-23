using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingdomRising {
	public class Knight : ModelBasedSprite {
		public KnightModel model;

		public Knight (KnightModel model) : 
			base(KingdomRising.textures[Textures.KNIGHT]) {
				this.model = model;
		}

		public void Draw(SpriteBatch batch) {
			Rectangle rect = new Rectangle (UpperLeftCorner(model.position, model.size).X, 
				UpperLeftCorner(model.position, model.size).Y, 
				this.model.size.Width,
				this.model.size.Height);
			batch.Draw (texture,
				rect,
				null,
				Color.White,
				0f,
				new Vector2(0, 0),
				SpriteEffects.None,
				LayerOrder.KNIGHT_LAYER);
		}
	}
}