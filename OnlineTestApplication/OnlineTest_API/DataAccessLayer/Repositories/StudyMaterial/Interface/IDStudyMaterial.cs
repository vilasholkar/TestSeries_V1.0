using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.StudyMaterial;

namespace DataAccessLayer
{
    public interface IDStudyMaterial
    {
        List<StudyMaterialViewModel> GetStudyMaterial();

        string AddUpdateStudyMaterial(StudyMaterialViewModel objStudyMaterial);

        string DeleteStudyMaterial(StudyMaterialViewModel objStudyMaterial);
    }
}
