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
    public class DTestType:IDTestType
    {
        SqlConnection con = new SqlConnection(@"Data Source=103.195.185.254;Initial Catalog=aayam6q8_OnlineTestDev;Persist Security Info=True;User ID=aayam;Password=P@ssw0rd4203#;");
        public List<TestTypeViewModel> GetTestType()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from TestType", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0].AsEnumerable().Select(s => new TestTypeViewModel()
                {
                    TestTypeID = Convert.ToInt32(s["TestTypeID"]),
                    TestType = s["TestType"].ToString(),
                }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            //try
            //{
            //    List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            //    List<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
            //    dataTableMappingList.Add(new DataTableMapping("Table1", "TestType"));
            //    dataTableMappingList.Add(new DataTableMapping("Table2", "QuestionType"));
            //    dataTableMappingList.Add(new DataTableMapping("Table3", "Subject"));
            //    Task<DataSet> ds = DGeneric.RunSP_ReturnDataSet("sp_GetAllMasterData", sqlParameterList, dataTableMappingList);
            //    return ds.Result.Tables["TestType"].AsEnumerable().Select(s => new TestTypeViewModel()
            //    {
            //        TestTypeID = Convert.ToInt32(s["TestTypeID"]),
            //        TestType = s["TestType"].ToString(),                    
            //    }).ToList();              
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
    }
}
