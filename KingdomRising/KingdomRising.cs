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

		SpriteStore<KnightSprite> knightSprites;
		SpriteStore<LocationSprite> locationSprites;

		public static Dictionary<string, Texture2D> textures;

		HashSet<LocationSprite> sources = new HashSet<LocationSprite>();

		MouseState myPreviousMouseState;

		public static CountryModel country;

		#endregion

		#region Initialization

		public KingdomRising ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 723;
            graphics.PreferredBackBufferWidth = 804;
		}

		/// <summary>
		/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
		/// we'll use the viewport to initialize some values.
		/// </summary>
		protected override void Initialize () {
			this.IsMouseVisible = true;

			bool[, ] adjacencies = new bool[, ] {
				{false, false, true, false, false},
				{false, false, false, false, false},
				{false, false, false, false, true},
				{false, true, false, false, false},
				{false, false, false, true, false} };

			country = new CountryModel (adjacencies, 5);

			country.addLocation (0, 400, 200);
			country.addLocation (1, 250, 300);
			country.addLocation (2, 550, 400);
			country.addLocation (3, 350, 550);
			country.addLocation (4, 385, 400);

			country.calcDistance ();

			knightSprites = new SpriteStore<KnightSprite> ();
			locationSprites = new SpriteStore<LocationSprite> ();

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
			foreach (string name in TextureNames.ALL) {
				Texture2D texture = Content.Load<Texture2D> (name);
				textures.Add (name, texture);
			}
            

			foreach (LocationModel location in country.locations.Values) {
				locationSprites.Add(new LocationSprite(location));
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

			// is user clicking?
			if (myCurrentMouseState.LeftButton == ButtonState.Pressed) {

				// if clicking, is click new?
				if (myPreviousMouseState.LeftButton == ButtonState.Released) {

					// if click is new, is it on location?
					foreach (LocationSprite sprite in locationSprites) {
						if (sprite.model.contains (new Point(myCurrentMouseState.X, myCurrentMouseState.Y))) {
							sources.Add (sprite);
							break;
						}
					}
				} else {
					foreach (LocationSprite location in locationSprites) {
						if (location.model.contains (new Point(myCurrentMouseState.X, myCurrentMouseState.Y))) {
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
					foreach (LocationSprite target in locationSprites) {
						if (target.model.contains (new Point(myCurrentMouseState.X, myCurrentMouseState.Y))) {
							foreach (LocationSprite source in sources) {
								int fromID = source.model.id;
								int toID = target.model.id;
								if (fromID != toID) {
									knightSprites.Add (new KnightSprite (
										new KnightModel (country, fromID, toID, 2)));
								}
							}
							break;
						}
					}
					sources.Clear ();
				}
			}


			knightSprites.RemoveWhere(s => s.model.timeToDelete == true);
			myPreviousMouseState = myCurrentMouseState;
			foreach (KnightSprite sprite in knightSprites) {
				sprite.model.Update (gameTime);
			}

			base.Update (gameTime);
		}

		protected override void Draw (GameTime gameTime) {
			graphics.GraphicsDevice.Clear (Microsoft.Xna.Framework.Color.White);
			batch.Begin (SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			Texture2D texture = textures [TextureNames.BACKGROUND];
			batch.Draw (textures [TextureNames.BACKGROUND], new Vector2 (0, 0), null,
				Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 1f);
			Rectangle rect = new Rectangle (0, 
				0, 
				texture.Width - 80,
				texture.Height - 80);
			batch.Draw (texture,
				rect,
				null,
				Color.White,
				0f,
				new Vector2(0, 0),
				SpriteEffects.None,
				1f);
			country.Draw (batch);

			foreach (KnightSprite sprite in knightSprites) {
				sprite.Draw (batch);
			}

			// draw the logo
			foreach (LocationSprite sprite in locationSprites) {
				sprite.Draw (batch);
				if (sources.Contains(sprite)) {
					new SourceHighlightSprite().Draw (batch, sprite);
				}
			}

			batch.End ();

			//TODO: Add your drawing code here
			base.Draw (gameTime);
		}

		#endregion

	}
}
