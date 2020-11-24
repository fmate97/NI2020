using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NI_torpedo.Model
{
    public class GameWindow_Player_model
    {
        Random rnd = new Random();
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int TableSize { get; } = 10;
        public int TableHight { get; } = 300;
        public int TableWidth { get; } = 300;
        public int MarginSize { get; } = 3;
        public int CubeSize { get; set; } = 33;
        public bool Fuggoleges { get; set; }
        public List<Vector> FirstPlayerShip { get; set; } = new List<Vector>();
        public List<Vector> SecondPlayerShip { get; set; } = new List<Vector>();
        public bool NextPlayer { get; set; }
        public List<Vector> FirstPlayer_GoodTipp { get; set; } = new List<Vector>();
        public List<Vector> FirstPlayer_WrongTipp { get; set; } = new List<Vector>();
        public List<Vector> SecondPlayer_GoodTipp { get; set; } = new List<Vector>();
        public List<Vector> SecondPlayer_WrongTipp { get; set; } = new List<Vector>();

        public GameWindow_Player_model(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        public int Coord_Conv(double number, int seged)
        {
            var unit = TableHight / TableSize;

            if (number < 0 || number > TableHight)
            {
                return -1;
            }
            else if (number <= unit)
            {
                seged++;
                return seged;
            }
            else
            {
                seged++;
                seged = Coord_Conv(number - unit, seged);
                return seged;
            }
        }

        public bool Start()
        {
            int number = rnd.Next(1);
            if(number == 1)
            {
                return NextPlayer=true;
            }
            return NextPlayer=false;
        }

    }
}
