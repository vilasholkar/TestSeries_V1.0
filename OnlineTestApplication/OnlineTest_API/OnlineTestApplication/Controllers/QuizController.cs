﻿using BusinessAccessLayer;
using Newtonsoft.Json;
using OnlineTestApplication.CustomFilters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels.Question;
using ViewModels.Test;
namespace OnlineTestApplication.Controllers
{
    [CustomExceptionFilter]
    public class QuizController : ApiController
    {
        private readonly IBQuiz _iBQuiz;
        public QuizController(IBQuiz iBQuiz)
        {
            _iBQuiz = iBQuiz;
        }
        public class Option
        {
            public int optionID { get; set; }
            public int questionID { get; set; }
            public string option { get; set; }
            public bool isAnswer { get; set; }
            public bool selected { get; set; }
            
        }

        public class QuestionType
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool isActive { get; set; }
        }

        public class Question
        {
            public int questionID { get; set; }
            //public int questionTypeID { get; set; }
            //public string image_English { get; set; }
            //public string image_Hindi { get; set; }
            //public List<Option> options { get; set; }
           // public QuestionType questionType { get; set; }
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
        public HttpResponseMessage GetQuiz(int OnlineTestID, int StudentID)
        {
            return Request.CreateResponse(_iBQuiz.GetQuiz(OnlineTestID, StudentID));
        }
        [HttpPost]
        [Route("api/SubmitQuiz", Name = "SubmitQuiz")]
        public HttpResponseMessage SubmitQuiz(QuizViewModel QuizViewModel)
       {
            return Request.CreateResponse(HttpStatusCode.OK,_iBQuiz.SubmitQuiz(QuizViewModel));
        }

    }
}
