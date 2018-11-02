using System.Web.Mvc;

namespace HCSV.Web.Controllers
{    
    public class AboutController : BaseController
    {
        
        //cpanelEntities entities = new cpanelEntities();
        //
        // GET: /About/
        public ActionResult Index()
        {           
            /*int intLangId = Commons.Commons.getLanguageID (this.Request, entities);

            jos_content objContent = new jos_content();

            //get page's name & Category ID
            int intCatID = Commons.Commons.getCategoryIDFromURL(this.Url.Action(), objContent, entities, intLangId);

            objContent.BANNER = entities.jos_content.Where(x => x.catid == intCatID && x.position.Equals("BANNER") && x.state == 1 && x.lang_id == intLangId).ToList();

            objContent.RIGHT = entities.jos_content.Where(x => x.catid == intCatID && x.position.Equals("RIGHT") && x.state == 1 && x.lang_id == intLangId).FirstOrDefault();
            return View(objContent);*/
            return View();
        }

        
	}
}