using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CalcShooter
{
    public class Equation
    {
        // właściwości równań 
        public string rownanie { get; set; }
        public int result { get; set; }//wynik
        private Random random = new Random(); // randomizacja cyfr oraz znaków.
        private int number1, sign, number2, upOrDown; // ciało równania oraz pozycja góra/dół wygenerowanego równania
        private char[] stringChar = { '-', '+', '*', '/' }; // zbiór znaków
        public static float velocity = 4; // prędkość 
        public Vector2 position { get; set; }
        public bool isVisible = false; // widocznośc na ekranie

        /// <summary>
        ///  wygenerowanie parametrów poszczególnych równań
        /// </summary>
        public Equation() 
        {
            number1 = random.Next(1, 9); // losowanie znaku oraz liczb
            sign = random.Next(0, 4);
            if (sign == 3) // dzięki temu liczba zawsze będzie podzielna przez pierwszą w przypadku znaku dzielenia.
            {
                while (true)
                {
                    number2 = random.Next(1, number1);
                    if (number1 % number2 == 0)
                        break;
                }
            }
            else
                number2 = random.Next(1, 9);

            switch (sign)// obliczanie wyniku wygenerowanych parametrów równania
            {
                case 0: result = number1 - number2; break;
                case 1: result = number1 + number2; break;
                case 2: result = number1 * number2; break;
                case 3: result = number1 / number2; break;
            }
            rownanie = number1.ToString() + stringChar[sign] + number2.ToString(); // przypisanie parametró do równania, później, obiektu
            int xpos, ypos;
            xpos = random.Next(0, 1200);// wygenerowanie pozycji na obramowaniu ekranu, z której będzie nadlatywało równanie

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
            position = new Vector2(xpos, ypos);// przypisanie pozycji
        }

        /// <summary>
        ///  metoda obsługująca lot równania do środka, przy użyciu funkcji matematycznych i zmiennych.
        /// </summary>
        public void Follow()
        {
            if (this.result != 200 && this.isVisible == true)
            {
                var distance = new Vector2(640, 360) - this.position;
                var _rotation = (float)Math.Atan2(distance.Y, distance.X);
                var Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
                var currentDistance = Vector2.Distance(this.position, new Vector2(640, 360));
                position += Direction * velocity;
            }
            if (this.position.X > 600 & this.position.X < 760 & this.position.Y > 330 & this.position.Y < 400) // ustawienie "hitboxa" w przypadku przelotu równania do postaci znajdującej się na środku ekranu.
            {
                this.result = 300; // pomocniczo zmieniany jest wynik do rozdzielenia elementów dobrze wpisanych, od źle wpisanych, oraz od tyh nadlatujących.
                this.isVisible = false;
                this.position = new Vector2(random.Next(600, 650), random.Next(10, 760)); 
                GameState.lives--; // odjęcie życia, zmiana pozycji oraz widoczności danego równania
            }
        }

        /// <summary>
        ///  aktualizacja ruchu, oraz przejście do innego stanu gry w przypadku utraty żyć, reset zegara
        /// </summary>
        public void Update() 
        {
            Follow();
            if (GameState.lives == 0)
            {
                Data.CurrentState = Data.States.loss;
                GameState.clock = 0;
            };
        }

        /// <summary>
        ///  rysowanie wygenerowanych równań.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="font"></param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font) 
        {
            spriteBatch.DrawString(font, rownanie, position, Color.Black);
        }
    }
}