using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Subscriber : BaseEntity
    {
        public string SubscriberEmail { get; set; }
    }
}
