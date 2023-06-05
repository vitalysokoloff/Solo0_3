using System.Collections.Generic;
using Microsoft.Xna.Framework;

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

        public StickDirections GetStickDirections()
        {
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