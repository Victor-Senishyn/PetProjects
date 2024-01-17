using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaSim.Users;

namespace CinemaSim.Authorization
{
    public interface IAuthorizationCommand
    {
        public User Execute(string name);
    }
}
