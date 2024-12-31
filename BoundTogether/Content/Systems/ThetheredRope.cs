using Microsoft.Xna.Framework.Graphics;

public void DrawRope()
{
    Vector2 start = player1.Center;
    Vector2 end = player2.Center;

    
    Texture2D tongueTexture = TextureAssets.Projectile[ProjectileID.Tongue].Value;

    Vector2 direction = end - start;
    float rotation = (float)Math.Atan2(direction.Y, direction.X);
    float length = direction.Length();

    
    for (float i = 0; i < length; i += tongueTexture.Height * 0.5f)
    {
        Vector2 position = start + direction * (i / length);
        Main.spriteBatch.Draw(tongueTexture, position - Main.screenPosition, null, Color.White,
                              rotation, new Vector2(tongueTexture.Width * 0.5f, tongueTexture.Height * 0.5f),
                              1f, SpriteEffects.None, 0f);
    }
}
