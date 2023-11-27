using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace PetStoreConsoleApp.Services
{
    public class PetService : IPetService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PetService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<List<Models.Pet>> GetAvailablePetsAsync()
        {
            try
            {
                // Make API request to get available pets
                var apiUrl = _configuration["PetStoreApiUrl"];
                var response = await _httpClient.GetStringAsync(apiUrl);

                if (!string.IsNullOrEmpty(response))
                {
                    return JsonConvert.DeserializeObject<List<Models.Pet>>(response) ?? new List<Models.Pet>();
                }
                else
                {
                    Console.WriteLine("Error: Empty response from PetStore API.");
                    return new List<Models.Pet>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Models.Pet>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Models.Pet>();
            }
        }
    }
}