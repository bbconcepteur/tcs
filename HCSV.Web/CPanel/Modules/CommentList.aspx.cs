using CPanel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class CommentList : System.Web.UI.Page

    {
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

            }
            searchComments();
        }

        public void searchComments()
        {
            //get info from session for finding Comment
            SessionForFindingComment objSessionForFindingComment = (SessionForFindingComment)Session[Commons.ConstValues.SESSION_COMMENT];
            if (objSessionForFindingComment != null)
            {
                drpLanguages.SelectedValue = objSessionForFindingComment.ID_LANGUAGE;
            }

            //Search Comment
            int intLanguageID = Convert.ToInt32(drpLanguages.SelectedValue);
            // Sap xep theo order va ten
            
            //var lstComments = entities.jos_Comment.Where(x => (x.lang_id == intLanguageID) && (x.published == true)).OrderBy(m => new { m.order,m.name}).ToList();

            List<jos_comment> lstComments = new List<jos_comment>();
            getTreeViewOfComments(0, null, lstComments, intLanguageID);
            grvComments.DataSource = lstComments;
            grvComments.DataBind();

            //reset is null for session
            Session[Commons.ConstValues.SESSION_COMMENT] = null;
        }

        protected void getTreeViewOfComments(int intLevel, string strObjID, List<jos_comment> lstResult, int intLangID)
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

            var lstObjects = entities.jos_comment.ToList();


            if (lstObjects != null)
            {
                foreach (var item in lstObjects)
                {
                    string strLine = "";
                    strLine = strLine.PadLeft(intLevel * 6, (char)Commons.TitleConst.getTitleConst("TITLE_ICON").ElementAt(0));
                    jos_comment obj = new jos_comment();
                    obj.ID = item.ID;
                    obj.content_id = item.content_id;
                    obj.CommentContent = item.CommentContent;
                    obj.SenderName = item.SenderName;
                    obj.SenderEmail = item.SenderEmail; 
                    obj.SentDate = item.SentDate;
                    obj.LastEdited = item.LastEdited;
                    obj.Status = item.Status; 
                    lstResult.Add(obj);
                    getTreeViewOfComments(intLevel + 1, item.ID.ToString(), lstResult, intLangID);
                }
            }
        }

        protected void getUsers()
        {

        }

        protected void grvComments_DataBinding(object sender, EventArgs e)
        {
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstComments = grvComments.GetSelectedFieldValues(grvComments.KeyFieldName);
            foreach (int i in lstComments)
            {
                jos_comment objComment = entities.jos_comment.FirstOrDefault(x => x.ID == i);
                if (objComment != null) objComment.Status = 0;
                entities.SaveChanges();
            }
            Response.Redirect(Commons.ConstURL.URL_COMMENT_VIEW);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //set session for finding Comment
            setSessionForFindingComment();
            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_COMMENT_EDIT);
        }

        protected void drpLanguage_DataBinding(object sender, EventArgs e)
        {

        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchComments();
        }

        protected void btnViewComment_Click(object sender, EventArgs e)
        {
            //set session for finding Comment
            setSessionForFindingComment();

            //redirect
            Response.Redirect(Commons.ConstURL.URL_COMMENT_EDIT );
        }

        //set session for finding Comment
        protected void setSessionForFindingComment()
        {
            SessionForFindingComment objSessionForFindingComment = new SessionForFindingComment();
            objSessionForFindingComment.ID_COMMENT = txtCommentID.Text;
            objSessionForFindingComment.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_COMMENT] = objSessionForFindingComment;
        } 
    }
}