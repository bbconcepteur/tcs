using CPanel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Models;
namespace CPanel.Modules
{
    public partial class CopyDBFromTCS : System.Web.UI.Page
    {
        //public static string LANGUAGE_VIETNAM = "vi_VN";
        //public static string TBL_JOS_CONTENT = "jos_content";
        //public static string TBL_JOS_MENU = "jos_menu";

        cpanelEntities entities = new cpanelEntities();
        tcs_newEntities tcs_entities = new tcs_newEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //STEP 1
            //Insert into JOS_MENU_TYPE tbl
            //saveIntoTbl_JOS_MENU_TYPES();

            //STEP 2
            //saveIntoTbl_JOS_MENU();

            //STEP 3
            //saveIntoTbl_JOS_SECTIONS();

            //STEP 4
            //saveIntoTbl_JOS_CATEGORIES();

            //STEP 5
            //saveIntoTbl_JOS_CONTENT();

            //STEP 6
            //saveIntoTbl_JOS_LANGUAGE_TRANSLATION();

            //STEP 7
            //saveIntoTbl_JOS_CONTENT_from_FAQ();

            //STEP 8
            //createEnglishMenu_updateLangID_JOS_MENU();

            //STEP 9
            //func_SaveIntoTbl_JOS_LINKS();

            //STEP 10
            //func_SaveIntoTbl_JOS_CONTACTS();

            //STEP 11
            func_SaveIntoTbl_JOS_CONTENT__JOS_TRANSLATION_From_MR_AN();//Khong dung khi dong bo tu TCS

            //STEP ...
            //updateTbl_JOS_MENU();

            
        }

        public int func_SaveIntoTbl_JOS_CONTENT_From_MR_AN(tcs_jos_content item)
        {
            //Save into Jos_Contents
            jos_content objJosContent = new jos_content();
            objJosContent.title = item.title;
            objJosContent.alias = item.alias;
            objJosContent.state = item.state;
            objJosContent.catid = item.catid;

            objJosContent.created = item.created;
            objJosContent.modified = item.modified;
            objJosContent.introtext = item.introtext;
            objJosContent.fulltext = item.fulltext;

            objJosContent.publish_up = item.publish_up;
            objJosContent.lang_id = item.lang_id;

            objJosContent.ordering = item.ordering;

            entities.jos_content.Add(objJosContent);
            entities.SaveChanges();
            return (int)objJosContent.id;
        }

        public void func_SaveIntoTbl_JOS_CONTENT__JOS_TRANSLATION_From_MR_AN() {
            //get from "service" tbl
            long lgMinID = 119;
            long lgMaxID = 130;
            var lstContens = tcs_entities.tcs_jos_content.Where(x=>x.catid >= lgMinID && x.catid <=lgMaxID).ToList();
            if (lstContens != null && lstContens.Count() > 0)
            {
                foreach (var item in lstContens)
                {
                    int intEnglishContentID = func_SaveIntoTbl_JOS_CONTENT_From_MR_AN(item);
                    
                    //Save into Translation
                    var objTrans = tcs_entities.tcs_jos_language_translation.Where(x=>x.origin_id == item.id).FirstOrDefault();
                    if (objTrans != null)
                    {
                        //Save into Vietnamese Content
                        var objVietnamJosContent = tcs_entities.tcs_jos_content.Where(x=>x.id == objTrans.reference_id).FirstOrDefault();
                        if (objVietnamJosContent != null) {
                            int intVietnamContentID = func_SaveIntoTbl_JOS_CONTENT_From_MR_AN(objVietnamJosContent);

                            //Save into Translation
                            jos_language_translation objLangTranslation = new jos_language_translation();
                            objLangTranslation.language_id = (int)objTrans.language_id;
                            objLangTranslation.origin_id = intEnglishContentID;
                            objLangTranslation.reference_id = intVietnamContentID;
                            objLangTranslation.reference_table = Commons.ConstTable.TBL_JOS_CONTENT;
                            entities.jos_language_translation.Add(objLangTranslation);
                            entities.SaveChanges();
                        }
                    }
                }
            }
        }


