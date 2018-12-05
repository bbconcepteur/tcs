using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace CPanel.CAPTCHA
{
    public class RecaptchaValidator
    {
        private const string VerifyUrl = "https://www.google.com/recaptcha/api/siteverify";
        private string privateKey;
        private string remoteIp;
        private string response;

        public string PrivateKey
        {
            get
            {
                return this.privateKey;
            }
            set
            {
                this.privateKey = value;
            }
        }

        public string RemoteIP
        {
            get
            {
                return this.remoteIp;
            }
            set
            {
                IPAddress ipAddress = IPAddress.Parse(value);
                if (ipAddress == null || ipAddress.AddressFamily != AddressFamily.InterNetwork && ipAddress.AddressFamily != AddressFamily.InterNetworkV6)
                    throw new ArgumentException("Expecting an IP address, got " + (object)ipAddress);
                this.remoteIp = ipAddress.ToString();
            }
        }

        public string Response
        {
            get
            {
                return this.response;
            }
            set
            {
                this.response = value;
            }
        }

        private void CheckNotNull(object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name);
        }

        public RecaptchaResponse Validate()
        {
            this.CheckNotNull((object)this.PrivateKey, "PrivateKey");
            this.CheckNotNull((object)this.RemoteIP, "RemoteIp");
            if (string.IsNullOrWhiteSpace(this.response))
                return RecaptchaResponse.CaptchaRequired;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
            httpWebRequest.ProtocolVersion = HttpVersion.Version11;
            httpWebRequest.Timeout = 30000;
            httpWebRequest.Method = "POST";
            httpWebRequest.UserAgent = "reCAPTCHA/TCS";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = Encoding.ASCII.GetBytes(string.Format("secret={0}&response={1}&remoteip={2}", (object)HttpUtility.UrlEncode(this.PrivateKey), (object)HttpUtility.UrlEncode(this.Response), (object)HttpUtility.UrlEncode(this.RemoteIP)));
            using (Stream requestStream = httpWebRequest.GetRequestStream())
                requestStream.Write(bytes, 0, bytes.Length);
            CaptchaResponse captchaResponse = new CaptchaResponse();
            try
            {
                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    using (TextReader textReader = (TextReader)new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        captchaResponse = (CaptchaResponse)JsonConvert.DeserializeObject<CaptchaResponse>(textReader.ReadToEnd());
                }
            }
            catch
            {
                return RecaptchaResponse.RecaptchaNotReachable;
            }
            if (captchaResponse.Success)
                return RecaptchaResponse.Valid;
            string errorCode = string.Empty;
            if (captchaResponse.ErrorCodes != null)
                errorCode = string.Join(",", captchaResponse.ErrorCodes.ToArray());
            return new RecaptchaResponse(false, errorCode);
        }
    }
}
