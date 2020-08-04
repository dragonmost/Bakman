using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	[Flags]
	public enum Direction
	{
		None = 0,
		Right = 1,
		Down = 2,
		Left = 4,
		Up = 8
	}
}
