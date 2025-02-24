using System.Net.Http.Json;
using Shared.Responses;
using Shared.Models;

namespace Web.Services
{
    public class SensorsService(IHttpClientFactory httpClientFactory) : ISensorsService
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        public async Task<Response<List<SensorData>>> LoadSensorsDataAsync(DateTime? request, int sensorID)
        {
            string formattedDate = request?.ToString("yyyy-MM-ddTHH:mm:ss");
            var response = await _client.GetFromJsonAsync<Response<List<SensorData>>>($"api/Sensor?date={formattedDate}&sensorId={sensorID}");

            return response ?? new Response<List<SensorData>>(null, 400, "Não foi possível obter a categoria");
        }

        public async Task<Response<List<SensorData>>> LoadSensorsDataByDayAndModuleIdAsync(DateTime? request, int moduleId)
        {
            string formattedDate = request?.ToString("yyyy-MM-ddTHH:mm:ss");
            var response = await _client.GetFromJsonAsync<Response<List<SensorData>>>($"api/Sensor/module?date={formattedDate}&moduleId={moduleId}");

            return response ?? new Response<List<SensorData>>(null, 400, "Não foi possível obter a categoria");
        }
    }
}
