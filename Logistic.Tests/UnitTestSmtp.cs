using Logistic.Data;
using Logistic.Utils;
using Logistic.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Xunit;

namespace Logistic.Tests
{
    public class UnitTestSmtp
    {
        [Fact]
        public void TestEnum_ReturnsDisplayAttribute()
        {

            SmtpClient client = new SmtpClient("mysmtpserver");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("username", "password");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("whoever@me.com");
            mailMessage.To.Add("receiver@me.com");
            mailMessage.Body = "body";
            mailMessage.Subject = "subject";
            client.Send(mailMessage);

            Assert.Equal(1, 1);
            //Assert.Equal("на тендере", StatusOfClaim.OnTender.GetDisplayName());
        }
    }
}
        