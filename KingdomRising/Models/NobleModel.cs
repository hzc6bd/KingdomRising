using System;
using System.Collections.Generic;
using Size = System.Drawing.Size;

using Microsoft.Xna.Framework;
using Point = Microsoft.Xna.Framework.Point;
using Microsoft.Xna.Framework.Graphics;

namespace KingdomRising
{
	public class NobleModel {
		public Point position;
		public List<int> path;
		public int previous;
		public int next;
		public int end;
		int speed;
		public Size size;
        // pop, attack, def
		public string type; 
		int pathCounter = 2;
		public bool timeToDelete = false;
		float fX;
		float fY;

		public NobleModel (CountryModel country, int a, int b, int speed, string type) {
			this.path = construct (a, b, BFS (a, b, country.adjacencies, country.distances, country.n));
			this.position = KingdomRising.country.locations[a].model.center;
			this.fX = position.X;
			this.fY = position.Y;
			this.previous = path [0];
			this.next = path [1];
			this.speed = speed;
			this.size = new Size(Dimensions.KNIGHT_WIDTH, Dimensions.KNIGHT_HEIGHT);
			this.end = b;
			this.type = type; 
		}

		public void Update (GameTime gameTime) {
			Point a = KingdomRising.country.locations[previous].model.center;
			Point b = KingdomRising.country.locations[next].model.center;

			if (touch (b)) {
				if (next != end) {
					previous = next;
					next = path [pathCounter];
					pathCounter++;
				} else {
					timeToDelete = true;
				}
			}

			Vector2 travelVector = new Vector2 (b.X - a.X, b.Y - a.Y);
			travelVector = new Vector2 ((float) (travelVector.X / travelVector.Length()), (float) (travelVector.Y / travelVector.Length()));
			fX += travelVector.X * speed;
			fY += travelVector.Y * speed;
			position.X = (int) fX;
			position.Y = (int) fY;
		}

		public bool touch(Point b) {
			return Math.Sqrt (Math.Pow (position.X - b.X, 2) + Math.Pow (position.Y - b.Y, 2)) < 5;
		}

		public int[] BFS (int start, int end, bool[, ] adjacencies, float[, ] distances, int n) {
			HashSet<int> white = new HashSet<int> ();
			HashSet<int> gray = new HashSet<int> ();
			HashSet<int> black = new HashSet<int> ();
			float[] d = new float[n];
			int[] pi = new int[n];

			for (int i = 0; i < n; i++) {
				if (i != start) {
					white.Add (i);
					d [i] = float.PositiveInfinity;
					pi [i] = -1;
				}
			}

			gray.Add (start);
			d [start] = 0;
			pi [start] = -1;

			Queue<int> Q = new Queue<int> ();
			Q.Enqueue (start);
			while (Q.Count > 0) {
				int u = Q.Dequeue ();
				for (int i = 0; i < n; i++) {
					if (adjacencies [u, i]) {
						if (white.Contains (i)) {
							white.Remove (i);
							gray.Add (i);
							d [i] = d [u] + 1;
							pi [i] = u;
							Q.Enqueue (i);
						}
					}
				}
				gray.Remove (u);
				black.Add (u);
			}
			return pi;
		}

		private List<int> construct(int start, int end, int[] pi) {
			List<int> path = new List<int> ();
			path.Add (end);
			while (pi [end] != start && pi[end] != -1) {
				path.Add (pi [end]);
				end = pi [end];
			} path.Add (start);
			path.Reverse ();
			return path;
		}
	}
}

