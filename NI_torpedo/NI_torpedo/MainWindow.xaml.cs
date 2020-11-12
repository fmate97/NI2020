using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NI_torpedo
{
    public partial class MainWindow : Window
    {
        public bool isTwoPlayer;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void al_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            NameWindow nameWindow = new NameWindow();

            isTwoPlayer = false;
            nameWindow.Show();
            this.Close();
        }

        private void twoPlayer_Checked(object sender, RoutedEventArgs e)
        {
            NameWindow firstPlayer = new NameWindow();
            NameWindow secondPlayer = new NameWindow();

            isTwoPlayer = true;
            firstPlayer.Show();
            secondPlayer.Show();
            this.Close();
        }
    }
}
