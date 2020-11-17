using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace NI_torpedo.ViewModel
{
    public class GameWindow_Al_viewmodel
    {
        public GameWindow_Al_model Model { get; set; }

        public GameWindow_Al_viewmodel()
        {
            Model = new GameWindow_Al_model();
        }

        public int[] Kocka_Unit()
        {
            int[] unit = new int[3];
            unit[0] = Model.Tabla_Szelessege / Model.Tabla_Merete;
            unit[1] = Model.Tabla_Magassaga / Model.Tabla_Merete;
            unit[2] = Model.Kocka_Margo;
            return unit;
        }

        public List<Vector> Init_Vector()
        {
            return Model.Init_Vector();
        }

        public bool Mentett_Jatek()
        {
            return Model.Mentett_Jatek;
        }

        public void Ellenfel_Hajo_Mentes()
        {
            Model.Ellenfel_Hajo_Mentes();
        }

        public void Mentett_Jatek_Set(bool mentett_jatek)
        {
            Model.Mentett_Jatek = mentett_jatek;
        }

        public bool Game_End_Bool()
        {
            return Model.Game_End;
        }

        public int Game_End()
        {
            if(Model.Player_Jo_Tipp.Count == Model.Jo_Kockak_Szama())
            {
                Model.Game_End = true;
                return 0;
            }
            else if (Model.Al_Jo_Tipp.Count == Model.Jo_Kockak_Szama())
            {
                Model.Game_End = true;
                return 1;
            }
            Model.Game_End = false;
            return 2;
        }

        public Brush Player_Lepese(Vector eger_pos_vector)
        {
            bool volt_mar = false;
            foreach (Vector player_jo in Model.Player_Jo_Tipp)
            {
                if (player_jo == eger_pos_vector)
                {
                    volt_mar = true;
                    break;
                }
            }
            foreach (Vector tipp_seged in Model.Player_Rossz_Tipp)
            {
                if (tipp_seged == eger_pos_vector)
                {
                    volt_mar = true;
                    break;
                }
            }
            if (!volt_mar)
            {
                bool talalat = false;
                foreach (Vector hajo_coord in Model.Random_Hajo_Pos)
                {
                    if (hajo_coord == eger_pos_vector)
                    {
                        Model.Elem_Talalt(eger_pos_vector);
                        Model.Player_Jo_Tipp.Add(eger_pos_vector);
                        Model.Sajat_Talalat++;
                        return Brushes.Green;
                    }
                }
                if (!talalat)
                {
                    Model.Player_Rossz_Tipp.Add(eger_pos_vector);
                    return Brushes.Red;
                }
            }
            return Brushes.Gray;
        }
        
        public List<Vector> Random_Hajo_Pos()
        {
            return Model.Random_Hajo_Pos;
        }

        public List<Vector> Player_Jo_Tipp()
        {
            return Model.Player_Jo_Tipp;
        }

        public List<Vector> Player_Rossz_Tipp()
        {
            return Model.Player_Rossz_Tipp;
        }

        public int[] Eredmenyjelzo()
        {
            Model.Korok_Szama++;
            return new int[] { Model.Korok_Szama, Model.Sajat_Talalat, Model.Ellenfel_Talalat, Model.Hajo2, Model.Hajo3, Model.Hajo4, Model.Hajo5 };
        }

        public int Start_Game()
        {
            return Model.Player_Number = Model.Get_Random_Number(1);
        }

        public void Player_Number_Nov()
        {
            Model.Player_Number++;
        }

        public void TestMetod(Vector Sikeres_Tipp, List<Vector> Al_Jo_Tipp)
        {
            Model.Elozo_Tipp_Siker = true;
            Model.Sikeres_Tipp = Sikeres_Tipp;
            Model.Al_Jo_Tipp = Al_Jo_Tipp;
        }

        public List<int> Al_Tipp()
        {
            List<int> return_value = new List<int>();
            if (Model.Lehelyezheto_Tippek_Szama() > 0)
            {
                Model.Elozo_Tipp_Siker = true;
            }
            if (!Model.Elozo_Tipp_Siker)
            {
                Vector tipp = new Vector(Model.Get_Random_Number(Model.Tabla_Merete - 1), Model.Get_Random_Number(Model.Tabla_Merete - 1));
                bool ujra_general = true;
                while (ujra_general)
                {
                    ujra_general = false;
                    foreach (Vector tipp_seged in Model.Al_Jo_Tipp)
                    {
                        if (tipp_seged == tipp)
                        {
                            //tipp = new Vector(Model.Get_Random_Number(Model.Tabla_Merete - 1), Model.Get_Random_Number(Model.Tabla_Merete - 1));
                            ujra_general = true;
                            break;
                        }
                    }
                    foreach (Vector tipp_seged in Model.Al_Rossz_Tipp)
                    {
                        if (tipp_seged == tipp)
                        {
                            //tipp = new Vector(Model.Get_Random_Number(Model.Tabla_Merete - 1), Model.Get_Random_Number(Model.Tabla_Merete - 1));
                            ujra_general = true;
                            break;
                        }
                    }
                }
                bool sikeres_tipp_bool = false;
                foreach (Vector hajo in Model.Player_Hajo_Pos)
                {
                    if (hajo == tipp)
                    {
                        Model.Ellenfel_Talalat++;
                        Model.Sikeres_Tipp = tipp;
                        Model.Elozo_Tipp_Siker = true;
                        Model.Al_Jo_Tipp.Add(tipp);
                        return_value.Add((int)tipp.X);
                        return_value.Add((int)tipp.Y);
                        return_value.Add(0);
                        return return_value;
                    }
                }
                if (!sikeres_tipp_bool)
                {
                    Model.Al_Rossz_Tipp.Add(tipp);
                    return_value.Add((int)tipp.X);
                    return_value.Add((int)tipp.Y);
                    return_value.Add(1);
                    return return_value;
                }
            }
            else
            {
                Vector tipp = new Vector(-1, -1);
                bool sikeres_tipp_bool = false, ujra_general = false;
                int irany = -1;
                while (!(tipp.X >= 0 && tipp.X < Model.Tabla_Merete && tipp.Y >= 0 && tipp.Y < Model.Tabla_Merete && ujra_general))
                {
                    irany = Model.Get_Random_Number(3);
                    switch (irany)
                    {
                        case 0:
                            tipp = new Vector(Model.Sikeres_Tipp.X - 1, Model.Sikeres_Tipp.Y);
                            break;
                        case 1:
                            tipp = new Vector(Model.Sikeres_Tipp.X, Model.Sikeres_Tipp.Y - 1);
                            break;
                        case 2:
                            tipp = new Vector(Model.Sikeres_Tipp.X + 1, Model.Sikeres_Tipp.Y);
                            break;
                        case 3:
                            tipp = new Vector(Model.Sikeres_Tipp.X, Model.Sikeres_Tipp.Y + 1);
                            break;
                    }
                    ujra_general = true;
                    foreach (Vector tipp_seged in Model.Al_Jo_Tipp)
                    {
                        if (tipp_seged == tipp)
                        {
                            //tipp = new Vector(Model.Get_Random_Number(Model.Tabla_Merete - 1), Model.Get_Random_Number(Model.Tabla_Merete - 1));
                            ujra_general = false;
                            break;
                        }
                    }
                    foreach (Vector tipp_seged in Model.Al_Rossz_Tipp)
                    {
                        if (tipp_seged == tipp)
                        {
                            //tipp = new Vector(Model.Get_Random_Number(Model.Tabla_Merete - 1), Model.Get_Random_Number(Model.Tabla_Merete - 1));
                            ujra_general = false;
                            break;
                        }
                    }
                }
                foreach (Vector hajo in Model.Player_Hajo_Pos)
                {
                    if (hajo == tipp)
                    {
                        Model.Ellenfel_Talalat++;                        
                        sikeres_tipp_bool = true;
                        Model.Sikeres_Tipp = tipp;
                        Model.Elozo_Tipp_Siker = true;
                        Model.Al_Jo_Tipp.Add(tipp);
                        Model.Sikeres_Al_Tipp_seged.Clear();
                        return_value.Add((int)tipp.X);
                        return_value.Add((int)tipp.Y);
                        return_value.Add(0);
                        return return_value;
                    }
                }
                if (!sikeres_tipp_bool)
                {
                    Model.Elozo_Tipp_Siker = false;
                    Model.Sikeres_Al_Tipp_seged.Add(irany);
                    Model.Al_Rossz_Tipp.Add(tipp);
                    return_value.Add((int)tipp.X);
                    return_value.Add((int)tipp.Y);
                    return_value.Add(1);
                    return return_value;
                }
            }
            return return_value;
        }

        public int Player_Number_Get()
        {
            return Model.Player_Number;
        }

        public void Player_Hajo_Pos_Clear()
        {
            Model.Player_Hajo_Pos.Clear();
        }

        public int Coord_Conv(double number)
        {
            return Model.Coord_Conv(number, 0);
        }

        public void Fuggoleges_Set(bool fuggoleges)
        {
            Model.Fuggoleges = fuggoleges;
        }

        public bool Fuggoleges_Get()
        {
            return Model.Fuggoleges;
        }

        public bool Player_Kovetkezik()
        {
            if (Model.Player_Number % 2 == 0)
                return true;
            return false;
        }

        public void Player_Hajo_Add(Vector vector)
        {
            Model.Player_Hajo_Pos.Add(vector);
        }

        public bool Hajo_Lehelyezheto(Vector eger_pos_vector, int length)
        {
            if (Model.Fuggoleges)
            {
                foreach (Vector item in Model.Player_Hajo_Pos)
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
                    if (eger_pos_vector.Y + i > Model.Tabla_Merete - 1)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Vector item in Model.Player_Hajo_Pos)
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
                    if (eger_pos_vector.X + i > Model.Tabla_Merete - 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Random_Hajo_Gen()
        {
            if (Model.Random_Hajo_Pos.Count != 0)
            {
                Model.Random_Hajo_Pos.Clear();
            }
            foreach (int hajo_hossza in Model.Hajok_Hossza)
            {
                int irany = Model.Get_Random_Number(3);
                Vector random_pos;
                bool helyes_pos = false;
                while (!helyes_pos)
                {
                    random_pos = new Vector(Model.Get_Random_Number(Model.Tabla_Merete - 1), Model.Get_Random_Number(Model.Tabla_Merete - 1));
                    bool foglalt = true;
                    if (Model.Random_Hajo_Pos.Count > 0)
                    {
                        while (foglalt)
                        {
                            foreach (Vector random_hajo in Model.Random_Hajo_Pos)
                            {
                                if (random_pos == random_hajo)
                                {
                                    foglalt = true;
                                    break;
                                }
                                foglalt = false;
                            }
                            if (foglalt)
                            {
                                random_pos = new Vector(Model.Get_Random_Number(Model.Tabla_Merete - 1), Model.Get_Random_Number(Model.Tabla_Merete - 1));
                            }
                        }
                    }
                    switch (irany)
                    {
                        case 0:
                            if (random_pos.X - hajo_hossza >= 0)
                            {
                                helyes_pos = true;
                            }
                            else
                            {
                                helyes_pos = false;
                            }
                            break;
                        case 1:
                            if (random_pos.Y - hajo_hossza >= 0)
                            {
                                helyes_pos = true;
                            }
                            else
                            {
                                helyes_pos = false;
                            }
                            break;
                        case 2:
                            if (random_pos.X + hajo_hossza <= Model.Tabla_Merete - 1)
                            {
                                helyes_pos = true;
                            }
                            else
                            {
                                helyes_pos = false;
                            }
                            break;
                        case 3:
                            if (random_pos.Y + hajo_hossza <= Model.Tabla_Merete - 1)
                            {
                                helyes_pos = true;
                            }
                            else
                            {
                                helyes_pos = false;
                            }
                            break;
                    }
                    if (helyes_pos)
                    {
                        if (Model.Random_Hajo_Pos.Count > 0)
                        {
                            for (int i = 1; i < hajo_hossza; i++)
                            {
                                Vector teszt;
                                switch (irany)
                                {
                                    case 0:
                                        teszt = new Vector(random_pos.X - i, random_pos.Y);
                                        break;
                                    case 1:
                                        teszt = new Vector(random_pos.X, random_pos.Y - i);
                                        break;
                                    case 2:
                                        teszt = new Vector(random_pos.X + i, random_pos.Y);
                                        break;
                                    case 3:
                                        teszt = new Vector(random_pos.X, random_pos.Y + i);
                                        break;
                                }
                                foreach (Vector random_hajo in Model.Random_Hajo_Pos)
                                {
                                    if (teszt == random_hajo)
                                    {
                                        helyes_pos = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                Model.Random_Hajo_Pos.Add(random_pos);
                for (int i = 1; i < hajo_hossza; i++)
                {
                    switch (irany)
                    {
                        case 0:
                            random_pos = new Vector(random_pos.X - 1, random_pos.Y);
                            break;
                        case 1:
                            random_pos = new Vector(random_pos.X, random_pos.Y - 1);
                            break;
                        case 2:
                            random_pos = new Vector(random_pos.X + 1, random_pos.Y);
                            break;
                        case 3:
                            random_pos = new Vector(random_pos.X, random_pos.Y + 1);
                            break;
                    }
                    Model.Random_Hajo_Pos.Add(random_pos);
                }
            }
        }
    }
}
