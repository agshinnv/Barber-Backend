using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Slider : BaseEntity
    {
        public string SliderTitle { get; set; }
        public string SliderDescription { get; set; }

        public ICollection<SliderImage> SliderImages { get; set; }

    }
}
