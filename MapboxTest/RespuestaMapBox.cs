using System;
using System.Collections.Generic;
using System.Text;

namespace MapboxTest
{
    /// <summary>
    /// Objeto contendor de la respuesta de MapBox
    /// </summary>
    public class RespuestaMapBox
    {
        /// <summary>
        /// Lista de rutas posibles
        /// </summary>
        public List<Route> Routes { get; set; }

        /// <summary>
        /// Waypoints que definen las rutas
        /// </summary>
        public List<Waypoint> Waypoints { get; set; }

        /// <summary>
        /// Status Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// UUID de la respuesta
        /// </summary>
        public string Uuid { get; set; }
    }
}
