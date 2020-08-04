using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal abstract class Enemy : Entity
	{
		private const int lockTime = 2000;

		private Position previousPosition;

		private int lockEndTime;

		public bool IsLocked => Game.Time < lockEndTime;

		public override ConsoleColor Color => Game.Player.IsPoweredUp ? ConsoleColor.Blue : base.Color;

		public int Score { get; }

		protected Enemy(char name, Game game, ConsoleColor color, int score = 10)
			: base(name, game, color)
		{
			Score = score;
		}

		public override void UpdateCollisions()
		{
			base.UpdateCollisions();

			Blep player = Game.Player;

			if (player.Position.X == Position.X && player.Position.Y == Position.Y && !player.IsPoweredUp)
			{
				player.Die();
			}
		}

		public override void UpdateDirection()
		{
			base.UpdateDirection();

			if (previousPosition == Position)
			{
				List<Direction> directions = new List<Direction>
				{
					Direction.Left,
					Direction.Right,
					Direction.Up,
					Direction.Down,
				};

				directions.Remove(Direction);

				NextDirection = directions[Rand.Next() % directions.Count];
			}

			previousPosition = Position;
		}

		protected override Position GetSpawnPosition()
		{
			Position[] startPositions = Game.Board.EnemyStartPositions.ToArray();

			return startPositions[Rand.Next() % startPositions.Length];
		}

		public override void Respawn()
		{
			base.Respawn();

			lockEndTime = Game.Time + lockTime;
		}

		public override void Die()
		{
			base.Die();

			Game.Player.Score += Score;

			Respawn();
		}
	}
}
