using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public interface IGameObject : IEntity 
    {
        public event MoveDelegate MoveEvent;
        public event RotateDelegate RotateEvent;
        public Vector2 Postion {get; set;}
        public float Angle {get; set;}
        public string Type {get; set;}
        public string Name {get; set;}
        public Vector2 Direction {get; set;}
        public ICollider Collider {get; set;}
        public ICollider CheckCollision(IGameObject go);
        public void OnCollision(GameObjectInfo GOInfo);
        public void Move(Vector2 delta);
        public void Rotate(float delta);        
    }
}