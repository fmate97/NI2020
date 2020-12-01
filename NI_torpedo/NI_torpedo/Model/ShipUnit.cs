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

        public ShipUnit()
        {
        }

        public ShipUnit(Vector vector, bool talalt)
        {
            this.vector = vector;
            this.hit = talalt;
        }
    }
}
