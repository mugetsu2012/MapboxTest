using System;
using System.Collections.Generic;
using System.Text;

namespace MapboxTest
{
    /// <summary>
    /// Representa una etapa de la ruta
    /// </summary>
    public class Leg
    {
        public string Summary { get; set; }
        public double Weight { get; set; }
        public double Duration { get; set; }
        public List<object> Steps { get; set; }
        public double Distance { get; set; }
    }
}
