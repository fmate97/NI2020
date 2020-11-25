using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NI_torpedo.Model
{
    public class ShipUnit
    {
        public Vector vector;
        public bool hit;

        public ShipUnit(Vector vector, bool hit)
        {
            this.vector = vector;
            this.hit = hit;
        }
    }
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

        public int NumberOfRounds { get; set; } = 0;
        public int NumberOfFirstPlayerHits { get; set; } = 0;
        public int NumberOfSecondPlayerHits { get; set; } = 0;
        public List<List<ShipUnit>> FirstShips { get; set; } = new List<List<ShipUnit>>();
        public List<List<ShipUnit>> SecondShips { get; set; } = new List<List<ShipUnit>>();
        public int FirstShip2 { get; set; } = 0;
        public int FirstShip3 { get; set; } = 0;
        public int FirstShip4 { get; set; } = 0;
        public int FirstShip5 { get; set; } = 0;
        public int SecondShip2 { get; set; } = 0;
        public int SecondShip3 { get; set; } = 0;
        public int SecondShip4 { get; set; } = 0;
        public int SecondShip5 { get; set; } = 0;

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
            int number = rnd.Next(1,2);
            if(number == 1)
            {
                return NextPlayer=true;
            }
            return NextPlayer=false;
        }

        public void FirstShipHit(Vector hit)
        {
            for (int i = 0; i < SecondShips.Count; i++)
            {
                for (int j = 0; j < SecondShips[i].Count; j++)
                {
                    if (SecondShips[i][j].vector == hit)
                    {
                        SecondShips[i][j].hit = true;
                        FirstSank(i);
                        break;
                    }
                }
            }

        }

        private void FirstSank(int i)
        {
            int db = 0;
            for (int j = 0; j < SecondShips[i].Count; j++)
            {
                if (SecondShips[i][j].hit == true)
                {
                    db++;
                }
            }
            if (SecondShips[i].Count == db)
            {
                switch (db)
                {
                    case 2:
                        FirstShip2++;
                        break;
                    case 3:
                        FirstShip3++;
                        break;
                    case 4:
                        FirstShip4++;
                        break;
                    case 5:
                        FirstShip5++;
                        break;
                }
            }
        }
        public void SecondShipHit(Vector hit)
        {
            for (int i = 0; i < FirstShips.Count; i++)
            {
                for (int j = 0; j < FirstShips[i].Count; j++)
                {
                    if (FirstShips[i][j].vector == hit)
                    {
                        FirstShips[i][j].hit = true;
                        SecondSank(i);
                        break;
                    }
                }
            }

        }

        private void SecondSank(int i)
        {
            int db = 0;
            for (int j = 0; j < FirstShips[i].Count; j++)
            {
                if (FirstShips[i][j].hit == true)
                {
                    db++;
                }
            }
            if (FirstShips[i].Count == db)
            {
                switch (db)
                {
                    case 2:
                        SecondShip2++;
                        break;
                    case 3:
                        SecondShip3++;
                        break;
                    case 4:
                        SecondShip4++;
                        break;
                    case 5:
                        SecondShip5++;
                        break;
                }
            }
        }
    }
}
