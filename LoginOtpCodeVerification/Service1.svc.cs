﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace LoginOtpCodeVerification
{
    public class Service1 : IService1
    {
        private static Random r = new Random();
        private static Dictionary<string, string> otpDictionary = new Dictionary<string, string>();

        public OtpResponseModel CheckOtp(EmailSentModel emailSentModel)
        {
            string otp = GenerateOtp();
            otpDictionary[emailSentModel.Email] = otp;

            try
            {
                using (MailMessage mm = new MailMessage("notifyproject101@hotmail.com", emailSentModel.Email))
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
                return new OtpResponseModel
                {
                    Response = "email gönderilemedi."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return new OtpResponseModel
                {
                    Response = "email gönderilemedi."
                };
            }

            return new OtpResponseModel
            {
                Response = "email başarılı bir şekilde gönderildi."
            };
        }
        private string GenerateOtp()
        {
            return r.Next(100000, 999999).ToString();
        }

        public ValidateResponseModel ValidateOtp(string email, string otpCode)
        {
            ValidateResponseModel res = new ValidateResponseModel();

            if (string.IsNullOrWhiteSpace(email) || !otpDictionary.ContainsKey(email))
            {
                res.Response = false;
                return res;
            }

            if (otpDictionary[email] == otpCode)
            {
                res.Response = true;
                otpDictionary.Remove(email); 
                return res;
            }

            res.Response = false;
            return res;
        }
    }
}