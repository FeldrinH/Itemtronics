using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Itemtronics.Util;

namespace Itemtronics.Tiles
{
	public class EjectorChest : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSpelunker[Type] = true;
			Main.tileContainer[Type] = true;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 1200;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileValue[Type] = 500;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
			TileObjectData.newTile.HookCheck = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[] { 127 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(200, 200, 200), "Ejector Chest", MapChestName);
			dustType = mod.DustType("Sparkle");
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.Containers };
			chest = "Ejector Chest";
			chestDrop = mod.ItemType("EjectorChest");
		}

		public override void HitWire(int x, int y)
		{
			int chestID = ChestUtils.GetChestID(x, y);
			int ownerID = Main.netMode == 2 ? Chest.UsingChest(chestID) : -1;
			Chest chest = Main.chest[chestID];

			Wiring.SkipWire(chest.x, chest.y);
			Wiring.SkipWire(chest.x+1, chest.y);
			Wiring.SkipWire(chest.x, chest.y+1);
			Wiring.SkipWire(chest.x+1, chest.y+1);

			for (int i = 0; i < chest.item.Length; ++i)
			{
				if (chest.item[i].type != 0)
				{
					Item.NewItem(chest.x * 16, chest.y * 16, 32, 32, chest.item[i].type, chest.item[i].stack, false, chest.item[i].prefix);
					chest.item[i].SetDefaults(0, true);

					if (ownerID != -1)
					{
						NetMessage.SendData(32, ownerID, -1, "", chestID, i, 0f, 0f, 0, 0, 0);
					}

					break;
				}
			}
		}

		public string MapChestName(string name, int i, int j)
		{
			string chestName = ChestUtils.GetChest(i, j).name;
			if (chestName == "")
			{
				return name;
			}
			else
			{
				return name + ": " + chestName;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, chestDrop);
			Chest.DestroyChest(i, j);
		}

		public override void RightClick(int i, int j)
		{
			Player player = Main.player[Main.myPlayer];
			Tile tile = Main.tile[i, j];
			Main.mouseRightRelease = false;
			int left = i;
			int top = j;
			if (tile.frameX != 0)
			{
				left--;
			}
			if (tile.frameY != 0)
			{
				top--;
			}

			if (player.sign >= 0)
			{
				Main.PlaySound(11, -1, -1, 1);
				player.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
			}
			if (Main.editChest)
			{
				Main.PlaySound(12, -1, -1, 1);
				Main.editChest = false;
				Main.npcChatText = "";
			}
			if (player.editedChestName)
			{
				NetMessage.SendData(33, -1, -1, Main.chest[player.chest].name, player.chest, 1f, 0f, 0f, 0, 0, 0);
				player.editedChestName = false;
			}
			if (Main.netMode == 1)
			{
				if (left == player.chestX && top == player.chestY && player.chest >= 0)
				{
					player.chest = -1;
					Recipe.FindRecipes();
					Main.PlaySound(11, -1, -1, 1);
				}
				else
				{
					NetMessage.SendData(31, -1, -1, "", left, (float)top, 0f, 0f, 0, 0, 0);
					Main.stackSplit = 600;
				}
			}
			else
			{
				int chest = Chest.FindChest(left, top);
				if (chest >= 0)
				{
					Main.stackSplit = 600;
					if (chest == player.chest)
					{
						player.chest = -1;
						Main.PlaySound(11, -1, -1, 1);
					}
					else
					{
						player.chest = chest;
						Main.playerInventory = true;
						Main.recBigList = false;
						player.chestX = left;
						player.chestY = top;
						Main.PlaySound(player.chest < 0 ? 10 : 12, -1, -1, 1);
					}
					Recipe.FindRecipes();
				}
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.player[Main.myPlayer];
			Chest chest = ChestUtils.GetChestSafe(i, j);
			player.showItemIcon2 = -1;
			if (chest == null)
			{
				player.showItemIconText = Lang.chestType[0];
			}
			else
			{
				player.showItemIconText = chest.name.Length > 0 ? chest.name : "Ejector Chest";
				if (player.showItemIconText == "Ejector Chest")
				{
					player.showItemIcon2 = mod.ItemType("EjectorChest");
					player.showItemIconText = "";
				}
			}
			player.noThrow = 2;
			player.showItemIcon = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			MouseOver(i, j);
			Player player = Main.player[Main.myPlayer];
			if (player.showItemIconText == "")
			{
				player.showItemIcon = false;
				player.showItemIcon2 = 0;
			}
		}
	}
}