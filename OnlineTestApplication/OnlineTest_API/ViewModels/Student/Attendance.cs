using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class Attendance
    {
        public long AttendanceId { get; set; }
        public string PunchDate { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public string ArrivalDeparture { get; set; }
        public string Status { get; set; }
    }
}
