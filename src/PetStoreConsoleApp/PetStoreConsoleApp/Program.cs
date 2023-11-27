using PetStoreConsoleApp.Services;
using PetStoreConsoleApp.Controllers;
using PetStoreConsoleApp.Views;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main()
    {
        try
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();


            var httpClient = new HttpClient();
            var petService = new PetService(httpClient, configuration);
            var petController = new PetController(petService);
            var petView = new PetView();

            var availablePets = await petController.GetAvailablePets();

            if (availablePets != null)
            {
                // Print available pets sorted by category and in reverse order by name
                PetView.PrintPets(availablePets, pets => pets.OrderByDescending(p => p.Name));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }


    }
}
