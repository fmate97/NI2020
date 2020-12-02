using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using NI_torpedo.ViewModel;
using NI_torpedo.Model;

namespace NI_torpedo.View
{
    public partial class GameWindow : Window
    {
        private bool _exit = false, _key_down = false;
        private string _player_name;
        private readonly GameWindow_Al_viewmodel _viewModel = new GameWindow_Al_viewmodel();
        private Canvas _hajo_klikk;
        private Dictionary<Canvas, Visibility> _hajok_neve = new Dictionary<Canvas, Visibility>();
        int _shipIndex = 0;

        public GameWindow(string nev)
        {
            InitializeComponent();
            _player_name = nev;
            Jatekablak_Init();
        }

        public GameWindow(string nev, byte a)
        {
            InitializeComponent();
            if (_viewModel.Restore_Game() == 0)
            {
                _player_name = _viewModel.Player_Name_Get();
                Jatekablak_Init_Restore();
                Eredmenyjelzo_Update();
                _viewModel.Mentett_Jatek_Set(true);

                foreach (Vector random_hajo in _viewModel.Player_Hajo_Pos())
                {
                    Jatektabla_Setup(random_hajo, sajat_jatektabla, Brushes.Blue);
                }
                foreach (Vector random_hajo in _viewModel.Player_Jo_Tipp())
                {
                    Jatektabla_Setup(random_hajo, masik_player_jatektabla, Brushes.Green);
                }
                foreach (Vector random_hajo in _viewModel.Player_Rossz_Tipp())
                {
                    Jatektabla_Setup(random_hajo, masik_player_jatektabla, Brushes.Red);
                }
                foreach (Vector random_hajo in _viewModel.Al_Jo_Tipp())
                {
                    Jatektabla_Setup(random_hajo, sajat_jatektabla, Brushes.Green);
                }
                foreach (Vector random_hajo in _viewModel.Al_Rossz_Tipp())
                {
                    Jatektabla_Setup(random_hajo, sajat_jatektabla, Brushes.Red);
                }
                Game();
            }
            else
            {
                MessageBox.Show("Az előző mentési fájl sérült!\nÚj játék lett elkezdve!");
                File.Delete(Globals.Restore_File_Name);
                _player_name = nev;
                Jatekablak_Init();
            }
        }

        private void HajokNeve_Init(bool torles)
        {
            if (!torles)
            {
                _hajok_neve.Add(hajo21, Visibility.Visible);
                _hajok_neve.Add(hajo22, Visibility.Visible);
                _hajok_neve.Add(hajo23, Visibility.Visible);
                _hajok_neve.Add(hajo24, Visibility.Visible);
                _hajok_neve.Add(hajo31, Visibility.Visible);
                _hajok_neve.Add(hajo32, Visibility.Visible);
                _hajok_neve.Add(hajo33, Visibility.Visible);
                _hajok_neve.Add(hajo34, Visibility.Visible);
                _hajok_neve.Add(hajo41, Visibility.Visible);
                _hajok_neve.Add(hajo42, Visibility.Visible);
                _hajok_neve.Add(hajo51, Visibility.Visible);
            }
            else
            {
                List<Canvas> keys = new List<Canvas>(_hajok_neve.Keys);
                foreach (var item in keys)
                {
                    _hajok_neve[item] = Visibility.Visible;
                }
            }
        }

        public Canvas Hajok_Elhelyezese(MouseButtonEventArgs e)
        {
            List<Canvas> keys = new List<Canvas>(_hajok_neve.Keys);
            foreach (var item in keys)
            {
                if(_hajok_neve[item] == Visibility.Visible && e.GetPosition(item).X >= 0 && e.GetPosition(item).X < item.Width && e.GetPosition(item).Y >= 0 && e.GetPosition(item).Y < item.Height)
                {
                    return item;
                }
            }
            return new Canvas();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpWindow helpWindow = new HelpWindow();
                helpWindow.Show();
            }
            else if (e.Key == Key.F2 && Keyboard.IsKeyDown(Key.LeftShift) && !_key_down)
            {
                _key_down = true;
                masik_player_jatektabla.IsEnabled = false;
                Show_Al_Hajo();
            }
            else if (e.Key == Key.LeftShift && Keyboard.IsKeyDown(Key.F2) && !_key_down)
            {
                _key_down = true;
                masik_player_jatektabla.IsEnabled = false;
                Show_Al_Hajo();
            }
        }

