using NI_torpedo.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace NI_torpedo.ViewModel
{
    public class GameWindow_Player_viewmodel
    {
        public static string _firstName;
        public static string _secondName;
        GameWindow_Player_model Player_Model = new GameWindow_Player_model(_firstName, _secondName);

        public GameWindow_Player_viewmodel(string firstName, string secondName)
        {
            _firstName = firstName;
            _secondName = secondName;
            Player_Model.FirstName = firstName;
            Player_Model.SecondName = secondName;
        }

        public GameWindow_Player_viewmodel()
        {
        }

        public GameWindow_Player_viewmodel(string firstName, string secondName,
            List<Vector> firstPlayerShip, List<Vector> secondPlayerShip,
            List<List<ShipUnit>> FirstShip, List<List<ShipUnit>> SecondShip)
        {
            Player_Model.FirstName = firstName;
            Player_Model.SecondName = secondName;
            foreach (var ship in firstPlayerShip)
            {
                Player_Model.FirstPlayerShip.Add(ship);
            }

            foreach (var ship in secondPlayerShip)
            {
                Player_Model.SecondPlayerShip.Add(ship);
            }

            for(int i=0; i<FirstShip.Count; i++)
            {
                Player_Model.FirstShips.Add(new List<ShipUnit>());
                for(int j=0; j < FirstShip[i].Count; j++)
                {
                    Player_Model.FirstShips[i].Add(FirstShip[i][j]);
                }
            }

            for (int i = 0; i < SecondShip.Count; i++)
            {
                Player_Model.SecondShips.Add(new List<ShipUnit>());
                for (int j = 0; j < SecondShip[i].Count; j++)
                {
                    Player_Model.SecondShips[i].Add(SecondShip[i][j]);
                }
            }
        }

        public string FirstName()
        {
            return Player_Model.FirstName;
        }

        public string SecondName()
        {
            return Player_Model.SecondName;
        }

        public int TableSize()
        {
            return Player_Model.TableSize;
        }

        public int TableHight()
        {
            return Player_Model.TableHight;
        }

        public int TableWidth()
        {
            return Player_Model.TableWidth;
        }

        public int MarginSize()
        {
            return Player_Model.MarginSize;
        }

        public int[] Kocka_Unit()
        {
            int[] unit = new int[3];
            unit[0] = Player_Model.TableWidth / Player_Model.TableSize;
            unit[1] = Player_Model.TableHight / Player_Model.TableSize;
            unit[2] = Player_Model.MarginSize;
            return unit;
        }

        public void Fuggoleges_Set(bool _fuggoleges)
        {
            Player_Model.Fuggoleges = _fuggoleges;
        }

        public bool Fuggoleges_Get()
        {
            return Player_Model.Fuggoleges;
        }

        public bool First_Ship_Check(Vector eger_pos_vector, int length)
        {
            if (Player_Model.Fuggoleges)
            {
                foreach (Vector item in Player_Model.FirstPlayerShip)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (eger_pos_vector.X == item.X && eger_pos_vector.Y + i == item.Y)
                        {
                            return false;
                        }
                    }
                }
                for (int i = 0; i < length; i++)
                {
                    if (eger_pos_vector.Y + i > Player_Model.TableSize - 1)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Vector item in Player_Model.FirstPlayerShip)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (eger_pos_vector.X + i == item.X && eger_pos_vector.Y == item.Y)
                        {
                            return false;
                        }
                    }
                }
                for (int i = 0; i < length; i++)
                {
                    if (eger_pos_vector.X + i > Player_Model.TableSize - 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Second_Ship_Check(Vector eger_pos_vector, int length)
        {
            if (Player_Model.Fuggoleges)
            {
                foreach (Vector item in Player_Model.SecondPlayerShip)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (eger_pos_vector.X == item.X && eger_pos_vector.Y + i == item.Y)
                        {
                            return false;
                        }
                    }
                }
                for (int i = 0; i < length; i++)
                {
                    if (eger_pos_vector.Y + i > Player_Model.TableSize - 1)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Vector item in Player_Model.SecondPlayerShip)
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (eger_pos_vector.X + i == item.X && eger_pos_vector.Y == item.Y)
                        {
                            return false;
                        }
                    }
                }
                for (int i = 0; i < length; i++)
                {
                    if (eger_pos_vector.X + i > Player_Model.TableSize - 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int Coord_Conv(double number)
        {
            return Player_Model.Coord_Conv(number, 0);
        }

        public void FirstShipAdd(Vector vector)
        {
            Player_Model.FirstPlayerShip.Add(vector);
        }

        public int FirstShipSize()
        {
            return Player_Model.FirstPlayerShip.Count;
        }

        public void FirstShipClear()
        {
            Player_Model.FirstPlayerShip.Clear();
        }

        public List<Vector> FirstShipGet()
        {
            return Player_Model.FirstPlayerShip;
        }

        public void FirstShipNewList()
        {
            Player_Model.FirstShips.Add(new List<ShipUnit>());
        }

        public void FirstShipAddVector(int s, Vector vector)
        {
            Player_Model.FirstShips[s].Add(new ShipUnit(vector, false));
        }

        public List<List<ShipUnit>> FirstShip()
        {
            return Player_Model.FirstShips;
        }

        public void SecondShipAdd(Vector vector)
        {
            Player_Model.SecondPlayerShip.Add(vector);
        }

        public int SecondShipSize()
        {
            return Player_Model.SecondPlayerShip.Count;
        }

        public void SecondShipClear()
        {
            Player_Model.SecondPlayerShip.Clear();
        }

        public List<Vector> SecondShipGet()
        {
            return Player_Model.SecondPlayerShip;
        }
        public void SecondShipAdd()
        {
            Player_Model.SecondShips.Add(new List<ShipUnit>());
        }
        public void SecondShipNewList()
        {
            Player_Model.SecondShips.Add(new List<ShipUnit>());
        }

        public void SecondShipAddVector(int s, Vector vector)
        {
            Player_Model.SecondShips[s].Add(new ShipUnit(vector, false));
        }

        public List<List<ShipUnit>> SecondShip()
        {
            return Player_Model.SecondShips;
        }
        public int CubeSize()
        {
            return Player_Model.CubeSize;
        }

        public bool RandomStart()
        {
            return Player_Model.Start();
        }
       
        public bool NextPlayer()
        {
            return Player_Model.NextPlayer;
        }
        public void NextPlayerSet(bool set)
        {
            Player_Model.NextPlayer = set;
        }
        public Brush FirstPlayerStep(Vector vector)
        {
            for(int i=0; i<Player_Model.FirstPlayer_GoodTipp.Count; i++)
            {
                if (Player_Model.FirstPlayer_GoodTipp[i] == vector)
                {
                    return Brushes.Black;
                }
            }
            for (int i = 0; i < Player_Model.FirstPlayer_WrongTipp.Count; i++)
            {
                if (Player_Model.FirstPlayer_WrongTipp[i] == vector)
                {
                    return Brushes.Black;
                }
            }
            for (int i=0; i<Player_Model.SecondPlayerShip.Count; i++)
            {
                if (Player_Model.SecondPlayerShip[i] == vector)
                {
                    Player_Model.FirstPlayer_GoodTipp.Add(vector);
                    Player_Model.NumberOfFirstPlayerHits++;
                    Player_Model.FirstShipHit(vector);
                    return Brushes.Green;
                }
            }
            Player_Model.FirstPlayer_WrongTipp.Add(vector);
            return Brushes.Red;
        }

        public Brush SecondPlayerStep(Vector vector)
        {
            for (int i = 0; i < Player_Model.SecondPlayer_GoodTipp.Count; i++)
            {
                if (Player_Model.SecondPlayer_GoodTipp[i] == vector)
                {
                    return Brushes.Black;
                }
            }
            for (int i = 0; i < Player_Model.SecondPlayer_WrongTipp.Count; i++)
            {
                if (Player_Model.SecondPlayer_WrongTipp[i] == vector)
                {
                    return Brushes.Black;
                }
            }
            for (int i = 0; i < Player_Model.FirstPlayerShip.Count; i++)
            {
                if (Player_Model.FirstPlayerShip[i] == vector)
                {
                    Player_Model.SecondPlayer_GoodTipp.Add(vector);
                    Player_Model.NumberOfSecondPlayerHits++;
                    Player_Model.SecondShipHit(vector);
                    return Brushes.Green;
                }
            }
            Player_Model.SecondPlayer_WrongTipp.Add(vector);
            return Brushes.Red;
        }

        public List<Vector> FirstPlayer_GoodTippGet()
        {
            return Player_Model.FirstPlayer_GoodTipp;
        }

        public int FirstPlayer_GoodTippCount()
        {
            return Player_Model.FirstPlayer_GoodTipp.Count;
        }

        public List<Vector> FirstPlayer_WrongTippGet()
        {
            return Player_Model.FirstPlayer_WrongTipp;
        }

        public List<Vector> SecondPlayer_GoodTippGet()
        {
            return Player_Model.SecondPlayer_GoodTipp;
        }

        public int SecondPlayer_GoodTippCount()
        {
            return Player_Model.SecondPlayer_GoodTipp.Count;
        }

        public List<Vector> SecondPlayer_WrongTippGet()
        {
            return Player_Model.SecondPlayer_WrongTipp;
        }

        public void NumberOfRoundsAdd()
        {
             Player_Model.NumberOfRounds++;
        }

        public int NumberOfRounds()
        {
            return Player_Model.NumberOfRounds;
        }

        public int FirstPlayerHits()
        {
            return Player_Model.NumberOfFirstPlayerHits;
        }

        public int SecondPlayerHits()
        {
            return Player_Model.NumberOfSecondPlayerHits;
        }

        public int[] FirstPlayer_ScoreBoard()
        {
            return Player_Model.FirstPlayer_ScoreBoardShip;
        }

        public int[] SecondPlayer_ScoreBoard()
        {
            return Player_Model.SecondPlayer_ScoreBoardShip;
        }

        public void Save_Game()
        {
           Player_Model.JSON_Save_Restore();
        }

        public int Restore_Game()
        {
            return Player_Model.Restore_Game();
        }

        public void WinnerName(string name)
        {
            Player_Model.WinnerName = name;
        }

        public void Save()
        {
            Player_Model.JSON_Save();
        }
    }     
}
