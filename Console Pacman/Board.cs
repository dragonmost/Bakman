using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal class Board
	{
		private readonly Tile[,] tiles;
		private readonly Position[] enemyStartPositions;

		public int Width { get; }

		public int Height { get; }

		public Position StartPosition { get; }

		public int FoodCount { get; private set; }

		public IEnumerable<Position> EnemyStartPositions => enemyStartPositions;

		public Board(Tile[,] tiles)
		{
			this.tiles = tiles;
			Width = tiles.GetLength(0);
			Height = tiles.GetLength(1);

			List<Position> enemyStartPositions = new List<Position>();

			for (int y = 0; y < tiles.GetLength(1); y++)
			{
				for (int x = 0; x < tiles.GetLength(0); x++)
				{
					Tile tile = tiles[x, y];

					if (tile == Tile.Start)
					{
						StartPosition = new Position(x, y);
					}
					else if (tile == Tile.GhostStart)
					{
						enemyStartPositions.Add(new Position(x, y));
					}
					else if (tile.IsFood)
					{
						FoodCount++;
					}
				}
			}

			this.enemyStartPositions = enemyStartPositions.ToArray();
		}

		public void EatFood(int x, int y)
		{
			this[x, y] = Tile.Empty;

			FoodCount--;
		}

		public void GetPosition(int x1, int y1, out int x2, out int y2)
		{
			x2 = MathHelper.Mod(x1, Width);
			y2 = MathHelper.Mod(y1, Height);
		}

		public Tile this[int x, int y]
		{
			get => tiles[x, y];
			set => tiles[x, y] = value;
		}
	}
}
