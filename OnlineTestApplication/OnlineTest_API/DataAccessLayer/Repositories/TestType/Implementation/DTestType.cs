using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Test;

namespace DataAccessLayer
{
    public class DTestType:IDTestType
    {
        SqlConnection con = new SqlConnection(@"Data Source=103.195.185.254;Initial Catalog=aayam6q8_OnlineTestDev;Persist Security Info=True;User ID=aayam;Password=P@ssw0rd4203#;");
        public List<TestTypeViewModel> GetTestType()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllMasterData", con);
                cmd.CommandType = CommandType.StoredProcedure;
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
        }
    }
}
