using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int BlogId { get; set; }
        public Blog Blogs { get; set; }
        public string CommentText { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
