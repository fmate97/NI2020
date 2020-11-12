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
    public partial class GameWindow : Window
    {
        private int _tabla_merete = 10, _tabla_magassaga = 300, _tabla_szelessege = 300, _margo_merete = 3;
        private List<Vector> _tabla_kocka_helyzete = new List<Vector>();
        public GameWindow(string nev)
        {
            InitializeComponent();

            nevLabel.Content = nev;
            tabla_kocka_helyzete_init();
            foreach (Vector kocka in _tabla_kocka_helyzete)
            {
                jatektabla_init(kocka, sajat_jatektabla);
                jatektabla_init(kocka, masik_player_jatektabla);
            }
            eredmenyjelzo.Content = "Eredmenyjelzo:" + '\n' + "TODO";            
        }
        private void tabla_kocka_helyzete_init()
        {
            for (int i = 0; i < _tabla_merete; i++)
            {
                for (int j = 0; j < _tabla_merete; j++)
                {
                    _tabla_kocka_helyzete.Add(new Vector(i, j));
                }
            }
        }
        private void jatektabla_init(Vector position, Canvas canvas_name)
        {
            var unitX = _tabla_szelessege / _tabla_merete;
            var unitY = _tabla_magassaga / _tabla_merete;

            var shape = new Rectangle();            
            shape.Fill = Brushes.Black;
            shape.Width = unitX;
            shape.Height = unitY;
            Canvas.SetTop(shape, position.Y * unitY);
            Canvas.SetLeft(shape, position.X * unitX);
            canvas_name.Children.Add(shape);

            var shape2 = new Rectangle();
            shape2.Fill = Brushes.White;
            shape2.Width = unitX - _margo_merete;
            shape2.Height = unitY - _margo_merete;
            Canvas.SetTop(shape2, position.Y * unitY + (_margo_merete / 2));
            Canvas.SetLeft(shape2, position.X * unitX + (_margo_merete / 2));
            canvas_name.Children.Add(shape2);

        }
        int teszt = 0;
        private void sajat_jatektabla_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            teszt = 0;
            Point eger_pos = e.GetPosition(sajat_jatektabla);
            egerPos.Content = "Saját player: " + coord_conv(eger_pos.X, teszt) + " " + coord_conv(eger_pos.Y, teszt);
        }
        private void masik_player_jatektabla_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            teszt = 0;
            Point eger_pos = e.GetPosition(masik_player_jatektabla);            
            egerPos.Content = "Másik player: " + coord_conv(eger_pos.X, teszt) + " " + coord_conv(eger_pos.Y, teszt);
        }


        private int coord_conv(double number, int teszt)
        {
            var unit = _tabla_magassaga / _tabla_merete;

            if(number < 0 || number > _tabla_magassaga)
            {
                return -1;
            }
            else if(number <= unit)
            {
                teszt++;
                return teszt;
            }
            else
            {
                teszt++;
                teszt = coord_conv(number - unit, teszt);
                return teszt;
            }
        }
    }
}
