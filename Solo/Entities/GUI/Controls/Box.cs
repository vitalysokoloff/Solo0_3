using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class Box : Control
    {
        public Box(Rectangle drawRect, GUIStyle style) : base (drawRect, style)
        {          
               
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, DrawRect, _sourceRect, _color, 0, Vector2.Zero, SpriteEffects.None, 0.40f);            
            if (Icon != null)
                spriteBatch.Draw(Icon, DrawRect, IconSourceRect, _color, 0, Vector2.Zero, SpriteEffects.None, 0.50f);
            spriteBatch.DrawString(Style.Font, _text, _textPosition, _textColor, 0, Vector2.Zero, 1, SpriteEffects.None, 0.80f);
        }        
    }
}