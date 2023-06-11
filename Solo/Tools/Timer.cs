using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo
{    
    public class Timer
    {
        protected int _currentTime;
        protected bool _isStart;

        public int Period;
        public int Count { get; protected set; }

        /// <summary>
        /// После объявления запустить используя Start().
        /// <param name="period">Миллисекунды</param>
        /// </summary>
        public Timer(int period)
        {
            _currentTime = 0;
            _isStart = false;
            Count = 0;
            Period = period;
        }

        public void Start()
        {
            if (!_isStart)
            {
                _isStart = true;
            }
        }

        public void Stop()
        {
            if (_isStart)
            {
                _isStart = false;
            }
        }

        public void Reset()
        {
            _currentTime = 0;
            Count = 0;
        }

        /// <summary>
        /// Тик-так
        /// </summary>
        public bool Update(GameTime gameTime)
        {
            if (_isStart)
            {
                _currentTime += gameTime.ElapsedGameTime.Milliseconds;
                if (_currentTime > Period)
                {
                    _currentTime = 0;
                    Count++;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Таймер с периодом в секунду.
        /// </summary>
        static public Timer GetDefault()
        {
            return new Timer(1000);
        }
    }
}