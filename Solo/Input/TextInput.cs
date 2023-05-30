using Microsoft.Xna.Framework.Input;
using Solo.Collections;

namespace Solo.Input
{
    public class TextInput
    {
        public Heap CharTable;

        private bool _keyDown;        

        public TextInput(Heap charTable)
        {
            _keyDown = false;
            CharTable = charTable;
        }

        public string Listen()
        {
            Keys[] ks = Keyboard.GetState().GetPressedKeys();
            
            if (ks.Length > 0)
            {
                if (!_keyDown)
                {
                    _keyDown = true;
                    return CharTable.GetString(ks[ks.Length - 1].ToString());
                }
                return null;
            }
            else
            {
                _keyDown = false;
                return null;
            }
        }
    }

    public static class Chars
    {
        public static Heap GetDefaultChars()
        {
            Heap resault = new Heap();
            resault.Add("D1", "1");
            resault.Add("D2", "2");
            resault.Add("D3", "3");
            resault.Add("D4", "4");
            resault.Add("D5", "5");
            resault.Add("D6", "6");
            resault.Add("D7", "7");
            resault.Add("D8", "8");
            resault.Add("D9", "9");
            resault.Add("D0", "0");
            resault.Add("OemMinus", "-");
            resault.Add("OemPlus", "+");
            resault.Add("Q", "q");
            resault.Add("W", "w");
            resault.Add("E", "e");
            resault.Add("R", "r");
            resault.Add("T", "t");
            resault.Add("Y", "y");
            resault.Add("U", "u");
            resault.Add("I", "i");
            resault.Add("O", "o");
            resault.Add("P", "p");
            resault.Add("A", "a");
            resault.Add("S", "s");
            resault.Add("D", "d");
            resault.Add("F", "f");
            resault.Add("G", "g");
            resault.Add("H", "h");
            resault.Add("J", "j");
            resault.Add("K", "k");
            resault.Add("L", "l");
            resault.Add("OemSemicolon", ":");
            resault.Add("OemQuotes", "\"");
            resault.Add("Z", "z");
            resault.Add("X", "x");
            resault.Add("C", "c");
            resault.Add("V", "v");
            resault.Add("B", "b");
            resault.Add("N", "n");
            resault.Add("M", "m");
            resault.Add("OemComma", ",");
            resault.Add("OemPeriod", ".");
            resault.Add("OemQuestion", "?");
            resault.Add("Tab", "  ");
            // resault.Add("OemTilde", "~");
            resault.Add("Space", " ");
            resault.Add("Enter", "\n");
            resault.Add("Back", "\t");
            return resault;
        }
    }
}