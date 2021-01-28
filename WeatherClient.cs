using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace HelloDotnet5
{
    public class WeatherClient
    {
        private readonly HttpClient HttpClient;
        private readonly ServiceSettings Settings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            HttpClient = httpClient;
            Settings = options.Value;
        }

        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] weather, Main main, long dt);

        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            var forecast =
                await HttpClient.GetFromJsonAsync<Forecast>(
                    $"https://{Settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={Settings.ApiKey}&units=metric");
            return forecast;
        }
    }
}