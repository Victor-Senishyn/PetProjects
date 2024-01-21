using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
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
        private static User CreateUserFromFile(XmlReader reader, long id)
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

            return new User(id, name, email, number);
        }

        public static IEnumerable<User> ReadDataFromXml(long startIndex, long count)
        {
            var users = new List<User>();
            using (XmlReader reader = XmlReader.Create(Constants.LastAssignedIdFilePath))
            {
                reader.ReadToFollowing("Id");

                while (reader.Read())
                {
                    if (reader.Value == startIndex.ToString())
                    {
                        users.Add(CreateUserFromFile(reader, startIndex));
                        if (--count == 0)
                            break;
                        startIndex++;
                    }
                }
            }
            return users;
        }
        public static User GetUserFromXmlById(string id)
        {
            using (XmlReader reader = XmlReader.Create(Constants.LastAssignedIdFilePath))
            {
                reader.ReadToFollowing("Id");

                while (reader.Read())
                {
                    if (reader.Value == id)
                        return CreateUserFromFile(reader,long.Parse(id));
                }
            }
            throw new ArgumentException("Wrong Id");
        }
    }
}
