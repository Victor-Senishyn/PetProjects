﻿using System;
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
        [XmlElement("Id")]
        public long Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        public User(){}

        public User(string name, string email, string phoneNumber)
            => (Name, Email, PhoneNumber) = (name, email, phoneNumber);

        public User(long id, string name, string email, string phoneNumber): this(name, email, phoneNumber)
            => (Id) = (id);

        public override string ToString() => $"ID: {Id}\tName: {Name}\tEmail: {Email}\tPhone Number: {PhoneNumber}";
    }
}
