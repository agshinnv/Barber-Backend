using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Blog : BaseEntity
    {
        public string BlogTitle { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Comment> Comments { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public ICollection<BlogImage> BlogImages { get; set; }
        public string Content { get; set; }
    }
}
