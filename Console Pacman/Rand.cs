using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal static class Rand
	{
		private static readonly Random random = new Random();

		public static int Next() => random.Next();
	}
}
