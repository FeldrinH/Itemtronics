using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Itemtronics.Tiles
{
	class ItemInlet : ModTile
	{
		public override void SetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

		}
	}
}
