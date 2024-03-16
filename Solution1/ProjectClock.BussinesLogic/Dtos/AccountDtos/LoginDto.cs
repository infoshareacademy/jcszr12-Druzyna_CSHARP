﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.AccountDtos
{
    public class LoginDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool LoginWasSuccessful { get; set; }
    }
}
