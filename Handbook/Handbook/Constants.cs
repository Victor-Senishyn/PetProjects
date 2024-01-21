using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Handbook
{
    public static class Constants
    {
        public readonly static string LastAssignedIdFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Users.xml");
        public readonly static string PathToLastAssignedId = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "LastAssignedId.txt");
    }
}
