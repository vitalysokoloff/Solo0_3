using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities
{
    public class Camera : IEntity    {
        
        public IGameObject Focus {get; set;}
        public Vector2 Center { get; set;}
        public Matrix Transform { get; protected set; }
        public Vector3 Scale 
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
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
            }
        }
        public float X
        {
            get
            {
                return _position.X;
            }
            set
            {
                _position.X = value;
            }
        }
        public float Y
        {
            get
            {
                return _position.Y;
            }
            set
            {
                _position.Y = value;
            }
        }

        protected Vector2 _position;
        protected float _angle;
        protected Vector2 _pivot;
        protected Vector3 _scale;

        public Camera(GraphicsDeviceManager graphics)
        {
            float viewportWidth = graphics.PreferredBackBufferWidth;
            float viewportHeight = graphics.PreferredBackBufferHeight;

            Center = new Vector2(viewportWidth / 2, viewportHeight / 2);
            _scale = new Vector3(1, 1, 1);
        }

        public virtual void Update(GameTime gameTime)
        {
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-_position.X, -_position.Y, 0) *
                        Matrix.CreateRotationZ(_angle) *
                        Matrix.CreateTranslation(_pivot.X, _pivot.Y, 0) *
                        Matrix.CreateScale(_scale);

            _pivot = new Vector2(Center.X / _scale.X, Center.Y / _scale.Y);           
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }
    }
}