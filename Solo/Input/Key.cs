using Microsoft.Xna.Framework.Input;

namespace Solo.Input
{
    public class Key
    {
        private Keys _key;
        private bool _pressed;
        public Key(Keys key)
        {
            _key = key;
            _pressed = false;
        }

        public SKeyState Listen()
        {
            if (Keyboard.GetState().IsKeyUp(_key))
            {
                _pressed = false;
                return SKeyState.Up;
            }
            else
            {
                if (!_pressed) 
                {
                    _pressed = true;
                    return SKeyState.Pressed;
                }
                return SKeyState.Down;
            }
        }
    }
}