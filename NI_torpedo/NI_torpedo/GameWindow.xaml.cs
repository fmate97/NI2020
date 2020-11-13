using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NI_torpedo
{
    public partial class GameWindow : Window
    {
        private int _tabla_merete = 10, _tabla_magassaga = 300, _tabla_szelessege = 300, _margo_merete = 3;
        private int _x_coord_seged = 0, _y_coord_seged = 0;
        private List<Vector> _tabla_kocka_helyzete = new List<Vector>();
        private List<Vector> _random_hajo_pos = new List<Vector>();
        private int[] _hajok_hossza = { 5, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2 };
        private bool _isTwoPlayer;

        public GameWindow(bool isTwoPlayer, string nev)
        {
            InitializeComponent();

            _isTwoPlayer = isTwoPlayer;
            nevLabel.Content = nev;
            tabla_kocka_helyzete_init();
            foreach (Vector kocka in _tabla_kocka_helyzete)
            {
                jatektabla_init(kocka, sajat_jatektabla);
                jatektabla_init(kocka, masik_player_jatektabla);
            }
            eredmenyjelzo.Content = "Kérem helyezze fel a következő hajókat:" + '\n' 
                + "1 x 5 egység hosszú" + '\n' + "2 x 4 egység hosszú" + '\n' + "4 x 3 egység hosszú" + '\n' + "4 x 2 egység hosszú";
            random_hajo_gen();
        }

        private void tabla_kocka_helyzete_init()
        {
            for (int i = 0; i < _tabla_merete; i++)
            {
                for (int j = 0; j < _tabla_merete; j++)
                {
                    _tabla_kocka_helyzete.Add(new Vector(i, j));
                }
            }
        }

        private void jatektabla_init(Vector position, Canvas canvas_name)
        {
            var unitX = _tabla_szelessege / _tabla_merete;
            var unitY = _tabla_magassaga / _tabla_merete;

            var shape = new Rectangle();            
            shape.Fill = Brushes.Black;
            shape.Width = unitX;
            shape.Height = unitY;
            Canvas.SetTop(shape, position.Y * unitY);
            Canvas.SetLeft(shape, position.X * unitX);
            canvas_name.Children.Add(shape);

            var shape2 = new Rectangle();
            shape2.Fill = Brushes.White;
            shape2.Width = unitX - _margo_merete;
            shape2.Height = unitY - _margo_merete;
            Canvas.SetTop(shape2, position.Y * unitY + (_margo_merete / 2));
            Canvas.SetLeft(shape2, position.X * unitX + (_margo_merete / 2));
            canvas_name.Children.Add(shape2);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                random_hajo_gen();
                /*HelpWindow helpWindow = new HelpWindow();
                helpWindow.Show();*/
            }
            else if (e.Key == Key.F2 && Keyboard.IsKeyDown(Key.LeftShift) && !_isTwoPlayer)
            {
                show_al_hajo(masik_player_jatektabla);
            }
            else if (e.Key == Key.LeftShift && Keyboard.IsKeyDown(Key.F2) && !_isTwoPlayer)
            {
                show_al_hajo(masik_player_jatektabla);
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.F2 || Keyboard.IsKeyUp(Key.LeftShift)) && !_isTwoPlayer)
            {
                hide_al_hajo(masik_player_jatektabla);
            }
        }

        private void show_al_hajo(Canvas canvas_name)
        {
            foreach (Vector random_hajo in _random_hajo_pos)
            {
                jatektabla_setup(random_hajo, canvas_name, Brushes.Blue);
            }
        }

        private void hide_al_hajo(Canvas canvas_name)
        {
            foreach (Vector random_hajo in _random_hajo_pos)
            {
                jatektabla_setup(random_hajo, canvas_name, Brushes.White);
            }
        }

        private void jatektabla_setup(Vector position, Canvas canvas_name, Brush brush)
        {
            var unitX = _tabla_szelessege / _tabla_merete;
            var unitY = _tabla_magassaga / _tabla_merete;

            var shape = new Rectangle();
            shape.Fill = brush;
            shape.Width = unitX - _margo_merete;
            shape.Height = unitY - _margo_merete;
            Canvas.SetTop(shape, position.Y * unitY + (_margo_merete / 2));
            Canvas.SetLeft(shape, position.X * unitX + (_margo_merete / 2));
            canvas_name.Children.Add(shape);
        }

        private void random_hajo_gen()
        {
            if(!_isTwoPlayer)
            {
                Random rnd = new Random();
                if(_random_hajo_pos.Count != 0)
                {
                    _random_hajo_pos.Clear();
                }
                foreach (int hajo_hossza in _hajok_hossza)
                {
                    int irany = rnd.Next(0, 3); // 0 - bal, 1 - fel, 2 - jobb, 3 - le
                    Vector random_pos;
                    bool helyes_pos = false;
                    while (!helyes_pos)
                    {
                        random_pos = new Vector(rnd.Next(0, _tabla_merete - 1), rnd.Next(0, _tabla_merete - 1));
                        bool foglalt = true;
                        if (_random_hajo_pos.Count > 0)
                        {
                            while (foglalt)
                            {
                                foreach (Vector random_hajo in _random_hajo_pos)
                                {
                                    if (random_pos == random_hajo)
                                    {
                                        foglalt = true;
                                        break;
                                    }
                                    foglalt = false;
                                }
                                if (foglalt)
                                {
                                    random_pos = new Vector(rnd.Next(0, _tabla_merete - 1), rnd.Next(0, _tabla_merete - 1));
                                }
                            }
                        }
                        switch (irany)
                        {
                            case 0:
                                if (random_pos.X - hajo_hossza >= 0)
                                {
                                    helyes_pos = true;
                                }
                                else
                                {
                                    helyes_pos = false;
                                }
                                break;
                            case 1:
                                if (random_pos.Y - hajo_hossza >= 0)
                                {
                                    helyes_pos = true;
                                }
                                else
                                {
                                    helyes_pos = false;
                                }
                                break;
                            case 2:
                                if (random_pos.X + hajo_hossza <= _tabla_merete - 1)
                                {
                                    helyes_pos = true;
                                }
                                else
                                {
                                    helyes_pos = false;
                                }
                                break;
                            case 3:
                                if (random_pos.X + hajo_hossza <= _tabla_merete - 1)
                                {
                                    helyes_pos = true;
                                }
                                else
                                {
                                    helyes_pos = false;
                                }
                                break;
                        }
                        if (helyes_pos)
                        {
                            if (_random_hajo_pos.Count > 0)
                            {
                                for (int i = 1; i < hajo_hossza; i++)
                                {
                                    Vector teszt;
                                    switch (irany)
                                    {
                                        case 0:
                                            teszt = new Vector(random_pos.X - i, random_pos.Y);
                                            break;
                                        case 1:
                                            teszt = new Vector(random_pos.X, random_pos.Y - i);
                                            break;
                                        case 2:
                                            teszt = new Vector(random_pos.X + i, random_pos.Y);
                                            break;
                                        case 3:
                                            teszt = new Vector(random_pos.X, random_pos.Y + i);
                                            break;
                                    }
                                    foreach (Vector random_hajo in _random_hajo_pos)
                                    {
                                        if (teszt == random_hajo)
                                        {
                                            helyes_pos = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    _random_hajo_pos.Add(random_pos);
                    for (int i = 1; i < hajo_hossza; i++)
                    {
                        switch (irany)
                        {
                            case 0:
                                random_pos = new Vector(random_pos.X - 1, random_pos.Y);
                                break;
                            case 1:
                                random_pos = new Vector(random_pos.X, random_pos.Y - 1);
                                break;
                            case 2:
                                random_pos = new Vector(random_pos.X + 1, random_pos.Y);
                                break;
                            case 3:
                                random_pos = new Vector(random_pos.X, random_pos.Y + 1);
                                break;
                        }
                        _random_hajo_pos.Add(random_pos);
                    }
                }
            }
        }

        private void sajat_jatektabla_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _x_coord_seged = 0;
            _y_coord_seged = 0;
            Point eger_pos = e.GetPosition(sajat_jatektabla);
            jatektabla_setup(new Vector(coord_conv(eger_pos.X, _x_coord_seged) - 1, coord_conv(eger_pos.Y, _y_coord_seged) - 1), sajat_jatektabla, Brushes.Red);
        }

        private void masik_player_jatektabla_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _x_coord_seged = 0;
            _y_coord_seged = 0;
            Point eger_pos = e.GetPosition(masik_player_jatektabla);
            //jatektabla_setup(new Vector(coord_conv(eger_pos.X, _x_coord_seged) - 1, coord_conv(eger_pos.Y, _y_coord_seged) - 1), masik_player_jatektabla, Brushes.Red);
        }

        private int coord_conv(double number, int seged)
        {
            var unit = _tabla_magassaga / _tabla_merete;

            if(number < 0 || number > _tabla_magassaga)
            {
                return -1;
            }
            else if(number <= unit)
            {
                seged++;
                return seged;
            }
            else
            {
                seged++;
                seged = coord_conv(number - unit, seged);
                return seged;
            }
        }
    }
}
