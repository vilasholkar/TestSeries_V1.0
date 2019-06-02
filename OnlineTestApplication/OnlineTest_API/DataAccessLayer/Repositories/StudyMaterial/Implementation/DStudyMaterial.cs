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
        public string AddUpdateStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("StudyMaterialID", objStudyMaterial.StudyMaterialID));
            sqlParameterList.Add(new SqlParameter("Tittle", objStudyMaterial.Tittle));
            sqlParameterList.Add(new SqlParameter("SubTittle", objStudyMaterial.SubTittle));
            sqlParameterList.Add(new SqlParameter("Description", !string.IsNullOrEmpty(objStudyMaterial.Description) ? objStudyMaterial.Description : string.Empty));
            sqlParameterList.Add(new SqlParameter("TopicID", objStudyMaterial.TopicID != null ? objStudyMaterial.TopicID > 0 ? objStudyMaterial.TopicID : 0 : 0));
            sqlParameterList.Add(new SqlParameter("SubTopicID", objStudyMaterial.SubTopicID != null ? objStudyMaterial.SubTopicID > 0 ? objStudyMaterial.SubTopicID : 0 : 0));
            sqlParameterList.Add(new SqlParameter("SessionID", objStudyMaterial.SessionID != null ? objStudyMaterial.SessionID > 0 ? objStudyMaterial.SessionID : 0 : 0));
            sqlParameterList.Add(new SqlParameter("StreamID", string.Join(",", objStudyMaterial.StreamID)));
            sqlParameterList.Add(new SqlParameter("CourseID", objStudyMaterial.CourseID != null ? objStudyMaterial.CourseID.Length > 0 ? string.Join(",", objStudyMaterial.CourseID) : string.Empty : string.Empty));
            sqlParameterList.Add(new SqlParameter("BatchID", objStudyMaterial.BatchID != null ? objStudyMaterial.BatchID.Length > 0 ? string.Join(",", objStudyMaterial.BatchID) : string.Empty : string.Empty));
            sqlParameterList.Add(new SqlParameter("SubjectID",  objStudyMaterial.SubjectID));
            sqlParameterList.Add(new SqlParameter("Thumbnail", !string.IsNullOrEmpty(objStudyMaterial.Thumbnail) ? objStudyMaterial.Thumbnail : string.Empty));
            sqlParameterList.Add(new SqlParameter("URL", !string.IsNullOrEmpty(objStudyMaterial.URL) ? objStudyMaterial.URL : string.Empty));
            sqlParameterList.Add(new SqlParameter("SubjectID", objStudyMaterial.SubjectID != null ? objStudyMaterial.SubjectID > 0 ? objStudyMaterial.SubjectID : 0 : 0));
            sqlParameterList.Add(new SqlParameter("IsActive", objStudyMaterial.IsActive));
            sqlParameterList.Add(new SqlParameter("CreatedByUserID",1));
            sqlParameterList.Add(new SqlParameter("CreatedOnDate", DGeneric.SystemDateTime));
            return DGeneric.RunSP_ExecuteNonQuery("sp_AddUpdateStudyMaterial", sqlParameterList);
        }
        public string DeleteStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("StudyMaterialID", objStudyMaterial.StudyMaterialID));
            return DGeneric.RunSP_ExecuteNonQuery("sp_DeleteStudyMaterial", sqlParameterList);
        }
    }
}
