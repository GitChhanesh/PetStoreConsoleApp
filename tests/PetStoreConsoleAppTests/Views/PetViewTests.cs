using PetStoreConsoleApp.Models;
using PetStoreConsoleApp.Views;
using Xunit;
using Assert = Xunit.Assert;

namespace PetStoreConsoleApp.UnitTests
{
    public class PetViewTests
    {
        [Fact]
        public void PrintPets_ShouldPrintPetsInCategoryAndReverseOrderByName()
        {
            // Arrange
            var petView = new PetView();
            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Dog", Category = new Category { Id = 1, Name = "Mammal" } },
                new Pet { Id = 2, Name = "Cat", Category = new Category { Id = 1, Name = "Mammal" } },
                new Pet { Id = 3, Name = "Parrot", Category = new Category { Id = 2, Name = "Bird" } }
                // Add more pets as needed
            };

            // Act
            var printedOutput = CaptureConsoleOutput(() => PetView.PrintPets(pets, p => p.OrderBy(pet => pet.Category?.Name).ThenByDescending(pet => pet.Name)));

            // Assert
            Assert.Contains("ID: 3, Name: Parrot, Category: Bird", printedOutput);
            Assert.Contains("ID: 1, Name: Dog, Category: Mammal", printedOutput);
            Assert.Contains("ID: 2, Name: Cat, Category: Mammal", printedOutput);
        }

        [Fact]
        public void PrintPets_ShouldHandleNullPets()
        {
            // Arrange
            var petView = new PetView();

            // Act
            var printedOutput = CaptureConsoleOutput(() => PetView.PrintPets(null!, p => p.OrderBy(pet => pet.Category?.Name).ThenByDescending(pet => pet.Name)));

            // Assert
            Assert.Contains("No pets available.", printedOutput);
        }

        [Fact]
        public void PrintPets_ShouldHandleException()
        {
            // Arrange
            var petView = new PetView();
            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Dog", Category = new Category { Id = 1, Name = "Mammal" } },
                new Pet { Id = 2, Name = "Cat", Category = new Category { Id = 1, Name = "Mammal" } },
                new Pet { Id = 3, Name = "Parrot", Category = new Category { Id = 2, Name = "Bird" } }
                // Add more pets as needed
            };

            // Mocking Console.Out to simulate an exception during printing
            using var consoleOutput = new ConsoleOutput();
            Console.SetOut(new StringWriter());

            // Act, Assert
            var exception = Assert.Throws<Exception>(() =>
            {
                PetView.PrintPets(pets, p => throw new Exception("Simulated error during printing"));
            });

            Assert.Equal("Simulated error during printing", exception.Message);
        }

        private static string CaptureConsoleOutput(Action action)
        {
            using var consoleOutput = new ConsoleOutput();
            action.Invoke();
            return consoleOutput.GetOutput();
        }

        private class ConsoleOutput : IDisposable
        {
            private readonly StringWriter stringWriter;
            private readonly TextWriter originalOutput;

            public ConsoleOutput()
            {
                stringWriter = new StringWriter();
                originalOutput = Console.Out;
                Console.SetOut(stringWriter);
            }

            public string GetOutput()
            {
                return stringWriter.ToString();
            }

            public void Dispose()
            {
                stringWriter.Dispose();
                Console.SetOut(originalOutput);
            }
        }
    }
}
