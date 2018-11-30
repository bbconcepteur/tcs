using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Core;
using HCSV.Models;
using HCSV.Models.ViewModels;

namespace HCSV.Business.Business
{
    public interface IMenuBusiness : IRepository<jos_menu>
    {
        jos_menu GetMenuById(long langId, long menuId);

        List<Menu> GetTopMenu(long langId, long defaultLangId);

        List<Menu> GetBottomMenu(long langId, long defaultLangId);

        List<jos_links> GetLinkMenu(long intLangId);
    }
    public class MenuBusiness : Repository<jos_menu>, IMenuBusiness
    {
        public MenuBusiness(TCSEntities db) : base(db)
        {

        }

        public MenuBusiness()
        {
            db = new TCSEntities();
        }

        public jos_menu GetMenuById(long langId, long menuId)
        {
            var menu = GetSingle(s => s.lang_id == langId && s.id == menuId && s.published);
            if (menu == null)
            {
                var defaultLang = db.jos_languages.AsNoTracking().FirstOrDefault(s => s.default_status == 1) ??
                                  new jos_languages();
                menu = GetSingle(s => s.lang_id == defaultLang.lang_id && s.id == menuId && s.published);
            }
            return menu;
        }

        public List<Menu> GetBottomMenu(long langId, long defaultLangId)
        {

            var menuTypes =
                             db.jos_menu_types.AsNoTracking()
                                 .Where(s => Constants.TcsContentType.MENU_BOTTOM.Equals(s.menutype))
                                 .Select(s => s.id)
                                 .ToList();

            var defauleMenus =
                GetMany(s => s.lang_id == defaultLangId && s.published && menuTypes.Contains(s.id_menutype ?? 0)).OrderBy(s=>s.ordering);
            var menus = new List<Menu>();
            if (langId != defaultLangId)
            {
                var translate = new TranslateBusiness(db);
                //todo: tra bang translate
                var defaultId = defauleMenus.Select(s => (long)s.id).ToList();
                var translateMenu = translate.GetTranslatetions(defaultId, Constants.TranslateTable.TBL_JOS_MENU).ToList();
                var translateIds = translateMenu.Select(s => s.reference_id);
                //todo: lay menu cua ngon ngu hien tai

                var currentMenus = GetMany(s => s.lang_id == langId && translateIds.Contains(s.id));
                translateMenu.ForEach(t =>
                {
                    var defaultMenu = defauleMenus.FirstOrDefault(s => s.id == t.origin_id);
                    var currMenu = currentMenus.FirstOrDefault(s => s.id == t.reference_id);
                    if (defaultMenu != null && currMenu != null)
                    {
                        menus.Add(new Menu
                        {
                            Id = currMenu.id,
                            ParentId = currMenu.parent,
                            LangId = currMenu.lang_id,
                            Name = currMenu.name,
                            Url = defaultMenu.link,
                            MenuType = MenuType.Bottom,
                            Level = 1,
                            OrdNumber = defaultMenu.ordering ?? 0
                        });
                    }
                });
            }
            else
            {
                menus = defauleMenus.Select(s => new Menu()
                {
                    Id = s.id,
                    ParentId = s.parent,
                    LangId = s.lang_id,
                    Name = s.name,
                    Url = s.link,
                    MenuType = MenuType.Bottom
                }).ToList();
            }
            return menus;
        }

