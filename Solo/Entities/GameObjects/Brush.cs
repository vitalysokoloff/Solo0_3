using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities 
{
    // Сделать массив массивов чтобы не считать каждый раз положение конкретной текстуры
    
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
        public float Layer {get; set;}
        public string Type {get; set;}
        public string Name {get; set;}
        public Vector2 Direction {get; set;}
        public ICollider Collider {get; set;}
        SMaterial Material {get; set;}

        protected Point _clonesQty;
        protected float _angle;
        protected Vector2 _position;
        protected Rectangle _rect;
        protected Vector2 _pivot;

        public Brush(SMaterial material)
        {
            Init(material, new Rectangle(0, 0, material.Texture.Width, material.Texture.Height), 0f);
        }
        public Brush(SMaterial material, float angle)
        {
            Init(material, new Rectangle(0, 0, material.Texture.Width, material.Texture.Height), angle);
        }
        public Brush(SMaterial material, Rectangle rect)
        {
            Init(material, rect, 0f);
        }
        public Brush(SMaterial material, Rectangle rect, float angle)
        {
            Init(material, rect, angle);
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
                    Point size = new Point(Material.SourceRectangle.Width, Material.SourceRectangle.Height);
                    Rectangle drawRect = new Rectangle(_rect.X + i * size.X, _rect.Y + j * size.Y, size.X, size.Y);
                    spriteBatch.Draw(Material.Texture, drawRect, Material.SourceRectangle, Color.White, _angle, _pivot, SpriteEffects.None, Layer);
                }
            }
            Collider.Draw(gameTime, spriteBatch);
        }

        protected void Init(SMaterial material, Rectangle rect, float angle)
        {
            _rect = rect;
            _pivot = new Vector2(_rect.Width / 2, _rect.Height / 2);
            Material = material;            
            int x = _rect.Width / material.Texture.Width;
            int y = _rect.Height / material.Texture.Height;
            Postion = new Vector2(_rect.Center.X, _rect.Center.Y);
            _clonesQty = new Point(x, y);
            Collider = new Collider(new SRectangle(_rect.X, _rect.Y, _rect.Width, _rect.Height), Vector2.Zero, (GraphicsDeviceManager)SConsole.Stuff.Get("graphics"))           
            {
                Parent = this
            };
            Angle = angle; 
        }    
    }
}