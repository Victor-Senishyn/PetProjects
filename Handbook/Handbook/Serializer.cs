using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Handbook
{
    public static class Serializer
    {
        public static void SerializeUserToXml(User user)
        {
            List<User> users = new List<User>();
            User.IsDeserialized = true;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("ArrayOfUsers"));

                using (FileStream fileStream = new FileStream(Constants.PathToUsersXml, FileMode.Open))
                {
                    users = (List<User>)serializer.Deserialize(fileStream);
                    users.Add(user);
                }
                using (FileStream fileStream = new FileStream(Constants.PathToUsersXml, FileMode.Create))
                {
                    serializer.Serialize(fileStream, users);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deserialization: {ex.StackTrace}");
            }
            User.IsDeserialized = false;
        }
    }

}
