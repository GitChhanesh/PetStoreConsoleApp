namespace PetStoreConsoleApp.Controllers
{
    public class PetController
    {
        private readonly Services.IPetService _petService;

        public PetController(Services.IPetService petService)
        {
            _petService = petService ?? throw new ArgumentNullException(nameof(petService));
        }

        public async Task<List<Models.Pet>> GetAvailablePets()
        {
            return await _petService.GetAvailablePetsAsync();
        }
    }
}
