using Terraria.ID;
using Terraria.ModLoader;

namespace Itemtronics.Items
{
	public class ItemTranslocator : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Item Translocator";
			item.width = 10;
			item.height = 12;
			item.maxStack = 99;
			AddTooltip("Moves a stack of items from one adjacent chest to another when activated");
			AddTooltip2("Right click to change direction");
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.mech = true;
			item.value = 1000;
			item.createTile = mod.TileType("ItemTranslocator");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BoosterTrack, 1);
			recipe.AddIngredient(ItemID.Wire, 5);
			recipe.AddIngredient(ItemID.Actuator, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}