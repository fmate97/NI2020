using System.Collections.Generic;
using System.Windows;

namespace NI_torpedo.Model
{
    public static class Globals
    {
        public static string Save_File_Name = "Eredmenyjelzo_Save.json";
        public static string Restore_File_Name = "Restore_Save.json";
    }

    public class DataSave_JSON
    {
        public int Data_number { get; set; }
        public List<DataSave_JSON_helper> Data { get; set; }

        public DataSave_JSON()
        {
            Data_number = 0;
            Data = new List<DataSave_JSON_helper>();
        }
    }

    public class DataSave_JSON_helper
    {
        public string Player1_Name { get; set; }
        public string Player2_Name { get; set; }
        public string Winner_Name { get; set; }
        public List<int> Scoreboard { get; set; }
        public int[] Player1_ShipScore { get; set; } = new int[4];
        public int[] Player2_ShipScore { get; set; } = new int[4];

    }

    public class Restore_File
    {
        public string Player1_Name { get; set; }
        public string Player2_Name { get; set; }
        public int Player_Number { get; set; }
        public List<int> Scoreboard { get; set; }
        public int[] Player1_ScoreBoardShip { get; set; } = new int[4];
        public int[] Player2_ScoreBoardShip { get; set; } = new int[4];
        public List<Vector> Player1_Ship_Pos { get; set; }
        public List<Vector> Player1_Good_Pos { get; set; }
        public List<Vector> Player1_Bad_Pos { get; set; }
        public List<Vector> Player2_Ship_Pos { get; set; }
        public List<Vector> Player2_Good_Pos { get; set; }
        public List<Vector> Player2_Bad_Pos { get; set; }
        public List<List<ShipUnit>> Player1_ScoreShips { get; set; } = new List<List<ShipUnit>>();
        public List<List<ShipUnit>> Player2_ScoreShips { get; set; } = new List<List<ShipUnit>>();
        public int CheckSum { get; set; }

        public int CheckSum_Calc()
        {
            int return_value = Player_Number;

            foreach (char item in Player1_Name.ToCharArray())
                return_value += item;
            foreach (char item in Player2_Name.ToCharArray())
                return_value += item;
            foreach (int item in Scoreboard)
                return_value += item;
            foreach (Vector item in Player1_Ship_Pos)
                return_value += (int)item.X + (int)item.Y;
            foreach (Vector item in Player1_Good_Pos)
                return_value += (int)item.X + (int)item.Y;
            foreach (Vector item in Player1_Bad_Pos)
                return_value += (int)item.X + (int)item.Y;
            foreach (Vector item in Player2_Ship_Pos)
                return_value += (int)item.X + (int)item.Y;
            foreach (Vector item in Player2_Good_Pos)
                return_value += (int)item.X + (int)item.Y;
            foreach (Vector item in Player2_Bad_Pos)
                return_value += (int)item.X + (int)item.Y;

            return return_value;
        }
    }
}
