using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Appointment : BaseEntity
    {
        public string Title { get; set; }
        public string IconImage { get; set; }
    }
}
