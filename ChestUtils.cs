using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Itemtronics
{
	internal static class ChestUtils
	{
		internal static Chest GetModChest(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return Main.chest[Chest.FindChest(tile.frameX == 0 ? x : x - 1, tile.frameY == 0 ? y : y - 1)];
		}

		internal static Chest GetChest(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return Main.chest[Chest.FindChest(tile.frameX % 36 == 0 ? x : x - 1, tile.frameY == 0 ? y : y - 1)];
		}

		internal static Chest GetModChestSafe(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			int chest = Chest.FindChest(tile.frameX == 0 ? x : x - 1, tile.frameY == 0 ? y : y - 1);
			return chest == -1 ? null : Main.chest[chest];
		}

		internal static Chest GetChestSafe(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			int chest = Chest.FindChest(tile.frameX % 36 == 0 ? x : x - 1, tile.frameY == 0 ? y : y - 1);
			return chest == -1 ? null : Main.chest[chest];
		}

		internal static int GetChestID(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return Chest.FindChest(tile.frameX % 36 == 0 ? x : x - 1, tile.frameY == 0 ? y : y - 1);
		}

		internal static int GetModChestID(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return Chest.FindChest(tile.frameX == 0 ? x : x - 1, tile.frameY == 0 ? y : y - 1);
		}

		internal static int FindFirstEmptySlot(Item[] items)
		{
			for (int i = 0; i < items.Length; ++i)
			{
				if (items[i].type == 0)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
