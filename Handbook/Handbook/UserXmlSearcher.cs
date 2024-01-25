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

        private static IEnumerable<IEnumerable<User>> ReadBatches(XmlReader reader)
        {
            long startIndex = 0;

            while (true)
            {
                reader = XmlReader.Create(Constants.UsersXmlPath);
                var users = CreateBatch(startIndex, reader);

                if (users.Count() == 0)
                    yield break;

                yield return users;
                startIndex += 100;
            }
        }

        public static User GetUserFromXmlById(long id)
        {
            using (XmlReader reader = XmlReader.Create(Constants.UsersXmlPath))
            {
                var batches = ReadBatches(reader);

                foreach (var users in batches)
                {
                    var userResult = users.FirstOrDefault(user => user.Id == id);

                    if (userResult != null)
                        return userResult;
                }
            }
            throw new ArgumentException("No user found by id");
        }

        public static IEnumerable<User> GetUsersByNumberCode(string numberCode)
        {
            Regex reg = new Regex($@"^(?:\+{Regex.Escape(numberCode)}|{Regex.Escape(numberCode)})\d+$");

            using (XmlReader reader = XmlReader.Create(Constants.UsersXmlPath))
            {
                var batches = ReadBatches(reader);

                return batches.SelectMany(users => users.Where(user => reg.IsMatch(user.PhoneNumber)));
            }
        }

        public static long GetCountOfUsersFromCountry(string country)
        {
            using (XmlReader reader = XmlReader.Create(Constants.UsersXmlPath))
            {
                var batches = ReadBatches(reader);

                return batches.Sum(usersBatch => usersBatch.Count(user => user.Country == country));
            }
        }
    }
}
