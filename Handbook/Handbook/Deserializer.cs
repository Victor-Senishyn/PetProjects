using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Handbook
{
    public static class Deserializer
    {
        public static void ReadDataFromXml(long startIndex, long count)
        {
            using (XmlReader reader = XmlReader.Create(Constants.PathToUsersXml))
            {
                reader.ReadToFollowing("Id");

                while (reader.Read())
                {
                    if (reader.Value == startIndex.ToString())
                    {
                        Console.WriteLine($"Id: {startIndex}");
                        reader.ReadToFollowing("Name");
                        reader.Read();
                        Console.Write($"Name: {reader.Value}");
                        reader.ReadToFollowing("Email");
                        reader.Read();
                        Console.Write($"Email: {reader.Value}");
                        reader.ReadToFollowing("PhoneNumber");
                        reader.Read();
                        Console.WriteLine($"Phone number: {reader.Value}");
                        reader.ReadToFollowing("Id");
                        if (--count == 0)
                            return;
                        startIndex++;
                    }
                }
            }
        }
        public static User GetUserFromXmlById(string id)
        {
            using (XmlReader reader = XmlReader.Create(Constants.PathToUsersXml))
            {
                reader.ReadToFollowing("Id");

                while (reader.Read())
                {
                    if (reader.Value == id)
                    {
                        string name, email, number;

                        reader.ReadToFollowing("Name");
                        reader.Read();
                        name = reader.Value;
                        reader.ReadToFollowing("Email");
                        reader.Read();
                        email = reader.Value;
                        reader.ReadToFollowing("PhoneNumber");
                        reader.Read();
                        number = reader.Value;

                        return new User(long.Parse(id), name, email, number);
                    }
                }
            }
            throw new ArgumentException("Wrong Id");
        }
    }
}
