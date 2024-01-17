using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaSim.Users;

namespace CinemaSim.Authorization
{
    public static class AuthorizationCommandFactory
    {
        public static IAuthorizationCommand BuildAuthorization(int command)
            => command switch
            {
                1 => new SignUpCommand(),
                2 => new SignInCommand(),
                3 => null,
                _ => throw new NotImplementedException()
            };

        
    }
}
