using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public class GameObjectInfo
    {
        public string Name {get; set;}
        public string Type {get; set;}
        public Vector2 Direction {get; set;}
        public Vector2 Position {get; set;}
        public Vector2 CollisionNormal {get; set;}

        public GameObjectInfo()
        {
            Name = "unknown";
            Type = "unknown";
            Direction = Vector2.Zero;
            Position = Vector2.Zero;
            CollisionNormal = Vector2.Zero;
        }

        public GameObjectInfo(string name)
        {
            Name = name;
            Type = "unknown";
            Direction = Vector2.Zero;
            Position = Vector2.Zero;
            CollisionNormal = Vector2.Zero;
        }

        public GameObjectInfo(string name, string type)
        {
            Name = name;
            Type = type;
            Direction = Vector2.Zero;
            Position = Vector2.Zero;
            CollisionNormal = Vector2.Zero;
        }

        public GameObjectInfo(string name, string type, Vector2 direction)
        {
            Name = name;
            Type = type;
            Direction = direction;
            Position = Vector2.Zero;
            CollisionNormal = Vector2.Zero;
        }

        public GameObjectInfo(string name, string type, Vector2 direction, Vector2 position)
        {
            Name = name;
            Type = type;
            Direction = direction;
            Position = position;
            CollisionNormal = Vector2.Zero;
        }

        public GameObjectInfo(string name, string type, Vector2 direction, Vector2 position, Vector2 normal)
        {
            Name = name;
            Type = type;
            Direction = direction;
            Position = position;
            CollisionNormal = normal;
        }

        public GameObjectInfo(IGameObject go, Vector2 normal)
        {
            Name = go.Name;
            Type = go.Type;
            Direction = go.Direction;
            Position = go.Postion;
            CollisionNormal = normal;
        }
    }
}