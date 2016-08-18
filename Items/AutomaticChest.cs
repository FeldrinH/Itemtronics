using Terraria.ID;
using Terraria.ModLoader;

namespace Itemtronics.Items
{
	public class AutomaticChest : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Automatic Chest";
			item.width = 26;
			item.height = 22;
			item.maxStack = 99;
			AddTooltip("Automatically picks up items.");
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 500;
			item.createTile = mod.TileType("AutomaticChest");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ItemID.Chest);
			recipe.AddIngredient(ItemID.DirtBlock);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}