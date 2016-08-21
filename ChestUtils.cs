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

		internal static int DepositItem(int chest, int owner, Item[] items, Item item)
		{
			for (int i = 0; i < items.Length; ++i)
			{
				if (item.IsTheSameAs(items[i]))
				{
					item.stack = items[i].stack - items[i].maxStack + item.stack;
					items[i].stack = Math.Min(items[i].maxStack + item.stack, items[i].maxStack);

					if (owner != -1)
					{
						NetMessage.SendData(32, owner, -1, "", chest, i, 0f, 0f, 0, 0, 0);
					}

					if (item.stack <= 0)
					{
						return 0;
					}
				}
			}

			for (int i = 0; i < items.Length; ++i)
			{
				if (items[i].type == 0)
				{
					items[i] = item;
					item.newAndShiny = true;

					if (owner != -1)
					{
						NetMessage.SendData(32, owner, -1, "", chest, i, 0f, 0f, 0, 0, 0);
					}

					return 0;
				}
			}

			return item.stack;
		}
	}
}
