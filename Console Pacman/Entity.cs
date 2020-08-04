using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal abstract class Entity
	{
		protected Direction Direction { get; set; }

		protected Direction NextDirection { get; set; }

		public char Name { get; }

		public virtual ConsoleColor Color { get; }

		public Game Game { get; }

		public Position Position { get; set; }

		public Entity(char name, Game game, ConsoleColor color)
		{
			Name = name;
			Game = game;
			Color = color;

			Respawn();
		}

		public virtual void UpdateDirection()
		{
			if (GetMoveDirection(NextDirection, out int x, out int y))
			{
				Game.Board.GetPosition(Position.X + x, Position.Y + y, out x, out y);

				if (!Game.Board[x, y].IsWallFor(this))
				{
					Direction = NextDirection;
				}
			}
		}

		public virtual void UpdateInput(ConsoleKeyInfo key) { }

		public virtual void UpdateCollisions()
		{
			Tile tile = Game.Board[Position.X, Position.Y];

			tile.Walk(this);
		}

		public void Move()
		{
			if (GetMoveDirection(Direction, out int x, out int y))
			{
				Game.Board.GetPosition(Position.X + x, Position.Y + y, out x, out y);

				Tile tile = Game.Board[x, y];

				if (!tile.IsWallFor(this))
				{
					Position = new Position(x, y);
				}
				else
				{
					HitWall(tile, x, y);
				}
			}
		}

		protected virtual void HitWall(Tile tile, int x, int y)
		{
		}

		public virtual void Die()
		{
		}

		public virtual void Respawn()
		{
			Position = GetSpawnPosition();
		}

		protected abstract Position GetSpawnPosition();

		private bool GetMoveDirection(Direction direction, out int x, out int y)
		{
			x = 0;
			y = 0;

			switch (direction)
			{
				case Direction.Right:
					x = 1;
					break;
				case Direction.Down:
					y = 1;
					break;
				case Direction.Left:
					x = -1;
					break;
				case Direction.Up:
					y = -1;
					break;
			}

			return x != 0 || y != 0;
		}
	}
}
