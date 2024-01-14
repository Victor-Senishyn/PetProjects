using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CinemaSim
{
    public static class UserExtensions
    {
        private static readonly string Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Users.xml");

        public static void SerializeToXml(this User user)
        {
            var serializer = new XmlSerializer(typeof(List<User>));

            if (!File.Exists(Path))
            {
                using (var fileStream = new FileStream(Path, FileMode.Create))
                {
                    var root = new XmlRootAttribute("Users");
                    var rootSerializer = new XmlSerializer(typeof(List<User>), root);
                    rootSerializer.Serialize(fileStream, new List<User>());
                }
            }

            var existingUsers = new List<User>();
            using (var existingFileStream = new FileStream(Path, FileMode.Open))
            {
                var root = new XmlRootAttribute("Users");
                var rootSerializer = new XmlSerializer(typeof(List<User>), root);
                existingUsers = (List<User>)rootSerializer.Deserialize(existingFileStream);
            }

            existingUsers.Add(user);

            using (var updatedFileStream = new FileStream(Path, FileMode.Create))
            {
                var root = new XmlRootAttribute("Users");
                var rootSerializer = new XmlSerializer(typeof(List<User>), root);
                rootSerializer.Serialize(updatedFileStream, existingUsers);
                Console.WriteLine("Object has been serialized");
            }
        }

        public static User GetUserFromXml(string name)
        {
            using (StreamReader streamReader = new StreamReader(Path))
            {
                var root = new XmlRootAttribute("Users");
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>), root);
                List<User> users = (List<User>)serializer.Deserialize(streamReader);
                return users.Find(u => u.Name == name);
            }
        }

        public static void UpdateUserInXml(this User updatedUser)
        {
            List<User> users;

            using (StreamReader streamReader = new StreamReader(Path))
            {
                var root = new XmlRootAttribute("Users");
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>), root);
                users = (List<User>)serializer.Deserialize(streamReader);
            }

            User existingUser = users.Find(u => u.Name == updatedUser.Name);

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