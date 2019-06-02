using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using ViewModels.StudyMaterial;
using ViewModels;

namespace BusinessAccessLayer
{
    public class BStudyMaterial : IBStudyMaterial
    {
        private readonly IDStudyMaterial _iDStudyMaterial;
        public BStudyMaterial(IDStudyMaterial iDStudyMaterial)
        {
            _iDStudyMaterial = iDStudyMaterial;
        }
        public Response<List<StudyMaterialViewModel>> GetStudyMaterial()
        {
            var StudyMaterialData = _iDStudyMaterial.GetStudyMaterial();
            if (StudyMaterialData != null)
            {
                return new Response<List<StudyMaterialViewModel>>
                {
                    IsSuccessful = true,
                    Object = StudyMaterialData,
                    Message = "Success"
                };
            }
            else
            {
                return new Response<List<StudyMaterialViewModel>>
                {
                    IsSuccessful = false,
                    Message = "error",
                    Object = null
                };
            }
        }
        public string AddUpdateStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            return _iDStudyMaterial.AddUpdateStudyMaterial(objStudyMaterial);
        }
        public string DeleteStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            return _iDStudyMaterial.DeleteStudyMaterial(objStudyMaterial);
        }
    }
}
