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
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("ArrayOfUsers"));

            using (FileStream fileStream = new FileStream(Constants.UsersXmlPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                users = (List<User>)serializer.Deserialize(fileStream);
                user.Id = (users.Count + 1);
                users.Add(user);
                serializer.Serialize(fileStream, users);
            }
        }
    }

}
