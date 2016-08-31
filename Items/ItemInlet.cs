using Terraria.ID;
using Terraria.ModLoader;

namespace Itemtronics.Items
{
	public class ItemInlet : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Item Inlet";
			item.width = 10;
			item.height = 12;
			item.maxStack = 99;
			AddTooltip("Takes items in from a chest and sends them to connected outlets");
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.mech = true;
			item.value = 1000;
			item.createTile = mod.TileType("ItemInlet");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddIngredient(ItemID.Actuator, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}