using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CalcShooter
{
    public class Equation
    {
        public string rownanie { get; set; }
        public int result { get; set; }
        private Random random = new Random();
        private int number1, sign, number2, upOrDown, LeftOrRight;
        private char[] stringChar = { '-', '+', '*', '/' };
        public Vector2 position { get; set; }

        public Equation()
        {
            number1 = random.Next(0, 9);// choosing between 0-10
            sign = random.Next(0, 3);
            number2 = random.Next(0, 9);

            switch (sign)
            {
                case 0: result = number1 - number2; break;
                case 1: result = number1 + number2; break;
                case 2: result = number1 * number2; break;
                case 3: result = number1 / number2; break;
            }
            rownanie = number1.ToString() + stringChar[sign] + number2.ToString();
            int xpos, ypos;
            xpos = random.Next(0, 1200);
            // ypos = random.Next(0, 780);
            if (xpos < 40 || xpos > 1100)
            {
                ypos = random.Next(10, 760);
            }
            else
            {
                upOrDown = random.Next(0, 2);
                if (upOrDown == 0)
                {
                    ypos = random.Next(5, 15);
                }
                else
                {
                    ypos = random.Next(600, 650);
                }
            }
            position = new Vector2(xpos, ypos);
        }

        public void Follow()
        {
            var distance = new Vector2(640, 360) - this.position;
            var _rotation = (float)Math.Atan2(distance.Y, distance.X);
            var Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
            var currentDistance = Vector2.Distance(this.position, new Vector2(640, 360));
            position += Direction * 0.5f;
        }

        public void Update()
        {
            Follow();
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
       {
          spriteBatch.DrawString(font, rownanie, position, Color.Black);
       }

        
    }
}