        public List<Menu> GetTopMenu(long langId, long defaultLangId)
        {
            var menuTypes =
                             db.jos_menu_types.AsNoTracking()
                                 .Where(s => Constants.TcsContentType.MENU_TOP.Equals(s.menutype))
                                 .Select(s => s.id)
                                 .ToList();

            var defauleMenus =
                GetMany(s => s.lang_id == defaultLangId && s.published && menuTypes.Contains(s.id_menutype ?? 0)).OrderBy(s=>s.ordering);
            var menus = new List<Menu>();

            if (langId != defaultLangId)
            {
                var translate = new TranslateBusiness(db);
                //todo: tra bang translate
                var defaultId = defauleMenus.Select(s => (long)s.id).ToList();
                var translateMenu = translate.GetTranslatetions(defaultId, Constants.TranslateTable.TBL_JOS_MENU).ToList();
                var translateIds = translateMenu.Select(s => s.reference_id);
                //todo: lay menu cua ngon ngu hien tai

                var currentMenus = GetMany(s => s.lang_id == langId && s.published && translateIds.Contains(s.id) && menuTypes.Contains(s.id_menutype ?? 0)).OrderBy(s=>s.ordering);
                translateMenu.ForEach(t =>
                {
                    var defaultMenu = defauleMenus.FirstOrDefault(s => s.id == t.origin_id);
                    var currMenu = currentMenus.FirstOrDefault(s => s.id == t.reference_id);
                    if (defaultMenu != null && currMenu != null)
                    {
                        menus.Add(new Menu
                        {
                            Id = currMenu.id,
                            ParentId = currMenu.parent,
                            LangId = currMenu.lang_id,
                            Name = currMenu.name,
                            Url = defaultMenu.link,
                            MenuType = MenuType.Top,
                            OrdNumber = defaultMenu.ordering ?? 0
                        });
                    }

                });

            }
            else
            {
                menus = defauleMenus.Select(s => new Menu()
                {
                    Id = s.id,
                    ParentId = s.parent,
                    LangId = s.lang_id,
                    Name = s.name,
                    Url = s.link,
                    MenuType = MenuType.Top,
                    OrdNumber = s.ordering ?? 0
                }).ToList();
            }

            List<Menu> menuModel = new List<Menu>();
            var parentMenu = menus.Where(s => s.ParentId == 0).ToList();
            if (parentMenu.Any())
            {
                int level = 0;
                foreach (var level1 in parentMenu)
                {
                    level = 1;
                    var menu = new Menu();
                    menu.Name = level1.Name;
                    menu.Url = level1.Url;
                    menu.Level = level;
                    menu.OrdNumber = level1.OrdNumber;
                    var subMenu = menus.Where(s => s.ParentId == level1.Id).ToList();
                    if (subMenu.Any())
                    {
                        level++;
                        var tmpLv2 = new List<Menu>();
                        foreach (var level2 in subMenu)
                        {
                            var menuLv2 = new Menu();
                            menuLv2.Name = level2.Name;
                            menuLv2.Url = level2.Url;
                            menuLv2.Level = level;
                            menuLv2.OrdNumber = level2.OrdNumber;
                            var subMenu2 = menus.Where(s => s.ParentId == level2.Id).ToList();
                            if (subMenu2.Any())
                            {
                                level++;
                                
                                var tmpLv3 = new List<Menu>();
                                foreach (var level3 in subMenu)
                                {
                                    var menuLv3 = new Menu();
                                    menuLv3.OrdNumber = level3.OrdNumber;
                                    menuLv3.Name = level3.Name;
                                    menuLv3.Url = level3.Url;
                                    menuLv3.Level = level;
                                    tmpLv3.Add(menuLv3);
                                }
                                menuLv2.Childrens = new List<Menu>();
                                menuLv2.Childrens.AddRange(tmpLv3.OrderBy(s=>s.OrdNumber));
                            }
                            tmpLv2.Add(menuLv2);
                        }
                        menu.Childrens = new List<Menu>();
                        menu.Childrens.AddRange(tmpLv2.OrderBy(s=>s.OrdNumber));
                    }

                    menuModel.Add(menu);
                }
            }

            return menuModel;

        }

        public List<jos_links> GetLinkMenu(long intLangId)
        {
            return db.jos_links.AsNoTracking()
                    .Where(x => x.lang_id == intLangId && x.published)
                    .OrderBy(s => s.order)
                    .ToList();
        }
    }
}
