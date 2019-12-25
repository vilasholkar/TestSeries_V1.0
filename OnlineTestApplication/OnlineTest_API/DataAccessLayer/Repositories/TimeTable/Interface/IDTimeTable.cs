using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.TimeTable;

namespace DataAccessLayer
{
   public interface IDTimeTable
    {
       string AddUpdateTimeTable(TimeTableViewModel objTimeTable);
       List<TimeTableViewModel> GetTimeTable();
       TimeTableViewModel GetTimeTableByDate(TimeTableViewModel objTimeTable);
    }
}
