using CPanel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class ContactList : System.Web.UI.Page

    {
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

            }
            searchContacts();
        }

        public void searchContacts()
        {
            //get info from session for finding Contact
            SessionForFindingContact objSessionForFindingContact = (SessionForFindingContact)Session[Commons.ConstValues.SESSION_CONTACT];
            if (objSessionForFindingContact != null)
            {
                drpLanguages.SelectedValue = objSessionForFindingContact.ID_LANGUAGE;
            }

            //Search Contact
            int intLanguageID = Convert.ToInt32(drpLanguages.SelectedValue);
            // Sap xep theo order va ten
            
            //var lstContacts = entities.jos_contact.Where(x => (x.lang_id == intLanguageID) && (x.published == true)).OrderBy(m => new { m.order,m.name}).ToList();

            List<jos_contact> lstContacts = new List<jos_contact>();
            getTreeViewOfContacts(0, null, lstContacts, intLanguageID);
            grvContacts.DataSource = lstContacts;
            grvContacts.DataBind();

            //reset is null for session
            Session[Commons.ConstValues.SESSION_CONTACT] = null;
        }

        protected void getTreeViewOfContacts(int intLevel, string strObjID, List<jos_contact> lstResult, int intLangID)
        {
            int intObjID = 0; bool blNumber = false;
            if (!String.IsNullOrEmpty(strObjID))
            {
                intObjID = Convert.ToInt32(strObjID);
                blNumber = true;
            }
            else //Begin --> reset DropDownlist
            {
                lstResult.Clear();
            }

            var lstObjects = entities.jos_contact.Where(x => ((blNumber && x.id_parent == intObjID) || (blNumber == false && x.id_parent == 0))
                                                   && (x.lang_id == intLangID)
                                                   ).OrderBy(y => y.order).ToList();


            if (lstObjects != null)
            {
                foreach (var item in lstObjects)
                {
                    string strLine = "";
                    strLine = strLine.PadLeft(intLevel * 6, (char)Commons.TitleConst.getTitleConst("TITLE_ICON").ElementAt(0));
                    jos_contact obj = new jos_contact();
                    obj.id = item.id;
                    obj.id_parent = item.id_parent;
                    obj.order = item.order;
                    obj.name = item.name;
                    obj.ext_tel = item.ext_tel;
                    obj.email = item.email;

                    obj.department_manager = item.department_manager;
                    obj.phone = item.phone;
                    obj.hotline = item.hotline;
                    obj.title_of_manager = item.title_of_manager;
                    obj.published = item.published;

                    if (intLevel == 0) //Begining Level
                    {
                        obj.name = strLine + item.name;
                    }
                    else obj.name = strLine + item.name;
                    lstResult.Add(obj);
                    getTreeViewOfContacts(intLevel + 1, item.id.ToString(), lstResult, intLangID);
                }
            }
        }

        protected void getUsers()
        {

        }

        protected void grvContacts_DataBinding(object sender, EventArgs e)
        {
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstContacts = grvContacts.GetSelectedFieldValues(grvContacts.KeyFieldName);
            foreach (int i in lstContacts)
            {
                jos_contact objContact = entities.jos_contact.Where(x => x.id == i).FirstOrDefault();
                objContact.published = false;
                entities.SaveChanges();
            }
            Response.Redirect(Commons.ConstURL.URL_CONTACT_VIEW);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //set session for finding Contact
            setSessionForFindingContact();
            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_CONTACT_EDIT);
        }

        protected void drpLanguage_DataBinding(object sender, EventArgs e)
        {

        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchContacts();
        }

        protected void btnViewContact_Click(object sender, EventArgs e)
        {
            //set session for finding Contact
            setSessionForFindingContact();

            //redirect
            Response.Redirect(Commons.ConstURL.URL_CONTACT_EDIT);
        }

        //set session for finding Contact
        protected void setSessionForFindingContact()
        {
            SessionForFindingContact objSessionForFindingContact = new SessionForFindingContact();
            objSessionForFindingContact.ID_CONTACT = txtContactID.Text;
            objSessionForFindingContact.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CONTACT] = objSessionForFindingContact;
        }


        protected void btnViewTranslation_Click(object sender, EventArgs e)
        {

            //set session for finding Contact
            setSessionForFindingContact();

            //redirect
            Response.Redirect(Commons.ConstURL.URL_CONTACT_TRANSLATION);

        }


    }
}