        public void createEnglishMenu_JOS_MENU (string strIDMenu, int intCapMenu, int intLangId_EN, int intLangId_VN)
        {
            int intIDMenu = 0; bool blNumber = false;            
            if (!String.IsNullOrEmpty(strIDMenu))
            {
                intIDMenu = Convert.ToInt32(strIDMenu);
                blNumber = true;
            }

            //get list of Vietnam Menus
            var objMenus = entities.jos_menu.Where(x => (x.lang_id == intLangId_VN) && (x.published) 
                                                  && ((blNumber && x.parent == intIDMenu) || (blNumber == false && x.parent == 0))).OrderBy(y => y.ordering).ToList();

            if (objMenus != null)
            {
                foreach (var item in objMenus)
                {   
                    var lstTransMenu = entities.jos_language_translation.Where(x => x.reference_id == item.id 
                                                            && Commons.ConstTable.TBL_JOS_MENU.Equals(x.reference_table) && (x.origin_id != x.reference_id)).FirstOrDefault();
                    
                    jos_menu objMenu = new jos_menu();
                    bool blCreateTransLange = false, blCreateEnglishMenu = true;



                    if (lstTransMenu != null)
                    {
                        var objMenu_FromTran = entities.jos_menu.Find(lstTransMenu.origin_id);
                        if (objMenu_FromTran == null)
                        {
                            objMenu.id = lstTransMenu.origin_id;
                        }
                        else
                        {
                            blCreateEnglishMenu = false;
                        }
                    }
                    else if (lstTransMenu == null)
                    {                        
                        if (entities.jos_menu.Count() > 0) objMenu.id = entities.jos_menu.Max(x => x.id) + 1;
                        else objMenu.id = 1;
                        blCreateTransLange = true;
                    }

                    
                    if (blCreateEnglishMenu)
                    {
                        objMenu.id_menutype = (int)item.id;
                        objMenu.menutype = item.menutype;
                        objMenu.alias = item.alias;
                        objMenu.name = item.name + " [ENGLISH]";

                        if (item.parent == 0)
                        {
                            objMenu.parent = item.parent;
                        }
                        else
                        {
                            //get Parent Menu
                            var lstParent_TransMenu = entities.jos_language_translation.Where(x => x.reference_id == item.parent
                                                            && Commons.ConstTable.TBL_JOS_MENU.Equals(x.reference_table) && (x.origin_id != x.reference_id)).FirstOrDefault();

                            if (lstParent_TransMenu != null)
                            {
                                objMenu.parent = lstParent_TransMenu.origin_id;
                            }
                        }


                        objMenu.ordering = item.ordering;
                        objMenu.published = item.published;
                        objMenu.lang_id = intLangId_EN;

                        entities.jos_menu.Add(objMenu);
                        entities.SaveChanges();

                        //save into Language Translation
                        if (blCreateTransLange)
                        {
                            func_saveIntoTbl_JOS_LANGUAGE_TRANSLATION(objMenu.id, item.id, Commons.ConstTable.TBL_JOS_MENU, Commons.ConstValues.LANGUAGE_VIETNAMESE);
                        }
                    }
                                        
                    createEnglishMenu_JOS_MENU(item.id.ToString(), intCapMenu + 1, intLangId_EN, intLangId_VN);
                }
            }

        }



