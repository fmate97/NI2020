using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace NI_torpedo.Model
{
    public class GameWindow_Player_model
    {
        Random rnd = new Random();
        private DataSave_JSON _dataSave_JSON = new DataSave_JSON();
        private Restore_File _restor_file_JSON = new Restore_File();


        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string WinnerName { get; set; }
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
        public int[] FirstPlayer_ScoreBoardShip { get; set; } = new int[4];
        public int[] SecondPlayer_ScoreBoardShip { get; set; } = new int[4];

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
            int number = rnd.Next(1, 2);
            if (number == 1)
            {
                return NextPlayer = true;
            }
            return NextPlayer = false;
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
                        FirstPlayer_ScoreBoardShip[0]++;
                        break;
                    case 3:
                        FirstPlayer_ScoreBoardShip[1]++;
                        break;
                    case 4:
                        FirstPlayer_ScoreBoardShip[2]++;
                        break;
                    case 5:
                        FirstPlayer_ScoreBoardShip[3]++;
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
                        SecondPlayer_ScoreBoardShip[0]++;
                        break;
                    case 3:
                        SecondPlayer_ScoreBoardShip[1]++;
                        break;
                    case 4:
                        SecondPlayer_ScoreBoardShip[2]++;
                        break;
                    case 5:
                        SecondPlayer_ScoreBoardShip[3]++;
                        break;
                }
            }
        }

        public void JSON_Save_Restore()
        {
            if (File.Exists(Globals.Restore_File_Name))
            {
                File.Delete(Globals.Restore_File_Name);
                JSON_Save_Restore();
            }
            else
            {
                _restor_file_JSON.Player1_Name = FirstName;
                _restor_file_JSON.Player2_Name = SecondName;

                _restor_file_JSON.Scoreboard = new List<int>() { NumberOfRounds, NumberOfFirstPlayerHits, NumberOfSecondPlayerHits };

                _restor_file_JSON.Player1_ScoreBoardShip = FirstPlayer_ScoreBoardShip;
                _restor_file_JSON.Player1_Ship_Pos = FirstPlayerShip;
                _restor_file_JSON.Player1_Good_Pos = FirstPlayer_GoodTipp;
                _restor_file_JSON.Player1_Bad_Pos = FirstPlayer_WrongTipp;
                _restor_file_JSON.Player1_ScoreShips = FirstShips;
                
                _restor_file_JSON.Player2_ScoreBoardShip = SecondPlayer_ScoreBoardShip;
                _restor_file_JSON.Player2_Ship_Pos = SecondPlayerShip;
                _restor_file_JSON.Player2_Good_Pos = SecondPlayer_GoodTipp;
                _restor_file_JSON.Player2_Bad_Pos = SecondPlayer_WrongTipp;
                _restor_file_JSON.Player2_ScoreShips = SecondShips;

                _restor_file_JSON.CheckSum = _restor_file_JSON.CheckSum_Calc();

                if (NextPlayer)
                {
                    _restor_file_JSON.Player_Number = 1;
                }
                else
                {
                    _restor_file_JSON.Player_Number = 2;
                }


                String jsonString = JsonSerializer.Serialize<Restore_File>(_restor_file_JSON);
                File.WriteAllText(Globals.Restore_File_Name, jsonString);
            }
        }

        public int Restore_Game()
        {
            if (File.Exists(Globals.Restore_File_Name))
            {
                string jsonString = File.ReadAllText(Globals.Restore_File_Name);
                _restor_file_JSON = JsonSerializer.Deserialize<Restore_File>(jsonString);

               /* if (_restor_file_JSON.CheckSum != _restor_file_JSON.CheckSum_Calc())
                {
                    return -1;
                }*/

                FirstName = _restor_file_JSON.Player1_Name;
                SecondName = _restor_file_JSON.Player2_Name;

                FirstPlayerShip = _restor_file_JSON.Player1_Ship_Pos;
                FirstPlayer_GoodTipp = _restor_file_JSON.Player1_Good_Pos;
                FirstPlayer_WrongTipp = _restor_file_JSON.Player1_Bad_Pos;
                FirstPlayer_ScoreBoardShip = _restor_file_JSON.Player1_ScoreBoardShip;
                FirstShips = _restor_file_JSON.Player1_ScoreShips;

                SecondPlayerShip = _restor_file_JSON.Player2_Ship_Pos;
                SecondPlayer_GoodTipp = _restor_file_JSON.Player2_Good_Pos;
                SecondPlayer_WrongTipp = _restor_file_JSON.Player2_Bad_Pos;
                SecondPlayer_ScoreBoardShip = _restor_file_JSON.Player2_ScoreBoardShip;
                SecondShips = _restor_file_JSON.Player2_ScoreShips;

                List<int> helper = _restor_file_JSON.Scoreboard;
                {
                    NumberOfRounds = helper[0];
                    NumberOfFirstPlayerHits = helper[1];
                    NumberOfSecondPlayerHits = helper[2];
                }

                if (_restor_file_JSON.Player_Number == 1)
                {
                    NextPlayer = true;
                }
                else
                {
                    NextPlayer = false;
                }
            }
            return 1;
        }

        public void JSON_Save()
        {
            String jsonString;
            if (File.Exists(Globals.Save_File_Name))
            {
                jsonString = File.ReadAllText(Globals.Save_File_Name);
                _dataSave_JSON = JsonSerializer.Deserialize<DataSave_JSON>(jsonString);

                _dataSave_JSON.Data_number++;
                _dataSave_JSON.Data.Add(new DataSave_JSON_helper()
                {
                    Player1_Name = FirstName,
                    Player2_Name = SecondName,
                    Winner_Name = WinnerName,
                    Scoreboard = new List<int>() { NumberOfRounds, NumberOfFirstPlayerHits, NumberOfSecondPlayerHits},
                    Player1_ShipScore = FirstPlayer_ScoreBoardShip,
                    Player2_ShipScore = SecondPlayer_ScoreBoardShip
                }) ;

                jsonString = JsonSerializer.Serialize<DataSave_JSON>(_dataSave_JSON);
                File.WriteAllText(Globals.Save_File_Name, jsonString);
            }
            else
            {
                _dataSave_JSON.Data_number = 1;
                _dataSave_JSON.Data.Add(new DataSave_JSON_helper()
                {
                    Player1_Name = FirstName,
                    Player2_Name = SecondName,
                    Winner_Name = WinnerName,
                    Scoreboard = new List<int>() { NumberOfRounds, NumberOfFirstPlayerHits, NumberOfSecondPlayerHits},
                    Player1_ShipScore = FirstPlayer_ScoreBoardShip,
                    Player2_ShipScore = SecondPlayer_ScoreBoardShip
                });

                jsonString = JsonSerializer.Serialize<DataSave_JSON>(_dataSave_JSON);
                File.WriteAllText(Globals.Save_File_Name, jsonString);
            }
        }
    }
}
