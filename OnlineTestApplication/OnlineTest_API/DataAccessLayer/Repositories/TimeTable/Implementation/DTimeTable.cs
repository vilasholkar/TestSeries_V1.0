using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Master;
using ViewModels.TimeTable;

namespace DataAccessLayer
{
    public class DTimeTable : IDTimeTable
    {
        public string AddUpdateTimeTable(TimeTableViewModel objTimeTable)
        {
            if (objTimeTable.DateType == "single")
            {
                objTimeTable.ToDate = objTimeTable.FromDate;
            }
            foreach (DateTime day in EachDay(Convert.ToDateTime( objTimeTable.FromDate),Convert.ToDateTime( objTimeTable.ToDate)))
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("TimeTableID", objTimeTable.TimeTableID));
                sqlParameterList.Add(new SqlParameter("Description", !string.IsNullOrEmpty(objTimeTable.Description) ? objTimeTable.Description : string.Empty));
                sqlParameterList.Add(new SqlParameter("TimeTableDate", day.Date));
                sqlParameterList.Add(new SqlParameter("ShiftID", objTimeTable.ShiftID));
                sqlParameterList.Add(new SqlParameter("SessionID", objTimeTable.SessionID));
                sqlParameterList.Add(new SqlParameter("BatchID", string.Join(",", objTimeTable.BatchID)));
                sqlParameterList.Add(new SqlParameter("LectureID", string.Join(",", objTimeTable.LectureID)));
                sqlParameterList.Add(new SqlParameter("IsActive", true));
                sqlParameterList.Add(new SqlParameter("CreatedByUserID", 1));
                sqlParameterList.Add(new SqlParameter("CreatedOnDate", DGeneric.SystemDateTime));

                int TimeTableID = Convert.ToInt32(DGeneric.RunSP_ReturnScaler("sp_AddUpdateTimeTable", sqlParameterList));

                if (TimeTableID > 0)
                {
                    for (int i = 0; i < objTimeTable.LectureList.Count; i++)
                    {
                        objTimeTable.LectureList[i].TimeTableID = TimeTableID;
                        objTimeTable.LectureList[i].FacultyID = objTimeTable.LectureList[i].FacultyID;
                        objTimeTable.LectureList[i].CreatedByUserID = 1;
                        objTimeTable.LectureList[i].CreatedOnDate = DGeneric.SystemDateTime;
                        objTimeTable.LectureList[i].IsActive = true;
                        objTimeTable.LectureList[i].Time_From = objTimeTable.LectureList[i].Lecture.Time_From;
                        objTimeTable.LectureList[i].Time_To = objTimeTable.LectureList[i].Lecture.Time_To;
                        objTimeTable.LectureList[i].BatchID = objTimeTable.LectureList[i].Batch.BatchID;
                        objTimeTable.LectureList[i].LectureID = objTimeTable.LectureList[i].Lecture.LectureID;
                    }
                    DataTable dt = DGeneric.ListToDataTable(objTimeTable.LectureList);
                    dt.Columns.Remove("Batch");
                    dt.Columns.Remove("Lecture");
                    dt.Columns.Remove("Faculty");
                    dt.Columns.Remove("ID");

                    List<SqlParameter> sqlParameterList1 = new List<SqlParameter>();
                    sqlParameterList1.Add(new SqlParameter("@TimeTableID", TimeTableID));
                    DGeneric.RunSP_ExecuteNonQuery("sp_DeleteLecture", sqlParameterList1);

                    List<SqlParameter> sqlParameterList2 = new List<SqlParameter>();
                    sqlParameterList2.Add(new SqlParameter("@LectureDetails", dt));
                    string LectureID = DGeneric.RunSP_ReturnScaler("sp_AddLecture", sqlParameterList2);
                }
            }

