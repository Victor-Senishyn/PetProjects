using Handbook;
using System.Text.RegularExpressions;

public class Program
{
    static void Main(string[] args)
    {
        Regex reg = new Regex(@"^(?:\+380|380)\d{9}$");
        //Console.WriteLine(Searcher.GetUserFromXmlById(100));
        
        foreach(var item in Searcher.GetUserFromXmlById(reg))
        {
            Console.WriteLine(item);
        }
        HandbookUI.Start();
    }
}