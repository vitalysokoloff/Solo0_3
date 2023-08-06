using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities 
{
    // У браша заменить айспрайт на айматериал, материал содержит в себе тестуру и сорс ректангл 
    /// <summary>
    /// Реализует игровой обект браш, это к примеру стены, фоны декорации, спрайт у брашей "замащивает"(заливает) весь игровой обект.
    /// Браш имеет прямоугольный коллайдер размером с сам браш.
    /// </summary>
     public class Brush : IGameObject
    {
        public event MoveDelegate MoveEvent;
        public event RotateDelegate RotateEvent;
        public Vector2 Postion 
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                MoveEvent?.Invoke(_position);
            }
        }
        public float Angle 
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
                RotateEvent?.Invoke(_angle);
            }
        }
        public string Type {get; set;}
        public string Name {get; set;}
        public Vector2 Direction {get; set;}
        public ICollider Collider {get; set;}
        public ISprite Sprite {get; set;}

        protected Point _clonesQty;
        protected float _angle;
        protected Vector2 _position;

        public Brush(ISprite sprite)
        {
            Init(sprite, new Rectangle(0, 0, sprite.GetSize().X, sprite.GetSize().Y), 0f);
        }
        public Brush(ISprite sprite, float angle)
        {
            Init(sprite, new Rectangle(0, 0, sprite.GetSize().X, sprite.GetSize().Y), angle);
        }
        public Brush(ISprite sprite, Rectangle rect)
        {
            Init(sprite, rect, 0f);
        }
        public Brush(ISprite sprite, Rectangle rect, float angle)
        {
            Init(sprite, rect, angle);
        }
        public ICollider CheckCollision(IGameObject go){return null;}
        public void OnCollision(GameObjectInfo GOInfo){}
        public void Move(Vector2 delta){}
        public void Rotate(float delta){}

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _clonesQty.X; i++)
            {
                for (int j = 0; j < _clonesQty.Y; j++)
                {
                    Sprite.Draw(gameTime, spriteBatch, new Point(i * Sprite.GetSize().X, j * Sprite.GetSize().Y));
                }
            }
            Collider.Draw(gameTime, spriteBatch);
        }

        protected void Init(ISprite sprite, Rectangle rect, float angle)
        {
            Sprite = sprite;            
            int x = rect.Width / sprite.GetSize().X;
            int y = rect.Height / sprite.GetSize().Y;
            Postion = new Vector2(rect.Center.X, rect.Center.Y);
            _clonesQty = new Point(x, y);
            Collider = new Collider(new SRectangle(rect.X, rect.Y, rect.Width, rect.Height), Vector2.Zero, (GraphicsDeviceManager)SConsole.Stuff.Get("graphics"))           
            {
                Parent = this
            };
            Angle = angle; 
        }    
    }
}