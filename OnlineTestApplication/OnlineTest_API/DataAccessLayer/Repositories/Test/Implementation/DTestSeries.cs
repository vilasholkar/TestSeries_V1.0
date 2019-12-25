using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace DataAccessLayer
{
    public class DTestSeries : IDTestSeries
    {
        public List<TestSeriesViewModel> GetTestSeries()
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            List<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
            dataTableMappingList.Add(new DataTableMapping("Table", "TestType"));
            dataTableMappingList.Add(new DataTableMapping("Table1", "QuestionType"));
            dataTableMappingList.Add(new DataTableMapping("Table2", "Subject"));
            dataTableMappingList.Add(new DataTableMapping("Table3", "TestSeries"));
            DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetAllMasterData", sqlParameterList, dataTableMappingList);
            List<TestSeriesViewModel> testSeriesList = new List<TestSeriesViewModel>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string tableName = Convert.ToString(dt.Rows[0]["TableName"]);
                        switch (tableName)
                        {
                            case "TestSeries":
                                testSeriesList = DGeneric.BindDataList<TestSeriesViewModel>(dt);
                                break;
                        }
                    }
                }
            }

            //return ds.Tables["TestSeries"].AsEnumerable().Select(s => new TestSeriesViewModel()
            //{
            //    TestSeriesID = Convert.ToInt32(s["TestSeriesID"]),
            //    TestSeries = s["TestSeries"].ToString(),
            //    TotalTest = s["TotalTest"].ToString(),
            //    Description = s["Description"].ToString(),
            //}).ToList();
            return testSeriesList;
        }
        public string AddUpdateTestSeries(TestSeriesViewModel objTestSeries)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("TestSeriesID", objTestSeries.TestSeriesID));
            sqlParameterList.Add(new SqlParameter("TestSeries", !string.IsNullOrEmpty(objTestSeries.TestSeries) ? objTestSeries.TestSeries : string.Empty));
            sqlParameterList.Add(new SqlParameter("TotalTest", !string.IsNullOrEmpty(objTestSeries.TotalTest) ? objTestSeries.TotalTest : string.Empty));
            sqlParameterList.Add(new SqlParameter("Description", !string.IsNullOrEmpty(objTestSeries.Description) ? objTestSeries.Description : string.Empty));
            return DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateTestSeries", sqlParameterList);
        }
        public string DeleteTestSeries(TestSeriesViewModel objTestSeries)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("TestSeriesID", objTestSeries.TestSeriesID));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteTestSeries", sqlParameterList);
        }
    }
}