        private void Show_Al_Hajo()
        {
            foreach (Vector random_hajo in _viewModel.Random_Hajo_Pos())
            {
                Jatektabla_Setup(random_hajo, masik_player_jatektabla, Brushes.Blue);
            }
            foreach (Vector random_hajo in _viewModel.Player_Jo_Tipp())
            {
                Jatektabla_Setup(random_hajo, masik_player_jatektabla, Brushes.Green);
            }
            foreach (Vector random_hajo in _viewModel.Player_Rossz_Tipp())
            {
                Jatektabla_Setup(random_hajo, masik_player_jatektabla, Brushes.Red);
            }
        }

        private void Eredmenyjelzo_Update()
        {
            var tomb = _viewModel.Eredmenyjelzo();   
            korok_szama.Content = tomb[0];
            sajat_talalatok.Content = tomb[1];
            ellenfel_talalatai.Content = tomb[2];
            hajo2.Content = tomb[3];
            hajo3.Content = tomb[4];
            hajo4.Content = tomb[5];
            hajo5.Content = tomb[6];
        }

        public void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.F2 || Keyboard.IsKeyUp(Key.LeftShift)) && _key_down)
            {
                Hide_Al_Hajo();
                masik_player_jatektabla.IsEnabled = true;
                _key_down = false;
            }
        }

        private void Hide_Al_Hajo()
        {
            Jatektabla_Init(masik_player_jatektabla);
            foreach (Vector random_hajo in _viewModel.Player_Jo_Tipp())
            {
                Jatektabla_Setup(random_hajo, masik_player_jatektabla, Brushes.Green);
            }
            foreach (Vector random_hajo in _viewModel.Player_Rossz_Tipp())
            {
                Jatektabla_Setup(random_hajo, masik_player_jatektabla, Brushes.Red);
            }
        }

        private void Sajat_Jatektabla_Mentes_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.Mentett_Jatek())
            {
                if (!Osszes_Hajo_Lehelyezve())
                {
                    MessageBox.Show("Nem helyezte le az összes hajót!");
                }
                else if (!(nehezseg_valasztas_combobox.SelectedIndex == 1 || nehezseg_valasztas_combobox.SelectedIndex == 2))
                {
                    MessageBox.Show("Nem választott nehézségi szintet!");
                }
                else
                {
                    _viewModel.Mentett_Jatek_Set(true);
                    hajok_elhelyezese.Visibility = Visibility.Collapsed;
                    eredmenyjelzo.Visibility = Visibility.Visible;
                    _viewModel.Ellenfel_Hajo_Mentes();
                    nehezseg_valasztas_combobox.IsEnabled = false;
                    if (nehezseg_valasztas_combobox.SelectedIndex == 1)
                        _viewModel.Nehezseg_Beallitas(false);
                    else
                        _viewModel.Nehezseg_Beallitas(true);
                    Start_Game();
                }
            }
            else
            {
                MessageBox.Show("Már elmentette a játékot.\nNem tudd még egyszer menteni csak ha új játékot kezd!");
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.Mentett_Jatek())
            {
                MessageBox.Show("A játék még nem kezdődött el így nem adható fel!");
            }
            else if (MessageBox.Show("Biztos fel akarja adni?", "Megerősítés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _viewModel.Save("Al");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                _exit = true;
                this.Close();
            }
        }

        private void Start_Game()
        {
            kovetkezoPlayer_label.Visibility = Visibility.Visible;
            if (_viewModel.Mentett_Jatek() && !_viewModel.Game_End_Bool())
            {
                if (_viewModel.Start_Game() % 2 == 0)
                {
                    kovetkezoPlayer.Content = _player_name;
                }
                else
                {
                    kovetkezoPlayer.Content = "Al";
                    _viewModel.Player_Number_Nov();
                    var return_value = _viewModel.Al_Tipp();
                    switch (return_value[2])
                    {
                        case 0:
                            Jatektabla_Setup(new Vector(return_value[0], return_value[1]), sajat_jatektabla, Brushes.Green);
                            break;
                        case 1:
                            Jatektabla_Setup(new Vector(return_value[0], return_value[1]), sajat_jatektabla, Brushes.Red);
                            break;
                    }                    
                }
                GameEnd();
                Game();
            }            
        }

        private void Game()
        {
            if (!_viewModel.Game_End_Bool())
            {
                if (_viewModel.Player_Kovetkezik())
                {
                    kovetkezoPlayer.Content = _player_name;
                }
                else
                {
                    kovetkezoPlayer.Content = "Al";
                    _viewModel.Player_Number_Nov();
                    var return_value = _viewModel.Al_Tipp();
                    switch (return_value[2])
                    {
                        case 0:
                            Jatektabla_Setup(new Vector(return_value[0], return_value[1]), sajat_jatektabla, Brushes.Green);
                            break;
                        case 1:
                            Jatektabla_Setup(new Vector(return_value[0], return_value[1]), sajat_jatektabla, Brushes.Red);
                            break;
                    }
                    GameEnd();
                    Game();
                }
            }
        }

        private bool Osszes_Hajo_Lehelyezve()
        {
            List<Canvas> keys = new List<Canvas>(_hajok_neve.Keys);
            foreach (var item in keys)
            {
                if (_hajok_neve[item] == Visibility.Visible)
                    return false;
            }
            return true;
        }

        private void Sajat_Jatektabla_Torles_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.Mentett_Jatek())
            {
                Jatektabla_Init(sajat_jatektabla);
                _viewModel.Player_Hajo_Pos_Clear();
                Hajo_Setup(true);
                hajok_elhelyezese.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("A játék már elkezdődött, ilyenkor már nem törölhető a tábla!");
            }
        }

        private void Hajok_Elhelyezese_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _hajo_klikk = Hajok_Elhelyezese(e);
            _viewModel.Fuggoleges_Set(true);
        }

        private void Hajok_Elhelyezese_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _hajo_klikk = Hajok_Elhelyezese(e);
            _viewModel.Fuggoleges_Set(false);
        }

        private void Sajat_Jatektabla_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            try
            {
                if (_hajo_klikk != null && !_viewModel.Mentett_Jatek() && _hajok_neve[_hajo_klikk] == Visibility.Visible)
                {
                    int length = (int)(_hajo_klikk.Height / _viewModel.Kocka_Unit()[0]);
                    Point eger_pos = e.GetPosition(sajat_jatektabla);
                    Vector eger_pos_vector = new Vector(_viewModel.Coord_Conv(eger_pos.X) - 1, _viewModel.Coord_Conv(eger_pos.Y) - 1);
                    if (_viewModel.Hajo_Lehelyezheto(eger_pos_vector, length))
                    {
                        _viewModel.PlayerShipNewList();
                        _shipIndex++;
                        for (int i = 0; i < length; i++)
                        {
                            if (_viewModel.Fuggoleges_Get())
                            {
                                Jatektabla_Setup(new Vector(eger_pos_vector.X, eger_pos_vector.Y + i), sajat_jatektabla, Brushes.Blue);
                                _viewModel.Player_Hajo_Add(new Vector(eger_pos_vector.X, eger_pos_vector.Y + i));
                                _viewModel.PlayerShipAddVector(_shipIndex - 1, new Vector(eger_pos_vector.X, eger_pos_vector.Y + i));
                            }
                            else
                            {
                                Jatektabla_Setup(new Vector(eger_pos_vector.X + i, eger_pos_vector.Y), sajat_jatektabla, Brushes.Blue);
                                _viewModel.Player_Hajo_Add(new Vector(eger_pos_vector.X + i, eger_pos_vector.Y));
                                _viewModel.PlayerShipAddVector(_shipIndex - 1, new Vector(eger_pos_vector.X + i, eger_pos_vector.Y));
                            }
                        }
                        List<Canvas> keys = new List<Canvas>(_hajok_neve.Keys);
                        foreach (var item in keys)
                        {
                            if (item == _hajo_klikk)
                            {
                                _hajok_neve[item] = Visibility.Hidden;
                                item.Visibility = Visibility.Hidden;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ide nem helyezhető le a hajó!");
                    }
                }
            }
            catch (Exception) { };
        }

        private void Masik_Player_Jatektabla_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.Player_Kovetkezik() && _viewModel.Mentett_Jatek() && !_viewModel.Game_End_Bool())
            {
                Point eger_pos = e.GetPosition(masik_player_jatektabla);
                Vector eger_pos_vector = new Vector(_viewModel.Coord_Conv(eger_pos.X) - 1, _viewModel.Coord_Conv(eger_pos.Y) - 1);
                Brush szin = _viewModel.Player_Lepese(eger_pos_vector);
                if (szin != Brushes.Gray)
                {
                    Jatektabla_Setup(eger_pos_vector, masik_player_jatektabla, szin);
                    _viewModel.Player_Number_Nov();
                    GameEnd();
                    Game();
                }

            }
        }

        private void GameEnd()
        {
            int return_value = _viewModel.Game_End();
            Eredmenyjelzo_Update();
            MainWindow window = new MainWindow();
            if (return_value == 0)
            {
                MessageBox.Show("Ön nyert!");
                _viewModel.Save(_player_name);
                window.Show();
                _exit = true;
                this.Close();
            }
            else if(return_value == 1)
            {
                MessageBox.Show("Gép nyert!");
                _viewModel.Save("Al");
                window.Show();
                _exit = true;
                this.Close();
            }
        }

        private void Jatekablak_Init()
        {
            nevLabel.Content = $"Jó játékot {_player_name}!";
            _viewModel.Player_Name(_player_name);
            Jatektabla_Init(sajat_jatektabla);
            Jatektabla_Init(masik_player_jatektabla);
            hajok_elhelyezese.Visibility = Visibility.Visible;
            eredmenyjelzo.Visibility = Visibility.Collapsed;
            Hajo_Setup(false);
            Random_Hajo_Gen();
        }

        private void Jatekablak_Init_Restore()
        {
            nevLabel.Content = $"Jó játékot {_player_name}!";
            Jatektabla_Init(sajat_jatektabla);
            Jatektabla_Init(masik_player_jatektabla);
            hajok_elhelyezese.Visibility = Visibility.Collapsed;
            eredmenyjelzo.Visibility = Visibility.Visible;
        }

        private void Jatektabla_Init(Canvas canvas)
        {
            List<Vector> init_vector = _viewModel.Init_Vector();
            foreach (Vector item in init_vector)
            {
                Jatektabla_Setup(item, canvas, Brushes.White);
            }
        }

        private void Jatektabla_Setup(Vector position, Canvas canvas_name, Brush brush)
        {
            var kocka_unit = _viewModel.Kocka_Unit();
            var unitX = kocka_unit[0];
            var unitY = kocka_unit[1];
            var margo = kocka_unit[2];

            var shape = new Rectangle();
            shape.Fill = brush;
            shape.Width = unitX - margo;
            shape.Height = unitY - margo;
            Canvas.SetTop(shape, position.Y * unitY + (margo / 2));
            Canvas.SetLeft(shape, position.X * unitX + (margo / 2));
            canvas_name.Children.Add(shape);
        }

        private void Hajo_Setup(bool torles)
        {
            HajokNeve_Init(torles);
            List<Canvas> keys = new List<Canvas>(_hajok_neve.Keys);
            foreach (var item in keys)
            {
                item.Visibility = _hajok_neve[item];
                var length = item.Height;
                var unit = _viewModel.Kocka_Unit()[1];
                for (int i = 0; i < (length / unit); i++)
                {
                    Jatektabla_Setup(new Vector(0, i), item, Brushes.Blue);
                }
            }
        }

        private void Random_Hajo_Gen()
        {
            _viewModel.Random_Hajo_Gen();
        }

        private void Help_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void Kesobb_Folyt_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.Mentett_Jatek())
            {
                MessageBoxResult result = MessageBox.Show("Biztos akarja később folytatni?", "Megerősítés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.Save_Game();
                    _exit = true;
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("A játék még nem kezdődött el így későbbi folytatása nem lehetséges!");
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_exit)
            {
                MessageBox.Show("Kérem használja a \"Feladás\" gombot a kilépéshez!");
                base.OnClosing(e);
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
            }
        }
    }
}
