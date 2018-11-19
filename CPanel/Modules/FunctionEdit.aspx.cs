
using System;
using System.Collections.Generic;
using System.Linq;
using CPanel.Commons;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class FunctionEdit : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strFunID = null;
                drawTreeInDropDownList_Funs(0, null, drpFunType);
                SessionForFindingFunction objSessionForFindingFun = (SessionForFindingFunction)Session["ss_Function"];

                if (objSessionForFindingFun != null)
                {
                    strFunID = objSessionForFindingFun.ID_FUNCTION;
                   
                }

               
                if (!String.IsNullOrEmpty(strFunID)) 
                {
                    int intFunID = Convert.ToInt32(strFunID);
                    jos_functions objFunction = entities.jos_functions.Where(x => x.id == intFunID).FirstOrDefault();
                    if (objFunction != null)
                    {
                        txtFunctionID.Text = objFunction.id.ToString();
                        txtName.Text = objFunction.name;
                        txtLink.Text = objFunction.link;
                        txtOrdering.Text = objFunction.ordering.ToString();
                        if (objFunction.access.ToString() == "1")
                        {
                            ckAccess.Checked = true;
                        }
                        else
                        {
                            ckAccess.Checked = false;
                        }
                        
                        //set begin value for Language
                        drpFunType.SelectedValue = objFunction.id_parent.ToString();
                        if (objFunction.published == true)
                        {
                            ckPublish.Checked = true;
                        }
                        else
                        {
                            ckPublish.Checked = false;
                        }
                    }
                }
                else //Create new Function
                {
                    ckAccess.Checked = true;
                }

            }

        }
        public void drawTreeInDropDownList_Funs(int intCapFun, string strIDFun, DropDownList drpDownList)
        {
            int intIDMenu = 0; bool blNumber_IDMenu = false;

            if (!String.IsNullOrEmpty(strIDFun))
            {
                intIDMenu = Convert.ToInt32(strIDFun);
                blNumber_IDMenu = true;
            }
            else //Begin --> reset DropDownlist
            {
                drpDownList.Items.Clear();
                ListItem objListItem = new ListItem();
                objListItem.Value = Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE");
                objListItem.Text = Commons.TitleConst.getTitleConst("BLANK_ITEM_TITLE");
                drpDownList.Items.Add(objListItem);
            }

            var objFuns = entities.jos_functions.Where(x => ((blNumber_IDMenu && x.id_parent == intIDMenu) || (blNumber_IDMenu == false && x.id_parent == 0)) ).ToList();

            if (objFuns != null)
            {
                foreach (var item in objFuns)
                {
                    string strLine = "";
                    strLine = strLine.PadLeft(intCapFun * 6, (char)Commons.TitleConst.getTitleConst("TITLE_ICON").ElementAt(0));
                    ListItem objListItem = new ListItem();
                    objListItem.Value = item.id.ToString();
                    objListItem.Text = strLine + item.name;
                    if (intCapFun == 0) //Begining Level
                    {
                        objListItem.Attributes.Add("style", "font-weight: bold");
                    }
                    drpDownList.Items.Add(objListItem);
                    drawTreeInDropDownList_Funs(intCapFun + 1, item.id.ToString(), drpDownList);
                }
            }
        }
        protected bool validate()
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Name", Page);
                return false;
            }

            if (String.IsNullOrEmpty(txtLink.Text))

            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Link", Page);
                return false;
            }

            return true;            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!validate())//Validation before updating            
            {
                return;
            }

            jos_functions objFuns;
            //in case of create new item
            if (String.IsNullOrEmpty(txtFunctionID.Text))
            {
                objFuns = new jos_functions();
                objFuns.id_parent = Convert.ToInt32(drpFunType.SelectedValue);
                objFuns.name = txtName.Text;
                objFuns.link = txtLink.Text;
                objFuns.published = (ckPublish.Checked ? true : false);
                objFuns.ordering = int.Parse(txtOrdering.Text);
                objFuns.access = (byte)(ckAccess.Checked ? 1 : 0);

                entities.jos_functions.Add(objFuns);
            }
            else//in case of update info
            {
                int intFunID = Convert.ToInt32(txtFunctionID.Text);
                objFuns = entities.jos_functions.Where(x => x.id == intFunID).FirstOrDefault();
            }

            objFuns.name = txtName.Text;
            objFuns.published = (ckPublish.Checked ? true : false);
            objFuns.id_parent = Convert.ToInt16(drpFunType.SelectedValue);
            objFuns.link = txtLink.Text;
            objFuns.ordering = int.Parse(txtOrdering.Text);

            objFuns.access = (byte)(ckAccess.Checked ? 1 : 0);
            
            entities.SaveChanges();

            //set session for finding content
            setSessionForFindingFunction();

            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_FUNCTION_VIEW);            
        }

        //set session for finding content
        protected void setSessionForFindingFunction()
        {
            SessionForFindingFunction objSessionForFindingFun = new SessionForFindingFunction();
            objSessionForFindingFun.ID_FUNCTION = "";
            Session["ss_Function"] = objSessionForFindingFun;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Commons.ConstURL.URL_FUNCTION_VIEW);
        }

        protected void drpFunType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get drpIDParent
        }
    }
}