using System;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using NI_torpedo.Model;

namespace NI_torpedo.View
{
    public partial class MainWindow : Window
    {
        private bool _exit = false;
        public MainWindow()
        {            
            InitializeComponent();
        }

        private void Al_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)al.IsChecked)
            {
                player_name.Visibility = Visibility.Visible;
                twoPlayer.IsChecked = false;
                player_name2.Visibility = Visibility.Collapsed;
                player_name_box2.Text = "";
                player_name_box3.Text = "";
            }
            else
            {
                player_name.Visibility = Visibility.Collapsed;
            }
        }

        private void TwoPlayer_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)twoPlayer.IsChecked)
            {
                al.IsChecked = false;
                player_name.Visibility = Visibility.Collapsed;
                player_name2.Visibility = Visibility.Visible;
                player_name_box.Text = "";
            }
            else
            {
                player_name2.Visibility = Visibility.Collapsed;
            }
        }

        private void Mentes_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(Globals.Restore_File_Name)) {
                if ((bool)al.IsChecked)
                {
                    if (player_name_box.Text.Length > 0)
                    {
                        GameWindow gamewindow = new GameWindow(player_name_box.Text);
                        gamewindow.Show();
                        _exit = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nem adott meg nevet!");
                    }
                }
                else if ((bool)twoPlayer.IsChecked)
                {
                    if (player_name_box2.Text.Length > 0 && player_name_box3.Text.Length > 0)
                    {
                        ShipPut ship = new ShipPut(player_name_box2.Text, player_name_box3.Text);
                        ship.Show();
                        _exit = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nem adott meg nevet!");
                    }
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Önnek van egy megkezdett játéka!\nAkarja folytatni?\nNem válasz esetén új játékot kezd és a régi mentés törlődik!", "Megerősítés", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                     Restore_File _restor_file_JSON = new Restore_File();
                    String jsonString = File.ReadAllText(Globals.Restore_File_Name);
                    _restor_file_JSON = JsonSerializer.Deserialize<Restore_File>(jsonString);

                    if (_restor_file_JSON.Player1_Name.Equals("Al"))
                    {
                        GameWindow window = new GameWindow(player_name_box.Text, 1);
                        window.Show();
                        _exit = true;
                        this.Close();
                    }
                    else
                    {
                        Player_GameWindow player = new Player_GameWindow();
                        player.Show();
                        _exit = true;
                        this.Close();
                    }
                    
                }
                else
                {
                    File.Delete(Globals.Restore_File_Name);
                    Mentes_Button_Click(sender, e);
                }
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F1)
            {
                HelpWindow helpwindow = new HelpWindow();
                helpwindow.Show();
            }
        }

        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HelpWindow helpwindow = new HelpWindow();
            helpwindow.Show();
        }

        private void Eredmenyek_Button_Click(object sender, RoutedEventArgs e)
        {
            ScoreBoardWindow scoreBoardWindow = new ScoreBoardWindow();
            scoreBoardWindow.Show();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Biztos ki akar lépni?", "Megerősítés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _exit = true;
                this.Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_exit)
            {
                MessageBox.Show("Kérem használja a \"Kilépés\" gombot a kilépéshez!");
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
