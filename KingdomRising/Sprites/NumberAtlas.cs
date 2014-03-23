using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace KingdomRising {
	public class NumberAtlas {
		public Texture2D texture;
		Dictionary<int, Point> translator = new Dictionary<int, Point> ();

		public NumberAtlas () {
			this.texture = KingdomRising.textures [Textures.NUMBERS];
			initialize ();
		}

		public Rectangle getSource(int digit) {
			int width = texture.Width / Textures.NUMBER_ATLAS_COLUMNS;
			int height = texture.Height / Textures.NUMBER_ATLAS_ROWS;
			return new Rectangle (translator [digit].X * width,
				translator [digit].Y * height,
				width,
				height);
		}

		public void initialize() {
			this.translator [0] = new Point (0, 0);
			this.translator [1] = new Point (1, 0);
			this.translator [2] = new Point (2, 0);
			this.translator [3] = new Point (3, 0);
			this.translator [4] = new Point (4, 0);
			this.translator [5] = new Point (0, 1);
			this.translator [6] = new Point (1, 1);
			this.translator [7] = new Point (2, 1);
			this.translator [8] = new Point (3, 1);
			this.translator [9] = new Point (4, 1);
	}
}
}

