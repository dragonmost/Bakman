using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal class CLEnemy : Enemy
	{
		public CLEnemy(char name, Game game)
			: base(name, game, ConsoleColor.Green)
		{
		}

		public override void UpdateInput(ConsoleKeyInfo key)
		{
			base.UpdateInput(key);

			switch (key.Key)
			{
				case ConsoleKey.UpArrow:
				case ConsoleKey.W:
					NextDirection = Direction.Down;
					break;
				case ConsoleKey.DownArrow:
				case ConsoleKey.S:
					NextDirection = Direction.Up;
					break;
				case ConsoleKey.LeftArrow:
				case ConsoleKey.A:
					NextDirection = Direction.Right;
					break;
				case ConsoleKey.RightArrow:
				case ConsoleKey.D:
					NextDirection = Direction.Left;
					break;
				default:
					break;
			}
		}
	}
}
