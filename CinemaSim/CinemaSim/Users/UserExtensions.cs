using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CinemaSim.Users
{
    public static class UserExtensions
    {
        private static readonly string Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Users.xml");
        private static readonly XmlRootAttribute Root = new XmlRootAttribute("ArrayOfUser");
        public static void SerializeToXml(this User user)
        {
            var serializer = new XmlSerializer(typeof(List<User>), Root);

            List<User> existingUsers;

            if (File.Exists(Path))
            {
                using (var fileStream = new FileStream(Path, FileMode.Open))
                {
                    existingUsers = (List<User>)serializer.Deserialize(fileStream)!;
                    existingUsers.Add(user);
                }
            }
            else
            {
                existingUsers = new List<User>();
            }

            using (var updatedFileStream = new FileStream(Path, FileMode.Create))
            {
                serializer.Serialize(updatedFileStream, existingUsers);
            }
        }

        public static User GetUserFromXml(string name)
        {
            var serializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("ArrayOfUser"));
            
            using (var streamReader = new StreamReader(Path))
            {
                var users = (List<User>)serializer.Deserialize(streamReader)!;
                return users.Find(u => u.Name == name)!;
            }
        }

        public static void UpdateUserInXml(this User updatedUser)
        {
            List<User> users;

            using (StreamReader streamReader = new StreamReader(Path))
            {
                var serializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("ArrayOfUser"));
                users = (List<User>)serializer.Deserialize(streamReader)!;
            }

            User existingUser = users.Find(u => u.Name == updatedUser.Name)!;

            if (existingUser != null)
            {
                users.Remove(existingUser);
                users.Add(updatedUser);

                using (StreamWriter streamWriter = new StreamWriter(Path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                    serializer.Serialize(streamWriter, users);
                }
            }
            else
            {
                Console.WriteLine($"User with name '{updatedUser.Name}' not found.");
            }
        }
    }
}