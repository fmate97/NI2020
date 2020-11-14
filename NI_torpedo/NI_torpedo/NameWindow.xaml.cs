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
    public partial class NameWindow : Window
    {
        private bool _isTwoPlayer;
        private string _name;
        public NameWindow(bool isTwoPlayer)
        {
            InitializeComponent();
            _isTwoPlayer = isTwoPlayer;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            _name = textBox.Text;
            if (_name.Length == 0)
            {
                MessageBox.Show("Nem írt nevet!");
            }
            else
            {
                this.Close();

                GameWindow gameWindow = new GameWindow(_isTwoPlayer, _name);
                gameWindow.Show();
            }
        }

    }
}
