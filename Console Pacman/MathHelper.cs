using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pacman
{
	internal static class MathHelper
	{
		public static int Mod(int x, int mod)
		{
			return ((x % mod) + mod) % mod;
		}
	}
}
