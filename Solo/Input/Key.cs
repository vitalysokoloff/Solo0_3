using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Solo.Input
{
    public class Key
    {
        protected Keys _key;
        protected Buttons _button;
        protected MouseButtons _mouseButton;
        protected int _state; // 0 - мышь, 1 - клава, 2 - геймпад
        protected bool _pressed;
        public Key(Keys key)
        {
            _key = key;
            _state = 1;
            _pressed = false;
        }

        public Key(Buttons button)
        {
            _button = button;
            _state = 2;
            _pressed = false;
        }

        public Key(MouseButtons button)
        {
            _mouseButton = button;
            _state = 0;
            _pressed = false;
        }

        public SKeyState Listen(PlayerIndex index)
        {
            switch (_state)
            {
                case 0:
                    if (GetPressedMouseButton() == _mouseButton)
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
                case 1:
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
                case 2:
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
                default:
                    return SKeyState.Up;
            }
        }

        public MouseButtons GetPressedMouseButton()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                return MouseButtons.Left;
            if (mouseState.RightButton == ButtonState.Pressed)
                return MouseButtons.Right;
            if (mouseState.MiddleButton == ButtonState.Pressed)
                return MouseButtons.Middle;
            return MouseButtons.None;
        }
    }
}