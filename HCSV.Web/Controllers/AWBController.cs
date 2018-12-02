using System;
using System.Web.Mvc;

namespace HCSV.Web.Controllers
{
    public class AWBController : BaseController
    {

        //public bool checkLogin()
        //{
        //    //Check Login
        //    var objUser = UnitOfWork.jos entities.jos_awb_users.Where(x => "aitsagent".Equals(x.user_name)).FirstOrDefault();
        //    if (objUser != null)
        //    {
        //        Session["user_code"] = objUser.user_code;
        //        Session["lg_codesgshdgsksasa"] = objUser.user_code;
        //        return true;
        //    }
        //    return false;
        //}

        public ActionResult SearchAWB_Direct_Consignee()
        {
            string strawbFirst = Request.Form["awbFirst"];
            string strawbLast = Request.Form["awbLast"];

            return RedirectToAction("View_03_cs", new { awbFirst = strawbFirst, awbLast = strawbLast });
        }

        //public ActionResult SearchAWB_ApplicableCharges()
        //{
        //    string strLang = Request.QueryString["lang"];
        //    if (HCSV.Core.CommonTCS.Commons.LANGUAGE_ENGLISH.Equals(strLang))//English
        //    {
        //        return RedirectToAction("View_Body_BangGia1_e");
        //    }
        //    else//Vietnamese
        //    {
        //        return RedirectToAction("View_Body_BangGia1");
        //    }
        //}

        //public ActionResult SearchAWB_Agent()
        //{
        //    if (checkLogin())
        //    {
        //        Session["navigation"] = "agent";
        //        return RedirectToAction("View_03_cs");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult SearchAWB_Airline()
        //{
        //    if (checkLogin())
        //    {
        //        Session["navigation"] = "airline";
        //        return RedirectToAction("View_03_cs");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}


        public ActionResult ChangePass()
        {
            return View();
        }

        public ActionResult View_03_cs()
        {
            return View();
        }

        public ActionResult View_Body_BangGia1()
        {
            return View();
        }

        public ActionResult View_Body_BangGia2()
        {
            return View();
        }

        public ActionResult View_Body_BangGia3()
        {
            return View();
        }

        public ActionResult View_Body_BangGia4()
        {
            return View();
        }

        public ActionResult View_Body_BangGia1_e()
        {
            return View();
        }

        public ActionResult View_Body_BangGia2_e()
        {
            return View();
        }

        public ActionResult View_Body_BangGia3_e()
        {
            return View();
        }

        public ActionResult View_Body_BangGia4_e()
        {
            return View();
        }


        public ActionResult View_Letter_v_cs()
        {
            return View();
        }

        public ActionResult View_07_cs()
        {
            return View();
        }

        public ActionResult View_08_cs()
        {
            return View();
        }

        public ActionResult m1()
        {
            return View();
        }

        public ActionResult View_lk_yc_imp_cs()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["checkin_date"]))
            {
                ViewData["expires_date"] = Request.QueryString["checkin_date"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["checkin_hour"]))
            {
                ViewData["hh"] = Request.QueryString["checkin_hour"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["checkin_minute"]))
            {
                ViewData["pp"] = Request.QueryString["checkin_minute"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["awbFirst"]))
            {
                Session["awbFirst"] = Request.QueryString["awbFirst"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["awbLast"]))
            {
                Session["awbLast"] = Request.QueryString["awbLast"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["section"]))
            {
                Session["section"] = Request.QueryString["section"];
            }

            return View();
        }

        public ActionResult test()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["checkin_date"]))
            {
                ViewData["expires_date"] = Request.QueryString["checkin_date"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["checkin_hour"]))
            {
                ViewData["hh"] = Request.QueryString["checkin_hour"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["checkin_minute"]))
            {
                ViewData["pp"] = Request.QueryString["checkin_minute"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["awbFirst"]))
            {
                Session["awbFirst"] = Request.QueryString["awbFirst"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["awbLast"]))
            {
                Session["awbLast"] = Request.QueryString["awbLast"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["section"]))
            {
                Session["section"] = Request.QueryString["section"];
            }

            return View();
        }

        
	}
}