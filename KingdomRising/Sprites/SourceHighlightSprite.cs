using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingdomRising {
	public class SourceHighlightSprite {
		Texture2D texture;

		public SourceHighlightSprite () {
			this.texture = KingdomRising.textures [Textures.DASHED_CIRCLE];
		}

		public void Draw(SpriteBatch batch, LocationSprite sprite) {
			batch.Draw (texture,
				new Rectangle (sprite.drawPoint.X - Dimensions.HIGHLIGHT_PADDING,
					sprite.drawPoint.Y - Dimensions.HIGHLIGHT_PADDING,
					sprite.model.size.Width + Dimensions.HIGHLIGHT_SCALE, 
					sprite.model.size.Height + Dimensions.HIGHLIGHT_SCALE),
				Color.White);
		}
	}
}

