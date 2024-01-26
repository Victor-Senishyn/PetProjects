namespace ChocoFactory;

public class ChocoFactoryManufacture
{
    private List<Ingredient> _ingredients;
    private SemaphoreSlim _lock = new SemaphoreSlim( 1, 1 ); 

    public ChocoFactoryManufacture(params Ingredient[] ingredients) => _ingredients = ingredients.ToList();
    
    public async Task<Product> CreateProductAsync(string productName, string ingredientName, int amount)
    {
        await _lock.WaitAsync();
        try
        {
            for (int i = 0; i < _ingredients.Count; i++)
            {
                var item = _ingredients[i];
                if (item.Name == ingredientName && item.Amount >= amount)
                {
                    item.Amount -= amount;
                    return new Product (productName, amount);
                }
            }
            
            throw new ArgumentException("Ingredient not found or insufficient");
        }
        finally
        {
            _lock.Release();
        }
    }
    
    public async Task PutIngredientsAsync(Ingredient ingredient)
    {
        await _lock.WaitAsync();
        try
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
        finally
        {
            _lock.Release();
        }
    }
}