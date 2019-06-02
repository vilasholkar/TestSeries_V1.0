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
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_StreamSelect", null, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<StreamViewModel>(dt);
            else
                return new List<StreamViewModel>();
            //return dt.AsEnumerable().Select(s => new StreamViewModel()
            //{
            //    StreamID = Convert.ToInt32(s["StreamID"]),
            //    Stream = Convert.ToString(s["Stream"])
            //}).ToList();
        }

        public List<CourseViewModel> GetCourseByStream(string StreamId)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@StreamID", StreamId));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStreamCourseBatch", parameter, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<CourseViewModel>(dt);
            else
                return new List<CourseViewModel>();
            //return dt.AsEnumerable().Select(s => new CourseViewModel()
            //{
            //    CourseID = Convert.ToInt32(s["CourseID"]),
            //    Course = Convert.ToString(s["Course"])
            //}).ToList();
        }

        public List<BatchViewModel> GetBatchByCourse(string CourseId)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("CourseID", CourseId));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStreamCourseBatch", parameter, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<BatchViewModel>(dt);
            else
                return new List<BatchViewModel>();
        }
        //return dt.AsEnumerable().Select(s => new BatchViewModel()
        //{
        //    BatchID = Convert.ToInt32(s["BatchID"]),
        //    Batch = s["Batch"].ToString(),
        //}).ToList();

        public List<SessionViewModel> GetSession()
        {
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_SessionSelect", null, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<SessionViewModel>(dt);
            else
                return new List<SessionViewModel>();
            //return dt.AsEnumerable().Select(s => new SessionViewModel()
            //{
            //    SessionID = Convert.ToInt32(s["SessionID"]),
            //    Session = Convert.ToString(s["Session"]),
            //}).ToList();
        }

        public MasterViewModel GetMasterData()
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
                            case "Subject":
                                masterData.Subject = DGeneric.BindDataList<SubjectViewModel>(dt);
                                break;
                            case "Topic":
                                masterData.Topic = DGeneric.BindDataList<TopicViewModel>(dt);
                                break;
                            case "SubTopic":
                                masterData.SubTopic = DGeneric.BindDataList<SubTopicViewModel>(dt);
                                break;
                        }
                    }
                }
            }

            return masterData;
        }

        public List<SubjectViewModel> GetSubject()
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            List<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
            dataTableMappingList.Add(new DataTableMapping("Table2", "Subject"));
            DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetAllMasterData", sqlParameterList, dataTableMappingList);
            List<SubjectViewModel> subjectViewModelList = new List<SubjectViewModel>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string tableName = Convert.ToString(dt.Rows[0]["TableName"]);
                        switch (tableName)
                        {
                            case "Subject":
                                subjectViewModelList = DGeneric.BindDataList<SubjectViewModel>(dt);
                                break;
                        }
                    }
                }
            }

            return subjectViewModelList;
        }
        
        #region Topic
        public List<TopicViewModel> GetTopic()
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            List<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
            dataTableMappingList.Add(new DataTableMapping("Table6", "Topic"));
            DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetAllMasterData", sqlParameterList, dataTableMappingList);
            List<TopicViewModel> testTypeList = new List<TopicViewModel>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string tableName = Convert.ToString(dt.Rows[0]["TableName"]);
                        switch (tableName)
                        {
                            case "Topic":
                                testTypeList = DGeneric.BindDataList<TopicViewModel>(dt);
                                break;
                        }
                    }
                }
            }

            return testTypeList;
        }
        public string AddUpdateTopic(TopicViewModel objTopic)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("TopicID", objTopic.TopicID));
            sqlParameterList.Add(new SqlParameter("Topic", objTopic.Topic));
            sqlParameterList.Add(new SqlParameter("Description", !string.IsNullOrEmpty(objTopic.Description) ? objTopic.Description : string.Empty));
            sqlParameterList.Add(new SqlParameter("SubjectID", objTopic.SubjectID));
            sqlParameterList.Add(new SqlParameter("IsActive", objTopic.IsActive));
            string temp=DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateTopic", sqlParameterList);
            return temp;
        }

        public string DeleteTopic(TopicViewModel objTopic)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("TopicID", objTopic.TopicID));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteTopic", sqlParameterList);
        }
        #endregion
     
        #region SubTopic
        public List<SubTopicViewModel> GetSubTopic()
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            List<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
            dataTableMappingList.Add(new DataTableMapping("Table7", "SubTopic"));
            DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetAllMasterData", sqlParameterList, dataTableMappingList);
            List<SubTopicViewModel> testTypeList = new List<SubTopicViewModel>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string tableName = Convert.ToString(dt.Rows[0]["TableName"]);
                        switch (tableName)
                        {
                            case "SubTopic":
                                testTypeList = DGeneric.BindDataList<SubTopicViewModel>(dt);
                                break;
                        }
                    }
                }
            }
            return testTypeList;
            //return ds.Tables["SubTopic"].AsEnumerable().Select(s => new SubTopicViewModel()
            //{
            //    SubTopicID = Convert.ToInt32(s["SubTopicID"]),
            //    SubTopic = s["SubTopic"].ToString(),
            //    Description = s["Description"].ToString(),
            //    TopicID = Convert.ToInt32(s["TopicID"]),
            //    IsActive = Convert.ToBoolean(s["IsActive"]),
            //}).ToList();
        }
        public string AddUpdateSubTopic(SubTopicViewModel objSubTopic)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("SubTopicID", objSubTopic.SubTopicID));
            sqlParameterList.Add(new SqlParameter("SubTopic", objSubTopic.SubTopic));
            sqlParameterList.Add(new SqlParameter("Description", !string.IsNullOrEmpty(objSubTopic.Description) ? objSubTopic.Description : string.Empty));
            sqlParameterList.Add(new SqlParameter("TopicID", objSubTopic.TopicID));
            sqlParameterList.Add(new SqlParameter("IsActive", objSubTopic.IsActive));
            return DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateSubTopic", sqlParameterList);
        }

        public string DeleteSubTopic(SubTopicViewModel objSubTopic)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("TopicID", objSubTopic.SubTopicID));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteSubTopic", sqlParameterList);
        }
        #endregion
    }
}
