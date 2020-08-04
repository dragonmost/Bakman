using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Resources;

namespace Console_Pacman
{
	internal static class BoardLoader
	{
		public static Board LoadBoard(string name)
		{
			string text = EmbeddedResources.Get<string>($"{name}.txt");
			string[] lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			int width = lines.Max(l => l.Length);
			int height = lines.Length;

			Tile[,] tiles = new Tile[width, height];

			for (int y = 0; y < lines.Length; y++)
			{
				string line = lines[y];

				for (int x = 0; x < line.Length; x++)
				{
					tiles[x, y] = (Tile)line[x];
				}
			}

			return new Board(tiles);
		}
	}
}
