using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.AccountDtos
{
    public class DeleteAccountDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }

    }
}
