using Handbook;

public class Program
{
    static void Main(string[] args)
    {
        IdGenerator.LoadLastAssignedIndex();

        HandbookUI.Start();        

        IdGenerator.SaveLastAssignedId();
    }
}
