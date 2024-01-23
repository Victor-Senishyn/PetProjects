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
    public class Searcher
    {
        private static IEnumerable<IGrouping<int, User>> GetGroupedUsers()
        {
            XDocument xmlDoc = XDocument.Load(Constants.UsersXmlPath);
            var users = xmlDoc.Descendants("User");

            return users.Select((user, index)
                => new User(
                    long.Parse(user.Element("Id")!.Value), 
                    user.Element("Name")!.Value, 
                    user.Element("Email")!.Value, 
                    user.Element("PhoneNumber")!.Value,
                    user.Element("Country")!.Value))
                .Select((user, index) => new { User = user, GroupIndex = index / 100 })
                .GroupBy(x => x.GroupIndex, y => y.User);
        }

        public static User GetUserFromXmlById(long id)
        {
            var groupedUsers = GetGroupedUsers();

            foreach (var group in groupedUsers)
            {
                var userArray = group.ToArray();
                var user = userArray.FirstOrDefault(user => user.Id == id);

                if(user != null)
                    return user;

                Array.Clear(userArray, 0, userArray.Length);
            }

            throw new ArgumentException("No user found by id");
        }

        public static IEnumerable<User> GetUsersByNumberCode(string numberCode)
        {
            Regex reg = new Regex($@"^(?:\+{Regex.Escape(numberCode)}|{Regex.Escape(numberCode)})\d+$");
            var groupedUsers = GetGroupedUsers();
            List<User> users = new List<User>();

            foreach (var group in groupedUsers)
            {
                var userArray = group.ToArray();
                users.AddRange(userArray.Where(user => reg.IsMatch(user.PhoneNumber)));
            }

            return users;
        }

        public static long GetCountOfUsersFromCountry(string country)
        {
            var groupedUsers = GetGroupedUsers();
            long count = 0;

            foreach (var group in groupedUsers)
            {
                var userArray = group.ToArray();
                var users = userArray.Where(user => user.Country == country);

                count += users.Count();

                Array.Clear(userArray, 0, userArray.Length);
            }

            return count;
        }
    }
}
