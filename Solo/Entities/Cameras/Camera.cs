using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities
{
    public class Camera : IEntity    {
        
        // Камера ректангл коллайдер для того чтобы не рисовать все объекты вне его
        public IGameObject Focus { get; set;}
        public Rectangle DrawRectangle { get; set;}
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
                Point newPosition = new Point((int)(_position.X - _pivot.X - 300), (int)(_position.Y - _pivot.Y - 300));
                Point size = new Point(_viewportWidth + 600, _viewportHeight + 600);
                DrawRectangle = new Rectangle(newPosition, size);
            }
        }

        public Vector2 LefUp
        {
            get
            {
                return new Vector2(_position.X - _pivot.X, _position.Y - _pivot.Y);
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
        protected int _viewportWidth;
        protected int _viewportHeight;

        public Camera(GraphicsDeviceManager graphics)
        {
            _viewportWidth = graphics.PreferredBackBufferWidth;
            _viewportHeight = graphics.PreferredBackBufferHeight;

            Center = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _scale = new Vector3(1, 1, 1);
            DrawRectangle = new Rectangle(-300, -300, _viewportWidth + 300, _viewportHeight + 300);
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