using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
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

        [HttpGet]
        [Route("api/GetStudyMaterialByID", Name = "GetStudyMaterialByID")]
        public HttpResponseMessage GetStudyMaterialByID(int StudyMaterialID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudyMaterial.GetStudyMaterialByID(StudyMaterialID));
        }
        
        [HttpPost]
        [Route("api/AddUpdateStudyMaterial", Name = "AddUpdateStudyMaterial")]
        public HttpResponseMessage AddUpdateStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudyMaterial.AddUpdateStudyMaterial(objStudyMaterial));
        }

        [HttpPost]
        [Route("api/DeleteStudyMaterial", Name = "DeleteStudyMaterial")]
        public HttpResponseMessage DeleteStudyMaterial(StudyMaterialViewModel objStudyMaterial)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudyMaterial.DeleteStudyMaterial(objStudyMaterial));
        }

        [HttpPost]
        [Route("api/UploadFile", Name = "UploadFile")]
        public HttpResponseMessage UploadFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string dynamicPath = string.Empty;
                    dynamicPath = "/Uploads/StudyMaterial/";
                    var filePath = HttpContext.Current.Server.MapPath("~" + dynamicPath + string.Format("File_{0}", postedFile.FileName));
                    var dbpath = dynamicPath + string.Format("File_{0}", postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    return Request.CreateResponse(HttpStatusCode.OK, dbpath);
                    
                }
            }
            return response; 

            //string imagename = null;
            //var httpRequest = HttpContext.Current.Request;
            ////Upload Image
            //var postedFile = httpRequest.Files["File"];
            //return Request.CreateResponse(HttpStatusCode.OK, _iBStudyMaterial.GetStudyMaterial());
        }

        [HttpGet]
        [Route("api/DownloadFile", Name = "DownloadFile")]
        public HttpResponseMessage DownloadFile(string filePath)
        {
            //below code locate physical file on server 
            var localFilePath = HttpContext.Current.Server.MapPath(filePath);
            // var localFilePath = HttpContext.Current.Server.MapPath("~/App_Data/File_html_gunjan.pdf");
            HttpResponseMessage response = null;
            if (!File.Exists(localFilePath))
            {
                //if file not found than return response as resource not present 
                response = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                //if file present than read file 
                var fStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read);
                //compose response and include file as content in it
                response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StreamContent(fStream)
                };
                //set content header of reponse as file attached in reponse
                response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(fStream.Name)
                };
                //set the content header content type as application/octet-stream as it      
                //returning file as reponse 
                response.Content.Headers.ContentType = new
                              MediaTypeHeaderValue("application/octet-stream");
            }
            return response;
        }
    
        [HttpGet]
        [Route("api/GetStudyMaterialBySubTopic", Name = "GetStudyMaterialBySubTopic")]
        public HttpResponseMessage GetStudyMaterialBySubjectTopicSubTopic(string SubTopicID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBStudyMaterial.GetStudyMaterialBySubTopic(SubTopicID));
        }
    }
}


