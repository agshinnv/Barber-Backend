﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WorkTime : BaseEntity
    {
        public string WorkDay { get; set; }
        public string WorkHour { get; set; }
        public bool Closed { get; set; } = false;
    }
}