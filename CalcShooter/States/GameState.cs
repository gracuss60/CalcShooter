using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcShooter
{
    /// <summary>
    ///  tutaj realizowany jest przebieg gry
    /// </summary>
    internal class GameState : Component 
    {
        public static List<Equation> rownania = new List<Equation>(); // lista obiektów równań
        private float spawn = 0;
        // parametry okna, tła, armaty, czcionki i pozycji
        public const int windowWidth = 1280;
        public const int windowHeight = 720; 
        private Texture2D backgroundSprite;
        public Texture2D tankSprite;
        private SpriteFont font;
        public int counter = 0; 
        public object CurrentLevel { get; private set; } // obsługa aktualnego poziomu
        // parametry textboxa, długości wpisanej frazy do boxa, parametry klawiatury oraz myszki.
        private TextBox _allCharacterTextBox;
        private Vector2 _allCharacterTextBoxPosition;
        private int _length=7;
        private MouseState _MouseOneClickState;
        private KeyboardState _KeyboardOnePressState;
        private int _timeBeforeNextDelete=10; 
        private Keys[] keys; // klawisze
        // zegar,punkty, ilość równań oraz życia
        public static int lives = 3;
        public static int HowMany = 10;
        private int Visiblitycounter = 0;
        public static int points = 0;
        public static float clock = 0;

        /// <summary>
        ///  załadowanie potrzebnych zasobów, grafiki, tła
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content) 
        {
            _allCharacterTextBoxPosition = new Vector2(470, 600);
            backgroundSprite = Content.Load<Texture2D>("Sprites/background");
            tankSprite = Content.Load<Texture2D>("Sprites/tankSmall");
            font = Content.Load<SpriteFont>("fonts/LightFont");
            _allCharacterTextBox = new TextBox(Content.Load<Texture2D>("Controls/textBox"), Content.Load<Texture2D>("Controls/FlashingCursor"), new Point(122, 24), new Point(2, 12), _allCharacterTextBoxPosition, _length, true, font, string.Empty, 0.9f);
        }

        /// <summary>
        /// rysowanie poszczególnych parametrów
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White); // tło
            for (int i = 0; i < HowMany; i++)
            {
                if (rownania[i].isVisible == true)
                {
                    rownania[i].Draw(spriteBatch, font); // rysowanie równań
                }
            }
            spriteBatch.Draw(tankSprite, new Vector2(windowWidth / 2 - tankSprite.Width / 2, windowHeight / 2 - tankSprite.Height / 2), Color.White); // działo
            spriteBatch.DrawString(font, "ilosc zyc " + lives.ToString(), new Vector2(200, 660), Color.Black); // życia
            spriteBatch.DrawString(font, "Punkty " + points.ToString(), new Vector2(1100, 660), Color.Black); // punkty
            spriteBatch.DrawString(font, "Czas " + clock.ToString("0.0"), new Vector2(0, 100), Color.White); // liczynik
            _allCharacterTextBox.Render(spriteBatch); // render textboxa, zawartości jego tekstu , wyświetlania |
        }

        /// <summary>
        ///  system rozgrywki
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) 
        {
            clock += (float)gameTime.ElapsedGameTime.TotalSeconds; // licznik czasu

            _KeyboardOnePressState = KeyboardOnePress.GetState();
            _MouseOneClickState = MouseOneclick.GetState(); // stan klawiatury oraz myszki
            HandleInput(gameTime);
            _allCharacterTextBox.Update();
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds; // obsługa odstępów w czasie pomiędzy generowaniem kolejnych równan na ekranie
            if (spawn >= 3 && counter < HowMany)
            {
                spawn = 0;
                rownania[counter].isVisible = true;

                counter++;
            }
            foreach (Equation equation in rownania)
                if (equation.isVisible == true && equation.result != 200) // aktualizacja obiektów równan gdy zostały już wygenerowane na ekranie oraz ich wyników jest różny od 200- co oznacza już poprawne wpisanie wyniku ktory został wcześniej wygenerowany
                {
                    equation.Update();
                }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter)) // sprawdzanie odpowiedzi po wciśnięciu entera
            {
                for (int i = 0; i < HowMany; i++)
                {
                    if (IsGoodAnswer(TextBox.CurrentText, rownania[i].rownanie, rownania[i].result, rownania[i].isVisible) == true) // sprawdzenie odpowiedzi odwołując się do metody isGoodAnswer
                    {
                        rownania[i].isVisible = false;
                        points = points + Math.Abs(rownania[i].result);
                        rownania[i].result = 200;
                        //zmiana parametrów po poprawnej odpowiedzi, dodanie punktów, zmiana widocznosci oraz pomocnicza zmiana wyniku
                    }
                }
                TextBox.CurrentText = ""; // reset stanu textboxa po wciśnięciu enter( usunięcie poprzednio wpisanego tekstu aby nie wciskac backspace
            }

            if (rownania[HowMany - 1].isVisible == false && rownania[HowMany - 1].result == 200) // jeden z warunków sprawdzania zwycięstwa, gdy ostatnie działanie zostało poprawnie wpisane
            {
                for (int i = 0; i < HowMany; i++)
                {
                    if (rownania[i].isVisible == false)
                    {
                        Visiblitycounter++; // pomocnicze sprawdzenie czy zniknęły wszystkie równania , licznik stanów
                    }
                }
                if (Visiblitycounter == HowMany) // w przypadku zniknięcia wszystkich działan- zwycięstwo
                {
                    Data.CurrentState = Data.States.win;
                }
                else
                {
                    Visiblitycounter = 0;
                }
            }
            if (rownania[HowMany - 1].result == 300 && GameState.lives > 0) // gdy ostatnie działanie zostało błędnie odpowiedziane, ale pozostały jeszcze życia
            {
                for (int i = 0; i < HowMany; i++)
                {
                    if (rownania[i].isVisible == false)
                    {
                        Visiblitycounter++;
                    }
                }
                if (Visiblitycounter == HowMany)
                {
                    Data.CurrentState = Data.States.win;
                }
                else
                {
                    Visiblitycounter = 0;
                }
            }
        }
        /// <summary>
        /// obsługa klawiszy
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        private Keys ExtractSingleCharacterOrNumber(Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if ((int)key >= 48 && (int)key <= 192) // jeśli znajduje się w podanym zakresie zwraca wartośc wciśniętego przycisku po np shifcie.
                    return key;
            }
            return Keys.None;
        }

        /// <summary>
        ///  sprawdzanie wpisanej odpowiedzi
        /// </summary>
        /// <param name="CurrentText"></param>
        /// <param name="rownanie"></param>
        /// <param name="result"></param>
        /// <param name="IsVisible"></param>
        /// <returns></returns>
        public bool IsGoodAnswer(string CurrentText, string rownanie, int result, bool IsVisible) 
        {
            if (CurrentText.Contains(rownanie) == true && IsVisible == true) // porównanie wpisanego podciągu z ciągiem który znajduje się w wygenerowanym obiekcie, np na ekranie 1*2, a wpisany został 1*2=2
            {
                if (CurrentText.EndsWith(result.ToString()) == true) // porównanie stringów, końca wpisanej odpowiedzi z wynikiem danego obiektu przekonwertowanym na stringa
                {
                    return true; // zwrot prawdy w przypadku porawnej odpowiedzi
                }
                else
                    return false; //fałsz w przypadku niepoprawnej odpowiedzi
            }
            else

                return false; // fałsz w przypadku niepoprawnego wpisania działania
        }

        /// <summary>
        ///  sprawdzanie pozycji kursora, wybór textboxa gdy klikniemy go
        /// </summary>
        private void HandleLeftMouseButtonClick() 
        {
            if (_MouseOneClickState.X >= _allCharacterTextBox.Position.X && _MouseOneClickState.X <= _allCharacterTextBox.Position.X + _allCharacterTextBox.CellWidth)
            {
                if (_MouseOneClickState.Y >= _allCharacterTextBox.Position.Y && _MouseOneClickState.Y <= _allCharacterTextBox.Position.Y + _allCharacterTextBox.CellHeight)
                {
                    _allCharacterTextBox.Selected = true;
                }
            }
        }

        /// <summary>
        ///  obsługa inputu, wpisywanych liter oraz znaków, liter wraz z przyciśniętym klawiszem SHIFT
        /// </summary>
        /// <param name="gameTime"></param>
        protected void HandleInput(GameTime gameTime) 
        {
             keys = _KeyboardOnePressState.GetPressedKeys(); // przyjmuje przyciśnięte klawisze
            string value = string.Empty;
            if (_MouseOneClickState.LeftButton == ButtonState.Pressed)
            {
                if (MouseOneclick.HasNotBeenPressed(true))
                {
                    HandleLeftMouseButtonClick(); // obsługa textboxa po jego kliknięciu
                }
            }

            if (_KeyboardOnePressState.IsKeyUp(Keys.Back) && _KeyboardOnePressState.IsKeyUp(Keys.Delete)) // ustawienie czasu usuwania
                _timeBeforeNextDelete = 10;
            if (keys.Count() > 0)
            {
                if (keys.Count() > 1) // po wciśnięciu klawisza shift , wywołanie metody ekstracji klawisza po shifcie
                    keys[0] = ExtractSingleCharacterOrNumber(keys);
                if (_KeyboardOnePressState.IsKeyDown(Keys.Back) || _KeyboardOnePressState.IsKeyDown(Keys.Delete))
                {
                    if (_timeBeforeNextDelete == 0)
                        _timeBeforeNextDelete = 10;
                    if (_timeBeforeNextDelete == 10) // zazębienie czasów usuwania aby było płynne , usuwanie tekstu po uprzednim wciśnięciu backspace/delete 
                    {
                        if (_allCharacterTextBox.Selected)
                            _allCharacterTextBox.AddMoreText('\b');
                        _timeBeforeNextDelete--; // po przytrzymaniu backspace przez conajmniej sekundę, usuwanie.
                    }
                    else
                        _timeBeforeNextDelete--;
                    return;
                }
                if (_allCharacterTextBox.Selected && (((int)keys[0] >= 48 && (int)keys[0] <= 192)) || keys[0] == Keys.RightShift || keys[0] == Keys.LeftShift) // jeśli textboxa wciśnięty, sprawdzanie shiftów oraz wciśnięcia litery 
                {
                    
                    if (keys[0] == Keys.RightShift || keys[0] == Keys.LeftShift)
                    {
                        if (KeyboardOnePress.HasNotBeenPressed(keys[0]))
                        {
                            return; //return gdy wciśnięty sam shift
                        }
                    }
                    if (KeyboardOnePress.HasNotBeenPressed(keys[0])) // gdy klawisz został wciśnięty
                    {
                        if ((int)keys[0] >= 84 && (int)keys[0] <= 192) // warunki na znaki specjalne, i wpisanie ich bądź liter
                        {
                            if (keys[0] == Keys.OemPlus)
                            {
                                value = "=";
                            }
                            else if (keys[0] == Keys.OemQuestion)
                            {
                                value = "/";
                            }
                            else if (keys[0] == Keys.OemMinus)
                            {
                                value = "-";
                            }
                            else if (keys[0] == Keys.Add)
                            {
                                value = "+";
                            }
                            else
                                value = keys[0].ToString().Substring(keys[0].ToString().Length - 1); // gdy wciśniety klawisz na numerycznej
                            _allCharacterTextBox.AddMoreText(value.ToCharArray()[0]); // wpisanie do textboxa
                        }
                        else
                            _allCharacterTextBox.AddMoreText((char)keys[0]); //wpisanie do textboxa
                    }
                }
            }
        }
    }
}