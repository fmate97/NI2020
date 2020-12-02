using System.ComponentModel;
using System.Windows;

namespace NI_torpedo.View
{
    public partial class HelpWindow : Window
    {
        private bool _exit = false;

        public HelpWindow()
        {
            InitializeComponent();
            OnePlayerGame.Text = "Az egy személyes játékmódban a játékos előtt két darab tábla van. " +
                "Az egyiken jelöli a lövéseit, a másikon a saját hajói vannak, és az ellenfél lövései. " +
                "A hajók lehelyzéséhez a megfelelő hajóra kell kattintani. Ha bal egérgombbal kattint rá akkor függőlegesen, ha jobbal akkor vízszintesen fogja lehelyezni. " +
                "Majd a hajó kezdőhelyére kattintva, elhelyezzük a saját játéktáblánkon. Mentés után" +
                "a játékos felváltva tippel a számítógéppel. A játékos úgy tud tippelni, hogy rákattint a tippelt területre." +
                "Találatnak számít, ha eltalálunk egy hajó egy kockáját, süllyedésnek, ha minden kockáját eltaláltuk. Ha nem találjuk el a hajót," +
                " azt a négyzetet pirossal, ha eltaláljuk zöldel jelöljük." +
                " A játék akkor ér véget, ha valamelyik játékosnak az összes hajója ki van lőve. \n \n" +
                "A hajó nagysága: \n" + "1 x 5 egység hosszú \n" + "2 x 4 egység hosszú \n" + "4 x 3 egység hosszú \n" + "4 x 2 egység hosszú";
            TwoPlayerGame.Text = "A két személyes játékmód nagyon hasonlít az első játékos módra. Annyi a különbség, hogy először egy táblát látnak a játékosok ahol el kell helyezniük a hajóikat. " +
                "Miután mind a két játkos elhelyzte a hajóit két tábla jön fel az elsőn az első játékos helyezheti el a tippeit, a másodikon a második játékos.\n \n" +
                "Ha később szeretnéd folytatni a játékot akkor nyomd meg a Késöbbi folytatás gombot. " +
                "Ha legközelebb belépsz a kiválasztásnál csak simán nyomd meg a mentés gombot és máris tudod folytatni a játékot.";
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            _exit = true;
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_exit)
            {
                MessageBox.Show("Kérem használja a \"Vissza a játékhoz\" gombot a kilépéshez!");
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
