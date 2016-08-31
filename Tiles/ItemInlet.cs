using Itemtronics.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Itemtronics.Tiles
{
	class ItemInlet : ModTile
	{
		public override void SetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			Main.tileSolid[Type] = false;
		}

		public override void RightClick(int i, int j)
		{
			Main.NewText(ChestUtils.GetDynamicChestID(i, j-1).ToString());
		}
	}
}
