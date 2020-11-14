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
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            rules.Content = "Játékszabály";
            game.Text = "Mindkét játékos előtt két darab tábla van. " +
                "Az egyiken ő jelöli a lövéseit, a másikon a saját hajói vannak, és az ellenfél lövései. " +
                "A játékosok felváltva tippelnek, és mindketten rákattintanak a tippelt területre. " +
                "Találatnak számít, ha eltalálunk egy hajót, süllyedésnek, ha minden kockáját eltaláltuk. Ha nem találjuk el a hajót," +
                " azt a négyzetet pirossal, ha eltaláljuk zöldel jelöljük." +
                " A játék akkor ér véget, ha valamelyik játékosnak az összes hajója ki van lőve." + '\n'+
                "A hajó nagysága: \n" + "1 x 5 egység hosszú" + '\n' + "2 x 4 egység hosszú" + '\n' + "4 x 3 egység hosszú" + '\n' + "4 x 2 egység hosszú";

        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
