using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Dashboard;

namespace DataAccessLayer
{
    public class DStudentDashboard : IDStudentDashboard
    {
        public StudentDashboardViewModel GetStudentDashboardDetail(int StudentID)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("StudentID", StudentID));
            List<DataTableMapping> dataTableMapping = new List<DataTableMapping>();
            dataTableMapping.Add(new DataTableMapping("Table", "TotalTest"));
            dataTableMapping.Add(new DataTableMapping("Table1", "NotStarted"));
            dataTableMapping.Add(new DataTableMapping("Table2", "Started"));
            dataTableMapping.Add(new DataTableMapping("Table3", "Completed"));
            dataTableMapping.Add(new DataTableMapping("Table4", "TestPercentage"));
            DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetStudentDashboard", sqlParameterList, dataTableMapping);
            var StudentDashboardData = new StudentDashboardViewModel();
            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string tableName = Convert.ToString(dt.TableName);
                        switch (tableName)
                        {
                            case "TotalTest":
                                StudentDashboardData.TotalTest = Convert.ToInt32(dt.Rows[0]["TotalTest"]);
                                break;
                            case "NotStarted":
                                StudentDashboardData.NotStarted = Convert.ToInt32(dt.Rows[0]["NotStarted"]);
                                break;
                            case "Started":
                                StudentDashboardData.Started = Convert.ToInt32(dt.Rows[0]["Started"]);
                                break;
                            case "Completed":
                                StudentDashboardData.Completed = Convert.ToInt32(dt.Rows[0]["Completed"]);
                                break;
                            case "TestPercentage":
                                StudentDashboardData.TestPercentage = DGeneric.BindDataList<TestPercentage>(dt).OrderBy(o => o.ResultID).ToList();
                                break;
                        }
                    }
                }
            }

            return StudentDashboardData;
        }
    }
}