        protected void createEnglishMenu_updateLangID_JOS_MENU()
        {
            /*
            //Update Lang ID =VIETNAM for menus
            var lstMenus = entities.jos_menu.Join(entities.jos_language_translation, M => M.id, L => L.origin_id, (M, L) => new { M, L })
                                            .Where(x => (x.M.id == x.L.reference_id) && Commons.ConstTable.TBL_JOS_MENU.Equals(x.L.reference_table)).Select(y=>y.M).ToList();
            
            if (lstMenus != null && lstMenus.Count > 0)
            {
                var objLang = entities.jos_languages.Where(x => x.lang_code.Trim().Equals(Commons.ConstValues.LANGUAGE_VIETNAMESE)).FirstOrDefault();
                foreach (var item in lstMenus)
                {
                    if (objLang != null)
                    {
                        item.lang_id = (int)objLang.lang_id;
                        entities.SaveChanges();
                    }                    
                }
            }

            //Update Lang ID =ENGLISH for menus            
            lstMenus = entities.jos_menu.Where (x=>x.lang_id == 0).ToList();            
            if (lstMenus != null && lstMenus.Count > 0)
            {
                var objLang = entities.jos_languages.Where(x => x.lang_code.Trim().Equals(Commons.ConstValues.LANGUAGE_ENGLISH)).FirstOrDefault();
                foreach (var item in lstMenus)
                {
                    if (objLang != null)
                    {
                        item.lang_id = (int)objLang.lang_id;
                        entities.SaveChanges();
                    }
                }
            }*/

            //Create English Menu
            var objLang_VN = entities.jos_languages.Where(x => x.lang_code.Trim().Equals(Commons.ConstValues.LANGUAGE_VIETNAMESE)).FirstOrDefault();
            var objLang_EN = entities.jos_languages.Where(x => x.lang_code.Trim().Equals(Commons.ConstValues.LANGUAGE_ENGLISH)).FirstOrDefault();

            createEnglishMenu_JOS_MENU(null, 0, (int)objLang_EN.lang_id, (int) objLang_VN.lang_id);


            //var lstSiteTree = tcs_entities.sitetrees.Where(x => (x.MenuTitle != null) && (x.ShowInMenus == true)).ToList();
            //if (lstSiteTree != null && lstSiteTree.Count() > 0)
            //{
            //    foreach (var item in lstSiteTree)
            //    {
            //        var objMenuType = entities.jos_menu_types.Where(x => x.menutype.Trim().ToUpper().Equals(item.ClassName.Trim().ToUpper())).FirstOrDefault();
            //        if (objMenuType != null)
            //        {
            //            jos_menu objMenu = new jos_menu();
            //            objMenu.id = item.ID;

            //            objMenu.id_menutype = (int)objMenuType.id;
            //            objMenu.menutype = objMenuType.menutype;
            //            objMenu.name = item.MenuTitle;
            //            objMenu.alias = item.URLSegment;
            //            objMenu.parent = item.ParentID;
            //            objMenu.ordering = item.Sort;
            //            objMenu.published = "Published".Equals(item.Status.Trim()) ? true : false;

            //            entities.jos_menu.Add(objMenu);
            //            entities.SaveChanges();
            //        }
            //    }
            //}
        }

        protected void saveIntoTbl_JOS_LANGUAGE_TRANSLATION()
        {
            //Insert into JOS_CONTENT tbl
            //get from "newsentry_translationgroups" tbl
            var lstNewEntryTrans = tcs_entities.newsentry_translationgroups.ToList();

            if (lstNewEntryTrans != null && lstNewEntryTrans.Count() > 0)
            {
                foreach (var item in lstNewEntryTrans)
                {
                    var obj_Old_OriginContent = tcs_entities.newsentries.Find(item.OriginalID);
                    var obj_Old_TransContent = tcs_entities.newsentries.Find(item.TranslationGroupID);

                    if (obj_Old_OriginContent != null && obj_Old_TransContent != null)
                    {
                        var obj_New_OriginContent = entities.jos_content.Where(x => (x.old_id == obj_Old_OriginContent.ID) && (x.old_cat_id == obj_Old_OriginContent.CategoryID)).FirstOrDefault();
                        var obj_New_TransContent = entities.jos_content.Where(x => (x.old_id == obj_Old_TransContent.ID) && (x.old_cat_id == obj_Old_TransContent.CategoryID)).FirstOrDefault();

                        if (obj_New_OriginContent != null && obj_New_TransContent != null)
                        {
                            func_saveIntoTbl_JOS_LANGUAGE_TRANSLATION((int)obj_New_OriginContent.id, (int)obj_New_TransContent.id, Commons.ConstTable.TBL_JOS_CONTENT, Commons.ConstValues.LANGUAGE_VIETNAMESE);
                        }

                    }
                }
            }

            //get from "service_translationgroups" tbl           
            var lstServiceTran = tcs_entities.service_translationgroups.ToList();
            if (lstServiceTran != null && lstServiceTran.Count() > 0)
            {
                foreach (var item in lstServiceTran)
                {
                    var obj_Old_OriginContent = tcs_entities.newsentries.Find(item.OriginalID);
                    var obj_Old_TransContent = tcs_entities.newsentries.Find(item.TranslationGroupID);

                    if (obj_Old_OriginContent != null && obj_Old_TransContent != null)
                    {
                        var obj_New_OriginContent = entities.jos_content.Where(x => (x.old_id == obj_Old_OriginContent.ID) && (x.old_cat_id == obj_Old_OriginContent.CategoryID)).FirstOrDefault();
                        var obj_New_TransContent = entities.jos_content.Where(x => (x.old_id == obj_Old_TransContent.ID) && (x.old_cat_id == obj_Old_TransContent.CategoryID)).FirstOrDefault();

                        if (obj_New_OriginContent != null && obj_New_TransContent != null)
                        {
                            func_saveIntoTbl_JOS_LANGUAGE_TRANSLATION((int)obj_New_OriginContent.id, (int)obj_New_TransContent.id, Commons.ConstTable.TBL_JOS_CONTENT, Commons.ConstValues.LANGUAGE_VIETNAMESE);
                        }

                    }
                }
            }

        }

