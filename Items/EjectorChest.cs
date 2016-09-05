using Terraria.ID;
using Terraria.ModLoader;

namespace Itemtronics.Items
{
	public class EjectorChest : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Ejector Chest";
			item.width = 26;
			item.height = 22;
			item.maxStack = 99;
			AddTooltip("Drops a stack of items when activated");
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.mech = true;
			item.value = 500;
			item.createTile = mod.TileType("EjectorChest");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Chest);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddIngredient(ItemID.Actuator, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}