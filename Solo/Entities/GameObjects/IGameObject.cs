using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public interface IGameObject : IEntity 
    {
       public event MoveDelegate MoveEvent;
       public event RotateDelegate RotateEvent;
       public Vector2 Postion {get; set;}
    }
}