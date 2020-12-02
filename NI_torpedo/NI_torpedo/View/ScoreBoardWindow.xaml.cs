using System;
using System.Windows;
using NI_torpedo.Model;
using System.IO;
using System.Text.Json;
using System.ComponentModel;

namespace NI_torpedo.View
{
    public partial class ScoreBoardWindow : Window
    {
        private DataSave_JSON _dataSave_JSON = new DataSave_JSON();
        private DataSave_JSON_helper _dataSave_JSON_helper = new DataSave_JSON_helper();
        private int _oldal_szam = 0, _max_oldal_szam;
        private bool _exit = false;

        public ScoreBoardWindow()
        {
            InitializeComponent();

            if (File.Exists(Globals.Save_File_Name))
            {
                nincsfile.Visibility = Visibility.Collapsed;
                vanfile.Visibility = Visibility.Visible;
                String jsonString = File.ReadAllText(Globals.Save_File_Name);
                _dataSave_JSON = JsonSerializer.Deserialize<DataSave_JSON>(jsonString);
                _max_oldal_szam = _dataSave_JSON.Data_number - 1;
                _dataSave_JSON_helper = _dataSave_JSON.Data[_oldal_szam];
                WindowInit();
            }
            else
            {
                nincsfile.Visibility = Visibility.Visible;
                vanfile.Visibility = Visibility.Collapsed;
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            _exit = true;
            this.Close();
        }

        private void Elozo_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_oldal_szam >= 1)
            {
                _oldal_szam--;
                _dataSave_JSON_helper = _dataSave_JSON.Data[_oldal_szam];
                WindowInit();
            }
        }

        private void Kovetkezo_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_max_oldal_szam > _oldal_szam)
            {
                _oldal_szam++;
                _dataSave_JSON_helper = _dataSave_JSON.Data[_oldal_szam];
                WindowInit();
            }
        }

        private void WindowInit()
        {
            oldal_szam_label.Content = _oldal_szam + 1;
            player1_name.Content = _dataSave_JSON_helper.Player1_Name;
            player2_name.Content = _dataSave_JSON_helper.Player2_Name;
            nyertes_name.Content = _dataSave_JSON_helper.Winner_Name;

            player1_hajoi.Content = player1_name.Content + " elsülyesztett hajói:  ";
            player2_hajoi.Content = player2_name.Content + " elsülyesztett hajói:  ";
            player1_talalatai.Content = player1_name.Content + " találatai: ";
            player2_talalatai.Content = player2_name.Content + " találatai: ";

            korok_szam.Content = _dataSave_JSON_helper.Scoreboard[0];
            player1_talalat.Content = _dataSave_JSON_helper.Scoreboard[1];
            player2_talalat.Content = _dataSave_JSON_helper.Scoreboard[2];

            player1_hajo2.Content = _dataSave_JSON_helper.Player1_ShipScore[0];
            player1_hajo3.Content = _dataSave_JSON_helper.Player1_ShipScore[1];
            player1_hajo4.Content = _dataSave_JSON_helper.Player1_ShipScore[2];
            player1_hajo5.Content = _dataSave_JSON_helper.Player1_ShipScore[3];

            player2_hajo2.Content = _dataSave_JSON_helper.Player2_ShipScore[0];
            player2_hajo3.Content = _dataSave_JSON_helper.Player2_ShipScore[1];
            player2_hajo4.Content = _dataSave_JSON_helper.Player2_ShipScore[2];
            player2_hajo5.Content = _dataSave_JSON_helper.Player2_ShipScore[3];
            Button_Enabled();
        }

        private void Torles_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Biztos törölni akarja az összes mentett eredményt?", "Megerősítés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                File.Delete(Globals.Save_File_Name);
                _exit = true;
                this.Close();
            }
        }

        private void Button_Enabled()
        {
            elozo_button.IsEnabled = true;
            kovetkezo_button.IsEnabled = true;
            if (_oldal_szam == 0)
                elozo_button.IsEnabled = false;
            if (_oldal_szam == _max_oldal_szam)
                kovetkezo_button.IsEnabled = false;
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
