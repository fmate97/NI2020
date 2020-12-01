using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NI_torpedo.Model
{
    public class ShipUnit
    {
        public Vector vector { get; set; }
        public bool hit { get; set; }
        

        public ShipUnit()
        {
        }

        public ShipUnit(Vector vector, bool hit)
        {
            this.vector = vector;
            this.hit = hit;
        }
    }
}
