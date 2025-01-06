using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace BoundTogether
{
    public class TetheredPlayer : ModPlayer
    {
        private const float MaxDistance = 500f;
        private const string ChainTexturePath = "Terraria/Images/Chain";

        public override void PostUpdate()
        {
            Player nextPlayer = FindNextPlayer();
            if (nextPlayer != null) {                
                RestrictMovement(nextPlayer); 
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            Player nextPlayer = FindNextPlayer();
            if (nextPlayer != null) {
                DrawChain(nextPlayer, drawInfo); 
            }
        }

        private Player FindNextPlayer() {
            foreach (Player otherPlayer in Main.player) {
                if (otherPlayer.active && otherPlayer.whoAmI > Player.whoAmI) {
                    return otherPlayer;
                }
            }
            
            return null;
        }

        private void RestrictMovement(Player targetPlayer)
        {
            float distance = Vector2.Distance(Player.Center, targetPlayer.Center);

            if (distance > MaxDistance)
            {
                Vector2 direction = Player.Center - targetPlayer.Center;
                direction.Normalize();

                Player.position = targetPlayer.Center + direction * MaxDistance - new Vector2(Player.width / 2, Player.height / 2);

                Player.AddBuff(BuffID.Dazed, 60);
                //targetPlayer.AddBuff(BuffID.Dazed, 60);
                // if (Main.myPlayer == Player.whoAmI)
                // {
                //     Main.NewText("You cannot move that far away from your partner!", Microsoft.Xna.Framework.Color.Red);
                // }
            }
        }

        private void DrawChain(Player targetPlayer, PlayerDrawSet drawInfo)
        {
            Vector2 start = Player.Center;
            Vector2 end = targetPlayer.Center;
            Vector2 direction = end - start;
            float length = direction.Length();
            direction.Normalize();

            Texture2D chainTexture = ModContent.Request<Texture2D>(ChainTexturePath).Value;

            for (float i = 0; i < length; i += chainTexture.Width)
            {
                Vector2 position = start + direction * i - Main.screenPosition;

                Main.EntitySpriteDraw(
                    chainTexture,
                    position,
                    null,
                    Color.White,
                    direction.ToRotation() + MathHelper.PiOver2,
                    new Vector2(chainTexture.Width / 2f, chainTexture.Height / 2f),
                    1f,
                    SpriteEffects.None,
                    0
                );
            }
        }
    }
}
