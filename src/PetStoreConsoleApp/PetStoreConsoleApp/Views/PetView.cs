using System;
using System.Collections.Generic;
using System.Linq;


namespace PetStoreConsoleApp.Views
{
    public class PetView
    {
        public static void PrintPets(List<Models.Pet> pets, Func<IEnumerable<Models.Pet>, IEnumerable<Models.Pet>> sortStrategy)
        {
            if (pets == null)
            {
                Console.WriteLine("No pets available.");
                return;
            }

            var sortedPets = sortStrategy(pets);

            Console.WriteLine("************************");
            Console.WriteLine("Print Pets Details");
            Console.WriteLine("************************");
            foreach (var pet in sortedPets)
            {
                Console.WriteLine($"ID: {pet.Id}, Name: {pet.Name}, Category: {pet.Category?.Name ?? "Uncategorized"}");
            }
            Console.WriteLine("**********************************************************************************");
        }
    }
}

