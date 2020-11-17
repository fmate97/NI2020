using System.Windows;

namespace NI_torpedo.View
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
                " A játék akkor ér véget, ha valamelyik játékosnak az összes hajója ki van lőve. \n \n" +
                "A hajók lehelyzéséhez a megfelelő hajóra kell kattintani. Ha bal egérgombbal kattint rá akkor függőlegesen, ha jobbal akkor vízszintesen fogja lehelyezni. " + 
                "Majd a hajó kezdőhelyére kattintani a saját játéktábláján. \n \n" +
                "A hajó nagysága: \n" + "1 x 5 egység hosszú \n" + "2 x 4 egység hosszú \n" + "4 x 3 egység hosszú \n" + "4 x 2 egység hosszú";

        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
