using System;
using Size = System.Drawing.Size;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace KingdomRising {
	public class LocationSprite : ModelBasedSprite {
		public LocationModel model;

		public LocationSprite (LocationModel model) : 
			base(KingdomRising.textures[TextureNames.LOCATION]) {
				this.model = model;
			this.SetDrawPoint (model.center, this.model.size);
			}

		public override void Draw(SpriteBatch batch) {
			Rectangle rect = new Rectangle (this.drawPoint.X, 
				this.drawPoint.Y, 
				this.model.size.Width,
				this.model.size.Height);
			batch.Draw (texture,
				rect,
				null,
				Color.White,
				0f,
				new Vector2(0, 0),
				SpriteEffects.None,
				0.9f);
		}
	}
}

