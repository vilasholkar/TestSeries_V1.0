using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Master;
using ViewModels.Test;

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
                    Stream = Convert.ToString(s["Stream"])
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Procedure::sp_StreamSelect::Error occured.", ex.InnerException);
            }
        }

        public List<CourseViewModel> GetCourseByStream(string StreamId)
        {
            try
            {
                List<SqlParameter> parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@StreamID", StreamId));
                DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStreamCourseBatch", parameter, null).Tables[0];
                return dt.AsEnumerable().Select(s => new CourseViewModel()
                {
                    CourseID = Convert.ToInt32(s["CourseID"]),
                    Course = Convert.ToString(s["Course"])
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Procedure::sp_StreamSelect::Error occured.", ex.InnerException);
            }
        }

        public List<BatchViewModel> GetBatchByCourse(string CourseId)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("CourseID", CourseId));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStreamCourseBatch", parameter, null).Tables[0];
            return dt.AsEnumerable().Select(s => new BatchViewModel()
            {
                BatchID = Convert.ToInt32(s["BatchID"]),
                Batch = s["Batch"].ToString(),
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
                    Session = Convert.ToString(s["Session"]),
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Procedure::sp_StreamSelect::Error occured.", ex.InnerException);
            }
        }

        public MasterViewModel GetMasterData()
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetAllMasterData", sqlParameterList, null);
                var masterData = new MasterViewModel();
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
                                    masterData.TestType = DGeneric.BindDataList<TestTypeViewModel>(dt);
                                    break;
                                case "TestSeries":
                                    masterData.TestSeries = DGeneric.BindDataList<TestSeriesViewModel>(dt);
                                    break;
                                case "Stream":
                                    masterData.Stream = DGeneric.BindDataList<StreamViewModel>(dt);
                                    break;
                                case "Session":
                                    masterData.Session = DGeneric.BindDataList<SessionViewModel>(dt);
                                    break;
                            }
                        }
                    }
                }

                return masterData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
