using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;

namespace Solo.Collections
{
    public class Heap
    {
        private Dictionary<string, int> _ints;
        private Dictionary<string, float> _floats;
        private Dictionary<string, string> _strings;
        private Dictionary<string, bool> _bools;
        private Dictionary<string, Point> _points;
        private Dictionary<string, Vector2> _vectors;
        private Dictionary<string, Heap> _heaps;

        public Heap()
        {
            _ints = new Dictionary<string, int>();
            _floats = new Dictionary<string, float>();
            _strings = new Dictionary<string, string>();
            _bools = new Dictionary<string, bool>();
            _points = new Dictionary<string, Point>();
            _vectors = new Dictionary<string, Vector2>();
            _heaps = new Dictionary<string, Heap>();
        }

        public void Add(string key, int value)
        {
            if (_ints.ContainsKey(key))
            {
                _ints[key] = value;
            }
            else
            {
                _ints.Add(key, value);
            }
        }

        public void Add(string key, float value)
        {
            if (_floats.ContainsKey(key))
            {
                _floats[key] = value;
            }
            else
            {
                _floats.Add(key, value);
            }
        }

        public void Add(string key, string value)
        {
            if (_strings.ContainsKey(key))
            {
                _strings[key] = value;
            }
            else
            {
                _strings.Add(key, value);
            }
        }

        public void Add(string key, bool value)
        {
            if (_bools.ContainsKey(key))
            {
                _bools[key] = value;
            }
            else
            {
                _bools.Add(key, value);
            }
        }

        public void Add(string key, Point value)
        {
            if (_points.ContainsKey(key))
            {
                _points[key] = value;
            }
            else
            {
                _points.Add(key, value);
            }
        }

        public void Add(string key, Vector2 value)
        {
            if (_vectors.ContainsKey(key))
            {
                _vectors[key] = value;
            }
            else
            {
                _vectors.Add(key, value);
            }
        }

        public void Add(string key, Heap value)
        {
            if (_heaps.ContainsKey(key))
            {
                _heaps[key] = value;
            }
            else
            {
                _heaps.Add(key, value);
            }
        }

        public int GetInt(string key)
        {
            if (_ints.ContainsKey(key))
            {
                return _ints[key];
            }
            else
            {
                return 0;
            }
        }

        public float GetFloat(string key)
        {
            if (_floats.ContainsKey(key))
            {
                return _floats[key];
            }
            else
            {
                return 0f
;
            }
        }

        public string GetString(string key)
        {
            if (_strings.ContainsKey(key))
            {
                return _strings[key];
            }
            else
            {
                return "";
            }
        }

        public bool GetBool(string key)
        {
            if (_bools.ContainsKey(key))
            {
                return _bools[key];
            }
            else
            {
                return false;
            }
        }

        public Point GetPoint(string key)
        {
            if (_points.ContainsKey(key))
            {
                return _points[key];
            }
            else
            {
                return Point.Zero;
            }
        }

        public Vector2 GetVector2(string key)
        {
            if (_vectors.ContainsKey(key))
            {
                return _vectors[key];
            }
            else
            {
                return Vector2.Zero;
            }
        }

        public Heap GetHeap(string key)
        {
            if (_heaps.ContainsKey(key))
            {
                return _heaps[key];
            }
            else
            {
                return null;
            }
        }

        public Dictionary<string, Heap>.KeyCollection GetHeapKeys()
        {
            return _heaps.Keys;
        }

        public Dictionary<string, string>.KeyCollection GetStringsKeys()
        {
            return _strings.Keys;
        }

        public void Save(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                WriteToDisk(sw, "");
            }
        }

        public static Heap Open(string path)
        {
            Heap heap = new Heap();
            string fileName = path;

            if (!File.Exists(fileName))
            {
                File.Create(fileName);
                return heap;
            }

            using (StreamReader sr = new StreamReader(fileName))
            {             
                heap = ReadFromDisk(sr);          
            }

            return heap;
        }

        protected void WriteToDisk(StreamWriter sw, string indent)
        {
            foreach (string key in _ints.Keys)
            {
                sw.WriteLine(indent + key + ": " + _ints[key]);                
            }

            foreach (string key in _floats.Keys)
            {
                sw.WriteLine(indent + key + ": " + _floats[key] + "f");
            }

            foreach (string key in _strings.Keys)
            {
                sw.WriteLine(indent + key + ": \"" + _strings[key] + "\"");
            }

            foreach (string key in _bools.Keys)
            {
                sw.WriteLine(indent + key + ": " + (_bools[key]? "True" : "False"));
            }

            foreach (string key in _points.Keys)
            {
                sw.WriteLine(indent + key + ": " + _points[key].X + "." + _points[key].Y);
            }

            foreach (string key in _vectors.Keys)
            {
                sw.WriteLine(indent + key + ": " + _vectors[key].X + "f." + _vectors[key].Y + "f");
            }

            foreach (string key in _heaps.Keys)
            {
                sw.WriteLine(indent + key + " {" );
                indent += "\t";
                _heaps[key].WriteToDisk(sw, indent);
                indent = indent.Remove(indent.Length - 1, 1);
                sw.WriteLine(indent + "}");                
            }
        }

