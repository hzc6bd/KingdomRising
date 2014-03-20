using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingdomRising {
	public class RoadSprite : ModelBasedSprite {
		private RoadModel model;

		public RoadSprite (RoadModel model) : 
			base (KingdomRising.textures [TextureNames.ROAD]) {
				this.model = model;
		}

		public override void Draw(SpriteBatch batch) {
			batch.Draw (texture,
				new Rectangle((int) model.x, (int) model.y, (int) model.length, Dimensions.ROAD_WIDTH), null,
				Color.SandyBrown, model.rotation, new Vector2(0, 0), SpriteEffects.None, 0.95f);
		}
	}
}

