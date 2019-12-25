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
using ViewModels.TimeTable;

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
            if(StreamId!="")
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
            else
            return new List<CourseViewModel>();


        }

        public List<BatchViewModel> GetBatchByCourse(string CourseId)
        {
            if (CourseId != "")
            {
                List<SqlParameter> parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("CourseID", CourseId));
                DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStreamCourseBatch", parameter, null).Tables[0];
                if (dt.Rows.Count > 0)
                    return DGeneric.BindDataList<BatchViewModel>(dt);
                else
                    return new List<BatchViewModel>();
            }
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
                            case "Batch":
                                masterData.Batch = DGeneric.BindDataList<BatchViewModel>(dt);
                                break;
                            case "DefaultLecture":
                                masterData.DefaultLecture = DGeneric.BindDataList<LectureModel>(dt);
                                break;
                            case "Users":
                                masterData.UserList = DGeneric.BindDataList<UserViewModel>(dt);
                                break;
                            case "Faculty":
                                masterData.FacultyList = DGeneric.BindDataList<FacultyViewModel>(dt);
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
        public TopicViewModel GetTopicByID(int TopicID)
        {
            var topicViewModelData = new TopicViewModel();
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@TopicID", TopicID));
            IList<DataTableMapping> dataTableMappingList = new List<DataTableMapping>();
            dataTableMappingList.Add(new DataTableMapping("Table", "Topic"));
            dataTableMappingList.Add(new DataTableMapping("Table1", "Course"));
            dataTableMappingList.Add(new DataTableMapping("Table2", "Batch"));
            DataSet ds = DGeneric.RunSP_ReturnDataSet("sp_GetTopicByID", sqlParameterList, dataTableMappingList);

            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        switch (dt.TableName)
                        {
                            case "Topic":
                                topicViewModelData = DGeneric.BindDataList<TopicViewModel>(dt).FirstOrDefault();
                                topicViewModelData.StreamID = dt.Rows[0]["StreamID"].ToString().Split(',').Select(int.Parse).ToArray();
                                topicViewModelData.CourseID = dt.Rows[0]["CourseID"].ToString() != string.Empty ? dt.Rows[0]["CourseID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
                                topicViewModelData.BatchID = dt.Rows[0]["BatchID"].ToString() != string.Empty ? dt.Rows[0]["BatchID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
                                break;
                            case "Course":
                                topicViewModelData.Course = DGeneric.BindDataList<CourseViewModel>(dt);
                                break;
                            case "Batch":
                                topicViewModelData.Batch = DGeneric.BindDataList<BatchViewModel>(dt);
                                break;
                        }
                    }
                }
            }
            return topicViewModelData;
        }
        public string AddUpdateTopic(TopicViewModel objTopic)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("TopicID", objTopic.TopicID));
            sqlParameterList.Add(new SqlParameter("Topic", objTopic.Topic));
            sqlParameterList.Add(new SqlParameter("Description", !string.IsNullOrEmpty(objTopic.Description) ? objTopic.Description : string.Empty));
            sqlParameterList.Add(new SqlParameter("SubjectID", objTopic.SubjectID));
            sqlParameterList.Add(new SqlParameter("SessionID", objTopic.SessionID));
            sqlParameterList.Add(new SqlParameter("StreamID", string.Join(",", objTopic.StreamID)));
            sqlParameterList.Add(new SqlParameter("CourseID", objTopic.CourseID != null ? objTopic.CourseID.Length > 0 ? string.Join(",", objTopic.CourseID) : string.Empty : string.Empty));
            sqlParameterList.Add(new SqlParameter("BatchID", objTopic.BatchID != null & objTopic.CourseID != null ? objTopic.BatchID.Length > 0 & objTopic.CourseID.Length > 0 ? string.Join(",", objTopic.BatchID) : string.Empty : string.Empty));

            sqlParameterList.Add(new SqlParameter("IsActive", objTopic.IsActive));
            string temp = DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateTopic", sqlParameterList);
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

        #region Slider
        public List<SliderViewModel> GetSlider(int SliderID)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("SliderID", SliderID > 0 ? SliderID : 0));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetSlider", sqlParameterList, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<SliderViewModel>(dt);
            else
                return new List<SliderViewModel>();
        }
        public string AddUpdateSlider(SliderViewModel objSlider)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("SliderID", objSlider.SliderID));
            sqlParameterList.Add(new SqlParameter("SliderNo", string.IsNullOrEmpty(objSlider.SliderNo) ? DGeneric.GetNewId("Slider", "SliderID").ToString() : objSlider.SliderNo));
            sqlParameterList.Add(new SqlParameter("Tittle", !string.IsNullOrEmpty(objSlider.Tittle) ? objSlider.Tittle : string.Empty));
            sqlParameterList.Add(new SqlParameter("SliderImage", !string.IsNullOrEmpty(objSlider.SliderImage) ? objSlider.SliderImage : string.Empty));
            sqlParameterList.Add(new SqlParameter("IsActive", objSlider.IsActive));
            sqlParameterList.Add(new SqlParameter("CreatedByUserID", objSlider.CreatedByUserID > 0 ? objSlider.CreatedByUserID : 0));
            sqlParameterList.Add(new SqlParameter("CreatedOnDate", DGeneric.SystemDateTime));
            return DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateSlider", sqlParameterList);
        }

        public string DeleteSlider(SliderViewModel objSlider)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("SliderID", objSlider.SliderID));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteSlider", sqlParameterList);
        }
        #endregion

        #region Notification
       public List<NotificationViewModel> GetNotification(int NotificationID, int ReciverID)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("NotificationID", NotificationID > 0 ? NotificationID : 0));
            sqlParameterList.Add(new SqlParameter("ReciverID", ReciverID > 0 ? ReciverID : 0));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetNotification", sqlParameterList, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<NotificationViewModel>(dt);
            else
                return new List<NotificationViewModel>();
        }
        public string AddUpdateNotification(List<NotificationViewModel> objList)
        {
            string response = string.Empty;
            foreach (var objNotification in objList)
            {
                response = DSMSGeneric.SendAndroidNotification(objNotification.DeviceToken, objNotification.Title, objNotification.Description);
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("NotificationID", objNotification.NotificationID));
                sqlParameterList.Add(new SqlParameter("ReciverID", objNotification.ReciverID));
                sqlParameterList.Add(new SqlParameter("NotificationDate", Convert.ToDateTime(objNotification.NotificationDate)));
                sqlParameterList.Add(new SqlParameter("Title", !string.IsNullOrEmpty(objNotification.Title) ? objNotification.Title : string.Empty));
                sqlParameterList.Add(new SqlParameter("Description", !string.IsNullOrEmpty(objNotification.Description) ? objNotification.Description : string.Empty));
                sqlParameterList.Add(new SqlParameter("ImageURL", !string.IsNullOrEmpty(objNotification.ImageURL) ? objNotification.ImageURL : string.Empty));
                sqlParameterList.Add(new SqlParameter("RedirectToURL", !string.IsNullOrEmpty(objNotification.RedirectToURL) ? objNotification.RedirectToURL : string.Empty));
                sqlParameterList.Add(new SqlParameter("IsRead", objNotification.IsRead));
                response = DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateNotification", sqlParameterList);
            }

            return response;
        }
        #endregion
        public List<TopicViewModel> GetTopicBySubject(string SubjectID)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@SubjectID", SubjectID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetSubjectTopicSubTopic", parameter, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<TopicViewModel>(dt);
            else
                return new List<TopicViewModel>();
        }

        public List<SubTopicViewModel> GetSubTopicByTopic(string TopicID)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("TopicID", TopicID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetSubjectTopicSubTopic", parameter, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<SubTopicViewModel>(dt);
            else
                return new List<SubTopicViewModel>();
        }

    }
}