        protected static Heap ReadFromDisk(StreamReader sr)
        {
            Heap heap = new Heap();
            
            while (sr.Peek() >= 0)
            {
                string[] data = ParseString(sr.ReadLine());

                switch (data[0])
                {
                    case "int":
                        heap.Add(data[1], Convert.ToInt32(data[2]));
                        break;
                    case "float":
                        heap.Add(data[1], (float)Convert.ToDouble(data[2]));
                        break;
                    case "string":
                        heap.Add(data[1], data[2]);
                        break;
                    case "bool":
                        heap.Add(data[1], data[2] == "+" || data[2] == "true" || data[2] == "True" || data[2] == "on" || data[2] == "On" ? true : false);
                        break;
                    case "point":
                        string[] tmp = data[2].Split('.');
                        heap.Add(data[1], new Point(Convert.ToInt32(data[2]), Convert.ToInt32(data[3])));
                        break;
                    case "vector2":
                        heap.Add(data[1], new Vector2((float)Convert.ToDouble(data[2]), (float)Convert.ToDouble(data[3])));
                        break;
                    case "heap":
                        heap.Add(data[1], ReadFromDisk(sr));                        
                        break;
                    case "end":
                        return heap;
                    default:
                        break;
                }
            }
            return heap;
        }

        protected static string[] ParseString(string str)
        {
            string[] answer;
            string intPattern = @"^.+\s*:\s*\d+\s*$";
            string floatPattern = @"^.+\s*:\s*((\d+,\d+)|(\d+))f\s*$";
            string stringPattern = "^.+\\s*:\\s*\".+\"\\s*$";
            string heapPattern = @"^.+\s*{\s*$";
            string endPattern = @"^\s*}\s*$";
            string boolPattern = @"^.+\s*:\s*True|False\s*$";
            string pointPattern = @"^.+\s*:\s*\d+\.\d+\s*$";
            string vectorPattern = @"^.+\s*:\s*((\d+,\d+)|(\d+))f\.((\d+,\d+)|(\d+))f\s*$";

            str = str.Trim(' ');            

            if (Regex.IsMatch(str, intPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[3];

                answer[0] = "int";
                answer[1] = tmp[0].Trim(' ', '\t');
                answer[2] = tmp[1].Trim(' ', '\t');

                return answer;    
            }

            if (Regex.IsMatch(str, floatPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[3];

                answer[0] = "float";
                answer[1] = tmp[0].Trim(' ', '\t');
                answer[2] = tmp[1].Trim(' ', '\t', 'f');

                return answer;
            }

            if (Regex.IsMatch(str, stringPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[3];

                answer[0] = "string";
                answer[1] = tmp[0].Trim(' ', '\t');
                answer[2] = tmp[1].Trim(' ', '\t').Trim('"');

                return answer;
            }

            if (Regex.IsMatch(str, boolPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[3];

                answer[0] = "bool";
                answer[1] = tmp[0].Trim(' ', '\t');
                answer[2] = tmp[1].Trim(' ', '\t');

                return answer;
            }

            if (Regex.IsMatch(str, pointPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[4];

                answer[0] = "point";
                answer[1] = tmp[0].Trim(' ', '\t');

                string[] tmp2 = tmp[1].Trim(' ', '\t').Split('.');

                answer[2] = tmp2[0];
                answer[3] = tmp2[1];

                return answer;
            }

            if (Regex.IsMatch(str, vectorPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[4];

                answer[0] = "vector2";
                answer[1] = tmp[0].Trim(' ', '\t');

                string[] tmp2 = tmp[1].Trim(' ', '\t').Split('.');

                answer[2] = tmp2[0].Trim('f');
                answer[3] = tmp2[1].Trim('f');

                return answer;
            }

            if (Regex.IsMatch(str, heapPattern))
            {
                answer = new string[2];

                answer[0] = "heap";
                answer[1] = str.Trim(' ', '\t', '{');

                return answer;
            }

            if (Regex.IsMatch(str, endPattern))
            {
                answer = new string[1];

                answer[0] = "end";

                return answer;
            }

            answer = new string[3];
            return answer;
        }      

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string key in _ints.Keys)
            {
                sb.Append("int  " + key + ": " + _ints[key] + "\n");
            }

            foreach (string key in _floats.Keys)
            {
                sb.Append("float  " + key + ": " + _floats[key] + "\n");
            }

            foreach (string key in _strings.Keys)
            {
                sb.Append("string  " + key + ": " + _strings[key] + "\n");
            }

            foreach (string key in _bools.Keys)
            {
                sb.Append("bool  " + key + ": " + _bools[key] + "\n");
            }

            foreach (string key in _points.Keys)
            {
                sb.Append("point  " + key + ": " + _points[key].ToString() + "\n");
            }

            foreach (string key in _vectors.Keys)
            {
                sb.Append("vector2  " + key + ": " + _vectors[key].ToString() + "\n");
            }

            foreach (string key in _heaps.Keys)
            {
                sb.Append("\nheap  " + key + "  {\n");
                sb.Append(_heaps[key].ToString());
                sb.Append("}\n");
            }

            return sb.ToString();
        }
    }
}