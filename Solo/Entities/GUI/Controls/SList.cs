using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class SList : Control
    {
        public string[] List {get; protected set;}

        protected SwitchButton[] switches;
        protected SwitchButton[] drawElements;
        protected int pointer;

        /// <summary>
        /// После объявления запустить используя Start().
        /// <param name="elemDrawRect">Координаты и размеры первого элемента в списке</param>
        /// <param name="drawElemQty">Количесвто одновременно отображаемых элементов списка</param>
        /// </summary>
        public SList(Rectangle elemDrawRect, GUIStyle style, string[] list, int drawElemQty) : base (elemDrawRect, style)
        {   
            pointer = 0;       
            List = list;
            drawElements = new SwitchButton[drawElemQty];
            SetList(list);
        }

        public void SetList(string[] list)
        {
            pointer = 0;
            List = list;
            switches = new SwitchButton[list.Length];
            for (int i = 0; i < switches.Length; i++)
            {
                switches[i] = new SwitchButton(new Rectangle(DrawRect.X, DrawRect.Y, DrawRect.Width, DrawRect.Height),
                                                Style, list[i], false);                
            }
            SetDrawElem();
        }

        public void ScrollUp(int delta)
        {
            pointer -= delta;
            if (pointer < 0)
                pointer = 0;
            SetDrawElem();
        }

        public void ScrollDown(int delta)
        {
            pointer += delta;
            if (pointer > switches.Length - 1)
                pointer = switches.Length - 1;
            SetDrawElem();
        }

        public override void OnGUI(Rectangle hoverRect, bool aButton, bool bButton, bool cButton)
        {
            for (int i = 0; i < drawElements.Length; i++)
            {
                if (drawElements[i] != null)
                    drawElements[i].OnGUI(hoverRect, aButton, bButton, cButton);
            }
        }

        public override void SetText(string text)
        {
            
            if (switches != null)
            {
                for (int i = 0; i < switches.Length; i++)
                {
                    switches[i].IsActive = false;
                }                
            }
            _text = text;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < drawElements.Length; i++)
            {
                if (drawElements[i] != null)
                    drawElements[i].Draw(gameTime, spriteBatch);
            }
        }

        protected void SetDrawElem()
        {
            for (int i = 0, j = pointer; i < drawElements.Length; i++, j++)
            {
                if (j < switches.Length)
                {
                    drawElements[i] = switches[j];
                    drawElements[i].DrawRect = new Rectangle( _drawRect.X,
                                                            _drawRect.Y + i * _drawRect.Height,
                                                            _drawRect.Width,
                                                            _drawRect.Height
                                                            );
                    string text = drawElements[i].GetText();
                    drawElements[i].SetText(text);
                    
                    drawElements[i].AButtonAction += () =>
                    {
                        SetText(text);    
                        SConsole.Write(GetText());                    
                    };
                }
                else
                {
                    drawElements[i] = null;
                }                
            }
        }   
    }
}