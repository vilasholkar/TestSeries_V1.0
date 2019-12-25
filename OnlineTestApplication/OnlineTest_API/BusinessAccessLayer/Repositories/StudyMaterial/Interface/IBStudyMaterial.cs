using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.StudyMaterial;

namespace BusinessAccessLayer
{
    public interface IBStudyMaterial
    {
       Response<List<StudyMaterialViewModel>> GetStudyMaterial();
       Response<StudyMaterialViewModel> GetStudyMaterialByID(int StudyMaterialID);
       Response<List<StudyMaterialViewModel>> GetStudyMaterialBySubTopic(string SubTopicID);
       string AddUpdateStudyMaterial(StudyMaterialViewModel objStudyMaterial);
       string DeleteStudyMaterial(StudyMaterialViewModel objStudyMaterial);
    }
}
