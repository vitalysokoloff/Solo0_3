using System.Collections.Generic;

namespace Solo.Collections
{
    public class Junk
    {
        private Dictionary<string, object> _objects;

        public Junk()
        {
            _objects = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            if (_objects.ContainsKey(key))
            {
                _objects[key] = value;
            }
            else
            {
                _objects.Add(key, value);
            }
        }

         public object Get(string key)
        {
            if (_objects.ContainsKey(key))
            {
                return _objects[key];
            }
            else
            {
                return new object();
            }
        }
    }
}