            return "Success";
        }

        public List<TimeTableViewModel> GetTimeTable()
        {
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetTimeTable", null, null).Tables[0];
            //if (dt.Rows.Count > 0)
            //    return DGeneric.BindDataList<OnlineTestViewModel>(dt);
            //else
            //    return new List<OnlineTestViewModel>();
            return dt.AsEnumerable().Select(s => new TimeTableViewModel()
            {
                TimeTableID = Convert.ToInt32(s["TimeTableID"]),
                Description = s["Description"].ToString(),
                FromDate = Convert.ToString(s["TimeTableDate"]).ConvertDateTimeToString(),
                ToDate = Convert.ToString(s["TimeTableDate"]).ConvertDateTimeToString(),
                ShiftID = Convert.ToInt32(s["ShiftID"]),
                SessionID = Convert.ToInt32(s["SessionID"]),
                Session = Convert.ToString(s["Session"]),
                //BatchID = Convert.ToInt32(s["BatchID"]).Split(',').ToArray(),
                //LectureID = Convert.ToInt32(s["LectureID"]),
                CreatedByUserID = Convert.ToInt32(s["CreatedByUserID"]),
                CreatedOnDate = s["CreatedOnDate"].ToString().ConvertDateTimeToString(),
                IsActive = Convert.ToBoolean(s["IsActive"])
            }).ToList();
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public TimeTableViewModel GetTimeTableByDate(TimeTableViewModel objTimeTable)
        {
            var timeTableViewModel = new TimeTableViewModel();
            List<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
            dataTableMappingList.Add(new DataTableMapping("Table", "TimeTable"));
            dataTableMappingList.Add(new DataTableMapping("Table1", "Lecture"));
            dataTableMappingList.Add(new DataTableMapping("Table2", "Batch"));
            dataTableMappingList.Add(new DataTableMapping("Table3", "DefaultLecture"));
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Date", Convert.ToDateTime(objTimeTable.FromDate)));
            sqlParameterList.Add(new SqlParameter("@ShiftID", Convert.ToInt32(objTimeTable.ShiftID)));
            sqlParameterList.Add(new SqlParameter("@SessionID", Convert.ToInt32(objTimeTable.SessionID)));

            DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetTimeTableByDate", sqlParameterList, dataTableMappingList);

            List<LectureViewModel> obj = new List<LectureViewModel>();

            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        switch (dt.TableName)
                        {
                            case "Lecture":
                                foreach (DataRow dr in ds.Tables[1].Rows)
                                {
                                    obj.Add(new LectureViewModel()
                                    {
                                        Lecture = new LectureViewModel()
                                        {
                                            LectureID = Convert.ToInt32(dr["LectureID"]),
                                            LectureName = dr["Lecture"].ToString(),
                                            Time_From = dr["Time_From"].ToString(),
                                            Time_To = dr["Time_To"].ToString()
                                        },
                                        Batch = new ViewModels.Master.BatchViewModel()
                                        {
                                            BatchID = Convert.ToInt32(dr["BatchID"]),
                                            Batch = dr["Batch"].ToString()
                                        },
                                        Subject = dr["Subject"].ToString(),
                                        FacultyID = Convert.ToInt32(dr["FacultyID"]),
                                        Faculty = dr["Faculty"].ToString(),
                                        TimeTableID = Convert.ToInt32(dr["TimeTableID"]),
                                        ID = Convert.ToInt32(dr["ID"])
                                    });
                                }
                                break;
                            case "TimeTable":
                                timeTableViewModel = DGeneric.BindDataList<TimeTableViewModel>(ds.Tables[0]).FirstOrDefault();
                                timeTableViewModel.DateType = "single";
                                timeTableViewModel.FromDate = Convert.ToString(ds.Tables[0].Rows[0]["TimeTableDate"]).ConvertDateTimeToString();
                                timeTableViewModel.LectureID = ds.Tables[0].Rows[0]["LectureID"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["LectureID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
                                timeTableViewModel.BatchID = ds.Tables[0].Rows[0]["BatchID"].ToString() != string.Empty ? ds.Tables[0].Rows[0]["BatchID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
                                timeTableViewModel.LectureList = obj;
                                break;
                            case "DefaultLecture":
                                timeTableViewModel.Lecture = DGeneric.BindDataList<LectureModel>(dt);

                                break;
                            case "Batch":
                                timeTableViewModel.Batch = DGeneric.BindDataList<BatchViewModel>(dt);
                                break;
                        }
                    }
                }
            }




            return timeTableViewModel;
            //return ds.Tables[0].AsEnumerable().Select(s => new TimeTableViewModel()
            //{
            //    TimeTableID = Convert.ToInt32(s["TimeTableID"]),
            //    Description = s["Description"].ToString(),
            //    FromDate = Convert.ToDateTime(s["TimeTableDate"]),
            //    ToDate = Convert.ToDateTime(s["TimeTableDate"]),
            //    ShiftID = Convert.ToInt32(s["ShiftID"]),
            //    SessionID = Convert.ToInt32(s["SessionID"]),
            //    BatchID = Convert.ToInt32(s["BatchID"]).Split(',').ToArray(),
            //    LectureID = Convert.ToInt32(s["LectureID"]),
            //    CreatedByUserID = Convert.ToInt32(s["CreatedByUserID"]),
            //    CreatedOnDate = Convert.ToDateTime(s["CreatedOnDate"]),
            //    IsActive = Convert.ToBoolean(s["IsActive"]),
            //    LectureList = obj
            //}).ToList();
        }
    }


}
