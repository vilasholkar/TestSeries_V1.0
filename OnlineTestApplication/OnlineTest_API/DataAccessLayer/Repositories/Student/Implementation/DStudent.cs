using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Student;

namespace DataAccessLayer
{
    public class DStudent : IDStudent
    {
        SqlConnection con = new SqlConnection(@"Data Source=HP\SQLEXPRESS;Initial Catalog=Demo;Persist Security Info=True;Integrated Security = true;");
        public List<StudentViewModel> GetStudent()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select id,name,institute from Student_Info", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.AsEnumerable().Select(s => new StudentViewModel()
                {
                    ID = Convert.ToInt32(s["ID"]),
                    Name = s["Name"].ToString(),
                    Institute = s["Institute"].ToString()
                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
