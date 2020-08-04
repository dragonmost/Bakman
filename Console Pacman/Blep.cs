using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal class Blep : Entity
	{
		private int powerUpEndTime;

		public int Score { get; set; }

		public bool IsPoweredUp => powerUpEndTime > Game.Time;

		public override ConsoleColor Color => IsPoweredUp ? ConsoleColor.Magenta : base.Color;

		public Blep(char name, Game game)
			: base(name, game, ConsoleColor.Yellow)
		{
		}

		public override void UpdateCollisions()
		{
			base.UpdateCollisions();

			Tile tile = Game.Board[Position.X, Position.Y];

			if (tile.IsFood)
			{
				Game.Board.EatFood(Position.X, Position.Y);
			}

			foreach (var entity in Game.Entities)
			{
				if (entity == this)
				{
					continue;
				}

				if (entity.Position.X == Position.X && entity.Position.Y == Position.Y && IsPoweredUp)
				{
					entity.Die();
				}
			}
		}

		public override void UpdateInput(ConsoleKeyInfo key)
		{
			base.UpdateInput(key);

			switch (key.Key)
			{
				case ConsoleKey.UpArrow:
				case ConsoleKey.W:
					NextDirection = Direction.Up;
					break;
				case ConsoleKey.DownArrow:
				case ConsoleKey.S:
					NextDirection = Direction.Down;
					break;
				case ConsoleKey.LeftArrow:
				case ConsoleKey.A:
					NextDirection = Direction.Left;
					break;
				case ConsoleKey.RightArrow:
				case ConsoleKey.D:
					NextDirection = Direction.Right;
					break;
				case ConsoleKey.Escape:
					Game.Close();
					break;
			}
		}

		public override void Die()
		{
			base.Die();

			Game.Close("YOU DED");
		}

		public void PowerUp(int powerUpTime)
		{
			powerUpEndTime = Game.Time + powerUpTime;
		}

		protected override Position GetSpawnPosition()
		{
			return Game.Board.StartPosition;
		}
	}
}
