using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LoginOtpCodeVerification
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        OtpResponseModel CheckOtp(EmailSentModel emailSentModel);

        [OperationContract]
        ValidateResponseModel ValidateOtp(string email, string otpCode);

    }


    [DataContract]
    public class EmailSentModel
    {
        string email = string.Empty;

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }

    [DataContract]
    public class OtpResponseModel
    {
        string response = string.Empty;

        [DataMember]
        public string Response
        {
            get { return response; }
            set { response = value; }
        }
    }

    [DataContract]
    public class ValidateResponseModel
    {
        bool response = false;

        [DataMember]
        public bool Response
        {
            get { return response; }
            set { response = value; }
        }
    }
}
