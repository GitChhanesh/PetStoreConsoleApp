namespace PetStoreConsoleApp.Services
{
    public interface IPetService
    {
        Task<List<Models.Pet>> GetAvailablePetsAsync();

    }
}
