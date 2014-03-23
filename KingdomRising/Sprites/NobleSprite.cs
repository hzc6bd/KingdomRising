using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingdomRising {
	public class NobleSprite : ModelBasedSprite {
		public NobleModel model;

		public NobleSprite (NobleModel model) : 
		//TODO: get noble texture (eventually use different texture for each type of noble)
			base(KingdomRising.textures[Textures.NOBLE]) {
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
				0f);
		}
	}
}