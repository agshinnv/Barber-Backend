using Domain.Common;
using Domain.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Reservation : BaseEntity
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    }
}
