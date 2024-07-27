using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class About : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Pro1 { get; set; }
        public string Pro2 { get; set; }
        public string Pro3 { get; set; }
        public string FirstImage { get; set; }
        public string SecondImage { get; set; }
    }
}
