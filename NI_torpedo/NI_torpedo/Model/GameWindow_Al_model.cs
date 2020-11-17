using System;
using System.Collections.Generic;
using System.Windows;

namespace NI_torpedo.ViewModel
{
    public class GameWindow_Al_model
    {
        class HajoEgyseg
        {
            public Vector vector;
            public bool talalt;

            public HajoEgyseg(Vector vector, bool talalt)
            {
                this.vector = vector;
                this.talalt = talalt;
            }
        }

        readonly Random RND = new Random();
        private readonly int _tabla_merete = 10, _tabla_magassaga = 300, _tabla_szelessege = 300, _margo_merete = 3;
        private readonly int[] _hajok_hossza = { 5, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2 };
        private List<Vector> _random_hajo_pos = new List<Vector>();
        private bool _mentett_jatek = false, _fuggoleges = false;
        private List<Vector> _player_hajo_pos = new List<Vector>();
        private int _player_number;
        private List<Vector> _player_jo_tipp = new List<Vector>(), _player_rossz_tipp = new List<Vector>();
        private int _korok_szam = 0, _sajat_talalat = 0, _ellenfel_talalat = 0;
        private int _hajo2 = 0;
        private int _hajo3 = 0;
        private int _hajo4 = 0;
        private int _hajo5 = 0;
        private bool _elozo_tipp_siker;
        private List<Vector> _al_rossz_tipp = new List<Vector>(), _al_jo_tipp = new List<Vector>();
        private Vector _sikeres_tipp;
        private List<int> _sikeres_al_tip_seged = new List<int>();
        private readonly List<List<HajoEgyseg>> _hajok = new List<List<HajoEgyseg>>();
        private bool _game_end = false;

        public bool Game_End
        {
            get { return _game_end; }
            set { _game_end = value; }
        }

        public List<int> Sikeres_Al_Tipp_seged
        {
            get { return _sikeres_al_tip_seged; }
            set { _sikeres_al_tip_seged = value; }
        }

        public int Jo_Kockak_Szama()
        {
            int kocka_db = 0;
            foreach(var item in _hajok_hossza)
            {
                kocka_db += item;
            }
            return kocka_db;
        }

        public Vector Sikeres_Tipp
        {
            get { return _sikeres_tipp; }
            set { _sikeres_tipp = value; }
        }

        public List<Vector> Al_Jo_Tipp
        {
            get { return _al_jo_tipp; }
            set { _al_jo_tipp = value; }
        }

        public List<Vector> Al_Rossz_Tipp
        {
            get { return _al_rossz_tipp; }
            set { _al_rossz_tipp = value; }
        }

        public bool Elozo_Tipp_Siker
        {
            get { return _elozo_tipp_siker; }
            set { _elozo_tipp_siker = value; }
        }

        public int Korok_Szama
        {
            get { return _korok_szam; }
            set { _korok_szam = value; }
        }

        public int Ellenfel_Talalat
        {
            get { return _ellenfel_talalat; }
            set { _ellenfel_talalat = value; }
        }

        public int Hajo2
        {
            get { return _hajo2; }
        }

        public int Hajo3
        {
            get { return _hajo3; }
        }

        public int Hajo4
        {
            get { return _hajo4; }
        }

        public int Hajo5
        {
            get { return _hajo5; }
        }

        public int Sajat_Talalat
        {
            get { return _sajat_talalat; }
            set { _sajat_talalat = value; }
        }

        public List<Vector> Player_Jo_Tipp
        {
            get { return _player_jo_tipp; }
        }

        public List<Vector> Player_Rossz_Tipp
        {
            get { return _player_rossz_tipp; }
        }

        public int Player_Number
        {
            get { return _player_number; }
            set { _player_number = value; }
        }

        public bool Fuggoleges
        {
            get { return _fuggoleges; }
            set { _fuggoleges = value; }
        }

