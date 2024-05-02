using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace ip_geolocate
{

    public class Info
    {
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string org { get; set; }
        public string timezone { get; set; }
        public string ip { get; set; }
        public string loc { get; set; }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Internet Protocol Information Provider";
            Console.Write("Enter IP Address: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("Request Successfully Made");

                    string responseData = await response.Content.ReadAsStringAsync();
                    Info Data = JsonConvert.DeserializeObject<Info>(responseData);

                    Console.Clear();
                    Console.WriteLine($"Country: {Data.country}");
                    Console.WriteLine($"City: {Data.city}");
                    Console.WriteLine($"Region: {Data.region}");
                    Console.WriteLine($"Location: {Data.loc}");
                    Console.WriteLine($"Organization: {Data.org}");
                    Console.WriteLine($"Time Zone: {Data.timezone}");
                    Console.WriteLine($"IP Address: {Data.ip}");
                    string[] Coordinates = Data.loc.Split(',');
                    Console.WriteLine($"Google Maps Link: https://www.google.com/maps/?q={Coordinates[0]},{Coordinates[1]}");

                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Issue: {ex.Message}");
                }
            }
        }
    }
}
