using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.TimeTable;

namespace BusinessAccessLayer
{
   public interface IBTimeTable
    {
       Response<List<TimeTableViewModel>> GetTimeTable();
       string AddUpdateTimeTable(TimeTableViewModel objTimeTable);
       Response<TimeTableViewModel> GetTimeTableByDate(TimeTableViewModel objTimeTable);
           
   }
}
