using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
