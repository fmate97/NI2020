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
    /// <summary>
    /// Interaction logic for NameWindow.xaml
    /// </summary>
    public partial class NameWindow : Window
    {
        private bool _name = true;
        private string _firstName;
        private string _secondName;
        public NameWindow()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (_name)
            {
                _firstName = textBox.Text;
                _name = false;
                this.Close();
            }
            else
            {
                _secondName = textBox.Text;
                this.Close();
            }
            GameWindow gameWindow = new GameWindow(_firstName);
            gameWindow.Show();
        }

    }
}