        protected void func_saveIntoTbl_JOS_LANGUAGE_TRANSLATION(int intOriginalID, int intTranslationGroupID, string strReferenceTable, string strLangCode)
        {
            //Insert into JOS_LANGUAGE_TRANSLATION tbl
            var objLang = entities.jos_languages.Where(x => x.lang_code.Trim().Equals(strLangCode)).FirstOrDefault();
            if (objLang != null)
            {
                jos_language_translation objLangTranslation = new jos_language_translation();
                objLangTranslation.language_id = (int)objLang.lang_id;
                objLangTranslation.origin_id = intOriginalID;
                objLangTranslation.reference_id = intTranslationGroupID;
                objLangTranslation.reference_table = strReferenceTable;

                entities.jos_language_translation.Add(objLangTranslation);
                entities.SaveChanges();
            }
        }



        //Insert into JOS_MENU_TYPE tbl
        protected void saveIntoTbl_JOS_MENU_TYPES()
        {
            //Delete JOS_MENU_TYPES tbl
            var lstJosMenuTypes = entities.jos_menu_types.ToList();
            if (lstJosMenuTypes != null && lstJosMenuTypes.Count() > 0)
            {
                foreach (var deletedItem in lstJosMenuTypes)
                {
                    entities.jos_menu_types.Remove(deletedItem);
                    entities.SaveChanges();
                }
            }

            //Insert into JOS_MENU_TYPE tbl
            var lstSiteTree = tcs_entities.sitetrees.Select(x => new { x.ClassName }).Distinct();
            if (lstSiteTree != null && lstSiteTree.Count() > 0)
            {
                foreach (var item in lstSiteTree)
                {
                    jos_menu_types objMenuType = new jos_menu_types();
                    if (entities.jos_menu_types.Count() > 0) objMenuType.id = entities.jos_menu_types.Max(x => x.id) + 1;
                    else objMenuType.id = 1;
                    objMenuType.menutype = item.ClassName;
                    objMenuType.title = item.ClassName;
                    objMenuType.description = item.ClassName;
                    entities.jos_menu_types.Add(objMenuType);
                    entities.SaveChanges();
                }
            }
        }


        //Insert into JOS_CONTENT tbl
        protected void saveIntoTbl_JOS_CONTENT()
        {
            //Delete JOS_CONTENT tbl
            var lstJosContent = entities.jos_content.ToList();
            if (lstJosContent != null && lstJosContent.Count() > 0)
            {
                foreach (var deletedItem in lstJosContent)
                {
                    entities.jos_content.Remove(deletedItem);
                    entities.SaveChanges();
                }
            }

            //Insert into JOS_CONTENT tbl
            //get from "newscategory" tbl
            var lstNewEntry = tcs_entities.newsentries.ToList();
            if (lstNewEntry != null && lstNewEntry.Count() > 0)
            {
                foreach (var item in lstNewEntry)
                {
                    var objClassName = tcs_entities.newscategories.Where(x => x.ID == item.CategoryID).FirstOrDefault();

                    func_SaveIntoTbl_JOS_CONTENT(item.ID, item.Title, item.URLSegment, (item.Show ? (sbyte)1 : (sbyte)0), item.CategoryID, (DateTime)item.Created, (DateTime)item.LastEdited, (DateTime)item.ReleaseDate,
                        item.Summary, item.Content, item.Locale, 0, objClassName != null ? objClassName.ClassName : "");
                }
            }

            //get from "service" tbl           
            var lstService = tcs_entities.services.ToList();
            if (lstService != null && lstService.Count() > 0)
            {
                foreach (var item in lstService)
                {
                    var objClassName = tcs_entities.servicecategories.Where(x => x.ID == item.ServiceCategoryID).FirstOrDefault();
                    func_SaveIntoTbl_JOS_CONTENT(item.ID, item.Name, null, (sbyte)1, item.ServiceCategoryID, (DateTime)item.Created, (DateTime)item.LastEdited, (DateTime)item.Created,
                                                 item.Description, null, item.Locale, item.OrderDisplay, objClassName != null ? objClassName.ClassName : "");
                }
            }
        }


