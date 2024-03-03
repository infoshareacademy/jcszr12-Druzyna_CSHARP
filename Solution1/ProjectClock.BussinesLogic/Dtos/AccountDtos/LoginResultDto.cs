using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.AccountDtos
{
    public class LoginResultDto
    {

        public bool LoginWasSuccessful { get; set; }

        public int? UserId { get; set; }

        public AuthenticationProperties? AuthProp { get; set; }

        public ClaimsIdentity? ClaimsIdentity { get; set; }
    }
}
