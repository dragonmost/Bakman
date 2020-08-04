using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal abstract class Tile
	{
		private static readonly Dictionary<char, Tile> tiles = new Dictionary<char, Tile>();

		public static readonly Tile Empty = new EmptyTile(' ');
		public static readonly Tile Start = new EmptyTile('S');
		public static readonly Tile GhostStart = new EmptyTile('G');
		public static readonly Tile P = new WallTile('╔', ConsoleColor.DarkBlue);
		public static readonly Tile Q = new WallTile('╗', ConsoleColor.DarkBlue);
		public static readonly Tile L = new WallTile('╚', ConsoleColor.DarkBlue);
		public static readonly Tile J = new WallTile('╝', ConsoleColor.DarkBlue);
		public static readonly Tile I = new WallTile('║', ConsoleColor.DarkBlue);
		public static readonly Tile Dash = new WallTile('═', ConsoleColor.DarkBlue);
		public static readonly Tile Door = new DoorTile('-', ConsoleColor.DarkGray);
		public static readonly Tile PowerUp = new PowerUpFoodTile('●', ConsoleColor.DarkYellow, 10, 10000);
		public static readonly Tile Dot = new FoodTile('.', ConsoleColor.Yellow, 1);

		private readonly char chr;

		public virtual bool IsFood => false;

		public virtual bool IsWall => false;

		public virtual bool IsDoor => false;

		public ConsoleColor Color { get; }

		public virtual char DisplayChar { get; }

		private Tile(char chr, ConsoleColor color = ConsoleColor.Black)
		{
			this.chr = chr;

			Color = color;

			tiles.Add(chr, this);
		}

		public virtual bool IsWallFor(Entity entity) => IsWall;

		public virtual void Walk(Entity entity) { }

		public static implicit operator Tile(char chr)
		{
			return tiles.TryGetValue(chr, out Tile tile) ? tile : Empty;
		}

		public static implicit operator char(Tile tile)
		{
			return tile?.chr ?? default;
		}

		private class DisplayedTile : Tile
		{
			public override char DisplayChar => chr;

			public DisplayedTile(char chr, ConsoleColor color)
				: base(chr, color)
			{
			}
		}

		private class EmptyTile : Tile
		{
			public override char DisplayChar => ' ';

			public EmptyTile(char chr)
				: base(chr)
			{
			}
		}

		private class FoodTile : DisplayedTile
		{
			public int Score { get; }

			public override bool IsFood => true;

			public FoodTile(char chr, ConsoleColor color, int score)
				: base(chr, color)
			{
				Score = score;
			}

			public override void Walk(Entity entity)
			{
				if (entity is Blep blep)
				{
					blep.Score += Score;
				}
			}
		}

		private class PowerUpFoodTile : FoodTile
		{
			private readonly int powerUpTime;

			public PowerUpFoodTile(char chr, ConsoleColor color, int score, int powerUpTime)
				: base(chr, color, score)
			{
				this.powerUpTime = powerUpTime;
			}

			public override void Walk(Entity entity)
			{
				base.Walk(entity);

				if (entity is Blep blep)
				{
					blep.PowerUp(powerUpTime);
				}
			}
		}

		private class WallTile : DisplayedTile
		{
			public override bool IsWall => true;

			public WallTile(char chr, ConsoleColor color)
				: base(chr, color)
			{
			}

			public override bool IsWallFor(Entity entity)
			{
				return IsWall;
			}
		}

		private class DoorTile : WallTile
		{
			public override bool IsDoor => true;

			public DoorTile(char chr, ConsoleColor color)
				: base(chr, color)
			{
			}

			public override bool IsWallFor(Entity entity)
			{
				return !(entity is Enemy enemy) || enemy.IsLocked;
			}
		}
	}
}
