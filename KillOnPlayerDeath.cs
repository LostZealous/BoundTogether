using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using System.IO;

namespace BoundTogether;
public class KillOnPlayerDeath : ModSystem
{
    public override void PostUpdatePlayers()
    {
        foreach (Player player in Main.player)
        {
            if (player.active && player.dead) // Check if the player is dead
            {
                HandleDeath(player);
            }
        }
    }

    private void HandleDeath(Player triggeringPlayer)
    {
        if (Main.netMode == NetmodeID.Server)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MessageType.PlayerDeathTriggered);
            packet.Write(triggeringPlayer.whoAmI);
            packet.Send();
        }

        KillAllPlayers(triggeringPlayer);
    }

    public static void KillAllPlayers(Player triggeringPlayer)
    {
        foreach (Player player in Main.player)
        {
            if (player.active && !player.dead && player.whoAmI != triggeringPlayer.whoAmI)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason($"{triggeringPlayer.name}'s death caused {player.name} to perish!"), 9999, 0);
            }
        }
    }

    public enum MessageType
    {
        PlayerDeathTriggered
    }
}
