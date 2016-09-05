using System;
using Terraria;
using Terraria.ObjectData;

namespace Itemtronics.Util
{
	internal enum ItemState
	{
		EMPTY,
		CHANGED,
		SAME
	}

	internal static class ChestUtils
	{
		public static readonly int[] xOffset = { 0, -1, 0, 1 };
		public static readonly int[] yOffset = { -1, 0, 1, 0 };

		public static Chest GetChest(int x, int y)
		{
			return Main.chest[GetChestID(x, y)];
		}

		public static Chest GetChestSafe(int x, int y)
		{
			int chest = GetChestID(x, y);
			return chest == -1 ? null : Main.chest[chest];
		}

		public static int GetChestID(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return Chest.FindChest(tile.frameX % 36 == 0 ? x : x - 1, tile.frameY == 0 ? y : y - 1);
		}

		//Should work for any size of chest, as long as chest location is upper right corner
		public static int GetVarSizeChestID(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			TileObjectData tileData = TileObjectData.GetTileData(tile);
			return Chest.FindChest(x - tile.frameX / 18 % (tileData == null ? 1 : tileData.Width), y - tile.frameY / 18);
		}

		public static int GetTargetChestID(int x, int y, int dir)
		{
			return GetVarSizeChestID(x + xOffset[dir], y + yOffset[dir]);
		}

		public static ItemState DepositItem(int chest, int owner, Item[] items, Item item)
		{
			ItemState state = ItemState.SAME;
			for (int i = 0; i < items.Length; ++i)
			{
				if (item.IsTheSameAs(items[i]) && items[i].stack != items[i].maxStack)
				{
					state = ItemState.CHANGED;

					item.stack = items[i].stack - items[i].maxStack + item.stack;
					items[i].stack = Math.Min(items[i].maxStack + item.stack, items[i].maxStack);

					if (owner != -1)
					{
						NetMessage.SendData(32, owner, -1, "", chest, i, 0f, 0f, 0, 0, 0);
					}

					if (item.stack <= 0)
					{
						return ItemState.EMPTY;
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

					return ItemState.EMPTY;
				}
			}

			return state;
		}
	}
}
