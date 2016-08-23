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
			AddTooltip("Recieves items from connected outlets and places them in an adjacent chest");
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.mech = true;
			item.value = 5000;
			item.createTile = mod.TileType("ItemInlet");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddIngredient(ItemID.Actuator, 5);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}