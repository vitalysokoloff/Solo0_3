using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Physics;

namespace Solo.Entities 
{
    /// <summary>
    /// Это как правило "активные" игровые объекты. Спрайт не заливает всю площадь браша, а является имено что спрайтом
    /// Коллайдер может быть произвольным шейпом.
    /// </summary>
    public class Prop : IGameObject
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
        public string Category 
        { 
            get
            {
                return _category;
            }
            set
            {

            }
        }
        public string Type {get; set;}
        public string Name {get; set;}
        public bool IsAlive {get; set;}
        public bool IsExist {get; set;}
        public Vector2 Direction {get; set;}
        public ICollider Collider {get; set;}
        public Rectangle DrawRect 
        {
            get
            {
                return _sprite.DrawRectangle;
            }
        } 

        protected string _category;
        protected float _angle;
        protected Vector2 _position;
        protected Sprite _sprite;

        public virtual void Init(string name, Vector2 position, Sprite sprite, Collider collider, float layer)
        {
            Postion = position;
            _sprite = sprite;
            Layer = layer;
            _sprite.Layer = Layer;
            Collider = collider;
            _category = "prop";
            Type = "unknown";
            Name = name;
            _angle = 0;
            IsAlive = true;
            IsExist = true;
            Direction = Vector2.Zero;
        }

        public void CheckCollision(IGameObject go)
        {
            if (go.IsExist)
            {
                if (go.Category == "prop" && go.Category == "brush")
                {
                    if (GJK.CheckCollision(go.Collider.GetShape(), Collider.GetShape()))
                    {
                        Vector2 normal = CollisionInformation.GetNormal(go.Collider.GetShape(), Collider.GetShape());
                        OnCollision(new GameObjectInfo(
                            go.Name,
                            go.Type,
                            go.Category,
                            go.Direction,
                            go.Postion,
                            normal)
                        );
                    }
                    else
                    {
                        return;
                    }
                } else if (go.Category == "trigger")
                {
                    if (go.DrawRect.Intersects(DrawRect))
                    OnCollision(new GameObjectInfo(
                            go.Name,
                            go.Type,
                            go.Category)
                        );
                }
            }
        }
        public virtual void OnCollision(GameObjectInfo GOInfo){}
        public virtual void OnTrigger(GameObjectInfo GOInfo){}
        public virtual void Move(Vector2 delta){}
        public virtual void Rotate(float delta){} 
        public virtual void GUI(GameTime gameTime, SpriteBatch spriteBatch){}
        public virtual void Debug(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Collider.Draw(gameTime, spriteBatch);
        } 
        public virtual void Update(GameTime gameTime){}
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(gameTime, spriteBatch);
        }  
    }
}