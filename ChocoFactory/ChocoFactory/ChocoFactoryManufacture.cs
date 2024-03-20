using ChocoFactory.CustomException;
using ChocoFactory.Models;
using System.Collections.Concurrent;

namespace ChocoFactory;

public class ChocoFactoryManufacture
{
    private ConcurrentBag<Ingredient> _ingredients;

    public ChocoFactoryManufacture(params Ingredient[] ingredients) => _ingredients = new ConcurrentBag<Ingredient>(ingredients);
    
    public async Task<Product> CreateProductAsync(string productName, string ingredientName, int amount)
    {
        for (int i = 0; i < _ingredients.Count; i++)
        {
            var item = _ingredients.ElementAt(i);
            if (item.Name == ingredientName && item.Amount >= amount)
            {
                item.Amount -= amount;
                return new Product (productName, amount);
            }
        }
            
        throw new InsufficientIngredientsException("Ingredient not found or insufficient");
    }
    
    public async Task PutIngredientsAsync(Ingredient ingredient)
    {
        foreach (var item in _ingredients)
        {
            if (item.Name == ingredient.Name)
            {
                item.Amount += ingredient.Amount;
                return;
            }
        }

        _ingredients.Add(ingredient);
    }
}