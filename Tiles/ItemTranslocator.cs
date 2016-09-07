using Itemtronics.Util;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Itemtronics.Tiles
{
	class ItemTranslocator : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			Main.tileValue[Type] = 1000;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
			//disableSmartCursor = true;
			drop = mod.ItemType("ItemTranslocator");
		}

		/*public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			Tile tile = Main.tile[i, j];
			tile.frameX = 36;
			//tile.frameY = 0;
			return true;
		}*/

		public override void RightClick(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			tile.frameX = (short)((tile.frameX + 18) % 72);
		}

		public override void HitWire(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			int ChestInID = ChestUtils.GetTargetChestID(x, y, tile.frameX / 18);
			int ChestOutID = ChestUtils.GetTargetChestID(x, y, (tile.frameX / 18 + 2) % 4);

			if (ChestInID != -1 && ChestOutID != -1)
			{
				Item[] ItemsIn = Main.chest[ChestInID].item;
				Item[] ItemsOut = Main.chest[ChestOutID].item;
				int OwnerInID = Main.netMode == 2 ? Chest.UsingChest(ChestInID) : -1;
				int OwnerOutID = Main.netMode == 2 ? Chest.UsingChest(ChestOutID) : -1;

				for (int i = 0; i < ItemsIn.Length; ++i)
				{
					if (ItemsIn[i].type != 0)
					{
						ItemState state = ChestUtils.DepositItem(ChestOutID, OwnerOutID, ItemsOut, ItemsIn[i]);
						if (state != ItemState.SAME)
						{
							if (state == ItemState.EMPTY)
							{
								ItemsIn[i] = new Item();
							}
							if (OwnerInID != -1)
							{
								NetMessage.SendData(32, OwnerInID, -1, "", ChestInID, i, 0f, 0f, 0, 0, 0);
							}
							break;
						}
					}
				}
			}
		}
	}
}