using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoundTogether
{
    public class UnityCrest : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.accessory = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Iterate through all active players
            foreach (Player otherPlayer in Main.player)
            {
                if (otherPlayer.active && !otherPlayer.dead)
                {
                    // Count the number of Unity Crest accessories equipped by this player
                    int unityCrestCount = 0;

                    for (int i = 0; i < otherPlayer.armor.Length; i++)
                    {
                        Item equippedItem = otherPlayer.armor[i];
                        if (equippedItem != null && equippedItem.type == Item.type)
                        {
                            unityCrestCount++;
                        }
                    }

                    // Apply the defense bonus to all active players
                    player.statDefense += unityCrestCount * 3;
                }
            }
        }


        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "UnityCrestTooltip", "Grants all players in the world +3 defense for each equipped Unity Crest.");
            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            Recipe ironRecipe = CreateRecipe();
            ironRecipe.AddIngredient(ItemID.Shackle, 1);
            ironRecipe.AddIngredient(ItemID.IronBar, 10);
            ironRecipe.AddIngredient(ItemID.FallenStar, 5);
            ironRecipe.AddTile(TileID.Anvils);
            ironRecipe.Register();

            Recipe leadRecipe = CreateRecipe();
            leadRecipe.AddIngredient(ItemID.Shackle, 1);
            leadRecipe.AddIngredient(ItemID.LeadBar, 10);
            leadRecipe.AddIngredient(ItemID.FallenStar, 5);
            leadRecipe.AddTile(TileID.Anvils);
            leadRecipe.Register();
        }
    }
}
