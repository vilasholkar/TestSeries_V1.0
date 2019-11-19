using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class AttendenceMainModel
    {
        public List<Student.StudentViewModel> StudentList { get; set; }
        public List<DateTime> DateRange { get; set; }
    }
}