        //Cau Hoi (FAQ)
        protected void saveIntoTbl_JOS_CONTENT_from_FAQ()
        {
            //Delete Manually Section and Category "FAQ" (If re-do)

            //Create section "FAQ"
            string strFAQ = "FAQ";
            int intSectionID = func_SaveIntoTbl_JOS_SECTIONS(strFAQ);
            
            //Create category "FAQ"
            int intCatID = func_SaveIntoTbl_JOS_CATEGORIES(strFAQ, strFAQ, -1);
            //Update category "FAQ" with Old_Id = new_ID
            jos_categories objNewCat = entities.jos_categories.Where (x=>x.id == intCatID).FirstOrDefault();
            objNewCat.old_id = intCatID;
            entities.SaveChanges();



            //Insert into JOS_CONTENT tbl
            //get from "faq" tbl
            var lstFAQ = tcs_entities.faqs.ToList();
            if (lstFAQ != null && lstFAQ.Count() > 0)
            {
                foreach (var item in lstFAQ)
                {
                    func_SaveIntoTbl_JOS_CONTENT(item.ID, item.Question, null, item.Show ? (sbyte)1 : (sbyte)0, intCatID, (DateTime)item.Created, (DateTime)item.LastEdited, (DateTime)item.Created,
                        item.Answer, null, item.Locale, 0, strFAQ);
                }
            }

            //Insert into JOS_LANGUAGE_TRANSLATION
            //get from "faq_translationgroups" tbl
            var lstFAQTrans = tcs_entities.faq_translationgroups.ToList();

            if (lstFAQTrans != null && lstFAQTrans.Count() > 0)
            {
                foreach (var item in lstFAQTrans)
                {
                    var obj_Old_OriginContent = tcs_entities.newsentries.Find(item.OriginalID);
                    var obj_Old_TransContent = tcs_entities.newsentries.Find(item.TranslationGroupID);

                    if (obj_Old_OriginContent != null && obj_Old_TransContent != null)
                    {
                        var obj_New_OriginContent = entities.jos_content.Where(x => (x.old_id == obj_Old_OriginContent.ID) && (x.old_cat_id == intCatID)).FirstOrDefault();
                        var obj_New_TransContent = entities.jos_content.Where(x => (x.old_id == obj_Old_TransContent.ID) && (x.old_cat_id == intCatID)).FirstOrDefault();

                        if (obj_New_OriginContent != null && obj_New_TransContent != null)
                        {
                            func_saveIntoTbl_JOS_LANGUAGE_TRANSLATION((int)obj_New_OriginContent.id, (int)obj_New_TransContent.id, Commons.ConstTable.TBL_JOS_CONTENT, Commons.ConstValues.LANGUAGE_VIETNAMESE);
                        }

                    }
                }
            }            
        }


        protected void func_SaveIntoTbl_JOS_LINKS()
        {
            //Save into JOS_LINKS
            var lstLink = tcs_entities.wlinks.ToList();
            jos_languages objLang;
            string strLocale;
            foreach (var i in lstLink)
            {
                jos_links obj = new jos_links();
                obj.id = i.ID;
                obj.created = i.Created;
                obj.modified = i.LastEdited;
                obj.name = i.Name;
                obj.link = i.Link;
                obj.description = i.Description;
                obj.order = i.OrderDisplay;
                obj.address = i.Address;
                obj.phone = i.Phone;
                obj.fax = i.Fax;
                obj.representative = i.Representative;
                obj.published = true;
                
                strLocale = i.Locale;
                objLang = entities.jos_languages.Where(x => strLocale.Trim().Equals(x.lang_code.Trim())).FirstOrDefault();
                if (objLang != null)
                {
                    obj.lang_id = (int)objLang.lang_id;
                }
                
                var lstFile = tcs_entities.files.Where(x => (x.ID == i.LogoID)).FirstOrDefault();
                obj.image = "<img src=\"" + lstFile.Filename + "\">";

                entities.jos_links.Add(obj);

                entities.SaveChanges();
            }

            //save into Translation
            func_SaveIntoTbl_JOS_LINKS_TRANSLATION();
        }

        protected void func_SaveIntoTbl_JOS_LINKS_TRANSLATION ()
        {
            var lstLink = tcs_entities.wlink_translationgroups.ToList();

            foreach (var i in lstLink)
            {
                func_saveIntoTbl_JOS_LANGUAGE_TRANSLATION (i.OriginalID, i.TranslationGroupID, Commons.ConstTable.TBL_JOS_LINKS, Commons.ConstValues.LANGUAGE_VIETNAMESE);
            }            
        }


