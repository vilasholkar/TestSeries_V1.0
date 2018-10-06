using BusinessAccessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels.Question;
namespace OnlineTestApplication.Controllers
{
    public class QuizController : ApiController
    {
        private readonly IBQuiz _iBQuiz;
        public QuizController(IBQuiz iBQuiz)
        {
            _iBQuiz = iBQuiz;
        }
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
     
        [HttpGet]
        [Route("api/GetQuiz", Name = "GetQuiz")]
        public HttpResponseMessage GetQuiz(string testID)
        {
            return Request.CreateResponse(_iBQuiz.GetQuiz(1));
        }
        [HttpPost]
        [Route("api/SubmitQuiz", Name = "SubmitQuiz")]
        public HttpResponseMessage SubmitQuiz(Object questionViewModel)
        {
            return Request.CreateResponse(HttpStatusCode.OK,"test");
        }

    }
}
