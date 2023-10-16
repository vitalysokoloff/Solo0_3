using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities 
{
    public interface IGameObject : IEntity 
    {
        public event MoveDelegate MoveEvent;
        public event RotateDelegate RotateEvent;
        public Vector2 Postion {get; set;}
        public float Angle {get; set;}
        public float Layer {get; set;}
        public string Type {get; set;}
        public string Name {get; set;}
        public bool IsAlive {get; set;}
        public bool IsExist {get; set;}
        public Vector2 Direction {get; set;}
        public ICollider Collider {get; set;}
        public Rectangle DrawRect {get;}    
        public ICollider CheckCollision(IGameObject go);
        public void OnCollision(GameObjectInfo GOInfo);
        public void OnTrigger(GameObjectInfo GOInfo);
        public void Move(Vector2 delta);
        public void Rotate(float delta);  
        public void GUI(GameTime gameTime, SpriteBatch spriteBatch); 
        public void Debug(GameTime gameTime, SpriteBatch spriteBatch);         
    }
}