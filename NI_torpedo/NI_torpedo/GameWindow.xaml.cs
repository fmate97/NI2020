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
        Random rnd = new Random();
        private int _tabla_merete = 10, _tabla_magassaga = 300, _tabla_szelessege = 300, _margo_merete = 3;
        private int _x_coord_seged = 0, _y_coord_seged = 0;
        private List<Vector> _tabla_kocka_helyzete = new List<Vector>();
        private List<Vector> _random_hajo_pos = new List<Vector>(), _player_hajo_pos = new List<Vector>();
        private Vector _sikeres_tipp;
        private bool _elozo_tipp_siker;
        private List<Vector> _player_rossz_tipp = new List<Vector>(), _player_jo_tipp = new List<Vector>(), _al_rossz_tipp = new List<Vector>(), _al_jo_tipp = new List<Vector>();
        private int[] _hajok_hossza = { 5, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2 };
        private int _jo_kockak_szama = 33;
        private bool _isTwoPlayer, _mentett_jatek = false, _player_jon = false;
        private int _korok_szam = 0, _sajat_talalat = 0, _ellenfel_talalat = 0, _player_number;

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
            //eredmenyjelzo.Content = "Kérem helyezze fel a következő hajókat:" + '\n' + "1 x 5 egység hosszú" + '\n' + "2 x 4 egység hosszú" + '\n' + "4 x 3 egység hosszú" + '\n' + "4 x 2 egység hosszú";
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
                HelpWindow helpWindow = new HelpWindow();
                helpWindow.Show();
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
            foreach (Vector random_hajo in _player_jo_tipp)
            {
                jatektabla_setup(random_hajo, canvas_name, Brushes.Green);
            }
            foreach (Vector random_hajo in _player_rossz_tipp)
            {
                jatektabla_setup(random_hajo, canvas_name, Brushes.Red);
            }
        }

        private void hide_al_hajo(Canvas canvas_name)
        {
            foreach (Vector random_hajo in _random_hajo_pos)
            {
                jatektabla_setup(random_hajo, canvas_name, Brushes.White);
            }
            foreach (Vector random_hajo in _player_jo_tipp)
            {
                jatektabla_setup(random_hajo, canvas_name, Brushes.Green);
            }
            foreach (Vector random_hajo in _player_rossz_tipp)
            {
                jatektabla_setup(random_hajo, canvas_name, Brushes.Red);
            }
        }

        private void help_button_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void sajat_jatektabla_mentes_button_Click(object sender, RoutedEventArgs e)
        {
            if (!_mentett_jatek)
            {
                if (_player_hajo_pos.Count != _jo_kockak_szama)
                {
                    MessageBox.Show("A hajók száma/hossza nem megfelelő!");
                }
                else
                {
                    _mentett_jatek = true;
                    start_game();
                }
            }
        }

        private void sajat_jatektabla_torles_button_Click(object sender, RoutedEventArgs e)
        {
            if (!_mentett_jatek)
            {
                foreach (Vector kocka in _tabla_kocka_helyzete)
                {
                    jatektabla_init(kocka, sajat_jatektabla);
                }
                _player_hajo_pos.Clear();
            }
        }

        private void start_game()
        {
            if (_mentett_jatek)
            {
                _player_number = rnd.Next(2);
                if (_player_number % 2 == 0)
                {
                    _player_jon = true;
                    MessageBox.Show("Ön kezd!");
                    _player_number++; 
                }
                else
                {
                    _player_jon = false;
                    MessageBox.Show("Gép kezd!");
                    al_tipp(false);
                    _player_number++;  
                }
            }
        }

        private void game()
        {
            _korok_szam++;
            eredmenyjelzo_update();
            if(_player_number % 2 == 0) //player
            {
                MessageBox.Show("Ön jön!");
                _player_number++;
                _player_jon = true;
            }
            else //gép
            {
                MessageBox.Show("Gép jön!");
                _player_number++;
                _player_jon = false;
                al_tipp(_elozo_tipp_siker);
            }
        }

        private void al_tipp(bool elozo_talalat)
        {
            if(!elozo_talalat)
            {
                Vector tipp = new Vector(rnd.Next(_tabla_merete), rnd.Next(_tabla_merete));
                bool ujra_general = true;
                while (ujra_general)
                {
                    ujra_general = false;
                    foreach (Vector tipp_seged in _al_jo_tipp)
                    {
                        if (tipp_seged == tipp)
                        {
                            tipp = new Vector(rnd.Next(_tabla_merete), rnd.Next(_tabla_merete));
                            ujra_general = true;
                            break;
                        }
                    }
                    foreach (Vector tipp_seged in _al_rossz_tipp)
                    {
                        if (tipp_seged == tipp)
                        {
                            tipp = new Vector(rnd.Next(_tabla_merete), rnd.Next(_tabla_merete));
                            ujra_general = true;
                            break;
                        }
                    }
                }
                bool sikeres_tipp_bool = false;
                foreach(Vector hajo in _player_hajo_pos)
                {
                    if(hajo == tipp)
                    {
                        _ellenfel_talalat++;
                        jatektabla_setup(tipp, sajat_jatektabla, Brushes.Green);
                        sikeres_tipp_bool = true;
                        _sikeres_tipp = tipp;
                        _elozo_tipp_siker = true;
                        _al_jo_tipp.Add(tipp);
                        if (_al_jo_tipp.Count == _jo_kockak_szama)
                        {
                            MessageBox.Show("Ön vesztett!");
                            game_end();
                        }
                        break;
                    }
                }
                if (!sikeres_tipp_bool)
                {
                    jatektabla_setup(tipp, sajat_jatektabla, Brushes.Red);
                    _elozo_tipp_siker = false;
                    _al_rossz_tipp.Add(tipp);
                }
            }
            else
            {
                Vector tipp = new Vector(-1, -1);
                bool sikeres_tipp_bool = false, ujra_general = false;
                while (!(tipp.X >= 0 && tipp.X < _tabla_merete && tipp.Y >= 0 && tipp.Y < _tabla_merete && ujra_general)) {
                    int irany = rnd.Next(4); // 0 - bal, 1 - fel, 2 - jobb, 3 - le
                    switch (irany)
                    {
                        case 0:
                            tipp = new Vector(_sikeres_tipp.X - 1, _sikeres_tipp.Y);
                            break;
                        case 1:
                            tipp = new Vector(_sikeres_tipp.X, _sikeres_tipp.Y - 1);
                            break;
                        case 2:
                            tipp = new Vector(_sikeres_tipp.X + 1, _sikeres_tipp.Y);
                            break;
                        case 3:
                            tipp = new Vector(_sikeres_tipp.X, _sikeres_tipp.Y + 1);
                            break;
                    }
                    ujra_general = true;
                    foreach (Vector tipp_seged in _al_jo_tipp)
                    {
                        if (tipp_seged == tipp)
                        {
                            tipp = new Vector(rnd.Next(_tabla_merete), rnd.Next(_tabla_merete));
                            ujra_general = false;
                            break;
                        }
                    }
                    foreach (Vector tipp_seged in _al_rossz_tipp)
                    {
                        if (tipp_seged == tipp)
                        {
                            tipp = new Vector(rnd.Next(_tabla_merete), rnd.Next(_tabla_merete));
                            ujra_general = false;
                            break;
                        }
                    }
                }
                foreach (Vector hajo in _player_hajo_pos)
                {
                    if (hajo == tipp)
                    {
                        _ellenfel_talalat++;
                        jatektabla_setup(tipp, sajat_jatektabla, Brushes.Green);
                        sikeres_tipp_bool = true;
                        _sikeres_tipp = tipp;
                        _elozo_tipp_siker = true;
                        _al_jo_tipp.Add(tipp);
                        if(_al_jo_tipp.Count == _jo_kockak_szama)
                        {
                            MessageBox.Show("Ön vesztett!");
                            game_end();
                        }
                        break;
                    }
                }
                if (!sikeres_tipp_bool)
                {
                    jatektabla_setup(tipp, sajat_jatektabla, Brushes.Red);
                    _elozo_tipp_siker = false;
                    _al_rossz_tipp.Add(tipp);
                }
            }
            game();
        }
        
        private void game_end()
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void eredmenyjelzo_update()
        { 
            korok_szama.Content = _korok_szam;
            sajat_talalatok.Content = _sajat_talalat;
            ellenfel_talalatai.Content = _ellenfel_talalat;
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
                if(_random_hajo_pos.Count != 0)
                {
                    _random_hajo_pos.Clear();
                }
                foreach (int hajo_hossza in _hajok_hossza)
                {
                    int irany = rnd.Next(4); // 0 - bal, 1 - fel, 2 - jobb, 3 - le
                    Vector random_pos;
                    bool helyes_pos = false;
                    while (!helyes_pos)
                    {
                        random_pos = new Vector(rnd.Next(_tabla_merete ), rnd.Next(_tabla_merete));
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
                                    random_pos = new Vector(rnd.Next(_tabla_merete), rnd.Next(_tabla_merete));
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
            if (!_mentett_jatek)
            {
                _x_coord_seged = 0;
                _y_coord_seged = 0;
                Point eger_pos = e.GetPosition(sajat_jatektabla);
                Vector eger_pos_vector = new Vector(coord_conv(eger_pos.X, _x_coord_seged) - 1, coord_conv(eger_pos.Y, _y_coord_seged) - 1);
                jatektabla_setup(eger_pos_vector, sajat_jatektabla, Brushes.Blue);
                _player_hajo_pos.Add(eger_pos_vector);
            }
        }

        private void masik_player_jatektabla_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_player_jon)
            {
                _x_coord_seged = 0;
                _y_coord_seged = 0;
                Point eger_pos = e.GetPosition(masik_player_jatektabla);
                Vector eger_pos_vector = new Vector(coord_conv(eger_pos.X, _x_coord_seged) - 1, coord_conv(eger_pos.Y, _y_coord_seged) - 1);
                bool volt_mar = false;
                foreach(Vector player_jo in _player_jo_tipp)
                {
                    if (player_jo == eger_pos_vector)
                    {
                        volt_mar = true;
                        break;
                    }
                }
                foreach (Vector tipp_seged in _player_rossz_tipp)
                {
                    if (tipp_seged == eger_pos_vector)
                    {
                        volt_mar = true;
                        break;
                    }
                }
                if (!volt_mar)
                {
                    bool talalat = false;
                    foreach (Vector hajo_coord in _random_hajo_pos)
                    {
                        if (hajo_coord == eger_pos_vector)
                        {
                            _player_jo_tipp.Add(eger_pos_vector);
                            jatektabla_setup(eger_pos_vector, masik_player_jatektabla, Brushes.Green);
                            _sajat_talalat++;
                            talalat = true;
                            if (_player_jo_tipp.Count == _jo_kockak_szama)
                            {
                                MessageBox.Show("Nyert");
                                game_end();
                            }
                            break;
                        }
                    }
                    if (!talalat)
                    {
                        jatektabla_setup(eger_pos_vector, masik_player_jatektabla, Brushes.Red);
                        _player_rossz_tipp.Add(eger_pos_vector);
                    }
                    _player_jon = false;
                    game();
                }
            }
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
