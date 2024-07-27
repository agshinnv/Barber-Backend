using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee : BaseEntity
    {
        public string BarberImage { get; set; }
        public string BarberName { get; set; }
        public string Specialty { get; set; }
        public string Description { get; set; }
        public string SocialIcon { get; set; }
        public string Skill1 { get; set; }
        public string Skill2 { get; set; }
        public string Biography { get; set; }
        public string Education { get; set; }
        public string Awards { get; set; }
        public string ContactDescription { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
