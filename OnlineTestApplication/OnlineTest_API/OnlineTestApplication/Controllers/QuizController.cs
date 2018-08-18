using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineTestApplication.Controllers
{
    public class QuizController : ApiController
    {
        public class Option
        {
            public int id { get; set; }
            public int questionId { get; set; }
            public string name { get; set; }
            public bool isAnswer { get; set; }
        }

        public class QuestionType
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool isActive { get; set; }
        }

        public class Question
        {
            public int id { get; set; }
            public string name { get; set; }
            public int questionTypeId { get; set; }
            public List<Option> options { get; set; }
            public QuestionType questionType { get; set; }
        }

        public class RootObject
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public List<Question> questions { get; set; }
        }
        public HttpResponseMessage GetQuiz(String Parm)
        {
            var personlist = new RootObject();
            using (StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/" + Parm)))
            {
                personlist = JsonConvert.DeserializeObject<RootObject>(sr.ReadToEnd());
            }
            return Request.CreateResponse(personlist);
        }
    }
}