        protected void func_SaveIntoTbl_JOS_CONTACTS ()
        {
            jos_languages objLang;
            string strLocale;

            var lstContacts = tcs_entities.contacts.OrderBy(m => new { m.OrderDisplay, m.Name }).ToList();

            foreach (var i in lstContacts)
            {
                strLocale = i.Locale;

                jos_contact obj = new jos_contact();
                obj.id = i.ID;
                obj.id_parent = i.ParentID;
                obj.created = i.Created;
                obj.modified = i.LastEdited;
                obj.name = i.Name;
                obj.ext_tel = i.Ext;
                obj.email = i.Email;
                obj.order = i.OrderDisplay;
                obj.department_manager = i.DepartmentManager;
                obj.phone = i.Phone;
                obj.hotline = i.Hotline;
                obj.title_of_manager = i.TitleOfManager;

                obj.published = true;// để cho hiển thị lên

                objLang = entities.jos_languages.Where(x => strLocale.Trim().Equals(x.lang_code.Trim())).FirstOrDefault();
                if (objLang != null)
                {
                    obj.lang_id = (int)objLang.lang_id;
                }

                entities.jos_contact.Add(obj);

                entities.SaveChanges();
            }

            //save into JOS_LANGUAGE_TRANSLATION
            var lstTranslation = tcs_entities.contact_translationgroups.ToList();
            foreach (var item in lstTranslation)
            {
                func_saveIntoTbl_JOS_LANGUAGE_TRANSLATION(item.OriginalID, item.TranslationGroupID, Commons.ConstTable.TBL_JOS_CONTACT, Commons.ConstValues.LANGUAGE_VIETNAMESE);
            }
        }

        protected int func_SaveIntoTbl_JOS_CONTENT(int intOldItemID, string strTitle, string strAlias,
            sbyte intStatus, int intOldCat, DateTime dtCreatedDate, DateTime dtModifiedDate,
            DateTime dtPublishedDate, string strIntroText, string strFullText, string strLang, int intOrdering, string strSectionName)
        {
            jos_languages objLang = entities.jos_languages.Where(x => strLang.Trim().ToUpper().Equals(x.lang_code)).FirstOrDefault();

            var objCat = entities.jos_categories.Join(entities.jos_sections, C => C.section, S => S.id, (C, S) => new { C, S })
                          .Where(x => x.C.old_id == intOldCat && x.S.name.Trim().ToUpper().Equals(strSectionName)).Select(z => z.C).FirstOrDefault();


            jos_content objJosContent = new jos_content();
            objJosContent.old_id = intOldItemID;
            objJosContent.old_cat_id = intOldCat;
            objJosContent.title = strTitle;
            objJosContent.alias = strAlias;
            objJosContent.state = intStatus;

            if (objCat != null)
            {
                objJosContent.catid = objCat.id;
            }

            objJosContent.created = dtCreatedDate;
            //objJosContent.created_by
            objJosContent.modified = dtModifiedDate;
            objJosContent.introtext = strIntroText;
            objJosContent.fulltext = strFullText;

            objJosContent.publish_up = dtPublishedDate;
            if (objLang != null)
            {
                objJosContent.lang_id = (int)objLang.lang_id;
            }
            objJosContent.ordering = intOrdering;
            //objJosContent.publish_down =
            //objJosContent.ordering =
            //objJosContent.modified_by =


            entities.jos_content.Add(objJosContent);
            entities.SaveChanges();

            return (int)objJosContent.id;
        }




        //Insert into JOS_CATEGORIES tbl
        protected void saveIntoTbl_JOS_CATEGORIES()
        {
            //Delete JOS_SECTIONS tbl
            var lstJosCat = entities.jos_categories.ToList();
            if (lstJosCat != null && lstJosCat.Count() > 0)
            {
                foreach (var deletedItem in lstJosCat)
                {
                    entities.jos_categories.Remove(deletedItem);
                    entities.SaveChanges();
                }
            }

            //Insert into JOS_CATEGORIES tbl
            //get from "newscategory" tbl
            var lstNewCategory = tcs_entities.newscategories.ToList();
            if (lstNewCategory != null && lstNewCategory.Count() > 0)
            {
                foreach (var item in lstNewCategory)
                {
                    func_SaveIntoTbl_JOS_CATEGORIES(item.ClassName, item.Name, item.ID);
                }
            }

            //get from "servicecategories" tbl           
            var lstServiceCategory = tcs_entities.servicecategories.ToList();
            if (lstServiceCategory != null && lstServiceCategory.Count() > 0)
            {
                foreach (var item in lstServiceCategory)
                {
                    func_SaveIntoTbl_JOS_CATEGORIES(item.ClassName, item.Name, item.ID);
                }
            }
        }

