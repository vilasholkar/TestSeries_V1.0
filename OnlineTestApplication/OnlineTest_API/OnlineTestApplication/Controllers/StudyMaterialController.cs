using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels.StudyMaterial;

namespace OnlineTestApplication.Controllers
{
    public class StudyMaterialController : ApiController
    {
        private readonly IBStudyMaterial _iBStudyMaterial;
        public StudyMaterialController(IBStudyMaterial iBStudyMaterial)
        {
            _iBStudyMaterial = iBStudyMaterial;
        }
        [HttpGet]
        [Route("api/GetStudyMaterial", Name = "GetStudyMaterial")]
        public HttpResponseMessage GetStudyMaterial()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudyMaterial.GetStudyMaterial());
        }

        [HttpPost]
        [Route("api/AddUpdateStudyMaterial", Name = "AddUpdateStudyMaterial")]
        public HttpResponseMessage AddUpdateTopic(StudyMaterialViewModel objStudyMaterial)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudyMaterial.AddUpdateStudyMaterial(objStudyMaterial));
        }

        [HttpPost]
        [Route("api/DeleteStudyMaterial", Name = "DeleteStudyMaterial")]
        public HttpResponseMessage DeleteStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudyMaterial.DeleteStudyMaterial(objStudyMaterial));
        }
    }
}
