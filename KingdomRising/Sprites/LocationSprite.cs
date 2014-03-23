using System;
using Size = System.Drawing.Size;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace KingdomRising {
	public class LocationSprite : ModelBasedSprite {
		public LocationModel model;
		public CounterSprite populationCounter;
		public int populationTimer;
        public HashSet<NobleSprite> nobles;

		public LocationSprite (LocationModel model) : 
			base(KingdomRising.textures[Textures.LOCATION]) {
				this.model = model;
			this.SetDrawPoint (model.center, this.model.size);
			Point counterPoint = new Point (this.model.center.X, this.model.center.Y + Dimensions.LOCATION_COUNTER_OFFSET);
			this.populationCounter = new CounterSprite(counterPoint);
			this.populationTimer = 0;
            this.nobles = new HashSet<NobleSprite>();
		}

        public void addNoble( NobleSprite noble){
            this.nobles.Add(noble);
        }

		public void Draw(SpriteBatch batch) {
			Rectangle draw = new Rectangle (this.drawPoint.X, 
				this.drawPoint.Y, 
				this.model.size.Width,
				this.model.size.Height);
			batch.Draw (this.texture, draw, null, Color.White, 0f, new Vector2(0, 0), 
				SpriteEffects.None, LayerOrder.LOCATION_LAYER);
			this.populationCounter.setCount (this.model.population);
			this.populationCounter.Draw (batch);
		}

		public void Update() {
            int populationUpdateInSeconds = 2; //how often you want new units to spawn
            int fps = 60; //game update frequency
            if (this.populationTimer == fps * populationUpdateInSeconds)
            {
                this.populationTimer = 0;
                //update populations:
                this.model.population++;
                    //save this for when nobles implemented
                    foreach(NobleSprite noble in this.nobles){
			            if (noble.model.type.Equals("pop")) {
                            this.model.population++; //add two if knight is present
			            }
                   }
                
            }
            populationTimer++;
		}
	}
}