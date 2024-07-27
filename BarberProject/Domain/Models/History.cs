using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class History : BaseEntity
    {
        public string Image { get; set; }
        public string UpTitle { get; set; }
        public string MainTitle { get; set; }
        public string Description { get; set; }

    }
}
