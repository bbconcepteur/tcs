using System.Web.Mvc;

namespace HCSV.Core.reCAPTCHA
{
    public class CaptchaValidatorAttribute : ActionFilterAttribute
    {
        public string ErrorMessage { get; set; }

        public string RequiredMessage { get; set; }

        public string PrivateKey { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RecaptchaResponse recaptchaResponse = new RecaptchaValidator()
            {
                PrivateKey = (string.IsNullOrWhiteSpace(this.PrivateKey) ? RecaptchaKeyHelper.ParseKey("[reCaptchaPrivateKey]") : this.PrivateKey),
                RemoteIP = filterContext.HttpContext.Request.UserHostAddress,
                Response = filterContext.HttpContext.Request.Form["g-recaptcha-response"]
            }.Validate();
            if (!recaptchaResponse.IsValid)
                ((Controller)filterContext.Controller).ModelState.AddModelError("ReCaptcha", this.GetErrorMessage(recaptchaResponse.ErrorCode));
            filterContext.ActionParameters["captchaValid"] = (object)recaptchaResponse.IsValid;
            base.OnActionExecuting(filterContext);
        }

        private string GetErrorMessage(string errorCode)
        {
            string str;
            switch (errorCode)
            {
                case "captcha-required":
                    str = string.IsNullOrWhiteSpace(this.RequiredMessage) ? "Captcha field is required." : this.RequiredMessage;
                    break;
                case "missing-input-secret":
                    str = "The secret parameter is missing.";
                    break;
                case "invalid-input-secret":
                    str = "The secret parameter is invalid or malformed.";
                    break;
                case "missing-input-response":
                    str = "The response parameter is missing.";
                    break;
                case "invalid-input-response":
                    str = string.IsNullOrWhiteSpace(this.ErrorMessage) ? "Incorrect Captcha" : this.ErrorMessage;
                    break;
                default:
                    str = string.IsNullOrWhiteSpace(this.ErrorMessage) ? "Incorrect Captcha" : this.ErrorMessage;
                    break;
            }
            return str;
        }
    }
}