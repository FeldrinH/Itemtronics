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
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			Main.tileValue[Type] = 1000;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
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
	}
}