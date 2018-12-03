
using System;
using System.Collections.Generic;
using System.Linq;
using CPanel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class CommentEdit : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strCommentID = null; 
                //get Comment ID from session for finding Comment
                SessionForFindingComment objSessionForFindingComment = (SessionForFindingComment)Session[Commons.ConstValues.SESSION_COMMENT];

                if (objSessionForFindingComment != null)
                {
                    strCommentID = objSessionForFindingComment.ID_COMMENT; 
                }

               
                //get Comment
                if (!String.IsNullOrEmpty(strCommentID)) //Edit Comment
                {
                    int intCommentID = Convert.ToInt32(strCommentID);
                    jos_comment objComment = entities.jos_comment.FirstOrDefault(x => x.ID == intCommentID);
                    if (objComment != null)
                    {
                        txtCommentID.Text = objComment.ID.ToString();
                        txtCommentContent.Text = objComment.CommentContent;
                        txtSenderName.Text = objComment.SenderName;
                        txtSenderEmail.Text = objComment.SenderEmail;
                        txtSentDate.Text = objComment.SentDate.ToString();
                        txtLastEdited.Text = objComment.LastEdited.ToString();
                        txtStatus.Text = objComment.Status.ToString(); 
                    }
                }
                else //Create new Comment
                {
                    ;
                }

            }

        }

        protected bool validate()
        {

            if (String.IsNullOrEmpty(txtCommentContent.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter comment content", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtSenderName.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter sender name", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtSenderEmail.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter sender email", Page);
                return false;
            }
             
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!validate())//Validation before updating            
            {
                return;
            }

            jos_comment objComment;
            //in case of create new item
            if (String.IsNullOrEmpty(txtCommentID.Text))
            {
                var lstID = entities.jos_comment.Max(m => m.ID);

                objComment = new jos_comment();
                objComment.CommentContent =  txtCommentContent.Text;
                objComment.Status = 1;
                objComment.LastEdited = DateTime.Now; 
                entities.jos_comment.Add(objComment);
            }
            else//in case of update info
            {
                int intCommentID = Convert.ToInt32(txtCommentID.Text);
                objComment = entities.jos_comment.FirstOrDefault(x => x.ID == intCommentID);
            }
            objComment.CommentContent = txtCommentContent.Text;
            objComment.SenderName = txtSenderName.Text;
            objComment.SenderEmail = txtSenderName.Text;
            objComment.SentDate = DateTime.Now; 
            objComment.Status = 0; 

            entities.SaveChanges();

            //set session for finding content
            setSessionForFindingComment();

            //redirect URL
            Response.Redirect(ConstURL.URL_COMMENT_VIEW  );
        }

        //set session for finding content
        protected void setSessionForFindingComment()
        {
            SessionForFindingComment objSessionForFindingComment = new SessionForFindingComment();
            objSessionForFindingComment.ID_COMMENT = ""; 
            Session[Commons.ConstValues.SESSION_COMMENT] = objSessionForFindingComment;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            setSessionForFindingComment();
            Response.Redirect(ConstURL.URL_COMMENT_VIEW);
        } 
    }
}