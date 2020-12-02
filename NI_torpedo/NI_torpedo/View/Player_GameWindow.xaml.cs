using NI_torpedo.Model;
using NI_torpedo.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for Player_GameWindow.xaml
    /// </summary>
    public partial class Player_GameWindow : Window
    {
        GameWindow_Player_viewmodel Player_viewmodel;
        ShipPut Ship= new ShipPut();
        bool _exit = false;
        bool _continue = false;

        public Player_GameWindow(string firstName, string secondName,
            List<Vector> firstPlayerShip, List<Vector> secondPlayerShip, 
            List<List<ShipUnit>> FirstShip, List<List<ShipUnit>> SecondShip)
        {
            InitializeComponent();
            Player_viewmodel = new GameWindow_Player_viewmodel(firstName, secondName, firstPlayerShip, 
                secondPlayerShip, FirstShip, SecondShip);
            Init_Game();
        }

        public Player_GameWindow()
        {
            InitializeComponent();
            _continue = true;
            Player_viewmodel = new GameWindow_Player_viewmodel();
            int help = Player_viewmodel.Restore_Game();

            if ( help== 1)
            {
                Init_Game();
                ScoreBoard();
                
                foreach (Vector tipp in Player_viewmodel.FirstPlayer_GoodTippGet())
                {
                    GameBoard_Setup(tipp, FirstPlayer_TippTable, Brushes.Green);
                }
                foreach (Vector tipp in Player_viewmodel.FirstPlayer_WrongTippGet())
                {
                    GameBoard_Setup(tipp, FirstPlayer_TippTable, Brushes.Red);
                }
                foreach (Vector tipp in Player_viewmodel.SecondPlayer_GoodTippGet())
                {
                    GameBoard_Setup(tipp, SecondPlayer_TippTable, Brushes.Green);
                }
                foreach (Vector tipp in Player_viewmodel.SecondPlayer_WrongTippGet())
                {
                    GameBoard_Setup(tipp, SecondPlayer_TippTable, Brushes.Red);
                }
                
            }
            else
            {
                MessageBox.Show("Az előző mentési fájl sérült!\nKezdjen új játékot!");
                File.Delete(Globals.Restore_File_Name);
                MainWindow newGame = new MainWindow();
                newGame.Show();
                _exit = true;
                this.Show();
            }
        }

        private void Init_Game()
        {
            FirstPlayer.Content = ($"Itt tippelj {Player_viewmodel.FirstName()}!");
            SecondPlayer.Content = ($"Itt tippelj {Player_viewmodel.SecondName()}!");
            Ship.GameTable_Init(FirstPlayer_TippTable);
            Ship.GameTable_Init(SecondPlayer_TippTable);
            if (!_continue)
            {
                Player_viewmodel.NextPlayerSet(Player_viewmodel.RandomStart());
            }
            
            NextPlayer(Player_viewmodel.NextPlayer());
            
        }

        private void FirstPlayer_TippTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Player_viewmodel.NextPlayer())
            {
                Point eger_pos = e.GetPosition(FirstPlayer_TippTable);
                Vector eger_pos_vector = new Vector(Player_viewmodel.Coord_Conv(eger_pos.X) - 1, Player_viewmodel.Coord_Conv(eger_pos.Y) - 1);
                Brush color = Player_viewmodel.FirstPlayerStep(eger_pos_vector);
                if(color== Brushes.Black)
                {
                    MessageBox.Show("Ezt már tippelted. Tippelj másikat!");
                }
                else 
                {
                    GameBoard_Setup(eger_pos_vector, FirstPlayer_TippTable, color);
                    Player_viewmodel.NextPlayerSet(false); 
                    NextPlayer(Player_viewmodel.NextPlayer());
                    ScoreBoard();
                    GameEnd(true);

                }

            }
            else
            {
                MessageBox.Show("Nem te jössz!");
            }
        }

        private void SecondPlayer_TippTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Player_viewmodel.NextPlayer())
            {
                Point eger_pos = e.GetPosition(SecondPlayer_TippTable);
                Vector eger_pos_vector = new Vector(Player_viewmodel.Coord_Conv(eger_pos.X) - 1, Player_viewmodel.Coord_Conv(eger_pos.Y) - 1);
                Brush color = Player_viewmodel.SecondPlayerStep(eger_pos_vector);
                if (color == Brushes.Black)
                {
                    MessageBox.Show("Ezt már tippelted. Tippelj másikat!");
                }
                else
                {
                    GameBoard_Setup(eger_pos_vector, SecondPlayer_TippTable, color);
                    Player_viewmodel.NextPlayerSet(true);
                    Player_viewmodel.NextPlayerSet(true);
                    NextPlayer(Player_viewmodel.NextPlayer());
                    Player_viewmodel.NumberOfRoundsAdd();
                    ScoreBoard();
                    GameEnd(false);
                }

            }
            else
            {
                MessageBox.Show("Nem te jössz!");
            }
        }

        private void NextPlayer(bool next)
        {
            if (next)
            {
                Next.Content = ($"{Player_viewmodel.FirstName()} következik!");
            }
            else
            {
                Next.Content = ($"{Player_viewmodel.SecondName()} következik!");
            }
        }

        private void GameEnd(bool help)
        {
            if (help)
            {
                if (Player_viewmodel.FirstPlayer_GoodTippCount() == Player_viewmodel.CubeSize())
                {
                    MessageBox.Show($"{Player_viewmodel.FirstName()} nyert!");
                    Player_viewmodel.WinnerName(Player_viewmodel.FirstName());
                    Player_viewmodel.Save();
                    MainWindow newGame = new MainWindow();
                    _exit=true;
                    newGame.Show();
                    this.Close();
                    
                }
            }
            else
            {
                if (Player_viewmodel.SecondPlayer_GoodTippCount() == Player_viewmodel.CubeSize())
                {
                    MessageBox.Show($"{Player_viewmodel.SecondName()} nyert!");
                    Player_viewmodel.WinnerName(Player_viewmodel.SecondName());
                    Player_viewmodel.Save();
                    MainWindow newGame = new MainWindow();
                    _exit = true;
                    newGame.Show();
                    this.Close();
                }
            }
        }
      

        private void GameBoard_Setup(Vector position, Canvas canvas_name, Brush brush)
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

        private void ScoreBoard()
        {
            First_NumberOfRound.Content = Player_viewmodel.NumberOfRounds();
            Second_NumberOfRounds.Content = Player_viewmodel.NumberOfRounds();

            First_FirstPlayer_Hits.Content = Player_viewmodel.FirstPlayerHits();
            Second_SecondPlayer_Hits.Content = Player_viewmodel.SecondPlayerHits();

            First_SecondPlayer_Hits.Content = Player_viewmodel.SecondPlayerHits();
            Second_FirstPlayer_Hits.Content = Player_viewmodel.FirstPlayerHits();

            FirstShip2.Content = Player_viewmodel.FirstPlayer_ScoreBoard()[0];
            SecondShip2.Content = Player_viewmodel.SecondPlayer_ScoreBoard()[0];

            FirstShip3.Content = Player_viewmodel.FirstPlayer_ScoreBoard()[1];
            SecondShip3.Content = Player_viewmodel.SecondPlayer_ScoreBoard()[1];

            FirstShip4.Content = Player_viewmodel.FirstPlayer_ScoreBoard()[2];
            SecondShip4.Content = Player_viewmodel.SecondPlayer_ScoreBoard()[2];

            FirstShip5.Content = Player_viewmodel.FirstPlayer_ScoreBoard()[3];
            SecondShip5.Content = Player_viewmodel.SecondPlayer_ScoreBoard()[3];
        }
    

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztos ki akar lépni?", "Megerősítés", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _exit = true;
                this.Close();
                _exit = true;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpWindow helpWindow = new HelpWindow();
                helpWindow.Show();
            }
            
        }

        private void Help_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }


        private void Later_Continue(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztos akarja később folytatni?", "Megerősítés", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Player_viewmodel.Save_Game();
                _exit = true;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }
    }
}
