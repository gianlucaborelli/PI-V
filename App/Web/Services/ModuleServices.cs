using Shared.Models;
using Shared.Models.DTOs;
using Shared.Responses;
using System.Net.Http.Json;

namespace Web.Services
{
    public class ModuleServices(IHttpClientFactory httpClientFactory) : IModulesService
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        public async Task<Response<List<ModuleEspDto>>> LoadModulesAsync()
        {            
            var response = await _client.GetFromJsonAsync<Response<List<ModuleEspDto>>>($"api/modules");

            return response ?? new Response<List<ModuleEspDto>>(null, 400, "Não foi possível obter a categoria");
        }

        public async Task<Response<ModuleEspDto>> LoadModuleDetailAsync(int id)
        {
            var response = await _client.GetFromJsonAsync<Response<ModuleEspDto>>($"api/modules/{id}");

            return response ?? new Response<ModuleEspDto>(null, 400, "Não foi possível obter a categoria");
        }

        public async Task<Response<int>> CreateModuleAsync(CreateModuleRequest? request)
        {
            var response = await _client.PostAsJsonAsync($"api/modules", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<int>>();
                return result ?? new Response<int>(0, 500, "Erro ao ler a resposta do servidor");
            }
            else
            {
                return new Response<int>(0, (int)response.StatusCode, "Falha ao criar o módulo");
            }
        }

        public async Task<Response<int>> AddSensorAsync(AddSensorOnModuleRequest? request)
        {
            var response = await _client.PostAsJsonAsync($"api/modules/sensor", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<int>>();
                return result ?? new Response<int>(0, 500, "Erro ao ler a resposta do servidor");
            }
            else
            {
                return new Response<int>(0, (int)response.StatusCode, "Falha ao criar o módulo");
            }
        }

        public async Task<Response<int>> UpdateModule(UpdateModuleRequest? request)
        {
            var response = await _client.PutAsJsonAsync($"api/modules", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<int>>();
                return result ?? new Response<int>(0, 500, "Erro ao ler a resposta do servidor");
            }
            else
            {
                return new Response<int>(0, (int)response.StatusCode, "Falha ao criar o módulo");
            }
        }

        public async Task<Response<int>> UpdateSensorOnModule(UpdateSensorOnModuleRequest? request)
        {
            var response = await _client.PostAsJsonAsync($"api/modules/sensor", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<int>>();
                return result ?? new Response<int>(0, 500, "Erro ao ler a resposta do servidor");
            }
            else
            {
                return new Response<int>(0, (int)response.StatusCode, "Falha ao criar o módulo");
            }
        }

        public async Task<Response<bool>> DeleteModuleAsync(int request)
        {
            var response = await _client.DeleteAsync($"api/modules/{request}" );

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result ?? new Response<bool>(result!.Data, 500, "Erro ao ler a resposta do servidor");
            }
            else
            {
                return new Response<bool>(false, (int)response.StatusCode, "Falha ao deletar módulo");
            }
        }

        public async Task<Response<bool>> DeleteSensorAsync(int request)
        {
            var response = await _client.DeleteAsync($"api/modules/sensor/{request}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                return result ?? new Response<bool>(result!.Data, 500, "Erro ao ler a resposta do servidor");
            }
            else
            {
                return new Response<bool>(false, (int)response.StatusCode, "Falha ao deletar sensor");
            }
        }


    }
}
