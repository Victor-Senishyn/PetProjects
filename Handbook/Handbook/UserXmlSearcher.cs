using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Handbook
{
    public class UserXmlSearcher
    {
        private static IEnumerable<User> CreateBatch(long startIndex, XmlReader reader)
        {
            List<User> users = new List<User> { };
            
            reader.ReadToFollowing("User");

            while (users.Count != 100 && !reader.EOF)
            {
                reader.Read();
                if (reader.Value == startIndex.ToString())
                    users.Add(Deserializer.CreateUserFromFile(reader, startIndex++));
            }
                
            return users;
        }

        public static User GetUserFromXmlById(long id)
        {
            long startIndex = 0;
            User userResult = null;

            using (XmlReader reader = XmlReader.Create(Constants.UsersXmlPath))
            {
                while (true)
                {
                    var users = CreateBatch(startIndex, reader).ToArray();

                    if (users.Length == 0)
                        break;

                    userResult = users.FirstOrDefault(user => user.Id == id);
                    startIndex += 100;

                    if (userResult != null)
                        return userResult;
                }
            }
            throw new ArgumentException("No user found by id");
        }

        public static IEnumerable<User> GetUsersByNumberCode(string numberCode)
        {
            long startIndex = 0;
            List<User> usersResult = new List<User> { };
            Regex reg = new Regex($@"^(?:\+{Regex.Escape(numberCode)}|{Regex.Escape(numberCode)})\d+$");

            using (XmlReader reader = XmlReader.Create(Constants.UsersXmlPath))
            {
                while (true)
                {
                    var users = CreateBatch(startIndex, reader).ToArray();

                    if (users.Length == 0)
                        break;

                    usersResult.AddRange(users.Where(user => reg.IsMatch(user.PhoneNumber)));
                    startIndex += 100;
                }
            }
            return usersResult;
        }

        public static long GetCountOfUsersFromCountry(string country)
        {
            long startIndex = 0;
            long count = 0;

            using (XmlReader reader = XmlReader.Create(Constants.UsersXmlPath))
            {
                while (true)
                {
                    var users = CreateBatch(startIndex, reader).ToArray();

                    if (users.Length == 0)
                        break;

                    count += (users.Where(user => user.Country == country)).Count();
                    startIndex += 100;
                }
            }
            return count;
        }
    }
}
