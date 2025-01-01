using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace BoundTogether.Content.Systems
{
    public class TetheredRope
    {
        private Player player1;
        private Player player2;

        public TetheredRope(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public void DrawRope()
        {
            Vector2 start = player1.Center;
            Vector2 end = player2.Center;
            CreateDustRope(start, end);
        }

        private void CreateDustRope(Vector2 start, Vector2 end)
        {
            Vector2 direction = end - start;
            float distance = direction.Length();
            int segments = (int)(distance / 8); 
            direction.Normalize();

            for (int i = 0; i <= segments; i++)
            {
                Vector2 position = start + direction * (i * 8); 
                Dust dust = Dust.NewDustPerfect(position, DustID.Smoke, Vector2.Zero, 100, Color.White, 1.2f);
                dust.noGravity = true; 
                dust.fadeIn = 1f; 
            }
        }
    }
}
