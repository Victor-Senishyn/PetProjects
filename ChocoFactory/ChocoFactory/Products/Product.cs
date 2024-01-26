namespace ChocoFactory;

public class Product
{
    public string Name { get; set; }
    public decimal Amount { get; set; }

    public Product(string name, decimal amount) => (Name, Amount) = (name, amount);
    
    public override string ToString() => $"Product's name {Name}, Count {Amount} ";
}