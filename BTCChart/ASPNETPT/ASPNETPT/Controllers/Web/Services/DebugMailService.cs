using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPT.Controllers.Web.Services
{
    public class DebugMailService : IMailService

    {
        public void SendMail(string to, string @from, string subject, string body)
        {
            Debug.WriteLine($"Sending to: {to} by: {from} about: {subject}");
        }
    }
}
