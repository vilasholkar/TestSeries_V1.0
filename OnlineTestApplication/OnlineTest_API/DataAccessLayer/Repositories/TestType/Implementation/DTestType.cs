using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.TestType;

namespace DataAccessLayer
{
    public class DTestType : IDTestType
    {
        //SqlConnection con = new SqlConnection(@"Data Source=103.195.185.254;Initial Catalog=aayam6q8_OnlineTestDev;Persist Security Info=True;User ID=aayam;Password=P@ssw0rd4203#;");
        public List<TestTypeViewModel> GetTestType()
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                List<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
                dataTableMappingList.Add(new DataTableMapping("Table", "TestType"));
                dataTableMappingList.Add(new DataTableMapping("Table1", "QuestionType"));
                dataTableMappingList.Add(new DataTableMapping("Table2", "Subject"));
                DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetAllMasterData", sqlParameterList, dataTableMappingList);
                List<TestTypeViewModel> testTypeList = new List<TestTypeViewModel>();
                if (ds.Tables.Count > 0)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string tableName = Convert.ToString(dt.Rows[0]["TableName"]);
                            switch (tableName)
                            {
                                case "TestType":
                                    testTypeList = DGeneric.BindDataList<TestTypeViewModel>(dt);
                                    break;
                            }
                        }
                    }
                }

                return ds.Tables["TestType"].AsEnumerable().Select(s => new TestTypeViewModel()
                {
                    TestTypeID = Convert.ToInt32(s["TestTypeID"]),
                    TestType = s["TestType"].ToString(),
                }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public string AddUpdateTestType(TestTypeViewModel objTestType)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("TestTypeID", objTestType.TestTypeID));
            sqlParameterList.Add(new SqlParameter("TestType", objTestType.TestType));
            return DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateTestType", sqlParameterList);
        }


        public string DeleteTestType(TestTypeViewModel objTestType)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("TestTypeID", objTestType.TestTypeID));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteTestType", sqlParameterList);
        }
    }
}
