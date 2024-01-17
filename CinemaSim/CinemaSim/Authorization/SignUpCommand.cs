using CinemaSim.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaSim.Users;

namespace CinemaSim.Authorization
{
    public class SignUpCommand : IAuthorizationCommand
    {
        public User Execute(string name)
        {
            var user = new User(name, 0);
            user.SerializeToXml();
            return user;
        }
    }
}