        protected int func_SaveIntoTbl_JOS_CATEGORIES(string strSectionName, string strName, int intOldItemID)
        {
            jos_sections objSection = entities.jos_sections.Where(x => strSectionName.Trim().ToUpper().Equals(x.name.Trim().ToUpper())).FirstOrDefault();
            if (objSection != null)
            {
                jos_categories objJosCat = new jos_categories();
                objJosCat.name = strName;
                objJosCat.title = strName;
                objJosCat.published = true;
                objJosCat.old_id = intOldItemID;
                objJosCat.section = objSection.id;
                entities.jos_categories.Add(objJosCat);
                entities.SaveChanges();
                return objJosCat.id;
            }
            return -1;

        }


        //Insert into JOS_SECTIONS tbl
        protected void saveIntoTbl_JOS_SECTIONS()
        {
            //Delete JOS_SECTIONS tbl
            var lstJosSections = entities.jos_sections.ToList();
            if (lstJosSections != null && lstJosSections.Count() > 0)
            {
                foreach (var deletedItem in lstJosSections)
                {
                    entities.jos_sections.Remove(deletedItem);
                    entities.SaveChanges();
                }
            }

            //Insert into JOS_SECTIONS tbl
            //get from "newscategory" tbl
            List<string> lstClassName = tcs_entities.newscategories.Select(x => x.ClassName).Distinct().ToList();
            if (lstClassName != null && lstClassName.Count() > 0)
            {
                foreach (var item in lstClassName)
                {
                    func_SaveIntoTbl_JOS_SECTIONS(item);
                }
            }

            //get from "servicecategories" tbl
            lstClassName.Clear();
            lstClassName = tcs_entities.servicecategories.Select(x => x.ClassName).Distinct().ToList();
            if (lstClassName != null && lstClassName.Count() > 0)
            {
                foreach (var item in lstClassName)
                {
                    func_SaveIntoTbl_JOS_SECTIONS(item);
                }
            }
        }

        protected int func_SaveIntoTbl_JOS_SECTIONS(string strName)
        {
            jos_sections objJosSection = new jos_sections();
            objJosSection.name = strName;
            objJosSection.title = strName;
            objJosSection.published = true;
            entities.jos_sections.Add(objJosSection);
            entities.SaveChanges();
            return objJosSection.id;
        }

        protected void updateTbl_JOS_MENU()
        {
            /*
            var lstSiteTree = tcs_entities.sitetrees.Where(x => (x.MenuTitle != null) && (x.ShowInMenus == true)).ToList();
            //Update link menu to Categories
            if (lstSiteTree != null && lstSiteTree.Count() > 0)
            {
                foreach (var item in lstSiteTree)
                {
                    if ("NewsHolder".Equals(item.ClassName.Trim())) {
                        //get old Category ID
                        var objNewsHolder = tcs_entities.newsholders.Where(x => x.ID == item.ID).FirstOrDefault();
                        if (objNewsHolder != null)
                        {
                            //get New Category ID
                            var objNewCat = entities.jos_categories.Join(entities.jos_sections, C => C.section, S => S.id, (C, S) => new { C, S })
                              .Where(x => x.S.name.Trim().Equals("NewsCategory") && x.C.old_id == objNewsHolder.NewsCategoryID).Select(y => y.C).FirstOrDefault();
                            if (objNewCat != null)
                            {
 
                                //Because old menu ID = new menu ID ==> process as following
                                jos_menu objNewMenu = entities.jos_menu.Find(item.ID);
                                objNewMenu.link = String.Format(Commons.CommonFuncs.LINK_MENU_TO_CATEGORY_NEWS, objNewCat.id, item.ID);
                                entities.SaveChanges();
                            }           
                        }
 
                    }
                }
            }
             */ 
 
            //Update link menu to Contents
            /*
            int intNewContentID = 0;
            var lstSiteTree = tcs_entities.sitetrees.Where(x => (x.MenuTitle != null) && (x.ShowInMenus == true) && (x.Content != null) && (!"RedirectorPage".Equals(x.ClassName.Trim()))).ToList();
            if (lstSiteTree != null && lstSiteTree.Count() > 0)
            {

                foreach (var item in lstSiteTree)
                {
                    //Old Category ID = 37; Section name = "Others" (Created by Manual);
                    intNewContentID = func_SaveIntoTbl_JOS_CONTENT(-1, item.Title, item.URLSegment, ("Published".Equals(item.Status.Trim()) ? (sbyte)1 : (sbyte)0), 37, (DateTime) item.Created, (DateTime)item.LastEdited, (DateTime)item.Created, item.Content, null, item.Locale, item.Sort, "Others");

                    //get New Menu
                    //Because old menu ID = new menu ID ==> process as following
                    jos_menu objNewMenu = entities.jos_menu.Find(item.ID);
                    if (objNewMenu != null)
                    {
                        objNewMenu.link = String.Format(Commons.CommonFuncs.LINK_MENU_TO_CONTENT_NEWS, intNewContentID, item.ID);
                        entities.SaveChanges();
                    }
                }
            }
             */ 
 
            //Update link menu to redirect to other menu

            //update "published", "language" status
            var lstSiteTree = tcs_entities.sitetrees.Where(x => (x.MenuTitle != null) && (x.ShowInMenus == true)).ToList();
            //Update link menu to Categories
            if (lstSiteTree != null && lstSiteTree.Count() > 0)
            {
                foreach (var item in lstSiteTree)
                {
                    //Because old menu ID = new menu ID ==> process as following
                    jos_menu objNewMenu = entities.jos_menu.Find(item.ID);
                    if (objNewMenu != null)
                    {
                        jos_languages objLang = entities.jos_languages.Where(x => item.Locale.Trim().ToUpper().Equals(x.lang_code)).FirstOrDefault();

                        if (!"Published".Equals(item.Status.Trim()))
                        {
                            objNewMenu.published = false;
                            objNewMenu.pollid = (int)objLang.lang_id;
                        }
                        entities.SaveChanges();
                    }
                }
            }
        }

