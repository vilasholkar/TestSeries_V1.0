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
       string AddUpdateStudyMaterial(StudyMaterialViewModel objStudyMaterial);
       string DeleteStudyMaterial(StudyMaterialViewModel objStudyMaterial);
    }
}
