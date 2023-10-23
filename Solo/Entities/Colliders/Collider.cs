using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities 
{    
    public class Collider : ICollider
    {
        public IGameObject Parent 
        { 
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
                _parent.MoveEvent += OnMove;
                _parent.RotateEvent += OnRotate;
                _shape.Position = _parent.Postion + Position;
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                if (_parent != null)
                    _shape.Position = _parent.Postion + value;
                else
                    _shape.Position = value;
            }
        }

        protected IGameObject _parent;
        protected Vector2 _position;
        protected bool _state;
        protected Shape _shape;
        
        public Collider(Shape shape, Vector2 position)
        {
            _shape = shape;
            Position = position;
            On();
        }

        public Collider(Shape shape, Color color, Vector2 position)
        {
            _shape = shape;
            Position = position;
            _shape.SetColor(color);
            On();
        }

        public void OnMove(Vector2 position)
        {
            _shape.Position = position + Position;
        }

        public void OnRotate(float angle)
        {
            _shape.Angle = angle;
        }

         public void On()
        {
            _state = true;
        }

        public void Off()
        {
            _state = false;
        }

        public void SetTexture(GraphicsDeviceManager graphics)
        {
            _shape.SetTexture(graphics);
        }

        public bool GetState()
        {
            return _state;
        }

        public Shape GetShape()
        {
            if (_state)
                return _shape;
            else
                return null;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_state)
                _shape.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_state)
                _shape.Draw(gameTime, spriteBatch);
        }

        public static Collider Box()
        {
            return new Collider(new SRectangle(0, 0, 50, 50), Vector2.Zero);
        }
        public static Collider Box(Vector2 position)
        {
            return new Collider(new SRectangle(0, 0, 50, 50), position);
        }
        public static Collider Box(int edgeSize)
        {
            return new Collider(new SRectangle(0, 0, edgeSize, edgeSize), Vector2.Zero);
        }
        public static Collider Box(int edgeSize, Vector2 position)
        {
            return new Collider(new SRectangle(0, 0, edgeSize, edgeSize), position);
        }
        public static Collider Box(int edgeSize, Color color, Vector2 position)
        {
            return new Collider(new SRectangle(0, 0, edgeSize, edgeSize), color, position);
        }
    }
}