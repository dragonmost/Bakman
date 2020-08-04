﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console_Pacman
{
	public struct Position
	{
		public int X;
		public int Y;

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static bool operator ==(Position a, Position b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=(Position a, Position b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return obj is Position position && position == this;
		}

		public override int GetHashCode()
		{
			return (X, Y).GetHashCode();
		}

		public override string ToString()
		{
			return $"{X}, {Y}";
		}
	}
}
