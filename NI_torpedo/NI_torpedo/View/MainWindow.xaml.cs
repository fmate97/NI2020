using System.Windows;

namespace NI_torpedo.View
{
    public partial class MainWindow : Window
    {
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
            }
            else
            {
                player_name2.Visibility = Visibility.Collapsed;
            }
        }

        private void Mentes_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)al.IsChecked)
            {
                if (player_name_box.Text.Length > 0)
                {
                    GameWindow gamewindow = new GameWindow(player_name_box.Text);
                    gamewindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nem adott meg nevet!");
                }
            }
            else if ((bool)twoPlayer.IsChecked)
            {
                if(player_name_box2.Text.Length > 0 && player_name_box3.Text.Length > 0)
                {
                    //TODO: 2playeres ablak
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nem adott meg nevet!");
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
            //TODO: Eredmenyek Show
        }
    }
}
