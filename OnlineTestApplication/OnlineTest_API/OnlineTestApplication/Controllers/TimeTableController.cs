using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels.TimeTable;

namespace OnlineTestApplication.Controllers
{
    public class TimeTableController : ApiController
    {
        private readonly IBTimeTable _iBTimeTable;
        public TimeTableController(IBTimeTable iBTimeTable)
        {
            _iBTimeTable = iBTimeTable;

        }

        [HttpGet]
        [Route("api/GetTimeTable", Name = "GetTimeTable")]
        public HttpResponseMessage GetTimeTable()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBTimeTable.GetTimeTable());
        }

        [HttpPost]
        [Route("api/AddUpdateTimeTable", Name = "AddUpdateTimeTable")]
        public HttpResponseMessage AddUpdateTimeTable(TimeTableViewModel objTimeTable)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBTimeTable.AddUpdateTimeTable(objTimeTable));
           // return null;
        }

        [HttpPost]
        [Route("api/GetTimeTableByDate", Name = "GetTimeTableByDate")]
        public HttpResponseMessage GetTimeTableByDate(TimeTableViewModel objTimeTable)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _iBTimeTable.GetTimeTableByDate(objTimeTable));
        }

        private void SendMail()
        {
            // Gmail Address from where you send the mail
            var fromAddress = "vaibhav007patidar@gmail.com";
            // any address where the email will be sending
            var toAddress = "vaibhav007patidar@gmail.com";
            //Password of your gmail address
            const string fromPassword = "********";
            // Passing the values and make a email formate to display
            string subject = "Test";
            string body = "From: Testing";
            //string body = "From: " + YourName.Text + "\n";
            //body += "Email: " + YourEmail.Text + "\n";
            //body += "Subject: " + YourSubject.Text + "\n";
            //body += "Question: \n" + Comments.Text + "\n";
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
        }
    }
}
