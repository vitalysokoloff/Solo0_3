using System.Collections.Generic;

namespace Solo.Input
{
    public class KeysInput : ISInput
    {
        private Dictionary<string, List<Key>> _keys;

        public KeysInput()
        {
            _keys = new Dictionary<string, List<Key>>();
        }

        public KeysInput(Dictionary<string, List<Key>> keys)
        {
            _keys = keys;
        }

        public SKeyState IsPressed(string keyName)
        {
            SKeyState resault = SKeyState.Up;
            List<Key> list = _keys[keyName];

            for (int i = 0; i < list.Count; i++)
            {
                resault = list[i].Listen();
                if (resault == SKeyState.Pressed)
                    return resault;
            }

            return resault;
        }

        public SKeyState IsDown(string keyName)
        {
            SKeyState resault = SKeyState.Up;
            List<Key> list = _keys[keyName];

            for (int i = 0; i < list.Count; i++)
            {
                resault = list[i].Listen();
                if (resault == SKeyState.Down)
                    return resault;
            }

            return resault;
        }

        public void Add(string keyName, Key key)
        {
            if (!_keys.ContainsKey(keyName))
                _keys.Add(keyName, new List<Key>());
            _keys[keyName].Add(key);
        }
    }
}