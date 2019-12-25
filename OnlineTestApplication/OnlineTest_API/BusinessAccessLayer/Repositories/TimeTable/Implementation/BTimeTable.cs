using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.TimeTable;

namespace BusinessAccessLayer
{
    public class BTimeTable : IBTimeTable
    {
        private readonly IDTimeTable _iDTimeTable;
        public BTimeTable(IDTimeTable iDTimeTable, IDMaster iDMaster)
        {
            _iDTimeTable = iDTimeTable;

        }
        public Response<List<TimeTableViewModel>> GetTimeTable()
        {
            var timeTableData = _iDTimeTable.GetTimeTable();
            if (timeTableData != null)
            {
                return new Response<List<TimeTableViewModel>>
                {
                    IsSuccessful = true,
                    Object = timeTableData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<TimeTableViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }

        public string AddUpdateTimeTable(TimeTableViewModel objTimeTable)
        {
            var timeTableData = _iDTimeTable.AddUpdateTimeTable(objTimeTable);
            if (!string.IsNullOrEmpty(timeTableData))
            {
                return timeTableData;
            }
            else
            {
                return timeTableData;
            }
        }

        public Response<TimeTableViewModel> GetTimeTableByDate(TimeTableViewModel objTimeTable)
        {
            var timeTableData = _iDTimeTable.GetTimeTableByDate(objTimeTable);
            if (timeTableData != null)
            {
                return new Response<TimeTableViewModel>
                {
                    IsSuccessful = true,
                    Object = timeTableData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<TimeTableViewModel>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }

    }
}
