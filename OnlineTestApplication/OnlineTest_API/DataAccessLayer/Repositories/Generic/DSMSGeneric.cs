using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccessLayer
{
    public class DSMSGeneric
    {
        public static string SendSingleSMS(string strNumber, string strMessage)
        {
            string SMSService = DGeneric.GetValue("select Value from GeneralSettings where [key]='SMSService'").ToString();
            if (SMSService == "Start")
            {
                string APIKey = "fc0e7fdc-2766-485b-800d-dcdce1ad6728";
                string SenderID = "AAYAMC";
                string MobileNo = strNumber;
#if debug
                string MobileNo = "8871171445";
#endif
                string Message = strMessage;
                string Route = "1";
                string url = string.Format("http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey={0}&senderid={1}&channel=2&DCS=0&flashsms=0&number={2}&text={3}&route={4}", APIKey, SenderID, MobileNo, Message, Route);
                string Result = string.Empty;

                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] data = encoder.GetBytes(Message);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                request.Expect = "application/json";
                request.GetRequestStream().Write(data, 0, data.Length);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (((HttpWebResponse)response).StatusDescription.Equals("OK"))
                {
                    Result = response.StatusDescription;
                }
                else
                {
                    Result = response.StatusDescription;
                }
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var result = readStream.ReadToEnd();

                readStream.Close();
                response.Close();
                //var result;
                return Result;
            }
            else
            {
                return "SMS Services are stoped. Please start it from settings ";

            }
        }

        public static string SendBulkSMS(string strNumber, string strMessage)
        {
            string SMSService = DGeneric.GetValue("select Value from GeneralSettings where [key]='SMSService'").ToString();
            if (SMSService == "Start")
            {
                string APIKey = "fc0e7fdc-2766-485b-800d-dcdce1ad6728";
                string SenderID = "AAYAMC";
                string MobileNo = strNumber;
                string Message = strMessage;
                string Route = "1";
                string url = string.Format("http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey={0}&senderid={1}&channel=2&DCS=0&flashsms=0&number={2}&text={3}&route={4}", APIKey, SenderID, MobileNo, Message, Route);

                string Result = string.Empty;

                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] data = encoder.GetBytes(Message);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                request.Expect = "application/json";
                request.GetRequestStream().Write(data, 0, data.Length);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (((HttpWebResponse)response).StatusDescription.Equals("OK"))
                {
                    Result = "Message Send Successfully";
                }
                else
                {
                    Result = response.StatusDescription;
                }
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var result = readStream.ReadToEnd();


                readStream.Close();
                response.Close();
                //var result;
                return Result;
            }
            else
            {
                return "SMS Services are stoped. Please start it from settings ";

            }

        }

    }
}
