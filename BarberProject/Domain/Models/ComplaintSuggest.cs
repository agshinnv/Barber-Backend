using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ComplaintSuggest : BaseEntity
    {
        public string UserFullName { get; set; }
        public string UserPhone { get; set; }
        public string Subject { get; set; }
        public string UserEmail { get; set; }
        public string UserSuggest { get; set; }
    }
}
