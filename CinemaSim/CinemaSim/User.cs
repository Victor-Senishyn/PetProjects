using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CinemaSim
{
    [XmlRoot("User")]
    public class User
    {
        [XmlElement("Ticket")]
        public List<Ticket> Tickets { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Balance")]
        public decimal Balance { get; set; }

        public User(){}
        public User(string name, decimal balance) => (Name, Balance, Tickets) = (name, balance, new List<Ticket>());
        
        private bool IsBalanceEnough(decimal money) => Balance >= money;

        public void ReplenishBalance(decimal money) => Balance += money;

        public void WithdrawBalance(decimal money)
        {
            if(IsBalanceEnough(money))
                Balance -= money;
            else
                throw new InsufficientExecutionStackException();
        }

        public override string ToString()
        {
            var result = new StringBuilder($"{Name,-12} Balance: {Balance}\nTickets:\n");

            foreach (var ticket in Tickets)
                result.Append($"{ticket}");

            return result.ToString();
        }
    }
}
