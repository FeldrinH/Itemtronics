using Itemtronics.Util;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Itemtronics.Tiles
{
	class ItemInlet : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
			drop = mod.ItemType("ItemInlet");
		}

		/*public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			Tile tile = Main.tile[i, j];
			tile.frameX = 36;
			//tile.frameY = 0;
			return true;
		}*/

		public override void RightClick(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			int dir = (tile.frameX / 18 + 1) % 4;
			tile.frameX = (short)(dir * 18);
			Main.NewText(ChestUtils.GetTargetChestID(x, y, dir).ToString());
		}

		public override void HitWire(int x, int y)
		{
			if (!(Wiring._currentWireColor == ItemWire.CurWireColor && Wiring._wireList.))
			{

			}
		}
	}
}
