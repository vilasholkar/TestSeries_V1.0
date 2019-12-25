using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using ViewModels.StudyMaterial;

namespace DataAccessLayer
{
    public class DStudyMaterial : IDStudyMaterial
    {
        public List<StudyMaterialViewModel> GetStudyMaterial()
        {
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStudyMaterial", null, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<StudyMaterialViewModel>(dt);
            else
                return new List<StudyMaterialViewModel>();
        }

        public StudyMaterialViewModel GetStudyMaterialByID(int StudyMaterialID)
        {
            var studyMaterialViewModelData = new StudyMaterialViewModel();
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@StudyMaterialID", StudyMaterialID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStudyMaterialByID", sqlParameterList, null).Tables[0];
            
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<StudyMaterialViewModel>(dt).FirstOrDefault();
            else
                return new List<StudyMaterialViewModel>().FirstOrDefault();
            //if (ds.Tables.Count > 0)
            //{
            //                    studyMaterialViewModelData = DGeneric.BindDataList<OnlineTestViewModel>(dt).FirstOrDefault();
            //                    studyMaterialViewModelData.StreamID = dt.Rows[0]["StreamID"].ToString().Split(',').Select(int.Parse).ToArray();
            //                    //Vaibhav Changed because of empty data  
            //                    // onlineTestViewModelData.CourseID = dt.Rows[0]["CourseID"].ToString().Split(',').Select(int.Parse).ToArray();
            //                    // onlineTestViewModelData.BatchID = dt.Rows[0]["BatchID"].ToString().Split(',').Select(int.Parse).ToArray();
            //                    studyMaterialViewModelData.CourseID = dt.Rows[0]["CourseID"].ToString() != string.Empty ? dt.Rows[0]["CourseID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
            //                    studyMaterialViewModelData.BatchID = dt.Rows[0]["BatchID"].ToString() != string.Empty ? dt.Rows[0]["BatchID"].ToString().Split(',').Select(int.Parse).ToArray() : null;
            //}
            //return studyMaterialViewModelData;
        }

        public List<StudyMaterialViewModel> GetStudyMaterialBySubTopic(string SubTopicID)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("SubTopicID", SubTopicID));
            DataTable dt = DGeneric.RunSP_ReturnDataSet("sp_GetStudyMaterialBySubTopic", parameter, null).Tables[0];
            if (dt.Rows.Count > 0)
                return DGeneric.BindDataList<StudyMaterialViewModel>(dt);
            else
                return new List<StudyMaterialViewModel>();
        }
        public string AddUpdateStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("StudyMaterialID", objStudyMaterial.StudyMaterialID));
            sqlParameterList.Add(new SqlParameter("Tittle", objStudyMaterial.Tittle));
            sqlParameterList.Add(new SqlParameter("SubTittle", objStudyMaterial.SubTittle));
            sqlParameterList.Add(new SqlParameter("Description", !string.IsNullOrEmpty(objStudyMaterial.Description) ? objStudyMaterial.Description : string.Empty));
            sqlParameterList.Add(new SqlParameter("TopicID", objStudyMaterial.TopicID > 0 ?  objStudyMaterial.TopicID : 0 ));
            sqlParameterList.Add(new SqlParameter("SubTopicID",  objStudyMaterial.SubTopicID > 0 ? objStudyMaterial.SubTopicID : 0));
            sqlParameterList.Add(new SqlParameter("SubjectID",  objStudyMaterial.SubjectID > 0 ? objStudyMaterial.SubjectID : 0 ));
            sqlParameterList.Add(new SqlParameter("Thumbnail", !string.IsNullOrEmpty(objStudyMaterial.Thumbnail) ? objStudyMaterial.Thumbnail : string.Empty));
            sqlParameterList.Add(new SqlParameter("URL_English", !string.IsNullOrEmpty(objStudyMaterial.URL_English) ? objStudyMaterial.URL_English : string.Empty));
            sqlParameterList.Add(new SqlParameter("URL_Hindi", !string.IsNullOrEmpty(objStudyMaterial.URL_Hindi) ? objStudyMaterial.URL_Hindi : string.Empty));
            sqlParameterList.Add(new SqlParameter("IsActive", objStudyMaterial.IsActive));
            sqlParameterList.Add(new SqlParameter("CreatedByUserID",1));
            sqlParameterList.Add(new SqlParameter("CreatedOnDate", DGeneric.SystemDateTime));
            string response = DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateStudyMaterial", sqlParameterList);
            return response;
        }
        public string DeleteStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("StudyMaterialID", objStudyMaterial.StudyMaterialID));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteStudyMaterial", sqlParameterList);
        }
    }
}
