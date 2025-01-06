using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BoundTogether.Content.Buffs
{
	public class Taut : ModBuff
	{

        // public static readonly int DefenseBonus = 10;

        // public override LocalizedText Description => base.Description.WithFormatArgs(DefenseBonus);

        public override void Update(Player player, ref int buffIndex) {
            player.moveSpeed = 1;
            player.jumpSpeedBoost = -20;
		}
	}
}