using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Solo.Input
{
    public class Key
    {
        private Keys _key;
        private Buttons _button;
        private bool _gamePad;
        private bool _pressed;
        public Key(Keys key)
        {
            _key = key;
            _gamePad = false;
            _pressed = false;
        }

        public Key(Buttons button)
        {
            _button = button;
            _gamePad = true;
            _pressed = false;
        }

        public SKeyState Listen(PlayerIndex index)
        {
            if (!_gamePad)
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
            else
            {
                if (GamePad.GetState(index).IsButtonUp(_button))
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
}