        public int Coord_Conv(double number, int seged)
        {
            var unit = _tabla_magassaga / _tabla_merete;

            if (number < 0 || number > _tabla_magassaga)
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

        public bool Mentett_Jatek
        {
            get { return _mentett_jatek; }
            set { _mentett_jatek = value; }
        }

        public List<Vector> Random_Hajo_Pos
        {
            get { return _random_hajo_pos; }
        }

        public List<Vector> Player_Hajo_Pos
        {
            get { return _player_hajo_pos; }
        }

        public int[] Hajok_Hossza
        {
            get { return _hajok_hossza; }
        }

        public int Tabla_Merete
        {
            get { return _tabla_merete; }
        }

        public int Tabla_Magassaga
        {
            get { return _tabla_magassaga; }
        }

        public int Tabla_Szelessege
        {
            get { return _tabla_szelessege; }
        }

        public int Kocka_Margo
        {
            get { return _margo_merete; }
        }

        public int Get_Random_Number(int maxvalue)
        {
            return RND.Next(maxvalue + 1);
        }

        public List<Vector> Init_Vector()
        {
            List<Vector> list = new List<Vector>();

            for (int i = 0; i < _tabla_merete; i++)
                for (int j = 0; j < _tabla_merete; j++)
                    list.Add(new Vector(i, j));

            return list;
        }

        public void Ellenfel_Hajo_Mentes()
        {
            List<Vector> seged = new List<Vector>();
            seged = _random_hajo_pos;
            int hajo_index = 0;

            _hajok.Add(new List<HajoEgyseg>());
            _hajok[hajo_index].Add(new HajoEgyseg(seged[0], false));
            _hajok[hajo_index].Add(new HajoEgyseg(seged[1], false));
            hajo_index++;


            for (int i = 2; i < seged.Count; i++)
            {
                if (seged[i - 1].Y == seged[i].Y)
                {
                    if (seged[i - 1].X != seged[i - 2].X)
                    {
                        _hajok[hajo_index - 1].Add(new HajoEgyseg(seged[i], false));
                    }
                    else
                    {
                        _hajok.Add(new List<HajoEgyseg>());
                        _hajok[hajo_index].Add(new HajoEgyseg(seged[i], false));
                        hajo_index++;
                    }
                }
                else if (seged[i].X == seged[i - 1].X)
                {
                    if (seged[i - 1].Y != seged[i - 2].Y)
                    {
                        _hajok[hajo_index - 1].Add(new HajoEgyseg(seged[i], false));
                    }
                    else
                    {
                        _hajok.Add(new List<HajoEgyseg>());
                        _hajok[hajo_index].Add(new HajoEgyseg(seged[i], false));
                        hajo_index++;
                    }

                }
                else
                {
                    _hajok.Add(new List<HajoEgyseg>());
                    _hajok[hajo_index].Add(new HajoEgyseg(seged[i], false));
                    hajo_index++;
                }

            }
        }

        public int Lehelyezheto_Tippek_Szama()
        {
            int return_value = 0;

            if (_sikeres_tipp.X > 0)
            {
                return_value++;
            }
            if (_sikeres_tipp.X < _tabla_merete - 1)
            {
                return_value++;
            }
            if (_sikeres_tipp.Y > 0)
            {
                return_value++;
            }
            if (_sikeres_tipp.Y < _tabla_merete - 1)
            {
                return_value++;
            }

            foreach (Vector item in _al_rossz_tipp)
            {
                if (item.X == _sikeres_tipp.X - 1 && item.Y == _sikeres_tipp.Y)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X + 1 && item.Y == _sikeres_tipp.Y)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X && item.Y == _sikeres_tipp.Y - 1)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X && item.Y == _sikeres_tipp.Y + 1)
                {
                    return_value--;
                }
            }

            foreach (Vector item in _al_jo_tipp)
            {
                if (item.X == _sikeres_tipp.X - 1 && item.Y == _sikeres_tipp.Y)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X + 1 && item.Y == _sikeres_tipp.Y)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X && item.Y == _sikeres_tipp.Y - 1)
                {
                    return_value--;
                }
                else if (item.X == _sikeres_tipp.X && item.Y == _sikeres_tipp.Y + 1)
                {
                    return_value--;
                }
            }

            return return_value;
        }

        public void Elem_Talalt(Vector talalt)
        {
            for (int i = 0; i < _hajok.Count; i++)
            {
                for (int j = 0; j < _hajok[i].Count; j++)
                {
                    if (_hajok[i][j].vector == talalt)
                    {
                        _hajok[i][j].talalt = true;
                        Elsullyedt(i);
                        break;
                    }
                }
            }

        }

        private void Elsullyedt(int i)
        {
            int db = 0;
            for (int j = 0; j < _hajok[i].Count; j++)
            {
                if (_hajok[i][j].talalt == true)
                {
                    db++;
                }
            }
            if (_hajok[i].Count == db)
            {
                switch (db)
                {
                    case 2:
                        _hajo2++;
                        break;
                    case 3:
                        _hajo3++;
                        break;
                    case 4:
                        _hajo4++;
                        break;
                    case 5:
                        _hajo5++;
                        break;
                }
            }
        }
    }
}
