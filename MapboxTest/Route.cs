using System;
using System.Collections.Generic;
using System.Text;

namespace MapboxTest
{
    public class Route
    {
        public string Geometry { get; set; }
        public List<Leg> Legs { get; set; }
        public string WeightName { get; set; }
        public double Weight { get; set; }
        public double Duration { get; set; }
        public double Distance { get; set; }
    }
}
