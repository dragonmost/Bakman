using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal class Game
	{
		private const int frameTime = 80;
		private const int xOffset = 1;
		private const int yOffset = 1;

		private bool closeGame;
		private string endText;
		private readonly List<Entity> entities = new List<Entity>();

		public Blep Player { get; }

		public Board Board { get; }

		public int Time { get; private set; }

		public int Width { get; }

		public int Height { get; }

		public IEnumerable<Entity> Entities => entities;

		public Game()
		{
			Board = BoardLoader.LoadBoard("map1");

			Player = new Blep('@', this);

			entities.Add(new JPEnemy('@', this));
			entities.Add(new CLEnemy('@', this));
			entities.Add(Player);

			Width = Board.Width * 2 + xOffset;
			Height = Board.Height + yOffset + 2;

			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
			Console.SetWindowSize(Width, Height);
			Console.SetBufferSize(Width, Height);
		}

		public void Run()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();

			while (!closeGame)
			{
				int time = frameTime;

				stopwatch.Restart();

				Update();
				Display();

				time -= (int)stopwatch.ElapsedMilliseconds;

				// yep
				Thread.Sleep(time > 0 ? time : 0);  // tellement la pire affaire
			}

			if (endText != null)
			{
				Console.Clear();

				int x = Console.BufferWidth / 2 - endText.Length / 2;
				int y = Console.BufferHeight / 2;

				Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition(x, y);
				Console.WriteLine(endText);
				Console.ReadKey(true);
			}
		}

		private void Update()
		{
			Time += frameTime;

			if (Console.KeyAvailable)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);

				foreach (var entity in entities)
				{
					entity.UpdateInput(key);
				}
			}

			foreach (var entity in entities)
			{
				entity.UpdateDirection();
				entity.Move();
				entity.UpdateCollisions();
			}

			if (Board.FoodCount <= 0)
			{
				Close("YOU WIN");
			}
		}

		private void Display()
		{
			Console.SetCursorPosition(0, 0);

			for (int i = 0; i < yOffset; i++)
			{
				Console.WriteLine();
			}

			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine($" BakerMan            Score: " + Player.Score);

			ConsoleColor currentColor = Console.ForegroundColor;

			void SetColor(ConsoleColor color)
			{
				if (currentColor == color)
				{
					return;
				}

				Console.ForegroundColor = color;
				currentColor = color;
			}

			Entity[] entities = this.entities.ToArray();

			void DisplayTile(Tile tile)
			{
				if (tile.Color != ConsoleColor.Black)
				{
					SetColor(tile.Color);
				}

				Console.Write(tile.DisplayChar);
			}

			for (int y = 0; y < Board.Height; y++)
			{
				Console.Write(new string(' ', xOffset));

				for (int x = 0; x < Board.Width; x++)
				{
					Tile tile = Board[x, y];

					Entity entity = entities.FirstOrDefault(e => e.Position.X == x && e.Position.Y == y);

					Tile spaceTile = Tile.Empty;

					if (x > 0 && tile.IsWall)
					{
						Tile previousTile = Board[x - 1, y];

						if (previousTile.IsWall)
						{
							if (tile.IsDoor && previousTile.IsDoor)
							{
								spaceTile = Tile.Door;
							}
							else if ((previousTile == Tile.P || previousTile == Tile.Dash || previousTile == Tile.L) && (tile == Tile.Q || tile == Tile.Dash || tile == Tile.J))
							{
								spaceTile = Tile.Dash;
							}
						}
					}

					DisplayTile(spaceTile);

					if (entity != null)
					{
						SetColor(entity.Color);

						Console.Write(entity.Name);
					}
					else
					{
						DisplayTile(tile);
					}
				}
			}
		}

		public void Close(string text = default)
		{
			closeGame = true;
			endText = text;
		}
	}
}
