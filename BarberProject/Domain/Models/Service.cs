using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Service : BaseEntity
    {
        public string IconImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<ServiceImage> ServiceImages { get; set; }
        public ICollection<SubService> SubServices { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
