using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Solo.Entities 
{
    public delegate void MoveDelegate(Vector2 position);
    public delegate void RotateDelegate(float angle);
    public delegate void GUIDelegate(MouseState mouseState, KeyboardState keyboardState);
}