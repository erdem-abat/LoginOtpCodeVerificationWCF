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
        OtpModel CheckOtp(ValidationModel validationModel);

    }


    [DataContract]
    public class ValidationModel
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
    public class OtpModel
    {
        string otpCode = string.Empty;
        bool status = false;

        [DataMember]
        public string OtpCode
        {
            get { return otpCode; }
            set { otpCode = value; }
        }

        [DataMember]
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
