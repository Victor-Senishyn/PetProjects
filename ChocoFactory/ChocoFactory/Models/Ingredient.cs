namespace ChocoFactory.Models;

public class Ingredient
{
    public string Name { get; }
    public int Amount { get; set; }

    public Ingredient(string name, int amount) => (Name, Amount) = (name, amount);

    public override string ToString() => $"Ingredient's name {Name}, Count {Amount} ";
}
