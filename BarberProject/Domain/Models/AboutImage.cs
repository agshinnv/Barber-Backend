using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AboutImage : BaseEntity
    {
        public string Image { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }
    }
}
