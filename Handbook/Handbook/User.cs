using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Handbook
{
    [XmlRoot("User")]
    public class User
    {
        [XmlIgnore]
        private static long _lastAssignedId = 0;
        [XmlElement("Id")]
        public long Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        public static bool IsDeserialized = false;
        public User() 
        { 
            if (!IsDeserialized)
                Id = _lastAssignedId++;
            Name = "Unknown";
            Email = "Unknown";
            PhoneNumber = "Unknown";
        }

        public User(long id, string name, string email, string phoneNumber)
            =>(Id,Name, Email,PhoneNumber) = (Id, name, email, phoneNumber);

        public User(string name, string email, string phoneNumber)
        {
            if (!IsDeserialized)
                Id = _lastAssignedId++;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static void LoadLastAssignedIndex()
        {
            try
            {
                if (File.Exists(Constants.PathToLastAssignedId))
                {
                    string content = File.ReadAllText(Constants.PathToLastAssignedId);
                    _lastAssignedId = long.Parse(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading last assigned ID: " + ex.Message);
            }
        }
        public static void SaveLastAssignedId()
        {
            try
            {
                File.WriteAllText(Constants.PathToLastAssignedId, _lastAssignedId.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving last assigned ID: " + ex.Message);
            }
        }
        public override string ToString() => $"ID: {Id}\tName: {Name}\tEmail: {Email}\tPhone Number: {PhoneNumber}";
    }
}
