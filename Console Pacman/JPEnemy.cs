using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal class JPEnemy : Enemy
	{
		public JPEnemy(char name, Game game)
			: base(name, game, ConsoleColor.Blue)
		{
		}
	}
}
