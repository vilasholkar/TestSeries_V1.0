using Newtonsoft.Json;
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

        //public static string SendAndroidNotification(string Token,string Title,string Body)
        //{
        //    string serverKey = "AIzaSyCbHahNdXea5XmqiHb6ZqoY-Tso6LUx_ug";
        //    var result = "-1";
        //    var webAddr = "https://fcm.googleapis.com/fcm/send";
        //    try
        //    {
        //        //var regID = "f3K5PXqpiZw:APA91bE9ciHkZUZi08OU275UjRmi41f5Z8pV5MzbAgJJNWEFKT5y4tDasEef2XQKQRjgza4xE7e8e7UNbTuRfP_okJPyqJE_UwaSHuh2_6Rnq7ktRmjIfrcDcbQfixpheQDrmVipi6S4";
        //        var regID = "fVfadVvz12c:APA91bHuWO-3dl8duadxHCdyFyd_nX00L9gJV_MOOcwsWVSf3NiDf7U9uiCXE2xbsqX_v0dYNrc6DNehYzouI4E-9seKj5eSwVZL3ieTpZ-H9QBAIxKxO-4rJUOnY2eEu4JS19v3QNsH";

        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
        //        httpWebRequest.Method = "POST";

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            //string json = "{\"to\": \"" + regID + "\",\"notification\": {\"title\": \"New deal\",\"body\": \"1000% deal!\"},\"priority\":10}";

        //            //string json = "@{\"to\": \"" + Token + "\",\"notification\": " +
        //            //              "{\"title\": \""+ Title + "\",\"body\": \"" + Body + "\"},\"priority\":10}";

        //             // string json = "{\"to\" : \""+regID+"\",\"collapse_key\" : \"type_a\",\"notification\" : {\"body\" : \"Body of Your Notification\",\"title\": \"Title of Your Notification\"}, \"data\" : { \"body\" : \"Body of Your Notification in Data\",  \"title\": \"Title of Your Notification in Title\", \"key_1\" : \"Value for key_1\",\"key_2\" : \"Value for key_2\"}}";

        //            string json = @"{
        //            ""to"": ""fVfadVvz12c:APA91bHuWO-3dl8duadxHCdyFyd_nX00L9gJV_MOOcwsWVSf3NiDf7U9uiCXE2xbsqX_v0dYNrc6DNehYzouI4E-9seKj5eSwVZL3ieTpZ-H9QBAIxKxO-4rJUOnY2eEu4JS19v3QNsH"",
        //            ""data"": {
        //                ""ShortDesc"": ""Some short desc"",
        //                ""IncidentNo"": ""000000000"",
        //                ""Description"": ""detail desc""
        //              },
        //              ""notification"": {
        //                            ""title"": ""ServiceNow: Incident No. number"",
        //                ""text"": ""This is Notification"",
        //            ""sound"":""default""
        //              }
        //                    }";

        //            //registration_ids, array of strings -  to, single recipient
        //            streamWriter.Write(json);
        //            streamWriter.Flush();
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            result = streamReader.ReadToEnd();
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return "err";
        //    }
        //}

        public static string SendAndroidNotification(string deviceToken, string strTitle, string strBody)
        {
            string status = "";
            try
            {
                string applicationID = "AIzaSyCFZUr715Kp6y86qtCHjL2BE7yNnYJZTDY";
                string senderId = "965750146557";
               // string deviceId = deviceToken;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceToken,
                    //data = new {
                    //    ShortDesc= "Some short desc",
                    //    IncidentNo= "000000000",
                    //    Description= "detail desc"
                    //},
                    notification = new
                    {
                        body = strBody,
                        title = strTitle,
                        sound = ""
                    }
                };
                //string output = JsonConvert.SerializeObject(data);
                //var serializer = new JavaScriptSerializer();
                //var json = serializer.Serialize(data);
                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                status = sResponseFromServer;
                            }
                        }
                    }
                    status = "Success :"+ status;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                status = "failure : "+str;
            }
            return status;

        }
       
    }
}
