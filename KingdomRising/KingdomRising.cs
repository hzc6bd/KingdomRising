#region File Description
//-----------------------------------------------------------------------------
// KingdomRisingGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

// For debugging
using System.Diagnostics;

#endregion
namespace KingdomRising
{
	/// <summary>
	/// Default Project Template
	/// </summary>
	public class KingdomRising : Game
	{

		#region Fields
		GraphicsDeviceManager graphics;
		SpriteBatch batch;
		MouseState myPreviousMouseState;

		HashSet<Knight> knights;
		HashSet<LocationSprite> locations;
        HashSet<NobleSprite> nobles;

		public static Dictionary<string, Texture2D> textures;

		HashSet<LocationSprite> sources = new HashSet<LocationSprite>();

		public static CountryModel country;

		bool gameStarted = false;

		#endregion

		#region Initialization

		public KingdomRising (){
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = false;
            Texture2D background = Content.Load<Texture2D>(Textures.BACKGROUND);
            graphics.PreferredBackBufferHeight = background.Height - 100;
            graphics.PreferredBackBufferWidth = background.Width - 80;
		}

		/// <summary>
		/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
		/// we'll use the viewport to initialize some values.
		/// </summary>
		protected override void Initialize () {
			this.IsMouseVisible = true;

			bool[, ] adjacencies = new bool[, ] {
				{false, true, true, false, false},
				{false, false, false, false, false},
				{false, false, false, false, true},
				{false, true, false, false, false},
				{false, false, false, true, false} };

			country = new CountryModel (adjacencies, 5);



			knights = new HashSet<Knight> ();
			locations = new HashSet<LocationSprite> ();
            nobles = new HashSet<NobleSprite> ();

            

			base.Initialize ();
		}

		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent () {
			// Create a new SpriteBatch, which can be use to draw textures.
			batch = new SpriteBatch (graphics.GraphicsDevice);

			// Load all textures
			textures = new Dictionary<string, Texture2D> ();
			foreach (string name in Textures.ALL) {
				Texture2D texture = Content.Load<Texture2D> (name);
				textures.Add (name, texture);
			}

			country.addLocation (0, 400, 200);
			country.addLocation (1, 250, 300);
			country.addLocation (2, 550, 400);
			country.addLocation (3, 350, 550);
			country.addLocation (4, 385, 400);
			
			country.calcDistance ();

            //create a population noble
            NobleModel noble = new NobleModel(country, 0, 0, 2, "pop");
            noble.position.X = noble.position.X + 50;
            NobleSprite popNoble = new NobleSprite(noble);
            nobles.Add(popNoble);

            //add the pop noble
            LocationSprite loc;

            if(country.locations.TryGetValue(0, out loc))
            {  
                loc.addNoble(popNoble);
            }
			


			foreach (LocationSprite location in country.locations.Values) {
				//location.populationTimer = 0; unecessary in constructor
				locations.Add(location);
			}

		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime) {
			MouseState myCurrentMouseState = Mouse.GetState ();

			if (!gameStarted) {
				gameStarted = true;
				foreach (LocationSprite location in locations) {
					location.populationTimer = gameTime.ElapsedGameTime.Milliseconds;
				}
			}

			// is user clicking?
			if (myCurrentMouseState.LeftButton == ButtonState.Pressed) {

				// if clicking, is click new?
				if (myPreviousMouseState.LeftButton == ButtonState.Released) {

					// if click is new, is it on location?
					foreach (LocationSprite sprite in locations) {
						if (sprite.model.contains (new Point(myCurrentMouseState.X,myCurrentMouseState.Y))) {
							sources.Add (sprite);
							break;
						}
					}
				} else {
					foreach (LocationSprite location in locations) {
                        if (location.model.contains(new Point(myCurrentMouseState.X, myCurrentMouseState.Y)))
                        {
							// if on location
							if (sources.Count > 0) {
								sources.Add (location);
								break;
							}
						}
					}
				}
			}

			else if (myCurrentMouseState.LeftButton == ButtonState.Released) {
				if (myPreviousMouseState.LeftButton == ButtonState.Pressed) {
					foreach (LocationSprite target in locations) {
                        if (target.model.contains(new Point(myCurrentMouseState.X, myCurrentMouseState.Y)))
                        {
							foreach (LocationSprite source in sources) {
								int fromID = source.model.ID;
								int toID = target.model.ID;
								if (fromID != toID) {
									source.model.population--;
									Knight k = new Knight (new KnightModel (country, fromID, toID, 2));
									source.model.routes.Add (k.model.path);
									knights.Add (k);
								}
							}
							break;
						}
					}
					sources.Clear ();
				}
			}

            //update population
            foreach (LocationSprite sprite in locations)
            {
                sprite.Update();
            }

			knights.RemoveWhere(s => s.model.timeToDelete == true);
			myPreviousMouseState = myCurrentMouseState;
			foreach (Knight sprite in knights) {
				sprite.model.Update (gameTime);
			}

			base.Update (gameTime);
		}

		protected override void Draw (GameTime gameTime) {
			graphics.GraphicsDevice.Clear (Microsoft.Xna.Framework.Color.White);
			batch.Begin (SpriteSortMode.BackToFront, BlendState.AlphaBlend);

			Texture2D background = textures [Textures.BACKGROUND];
			Rectangle draw = new Rectangle (0, 0, background.Width - 80, background.Height - 80);
			batch.Draw (background, draw, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, LayerOrder.BACKGROUND_LAYER);
			country.Draw (batch);

			foreach (Knight sprite in knights) {
				sprite.Draw (batch);
			}

            //this has been moved to locationsprite update()
			//country.locations [0].populationCounter.setCount (gameTime.TotalGameTime.Milliseconds);
			country.locations [0].Draw (batch);

			// draw the logo
			foreach (LocationSprite sprite in locations) {
				sprite.Draw (batch);
				if (sources.Contains(sprite)) {
					new SourceHighlightSprite().Draw (batch, sprite);
				}
			}
            
            foreach (NobleSprite sprite in nobles)
            {
                sprite.Draw(batch);
            }
			batch.End ();

			//TODO: Add your drawing code here
			base.Draw (gameTime);
		}

		#endregion

	}
}
