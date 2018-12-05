using HCSV.Models;

namespace CPanel.ViewModels
{
    public class jos_categories_ViewModel : jos_categories
    {
        public TCSEntities entities = new TCSEntities();
        private string _section_name;

        public string section_name
        {
            get
            {
                return Commons.CommonFunctionsAndProcedures.getSectionNameBySectionID(((section != null) && (section > 0)) ? (int)section : Commons.CommonFuncs.NUMBER_INVALID_INTEGER , entities);
            }
            set
            {
                _section_name = value;
            }
        }
    }
}