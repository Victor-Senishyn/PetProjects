using System.Threading.Channels;
using ChocoFactory;
using ChocoFactory.CustomException;
using ChocoFactory.Models;

public static class Program
{
    private static async Task Main()
    {
        var factory = new ChocoFactoryManufacture();
        var random = new Random();
        var tasks = new List<Task>();
        
        for (int i = 0; i < 100; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                var ingredient = new Ingredient("Chocolate",random.Next(14)+1);
                await factory.PutIngredientsAsync(ingredient);
                Console.WriteLine(ingredient);
            }));

            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    var product = await factory.CreateProductAsync("ChocolateBar", "Chocolate", random.Next(9)+1);
                    Console.WriteLine(product);
                }
                catch (InsufficientIngredientsException ex)
                {
                    Console.WriteLine($"Error creating product: {ex.Message}");
                }
            }));
        }
        await Task.WhenAll(tasks);
    }
}

