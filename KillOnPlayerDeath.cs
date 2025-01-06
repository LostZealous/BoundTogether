using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

public class KillOnPlayerDeath : ModSystem
{
    public override void OnPlayerKilled(Player player, double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MessageType.PlayerDeathTriggered);
                packet.Write(player.whoAmI);
                packet.Send();
            }
            return;
        }

        KillAllPlayers(player, damage, hitDirection);
    }

    public void KillAllPlayers(Player triggeringPlayer, double damage, int hitDirection)
    {
        foreach (Player p in Main.player)
        {
            if (p.active && !p.dead && p.whoAmI != triggeringPlayer.whoAmI)
            {
                p.KillMe(PlayerDeathReason.ByCustomReason($"{triggeringPlayer.name}'s death caused {p.name} to perish!"), damage, hitDirection);
            }
        }
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        MessageType msgType = (MessageType)reader.ReadByte();
        if (msgType == MessageType.PlayerDeathTriggered)
        {
            int triggeringPlayerId = reader.ReadInt32();
            Player triggeringPlayer = Main.player[triggeringPlayerId];
            if (triggeringPlayer != null && triggeringPlayer.active)
            {
                KillAllPlayers(triggeringPlayer, 9999, 0);
            }
        }
    }

    private enum MessageType
    {
        PlayerDeathTriggered
    }
}
