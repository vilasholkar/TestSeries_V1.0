using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace OnlineTestApplication.CustomFilters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private IDLog _iDLog;
        //public CustomExceptionFilter(IDLog iDLog)
        //{
        //    _iDLog = iDLog;
        //}
        public override void OnException(HttpActionExecutedContext context)
        {
            Exception ex = context.Exception;
            _iDLog = DLog.GetInsatance;
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage()
                {
                    ReasonPhrase = "Error Occured:" + ex.Message + ex.InnerException,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            //Logging Error to Database
            _iDLog.LogException(context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.Name, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name.ToString());

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log/"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log/");
            }
            //Logging Error to a txt file
            using (var w = new StreamWriter(File.Open(AppDomain.CurrentDomain.BaseDirectory + "Log/" + "Error" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt", FileMode.OpenOrCreate), Encoding.UTF8))
            {
                w.WriteLine("\r\nLog Entry : ");
                w.WriteLine("{0} {1} UserName: {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(),HttpContext.Current.User.Identity.Name.ToString());
                string err = "Error Message:" + ex.Message;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }
        }
    }
}