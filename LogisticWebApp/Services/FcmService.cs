using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Logistic.Web.Services
{
    public class FcmService
    {
        private static readonly string  serverURL= "https://fcm.googleapis.com/fcm/send";
       

        private string GenerateJson(string title, string token)
        {
            string data = @"{""notification"":{""title"":""" + title + @""",""body"":""Your response to claim approved"",""icon"":""/favicon.png"" }, ""to"":""" + token + @"""}";
            return data;
        }

        public string SendNotification(string title, string token)
        {
            var client = new WebClient();

            client.Headers.Add("Content-Type", "application/json");
            client.Headers.Add("Authorization", "key=AAAAwWUMwg4:APA91bGCIR6Ir_GloO-VbQlE2t8-JbRJt5RQ3miBA5_DK1yQMS91ZENS4HzF8sCpCEQ_vOJiCcm9oUMvqUqInr1eFxxexgXWpowdPEdAjWC7nTv8qeeuaoikjr-iU5YWsj9f8dOm1Epc");

            var data=GenerateJson(title, token);

            var result = client.UploadString(serverURL, "POST", data);

            return result;
        }



    }
}
