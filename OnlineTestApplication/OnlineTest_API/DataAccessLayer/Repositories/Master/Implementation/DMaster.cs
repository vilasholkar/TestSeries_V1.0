using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Master;

namespace DataAccessLayer
{
    public class DMaster : IDMaster
    {
        public List<StreamViewModel> GetStream()
        {
            try
            {
                DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_StreamSelect", null, null).Tables[0];
                return dt.AsEnumerable().Select(s => new StreamViewModel()
                {
                    StreamID = Convert.ToInt32(s["StreamID"]),
                    StreamName = Convert.ToString(s["Stream"])
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Procedure::sp_StreamSelect::Error occured.", ex.InnerException);
            }
        }

        public List<CourseViewModel> GetCourseByStream(int StreamId)
        {
            try
            {
                List<SqlParameter> parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@StreamID", StreamId));
                DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStreamCourseBatch", parameter, null).Tables[0];
                return dt.AsEnumerable().Select(s => new CourseViewModel()
                {
                    CourseID = Convert.ToInt32(s["CourseID"]),
                    CourseName = Convert.ToString(s["Course"])
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Procedure::sp_StreamSelect::Error occured.", ex.InnerException);
            }
        }


        public List<BatchViewModel> GetBatchByCourse(int CourseId)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("CourseID", CourseId));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStreamCourseBatch", parameter, null).Tables[0];
            return dt.AsEnumerable().Select(s => new BatchViewModel()
            {
                BatchID = Convert.ToInt32(s["BatchID"]),
                BatchName = s["Batch"].ToString(),
            }).ToList();
        }

        public List<SessionViewModel> GetSession()
        {
            try
            {
                DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_SessionSelect", null, null).Tables[0];
                return dt.AsEnumerable().Select(s => new SessionViewModel()
                {
                    SessionID = Convert.ToInt32(s["SessionID"]),
                    SessionName = Convert.ToString(s["Session"]),
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Procedure::sp_StreamSelect::Error occured.", ex.InnerException);
            }
        }
    }
}
