using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
        public ICollider CheckCollision(IGameObject go);
        public void onCollision(GameObjectInfo GOInfo); 
        public void OnGUI(MouseState mauseState);
    }
}