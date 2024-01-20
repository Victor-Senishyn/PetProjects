using Handbook;

public class Program
{
    static void Main(string[] args)
    {
        User.LoadLastAssignedIndex();

        HandbookUI.Start();        

        User.SaveLastAssignedId();
    }
}
