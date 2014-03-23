using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

namespace KingdomRising {

	public class CountryModel {
		public int n = 0;
		public Dictionary<int, LocationSprite> locations = new Dictionary<int, LocationSprite>();
		public bool[, ] adjacencies;
		public float[, ] distances;

		public CountryModel (bool[, ] adjacencies, int n) {
			this.adjacencies = reflect(adjacencies, n);
			this.n = n;
		}

		public void addLocation(int id, int x, int y) {
			LocationModel model = new LocationModel (id, x, y);
			locations.Add (id, new LocationSprite (model));
		}

		public void calcDistance() {
			this.distances = measure ();
		}

		public void Draw(SpriteBatch batch) {
			foreach (LocationSprite model in locations.Values) {
				model.Draw (batch);
			}

			for (int i = 0; i < 5; i++) {
				for (int j = i + 1; j < 5; j++) {
					if (adjacencies[i, j]) {
						new RoadSprite (new RoadModel (i, j)).Draw(batch);
					}
				}
			}
		}

		public bool[, ] reflect(bool[, ] matrix, int n) {
			bool[, ] reflection = new bool[matrix.Length, matrix.Length];
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					reflection [i, j] = matrix [i, j] || matrix [j, i];
				}
			}
			return reflection;
		}

		public float[, ] measure() {
			float[, ] distances = new float[n, n];
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					distances [i, j] = new RoadModel (i, j).length;
				}
			}
			return distances;
		}
	}
}