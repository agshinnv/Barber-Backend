using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SliderImage : BaseEntity
    {
        public string Image { get; set; }
        public int SliderId { get; set; }
        public Slider Slider { get; set; }
    }
}
