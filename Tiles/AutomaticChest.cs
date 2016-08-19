﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Itemtronics.Tiles
{
	public class AutomaticChest : ModTile
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
			AddMapEntry(new Color(200, 200, 200), "Automatic Chest", MapChestName);
			dustType = mod.DustType("Sparkle");
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.Containers };
			chest = "Automatic Chest";
			chestDrop = mod.ItemType("AutomaticChest");
		}

		public override void HitWire(int x, int y)
		{
			Chest chest = ChestUtils.GetModChest(x, y);

			for (int i = 0; i < 400; ++i)
			{
				if (Main.item[i].type != 0 && !ItemID.Sets.NebulaPickup[Main.item[i].type] && new Rectangle(chest.x * 16, chest.y * 16, 32, 32).Intersects(new Rectangle((int)Main.item[i].position.X, (int)Main.item[i].position.Y, Main.item[i].width, Main.item[i].height)))
				{
					int emptyIndex = Array.FindIndex(chest.item, item => item.type == 0);
					if (emptyIndex != -1)
					{
						chest.item[emptyIndex] = Main.item[i];
						Main.item[i] = new Item();
						if (Main.netMode == 2)
						{
							NetMessage.SendData(21, -1, -1, "", i, 0.0f, 0.0f, 0.0f, 0, 0, 0);
						}
					}
					else
					{
						break;
					}
				}
			}
		}

		public string MapChestName(string name, int i, int j)
		{
			string chestName = ChestUtils.GetModChest(i, j).name;
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
			Chest chest = ChestUtils.GetModChestSafe(i, j);
			player.showItemIcon2 = -1;
			if (chest == null)
			{
				player.showItemIconText = Lang.chestType[0];
			}
			else
			{
				player.showItemIconText = chest.name.Length > 0 ? chest.name : "Automatic Chest";
				if (player.showItemIconText == "Automatic Chest")
				{
					player.showItemIcon2 = mod.ItemType("AutomaticChest");
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