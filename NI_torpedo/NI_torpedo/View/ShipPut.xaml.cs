using NI_torpedo.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NI_torpedo.View
{
    /// <summary>
    /// Interaction logic for ShipPut.xaml
    /// </summary>
    public partial class ShipPut : Window
    {
        public static string _firstPlayer;
        public static string _secondPlayer;
        private bool _first;
        private bool _exit = false;
        private Canvas _ship;
        
        private Dictionary<Canvas, Visibility> ShipName = new Dictionary<Canvas, Visibility>();
       
        public ShipPut(string firstPlayer, string secondPlayer)
        {
            InitializeComponent();
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;
            _first = true;
            Init_Game();
        }

        public ShipPut()
        {

        }
        GameWindow_Player_viewmodel Player_viewmodel = new GameWindow_Player_viewmodel(_firstPlayer, _secondPlayer);


        private void Init_Game()
        {
            if (_first)
            {
                NameLabel.Content = $"Kérem helyeze le a hajóit {_firstPlayer}";
                GameTable_Init(Ship_Put_GameTable);
                Ships_table.Visibility = Visibility.Visible;
                Ship_Setup(false);
            }
            else
            {
                NameLabel.Content = $"Kérem helyeze le a hajóit {_secondPlayer}";
                GameTable_Init(Ship_Put_GameTable);
                Ships_table.Visibility = Visibility.Visible;
                Ship_Setup(true);
            }
            
        }

        public void GameTable_Init(Canvas canvas)
        {
            List<Vector> init_vector = new List<Vector>();
            for (int i = 0; i < Player_viewmodel.TableSize(); i++)
                for (int j = 0; j < Player_viewmodel.TableSize(); j++)
                    init_vector.Add(new Vector(i, j));

            foreach (Vector item in init_vector)
            {
                Jatektabla_Setup(item, canvas, Brushes.White);
            }
        }

        private void Jatektabla_Setup(Vector position, Canvas canvas_name, Brush brush)
        {
            var kocka_unit = Player_viewmodel.Kocka_Unit();
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


        private void Ship_Setup(bool exists)
        {
            ShipName_Init(exists);
            List<Canvas> keys = new List<Canvas>(ShipName.Keys);
            foreach (var item in keys)
            {
                item.Visibility = ShipName[item];
                var length = item.Height;
                var unit = Player_viewmodel.Kocka_Unit()[1];
                for (int i = 0; i < (length / unit); i++)
                {
                    Jatektabla_Setup(new Vector(0, i), item, Brushes.Blue);
                }
            }
        }

        private void ShipName_Init(bool exists)
        { 
            if (!exists)
            {
                ShipName.Add(hajo21, Visibility.Visible);
                ShipName.Add(hajo22, Visibility.Visible);
                ShipName.Add(hajo23, Visibility.Visible);
                ShipName.Add(hajo24, Visibility.Visible);
                ShipName.Add(hajo31, Visibility.Visible);
                ShipName.Add(hajo32, Visibility.Visible);
                ShipName.Add(hajo33, Visibility.Visible);
                ShipName.Add(hajo34, Visibility.Visible);
                ShipName.Add(hajo41, Visibility.Visible);
                ShipName.Add(hajo42, Visibility.Visible);
                ShipName.Add(hajo51, Visibility.Visible);
            }
            else
            {
                List<Canvas> keys = new List<Canvas>(ShipName.Keys);
                foreach (var item in keys)
                {
                    ShipName[item] = Visibility.Visible;
                }
            }
        }

        private void Ship_Put_Gametable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_ship != null  && ShipName[_ship] == Visibility.Visible)
                {
                    int length = (int)(_ship.Height / Player_viewmodel.Kocka_Unit()[0]);
                    Point eger_pos = e.GetPosition(Ship_Put_GameTable);
                    Vector eger_pos_vector = new Vector(Player_viewmodel.Coord_Conv(eger_pos.X) - 1, Player_viewmodel.Coord_Conv(eger_pos.Y) - 1);
                    if (_first)
                    {
                        if (Player_viewmodel.First_Ship_Check(eger_pos_vector, length))
                        {
                            for (int i = 0; i < length; i++)
                            {
                                if (Player_viewmodel.Fuggoleges_Get())
                                {
                                    Jatektabla_Setup(new Vector(eger_pos_vector.X, eger_pos_vector.Y + i), Ship_Put_GameTable, Brushes.Blue);
                                    Player_viewmodel.FirstShipAdd(new Vector(eger_pos_vector.X, eger_pos_vector.Y + i));
                                }
                                else
                                {
                                    Jatektabla_Setup(new Vector(eger_pos_vector.X + i, eger_pos_vector.Y), Ship_Put_GameTable, Brushes.Blue);
                                    Player_viewmodel.FirstShipAdd(new Vector(eger_pos_vector.X + i, eger_pos_vector.Y));
                                }
                            }
                            List<Canvas> keys = new List<Canvas>(ShipName.Keys);
                            foreach (var item in keys)
                            {
                                if (item == _ship)
                                {
                                    ShipName[item] = Visibility.Hidden;
                                    item.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ide nem helyezhető le a hajó!");
                        }
                    }
                    else
                    {
                        if (Player_viewmodel.Second_Ship_Check(eger_pos_vector, length))
                        {
                            for (int i = 0; i < length; i++)
                            {
                                if (Player_viewmodel.Fuggoleges_Get())
                                {
                                    Jatektabla_Setup(new Vector(eger_pos_vector.X, eger_pos_vector.Y + i), Ship_Put_GameTable, Brushes.Blue);
                                    Player_viewmodel.SecondShipAdd(new Vector(eger_pos_vector.X, eger_pos_vector.Y + i));
                                }
                                else
                                {
                                    Jatektabla_Setup(new Vector(eger_pos_vector.X + i, eger_pos_vector.Y), Ship_Put_GameTable, Brushes.Blue);
                                    Player_viewmodel.SecondShipAdd(new Vector(eger_pos_vector.X + i, eger_pos_vector.Y));
                                }
                            }
                            List<Canvas> keys = new List<Canvas>(ShipName.Keys);
                            foreach (var item in keys)
                            {
                                if (item == _ship)
                                {
                                    ShipName[item] = Visibility.Hidden;
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
            }
            catch (Exception) { };
        }
        private void Ships_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _ship = Ship_Put(e);
            Player_viewmodel.Fuggoleges_Set(true);
        }
        private void Ships_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _ship = Ship_Put(e);
            Player_viewmodel.Fuggoleges_Set(false);
        }

        public Canvas Ship_Put(MouseButtonEventArgs e)
        {
            List<Canvas> keys = new List<Canvas>(ShipName.Keys);
            foreach (var item in keys)
            {
                if (ShipName[item] == Visibility.Visible && e.GetPosition(item).X >= 0 && e.GetPosition(item).X < item.Width && e.GetPosition(item).Y >= 0 && e.GetPosition(item).Y < item.Height)
                {
                    return item;
                }
            }
            return new Canvas();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_first)
            {
                if (Player_viewmodel.FirstShipSize() != Player_viewmodel.CubeSize())
                {
                    MessageBox.Show("Nem helyezte le az összes hajóit!");
                }
                else
                {
                    _first = false;
                    Init_Game();
                }
            }
            else
            {
                if(Player_viewmodel.SecondShipSize()!= Player_viewmodel.CubeSize())
                {
                    MessageBox.Show("Nem helyezte le az összes hajóit!");
                }
                else
                {
                    MessageBox.Show("Indul a játék");
                    Player_GameWindow player_Game = new Player_GameWindow(_firstPlayer, _secondPlayer, Player_viewmodel.FirstShipGet(), Player_viewmodel.SecondShipGet());
                    player_Game.Show();
                    this.Close();
                }
            }

        }
        
        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_first)
            {
                GameTable_Init(Ship_Put_GameTable);
                Ships_table.Visibility = Visibility.Visible;
                Ship_Setup(true);
                Player_viewmodel.FirstShipClear();
            }
            else
            {
                GameTable_Init(Ship_Put_GameTable);
                Ships_table.Visibility = Visibility.Visible;
                Ship_Setup(true);
                Player_viewmodel.SecondShipClear();
            }
        }

       private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpWindow helpWindow = new HelpWindow();
                helpWindow.Show();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_exit)
            {
                MessageBox.Show("Kérem használja a Kilépés gombot a kilépéshez!");
                base.OnClosing(e);
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztos ki akar lépni?", "Megerősítés", MessageBoxButton.YesNo);
            if(result== MessageBoxResult.Yes)
            {
                _exit = true;
                this.Close();
            }
            
        }
    }
}

