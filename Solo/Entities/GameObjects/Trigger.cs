using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Physics;

namespace Solo.Entities 
{
    /// <summary>
    /// Это как правило "активные" игровые объекты. Спрайт не заливает всю площадь браша, а является имено что спрайтом
    /// Коллайдер может быть произвольным шейпом.
    /// </summary>
    public class Trigger : IGameObject
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
        public float Angle  {get; set;}
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
        public Rectangle DrawRect {get; set;}

        protected string _category;
        protected Vector2 _position;

        public virtual void Init()
        {
            
        }

        public void CheckCollision(IGameObject go)
        {
            if (go.IsExist)
            {
                if (go.Category == "prop")
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
           
        }  
    }
}