using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Master
{
    public class SliderViewModel
    {
        public int SliderID { get; set; }
        public string SliderNo { get; set; }
        public string Tittle { get; set; }
        public string SliderImage { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedOnDate { get; set; }
    }
}
