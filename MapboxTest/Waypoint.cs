using System;
using System.Collections.Generic;
using System.Text;

namespace MapboxTest
{
    public class Waypoint
    {
        public double Distance { get; set; }
        public string Name { get; set; }
        public List<double> Location { get; set; }
    }
}