        //Insert into JOS_MENU tbl
        protected void saveIntoTbl_JOS_MENU()
        {
            //Delete JOS_MENU tbl
            var lstJosMenu = entities.jos_menu.ToList();
            if (lstJosMenu != null && lstJosMenu.Count() > 0)
            {
                foreach (var deletedItem in lstJosMenu)
                {
                    entities.jos_menu.Remove(deletedItem);
                    entities.SaveChanges();
                }
            }

            //Insert into JOS_MENU tbl
            var lstSiteTree = tcs_entities.sitetrees.Where(x => (x.MenuTitle != null) && (x.ShowInMenus == true)).ToList();
            if (lstSiteTree != null && lstSiteTree.Count() > 0)
            {
                foreach (var item in lstSiteTree)
                {
                    var objMenuType = entities.jos_menu_types.Where(x => x.menutype.Trim().ToUpper().Equals(item.ClassName.Trim().ToUpper())).FirstOrDefault();
                    if (objMenuType != null)
                    {
                        jos_menu objMenu = new jos_menu();
                        objMenu.id = item.ID;

                        objMenu.id_menutype = (int)objMenuType.id;
                        objMenu.menutype = objMenuType.menutype;
                        objMenu.name = item.MenuTitle;
                        objMenu.alias = item.URLSegment;
                        objMenu.parent = item.ParentID;
                        objMenu.ordering = item.Sort;
                        objMenu.published = "Published".Equals(item.Status.Trim()) ? true : false;

                        entities.jos_menu.Add(objMenu);
                        entities.SaveChanges();
                    }
                }
            }

            //Process Language
            //Delete JOS_LANGUAGE_TRANSLATION tbl
            var lstJosLangTran = entities.jos_language_translation.ToList();
            if (lstJosLangTran != null && lstJosLangTran.Count() > 0)
            {
                foreach (var deletedItem in lstJosLangTran)
                {
                    entities.jos_language_translation.Remove(deletedItem);
                    entities.SaveChanges();
                }
            }

            //Insert into JOS_LANGUAGE_TRANSLATION tbl
            var lstsiteTree_TransGroup = tcs_entities.sitetree_translationgroups.ToList();
            if (lstsiteTree_TransGroup != null && lstsiteTree_TransGroup.Count() > 0)
            {
                var objLang = entities.jos_languages.Where(x => x.lang_code.Trim().Equals(Commons.ConstValues.LANGUAGE_VIETNAMESE)).FirstOrDefault();
                foreach (var item in lstsiteTree_TransGroup)
                {
                    func_saveIntoTbl_JOS_LANGUAGE_TRANSLATION(item.OriginalID, item.TranslationGroupID, Commons.ConstTable.TBL_JOS_MENU, Commons.ConstValues.LANGUAGE_VIETNAMESE);
                }
            }
        }
    }
}