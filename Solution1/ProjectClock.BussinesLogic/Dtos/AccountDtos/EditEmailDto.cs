using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.AccountDtos
{
    public class EditEmailDto
    {
        public int Id { get; set; }
        public string NewEmail { get; set; }
        public string CurrentEmail { get; set; }
    }
}
