using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Solo.Entities 
{
    public delegate void GUIDelegate(Rectangle hoverRect, bool aButton, bool bButton);
    public delegate void ControlAction();
}