using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaSim.Users;

namespace CinemaSim.Authorization
{
    public class SignInCommand : IAuthorizationCommand
    {
        public User Execute(string name) 
            => UserExtensions.GetUserFromXml(name) == null 
            ? throw new NullReferenceException() : UserExtensions.GetUserFromXml(name)!;
        
    }
}
