using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public interface IGameObject : IEntity 
    {
        public event MoveDelegate MoveEvent;
        public event RotateDelegate RotateEvent;
        public Vector2 Postion {get; set;}
        public string Type {get; protected set;}
        public string Name {get; protected set;}
        public Vector2 Direction {get; protected set;}
        public ICollider Collider {get; set;}
        public ISprite Sprite {get; set;}
        public ICollider CheckCollision(IGameObject go);
        public void OnCollision(GameObjectInfo GOInfo); 
    }
}