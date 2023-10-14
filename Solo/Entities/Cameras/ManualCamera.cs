using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Solo.Input;

namespace Solo.Entities
{
    public class ManualCamera : Camera
    {
        public Vector2 Speed { get; set; }

        protected KeysInput _input;
        public ManualCamera(GraphicsDeviceManager graphics) : base (graphics)
        {
            Speed = new Vector2(1, 1);
            _input = new KeysInput();
            _input.Add("Up", new Key(Keys.W));
            _input.Add("Down", new Key(Keys.S));
            _input.Add("Left", new Key(Keys.A));
            _input.Add("Right", new Key(Keys.D));
             _input.Add("Up", new Key(Keys.Up));
            _input.Add("Down", new Key(Keys.Down));
            _input.Add("Left", new Key(Keys.Left));
            _input.Add("Right", new Key(Keys.Right));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 delta = new Vector2(0, 0);
            if (_input.IsDown("Up"))
                delta.Y -= Speed.Y;
            if (_input.IsDown("Down"))
                delta.Y += Speed.Y;
            if (_input.IsDown("Left"))
                delta.X -= Speed.X;
            if (_input.IsDown("Right"))
                delta.X += Speed.X;
            Position += delta;
        }
    }
}