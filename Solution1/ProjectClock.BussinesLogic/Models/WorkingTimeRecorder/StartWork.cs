﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Models
{
    public class StartWork
    {
        public DateTime TimeStart { get; set; }       
        public int ProjectID { get; set; }
        public int UserID {  get; set; }
        
    }
}