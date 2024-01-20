using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handbook
{
    public class HandbookUI
    {
        public static void Start()
        {
            while (true)
            {
                Console.WriteLine("Press any button to clear console");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("Enter number of your choice:");
                Console.WriteLine("1. Show a selection of items");
                Console.WriteLine("2. Get user from file by ID");
                Console.WriteLine("3. Serialization of the user to file");
                Console.WriteLine("4. Exit");

                switch (Console.ReadLine())
                {
                    case ("1"):
                        Console.WriteLine("Enter start index");
                        if(long.TryParse(Console.ReadLine(),out var startIndex)) 
                        {
                            Console.WriteLine("Enter the number of items you want to see");
                            if (long.TryParse(Console.ReadLine(), out var count))
                                Deserializer.ReadDataFromXml(startIndex, count);
                            else 
                                Console.WriteLine("Wrong type for count");
                        }
                        else
                            Console.WriteLine("Wrong type for start index");
                        break;

                    case ("2"):
                        Console.WriteLine("Enter user ID");
                        try
                        {
                            Console.WriteLine(Deserializer.GetUserFromXmlById(Console.ReadLine()));
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    
                    case ("3"):
                        Console.WriteLine("Enter name");
                        string name = Console.ReadLine()!;
                        Console.WriteLine("Enter email");
                        string email = Console.ReadLine()!;
                        Console.WriteLine("Enter phone number");
                        string phoneNumber = Console.ReadLine()!;
                        var user = new User(name, email, phoneNumber);
                        Serializer.SerializeUserToXml(user);
                        Console.WriteLine($"User serialized his Id: {user.Id}");
                        break;

                    case ("4"):
                        return;

                    default: 
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
        }
    }
}
