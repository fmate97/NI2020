﻿using NI_torpedo.Model;
using NI_torpedo.ViewModel;
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

namespace NI_torpedo.View
{
    /// <summary>
    /// Interaction logic for Player_GameWindow.xaml
    /// </summary>
    public partial class Player_GameWindow : Window
    {
        GameWindow_Player_viewmodel Player_viewmodel;
        ShipPut Ship= new ShipPut();

        public Player_GameWindow(string firstName, string secondName,
            List<Vector> firstPlayerShip, List<Vector> secondPlayerShip, 
            List<List<ShipUnit>> FirstShip, List<List<ShipUnit>> SecondShip)
        {
            InitializeComponent();
            Player_viewmodel = new GameWindow_Player_viewmodel(firstName, secondName, firstPlayerShip, 
                secondPlayerShip, FirstShip, SecondShip);
            Init_Game();
        }



        private void Init_Game()
        {
            FirstPlayer.Content = ($"Itt tippelj {Player_viewmodel.FirstName()}!");
            SecondPlayer.Content = ($"Itt tippelj {Player_viewmodel.SecondName()}!");
            Ship.GameTable_Init(FirstPlayer_TippTable);
            Ship.GameTable_Init(SecondPlayer_TippTable);
            Player_viewmodel.NextPlayerSet(Player_viewmodel.RandomStart());
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
                if (Player_viewmodel.FirstPlayer_GoodTipp() == Player_viewmodel.CubeSize())
                {
                    MessageBox.Show($"{Player_viewmodel.FirstName()} nyert!");
                    MainWindow newGame = new MainWindow();
                    newGame.Show();
                    this.Close();
                    
                }
            }
            else
            {
                if (Player_viewmodel.SecondPlayer_GoodTipp() == Player_viewmodel.CubeSize())
                {
                    MessageBox.Show($"{Player_viewmodel.SecondName()} nyert!");
                    MainWindow newGame = new MainWindow();
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

            FirstShip2.Content = Player_viewmodel.FirstShip2();
            SecondShip2.Content = Player_viewmodel.SecondShip2();

            FirstShip3.Content = Player_viewmodel.FirstShip3();
            SecondShip3.Content = Player_viewmodel.SecondShip3();

            FirstShip4.Content = Player_viewmodel.FirstShip4();
            SecondShip4.Content = Player_viewmodel.SecondShip4();

            FirstShip5.Content = Player_viewmodel.FirstShip5();
            SecondShip5.Content = Player_viewmodel.SecondShip5();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {

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

        private void Kesobb_Folyt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}