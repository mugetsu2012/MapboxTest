using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MapboxTest
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            //Settings para leer el archivo de confiuracion
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", false)
                .Build();

            //Primero creamos un cliente
            HttpClient cliente = new HttpClient();

            //Obtenemos el objeto de configuracion
            List<Dictionary<string, object>> t = configuration.GetSection("MapboxConfig").GetChildren().Select(p =>
                new Dictionary<string, object>()
                {
                    {p.Key, p.Value}
                }).ToList();


            string json = ListToJsonString(t);

            MapBoxConfigObject mapBoxConfig = JsonConvert.DeserializeObject<MapBoxConfigObject>(json);

            Console.WriteLine("Ingrese la latitud inicial: ");
            decimal latitudInicial = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la longitud inicial: ");
            decimal longitudInicial = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la latitud final: ");
            decimal latitudFinal = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la longitud final: ");
            decimal longitudFinal = decimal.Parse(Console.ReadLine());



            //Procedemos a realizar pruebas, preparamos la URL template
            string urlGet = GetUrlPeticion(latitudInicial, longitudInicial, latitudFinal, longitudFinal, mapBoxConfig);

            //Ahora hacemos la peticion GET
            HttpResponseMessage responseMessage = await cliente.GetAsync(urlGet);
            string content = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode == false)
            {
               Console.WriteLine(
                    $"Lo siento, la peticion fallo. StatusCode: {responseMessage.StatusCode}, Message: {content}");
            }

            //En este punto, la peticion salio bien, procedemos a deserializar el objeto
            RespuestaMapBox respuestaMapBox = JsonConvert.DeserializeObject<RespuestaMapBox>(content);

            double distanciaKms = Math.Round(respuestaMapBox.Routes.First().Distance / 1000, 2);
            double tiempoMinutos = Math.Round(respuestaMapBox.Routes.First().Duration / 60, 2);

            Console.WriteLine($"La distancia entre los puntos es de: {distanciaKms} kms, tomaria llegar {tiempoMinutos} minutos en vehiculo tomando en cuenta el trafico");

            Console.ReadLine();
        }

        private static string ListToJsonString(List<Dictionary<string, object>> listado)
        {
            string json = "{";

            for (int i = 0; i < listado.Count; i++)
            {
                json += " " +"\""+listado[i].Keys.First() +"\"" + ":" + "\""+listado[i].Values.First() + "\"";

                if (i < listado.Count - 1)
                {
                    json += ", ";
                }
            }

            json += "}";

            return json;
        }

        private static string GetUrlPeticion(decimal latitudInicial, decimal longitudInicial,decimal latitudFinal, decimal longitudFinal, MapBoxConfigObject configObject)
        {
            string urlGet =
                $"{configObject.UrlBase}/{configObject.RoutingProfile}/{longitudInicial},{latitudInicial};{longitudFinal},{latitudFinal}" +
                $"?alternatives={configObject.UseAlternatives.ToString().ToLower()}" +
                $"&geometries={configObject.Geometries}" +
                $"&steps={configObject.UseSteps.ToString().ToLower()}" +
                $"&access_token={configObject.AccessToken}";

            return urlGet;
        }
    }
}
