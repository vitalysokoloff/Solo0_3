using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Solo.Input
{
    public class KeysInput : ISInput
    {
        private Dictionary<string, List<Key>> _keys;
        private PlayerIndex _index;

        public KeysInput()
        {
            _keys = new Dictionary<string, List<Key>>();
            _index = PlayerIndex.One;
        }
        public KeysInput(PlayerIndex index)
        {
            _keys = new Dictionary<string, List<Key>>();
            _index = index;
        }

        public KeysInput(Dictionary<string, List<Key>> keys)
        {
            _keys = keys;
            _index = PlayerIndex.One;
        }

        public KeysInput(Dictionary<string, List<Key>> keys, PlayerIndex index)
        {
            _keys = keys;
            _index = index;
        }

        public bool IsPressed(string keyName)
        {
            SKeyState resault = SKeyState.Up;
            List<Key> list = _keys[keyName];

            for (int i = 0; i < list.Count; i++)
            {
                resault = list[i].Listen(_index);
                if (resault == SKeyState.Pressed)
                    return true;
            }

            return false;
        }

        public bool IsDown(string keyName)
        {
            SKeyState resault = SKeyState.Up;
            List<Key> list = _keys[keyName];

            for (int i = 0; i < list.Count; i++)
            {
                resault = list[i].Listen(_index);
                if (resault == SKeyState.Down)
                    return true;
            }

            return false;
        }

        public StickDirections GetLeftStickDirections()
        {
            float x = GamePad.GetState(_index).ThumbSticks.Left.X;
            float y = GamePad.GetState(_index).ThumbSticks.Left.Y;

            if (y > 0.8f)
                return StickDirections.Up;
            if (x > 0.2f && y > 0.2f)
                return StickDirections.RightUp;
            if (x > 0.8f)
                return StickDirections.Right;
            if (x > 0.2f && y < -0.3f)
                return StickDirections.RightDown;
            if (y < -0.8f)
                return StickDirections.Down;
            if (x < -0.2f && y < -0.2f)
                return StickDirections.LeftDown;
            if (x < -0.8f)
                return StickDirections.Left;
            if (x < -0.2f && y > 0.2f)
                return StickDirections.LeftUp;

            return StickDirections.Undefined;
        }

        public StickDirections GetRightStickDirections()
        {
            float x = GamePad.GetState(_index).ThumbSticks.Right.X;
            float y = GamePad.GetState(_index).ThumbSticks.Right.Y;

            if (y > 0.8f)
                return StickDirections.Up;
            if (x > 0.2f && y > 0.2f)
                return StickDirections.RightUp;
            if (x > 0.8f)
                return StickDirections.Right;
            if (x > 0.2f && y < -0.3f)
                return StickDirections.RightDown;
            if (y < -0.8f)
                return StickDirections.Down;
            if (x < -0.2f && y < -0.2f)
                return StickDirections.LeftDown;
            if (x < -0.8f)
                return StickDirections.Left;
            if (x < -0.2f && y > 0.2f)
                return StickDirections.LeftUp;

            return StickDirections.Undefined;
        }

        public void Add(string keyName, Key key)
        {
            if (!_keys.ContainsKey(keyName))
                _keys.Add(keyName, new List<Key>());
            _keys[keyName].Add(key);
        }
    }
}