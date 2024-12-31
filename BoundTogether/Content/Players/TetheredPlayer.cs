using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using BoundTogether.Content.Systems;

namespace BoundTogether.Content.Players
{
    public class TetheredPlayer : ModPlayer
    {
        private const float MaxRopeLength = 500f;
        

        public override void PostUpdate()
        {
            foreach (Player otherPlayer in Main.player)
            {
                if (!otherPlayer.active || otherPlayer == Player) continue;

                
                float distance = Vector2.Distance(Player.Center, otherPlayer.Center);
                if (distance > MaxRopeLength)
                {
                    ApplyTetherEffects(Player, otherPlayer, distance);
                }

                
                TetheredRope rope = new TetheredRope(Player, otherPlayer);
                rope.DrawRope();
            }
        }


        public override void OnHurt(Player.HurtInfo info)
        {
            // Trigger shared damage for all active players
            foreach (Player otherPlayer in Main.player)
            {
                if (!otherPlayer.active || otherPlayer == Player) continue;

                Player.HurtInfo sharedInfo = new Player.HurtInfo
                {
                    Damage = info.Damage,
                    HitDirection = info.HitDirection,
                    CooldownCounter = info.CooldownCounter,
                    DamageSource = info.DamageSource
                };

                otherPlayer.Hurt(sharedInfo);
            }
        }

        private void ApplyTetherEffects(Player player1, Player player2, float distance)
        {
            Vector2 direction = player2.Center - player1.Center;
            direction.Normalize();

            float excessDistance = distance - MaxRopeLength;
            player2.position -= direction * (excessDistance * 0.5f);
            player1.position += direction * (excessDistance * 0.5f);

            player1.AddBuff(BuffID.Slow, 60);
            player2.AddBuff(BuffID.Slow, 60);
        }
    }
}
