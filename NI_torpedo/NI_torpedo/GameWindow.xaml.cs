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
    class HajoEgyseg{
        public Vector vector;
        public bool talalt;

        public HajoEgyseg(Vector vector, bool talalt)
        {
            this.vector = vector;
            this.talalt = talalt;
        }
    }

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
        private int _kivalasztott_hajo_hossza = 0;
        private bool _kivalasztott_hajo_fuggoleges = true;
        private Canvas _canvas_nev_seged = new Canvas();
        private List<int> _sikeres_al_tip_seged = new List<int>();
        private Vector return_value = new Vector(-1, -1);

        private List<List<HajoEgyseg>> hajok = new List<List<HajoEgyseg>>();
        private int _hajo2 = 0;
        private int _hajo3 = 0;
        private int _hajo4 = 0;
        private int _hajo5 = 0;

        public GameWindow(bool isTwoPlayer, string nev)
        {
            InitializeComponent();

            _isTwoPlayer = isTwoPlayer;
            nevLabel.Content = nev;
            tabla_kocka_helyzete_init();
            foreach (Vector kocka in _tabla_kocka_helyzete)
            {
                jatektabla_init(kocka, sajat_jatektabla, Brushes.White);
                jatektabla_init(kocka, masik_player_jatektabla, Brushes.White);
            }
            hajok_elhelyezese.Visibility = Visibility.Visible;
            eredmenyjelzo.Visibility = Visibility.Collapsed;
            hajo_setup();
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

        private void jatektabla_init(Vector position, Canvas canvas_name, Brush brush)
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
            shape2.Fill = brush;
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
                if (!osszes_hajo_lehelyezve())
                {
                    MessageBox.Show("Nem helyezte le az összes hajót!");
                }
                else
                {
                    _mentett_jatek = true;
                    hajok_elhelyezese.Visibility = Visibility.Collapsed;
                    eredmenyjelzo.Visibility = Visibility.Visible;
                    ellenfel_hajo_mentes();
                    start_game();
                }
            }
        }

        private bool osszes_hajo_lehelyezve()
        {
            if (hajo21.Visibility == Visibility.Visible)
                return false;
            else if (hajo22.Visibility == Visibility.Visible)
                return false;
            else if (hajo23.Visibility == Visibility.Visible)
                return false;
            else if (hajo24.Visibility == Visibility.Visible)
                return false;
            else if (hajo31.Visibility == Visibility.Visible)
                return false;
            else if (hajo32.Visibility == Visibility.Visible)
                return false;
            else if (hajo33.Visibility == Visibility.Visible)
                return false;
            else if (hajo34.Visibility == Visibility.Visible)
                return false;
            else if (hajo41.Visibility == Visibility.Visible)
                return false;
            else if (hajo42.Visibility == Visibility.Visible)
                return false;
            else if (hajo51.Visibility == Visibility.Visible)
                return false;

            return true;
        }

        private void sajat_jatektabla_torles_button_Click(object sender, RoutedEventArgs e)
        {
            if (!_mentett_jatek)
            {
                foreach (Vector kocka in _tabla_kocka_helyzete)
                {
                    jatektabla_init(kocka, sajat_jatektabla, Brushes.White);
                }
                _player_hajo_pos.Clear();
                hajo_setup();
                hajok_elhelyezese.Visibility = Visibility.Visible;
            }
        }

        private void hajok_elhelyezese_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int Xunit = _tabla_szelessege / _tabla_merete, Yunit = _tabla_magassaga / _tabla_merete;
            if (hajo21.Visibility == Visibility.Visible && e.GetPosition(hajo21).X >= 0 && e.GetPosition(hajo21).X < Xunit && e.GetPosition(hajo21).Y >= 0 && e.GetPosition(hajo21).Y < (2 * Yunit))
            {
                _canvas_nev_seged = hajo21;
                _kivalasztott_hajo_hossza = 2;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo22.Visibility == Visibility.Visible && e.GetPosition(hajo22).X >= 0 && e.GetPosition(hajo22).X < Xunit && e.GetPosition(hajo22).Y >= 0 && e.GetPosition(hajo22).Y < (2 * Yunit))
            {
                _canvas_nev_seged = hajo22;
                _kivalasztott_hajo_hossza = 2;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo23.Visibility == Visibility.Visible && e.GetPosition(hajo23).X >= 0 && e.GetPosition(hajo23).X < Xunit && e.GetPosition(hajo23).Y >= 0 && e.GetPosition(hajo23).Y < (2 * Yunit))
            {
                _canvas_nev_seged = hajo23;
                _kivalasztott_hajo_hossza = 2;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo24.Visibility == Visibility.Visible && e.GetPosition(hajo24).X >= 0 && e.GetPosition(hajo24).X < Xunit && e.GetPosition(hajo24).Y >= 0 && e.GetPosition(hajo24).Y < (2 * Yunit))
            {
                _canvas_nev_seged = hajo24;
                _kivalasztott_hajo_hossza = 2;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo31.Visibility == Visibility.Visible && e.GetPosition(hajo31).X >= 0 && e.GetPosition(hajo31).X < Xunit && e.GetPosition(hajo31).Y >= 0 && e.GetPosition(hajo31).Y < (3 * Yunit))
            {
                _canvas_nev_seged = hajo31;
                _kivalasztott_hajo_hossza = 3;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo32.Visibility == Visibility.Visible && e.GetPosition(hajo32).X >= 0 && e.GetPosition(hajo32).X < Xunit && e.GetPosition(hajo32).Y >= 0 && e.GetPosition(hajo32).Y < (3 * Yunit))
            {
                _canvas_nev_seged = hajo32;
                _kivalasztott_hajo_hossza = 3;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo33.Visibility == Visibility.Visible && e.GetPosition(hajo33).X >= 0 && e.GetPosition(hajo33).X < Xunit && e.GetPosition(hajo33).Y >= 0 && e.GetPosition(hajo33).Y < (3 * Yunit))
            {
                _canvas_nev_seged = hajo33;
                _kivalasztott_hajo_hossza = 3;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo34.Visibility == Visibility.Visible && e.GetPosition(hajo34).X >= 0 && e.GetPosition(hajo34).X < Xunit && e.GetPosition(hajo34).Y >= 0 && e.GetPosition(hajo34).Y < (3 * Yunit))
            {
                _canvas_nev_seged = hajo34;
                _kivalasztott_hajo_hossza = 3;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo41.Visibility == Visibility.Visible && e.GetPosition(hajo41).X >= 0 && e.GetPosition(hajo41).X < Xunit && e.GetPosition(hajo41).Y >= 0 && e.GetPosition(hajo41).Y < (4 * Yunit))
            {
                _canvas_nev_seged = hajo41;
                _kivalasztott_hajo_hossza = 4;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo42.Visibility == Visibility.Visible && e.GetPosition(hajo42).X >= 0 && e.GetPosition(hajo42).X < Xunit && e.GetPosition(hajo42).Y >= 0 && e.GetPosition(hajo42).Y < (4 * Yunit))
            {
                _canvas_nev_seged = hajo42;
                _kivalasztott_hajo_hossza = 4;
                _kivalasztott_hajo_fuggoleges = true;
            }
            else if (hajo51.Visibility == Visibility.Visible && e.GetPosition(hajo51).X >= 0 && e.GetPosition(hajo51).X < Xunit && e.GetPosition(hajo51).Y >= 0 && e.GetPosition(hajo51).Y < (5 * Yunit))
            {
                _canvas_nev_seged = hajo51;
                _kivalasztott_hajo_hossza = 5;
                _kivalasztott_hajo_fuggoleges = true;
            }
        }

        private void hajok_elhelyezese_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            int Xunit = _tabla_szelessege / _tabla_merete, Yunit = _tabla_magassaga / _tabla_merete;
            if (hajo21.Visibility == Visibility.Visible && e.GetPosition(hajo21).X >= 0 && e.GetPosition(hajo21).X < Xunit && e.GetPosition(hajo21).Y >= 0 && e.GetPosition(hajo21).Y < (2 * Yunit))
            {
                _canvas_nev_seged = hajo21;
                _kivalasztott_hajo_hossza = 2;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo22.Visibility == Visibility.Visible && e.GetPosition(hajo22).X >= 0 && e.GetPosition(hajo22).X < Xunit && e.GetPosition(hajo22).Y >= 0 && e.GetPosition(hajo22).Y < (2 * Yunit))
            {
                _canvas_nev_seged = hajo22;
                _kivalasztott_hajo_hossza = 2;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo23.Visibility == Visibility.Visible && e.GetPosition(hajo23).X >= 0 && e.GetPosition(hajo23).X < Xunit && e.GetPosition(hajo23).Y >= 0 && e.GetPosition(hajo23).Y < (2 * Yunit))
            {
                _canvas_nev_seged = hajo23;
                _kivalasztott_hajo_hossza = 2;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo24.Visibility == Visibility.Visible && e.GetPosition(hajo24).X >= 0 && e.GetPosition(hajo24).X < Xunit && e.GetPosition(hajo24).Y >= 0 && e.GetPosition(hajo24).Y < (2 * Yunit))
            {
                _canvas_nev_seged = hajo24;
                _kivalasztott_hajo_hossza = 2;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo31.Visibility == Visibility.Visible && e.GetPosition(hajo31).X >= 0 && e.GetPosition(hajo31).X < Xunit && e.GetPosition(hajo31).Y >= 0 && e.GetPosition(hajo31).Y < (3 * Yunit))
            {
                _canvas_nev_seged = hajo31;
                _kivalasztott_hajo_hossza = 3;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo32.Visibility == Visibility.Visible && e.GetPosition(hajo32).X >= 0 && e.GetPosition(hajo32).X < Xunit && e.GetPosition(hajo32).Y >= 0 && e.GetPosition(hajo32).Y < (3 * Yunit))
            {
                _canvas_nev_seged = hajo32;
                _kivalasztott_hajo_hossza = 3;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo33.Visibility == Visibility.Visible && e.GetPosition(hajo33).X >= 0 && e.GetPosition(hajo33).X < Xunit && e.GetPosition(hajo33).Y >= 0 && e.GetPosition(hajo33).Y < (3 * Yunit))
            {
                _canvas_nev_seged = hajo33;
                _kivalasztott_hajo_hossza = 3;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo34.Visibility == Visibility.Visible && e.GetPosition(hajo34).X >= 0 && e.GetPosition(hajo34).X < Xunit && e.GetPosition(hajo34).Y >= 0 && e.GetPosition(hajo34).Y < (3 * Yunit))
            {
                _canvas_nev_seged = hajo34;
                _kivalasztott_hajo_hossza = 3;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo41.Visibility == Visibility.Visible && e.GetPosition(hajo41).X >= 0 && e.GetPosition(hajo41).X < Xunit && e.GetPosition(hajo41).Y >= 0 && e.GetPosition(hajo41).Y < (4 * Yunit))
            {
                _canvas_nev_seged = hajo41;
                _kivalasztott_hajo_hossza = 4;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo42.Visibility == Visibility.Visible && e.GetPosition(hajo42).X >= 0 && e.GetPosition(hajo42).X < Xunit && e.GetPosition(hajo42).Y >= 0 && e.GetPosition(hajo42).Y < (4 * Yunit))
            {
                _canvas_nev_seged = hajo42;
                _kivalasztott_hajo_hossza = 4;
                _kivalasztott_hajo_fuggoleges = false;
            }
            else if (hajo51.Visibility == Visibility.Visible && e.GetPosition(hajo51).X >= 0 && e.GetPosition(hajo51).X < Xunit && e.GetPosition(hajo51).Y >= 0 && e.GetPosition(hajo51).Y < (5 * Yunit))
            {
                _canvas_nev_seged = hajo51;
                _kivalasztott_hajo_hossza = 5;
                _kivalasztott_hajo_fuggoleges = false;
            }
        }

        private void ellenfel_hajo_mentes()
        {
            List<Vector> seged = new List<Vector>();
            seged = _random_hajo_pos;
            int hajo_index = 0;

            hajok.Add(new List<HajoEgyseg>());
            hajok[hajo_index].Add(new HajoEgyseg(seged[0], false));
            hajok[hajo_index].Add(new HajoEgyseg(seged[1], false));
            hajo_index++;


            for (int i = 2; i < seged.Count; i++)
            {
                if (seged[i - 1].Y == seged[i].Y  )
                {
                    if (seged[i - 1].X != seged[i - 2].X)
                    {
                        hajok[hajo_index - 1].Add(new HajoEgyseg(seged[i], false));
                    }
                    else
                    {
                        hajok.Add(new List<HajoEgyseg>());
                        hajok[hajo_index].Add(new HajoEgyseg(seged[i], false));
                        hajo_index++;
                    }
                }
                else if(seged[i].X == seged[i - 1].X)
                {
                    if (seged[i - 1].Y != seged[i-2].Y)
                    {
                        hajok[hajo_index - 1].Add(new HajoEgyseg(seged[i], false));
                    }
                    else
                    {
                        hajok.Add(new List<HajoEgyseg>());
                        hajok[hajo_index].Add(new HajoEgyseg(seged[i], false));
                        hajo_index++;
                    }
                   
                }
                else
                {
                    hajok.Add(new List<HajoEgyseg>());
                    hajok[hajo_index].Add(new HajoEgyseg(seged[i], false));
                    hajo_index++;
                }

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
                    _player_number++;
                    MessageBox.Show("Ön kezd!");
                    
                }
                else
                {
                    _player_jon = false;
                    _player_number++;
                    MessageBox.Show("Gép kezd!");
                    al_tipp(false, _sikeres_tipp, out return_value);
                }
            }
        }

        private void game()
        {
            _korok_szam++;
            eredmenyjelzo_update();
            nevLabel.Content = return_value.X + " : " + return_value.Y;
            if (_player_number % 2 == 0) //player
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
                al_tipp(_elozo_tipp_siker, _sikeres_tipp, out return_value);
            }
        }

        public void al_tipp(bool elozo_talalat, Vector sikeres_tipp_seged, out Vector return_value_seged)
        {
            if (lehelyezheto_tippek_szama() > 0)
            {
                elozo_talalat = true;
            }
            if (!elozo_talalat)
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
                        return_value_seged = tipp;
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
                    _al_rossz_tipp.Add(tipp);
                    return_value_seged = tipp;
                }
            }
            else
            {
                Vector tipp = new Vector(-1, -1);
                bool sikeres_tipp_bool = false, ujra_general = false;
                int irany = -1;
                while (!(tipp.X >= 0 && tipp.X < _tabla_merete && tipp.Y >= 0 && tipp.Y < _tabla_merete && ujra_general)) {
                    irany = rnd.Next(4); // 0 - bal, 1 - fel, 2 - jobb, 3 - le
                    switch (irany)
                    {
                        case 0:
                            tipp = new Vector(sikeres_tipp_seged.X - 1, sikeres_tipp_seged.Y);
                            break;
                        case 1:
                            tipp = new Vector(sikeres_tipp_seged.X, sikeres_tipp_seged.Y - 1);
                            break;
                        case 2:
                            tipp = new Vector(sikeres_tipp_seged.X + 1, sikeres_tipp_seged.Y);
                            break;
                        case 3:
                            tipp = new Vector(sikeres_tipp_seged.X, sikeres_tipp_seged.Y + 1);
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
                        return_value_seged = tipp;
                        _sikeres_al_tip_seged.Clear();
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
                    return_value_seged = tipp;
                    _sikeres_al_tip_seged.Add(irany);
                    _al_rossz_tipp.Add(tipp);
                }
            }
            game();
        }

        private int lehelyezheto_tippek_szama()
        {
            int return_value = 0;

            if(_sikeres_tipp.X > 0)
            {
                return_value++;
            }
            if(_sikeres_tipp.X < 9)
            {
                return_value++;
            }
            if(_sikeres_tipp.Y > 0)
            {
                return_value++;
            }
            if(_sikeres_tipp.Y < 9)
            {
                return_value++;
            }

            foreach(Vector item in _al_rossz_tipp)
            {
                if(item.X == _sikeres_tipp.X - 1 && item.Y == _sikeres_tipp.Y)
                {
                    return_value--;
                }
                else if(item.X == _sikeres_tipp.X + 1 && item.Y == _sikeres_tipp.Y)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X && item.Y == _sikeres_tipp.Y - 1)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X && item.Y == _sikeres_tipp.Y + 1)
                {
                    return_value--;
                }
            }

            foreach (Vector item in _al_jo_tipp)
            {
                if (item.X == _sikeres_tipp.X - 1 && item.Y == _sikeres_tipp.Y)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X + 1 && item.Y == _sikeres_tipp.Y)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X && item.Y == _sikeres_tipp.Y - 1)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X && item.Y == _sikeres_tipp.Y + 1)
                {
                    return_value--;
                }
            }

            return return_value;
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
            hajo2.Content = _hajo2;
            hajo3.Content = _hajo3;
            hajo4.Content = _hajo4;
            hajo5.Content = _hajo5;
        }

        private void hajo_setup()
        {
            hajo21.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo21, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo21, Brushes.Blue);

            hajo22.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo22, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo22, Brushes.Blue);

            hajo23.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo23, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo23, Brushes.Blue);

            hajo24.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo24, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo24, Brushes.Blue);

            hajo31.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo31, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo31, Brushes.Blue);
            jatektabla_init(new Vector(0, 2), hajo31, Brushes.Blue);

            hajo32.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo32, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo32, Brushes.Blue);
            jatektabla_init(new Vector(0, 2), hajo32, Brushes.Blue);

            hajo33.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo33, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo33, Brushes.Blue);
            jatektabla_init(new Vector(0, 2), hajo33, Brushes.Blue);

            hajo34.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo34, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo34, Brushes.Blue);
            jatektabla_init(new Vector(0, 2), hajo34, Brushes.Blue);

            hajo41.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo41, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo41, Brushes.Blue);
            jatektabla_init(new Vector(0, 2), hajo41, Brushes.Blue);
            jatektabla_init(new Vector(0, 3), hajo41, Brushes.Blue);

            hajo42.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo42, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo42, Brushes.Blue);
            jatektabla_init(new Vector(0, 2), hajo42, Brushes.Blue);
            jatektabla_init(new Vector(0, 3), hajo42, Brushes.Blue);

            hajo51.Visibility = Visibility.Visible;
            jatektabla_init(new Vector(0, 0), hajo51, Brushes.Blue);
            jatektabla_init(new Vector(0, 1), hajo51, Brushes.Blue);
            jatektabla_init(new Vector(0, 2), hajo51, Brushes.Blue);
            jatektabla_init(new Vector(0, 3), hajo51, Brushes.Blue);
            jatektabla_init(new Vector(0, 4), hajo51, Brushes.Blue);
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
                                if (random_pos.Y + hajo_hossza <= _tabla_merete - 1)
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

                if (sajat_jatektabla_lehelyezheto(eger_pos_vector)) {
                    for (int i = 0; i < _kivalasztott_hajo_hossza; i++)
                    {
                        if (_kivalasztott_hajo_fuggoleges)
                        {
                            jatektabla_setup(new Vector(eger_pos_vector.X, eger_pos_vector.Y + i), sajat_jatektabla, Brushes.Blue);
                            _player_hajo_pos.Add(new Vector(eger_pos_vector.X, eger_pos_vector.Y + i));
                        }
                        else
                        {
                            jatektabla_setup(new Vector(eger_pos_vector.X + i, eger_pos_vector.Y), sajat_jatektabla, Brushes.Blue);
                            _player_hajo_pos.Add(new Vector(eger_pos_vector.X + i, eger_pos_vector.Y));
                        }
                    }
                    _canvas_nev_seged.Visibility = Visibility.Hidden;
                    _kivalasztott_hajo_hossza = 0;
                }
                else
                {
                    MessageBox.Show("Ide nem helyezhető le a hajó!");
                }
            }
        }

        private bool sajat_jatektabla_lehelyezheto(Vector eger_pos_vector)
        {
            if (_kivalasztott_hajo_fuggoleges) 
            {
                foreach (Vector item in _player_hajo_pos)
                {
                    for (int i = 0; i < _kivalasztott_hajo_hossza; i++)
                    {
                        if (eger_pos_vector.X == item.X && eger_pos_vector.Y + i == item.Y)
                        {
                            return false;
                        }
                    }
                }
                for(int i = 0; i < _kivalasztott_hajo_hossza; i++)
                {
                    if (eger_pos_vector.Y + i > _tabla_merete - 1)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Vector item in _player_hajo_pos)
                {
                    for (int i = 0; i < _kivalasztott_hajo_hossza; i++)
                    {
                        if (eger_pos_vector.X + i == item.X && eger_pos_vector.Y == item.Y)
                        {
                            return false;
                        }
                    }
                }
                for(int i= 0; i < _kivalasztott_hajo_hossza; i++) 
                {
                    if (eger_pos_vector.X + i > _tabla_merete - 1)
                    {
                        return false;
                    }
                }
            }

            return true;
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
                            elem_talalt(eger_pos_vector);
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

        private void elem_talalt(Vector talalt)
        {
            for(int i=0; i<hajok.Count; i++)
            {
                for(int j=0; j<hajok[i].Count; j++)
                {
                    if (hajok[i][j].vector == talalt)
                    {
                        hajok[i][j].talalt = true;
                        elsullyedt(i);
                        break;
                    }
                }
            }

        }

        private void elsullyedt(int i)
        {
            int db=0;
            for(int j=0; j< hajok[i].Count; j++)
            {
                if(hajok[i][j].talalt== true)
                {
                    db++;
                }
            }
            if(hajok[i].Count == db)
            {
                switch (db)
                {
                    case 2:
                        _hajo2++;
                        break;
                    case 3:
                        _hajo3++;
                        break;
                    case 4:
                        _hajo4++;
                        break;
                    case 5:
                        _hajo5++;
                        break;
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
