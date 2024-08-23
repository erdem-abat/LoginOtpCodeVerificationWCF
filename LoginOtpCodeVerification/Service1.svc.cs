using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.UI;

namespace LoginOtpCodeVerification
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public OtpModel CheckOtp(ValidationModel validationModel)
        {
            Random r = new Random();
            string otp = r.Next(10001,99999).ToString();

            try
            {
                using (MailMessage mm = new MailMessage("notifyproject101@hotmail.com", validationModel.Email))
                {
                    mm.Subject = "Otp Onay";
                    string msg = "Otp kodunuz: " + otp;
                    mm.Body = msg;
                    mm.IsBodyHtml = false;

                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.Host = "smtp-mail.outlook.com";
                        smtpClient.EnableSsl = true;
                        smtpClient.Port = 587;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential("notifyproject101@hotmail.com", "Erdemabat1.");

                        smtpClient.Send(mm);
                    }
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine("SMTP Exception: " + smtpEx.Message);
                Console.WriteLine("Status Code: " + smtpEx.StatusCode);
                if (smtpEx.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + smtpEx.InnerException.Message);
                }
                return new OtpModel
                {
                    OtpCode = otp,
                    Status = false
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return new OtpModel
                {
                    OtpCode = otp,
                    Status = false
                };
            }

            return new OtpModel
            {
                OtpCode = otp,
                Status = true
            };
        }

    }
}
