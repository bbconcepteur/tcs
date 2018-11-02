﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HCSV.Core;
using HCSV.Models;
using HCSV.Models.ViewModels;

namespace HCSV.Business.Business
{
    public interface IDashbroadBussiness
    {
        List<Menu> GetTopMenu(long langId);

        List<Menu> GetBottomMenu(long langId);
    }
    public class DashbroadBussiness : IDashbroadBussiness
    {
        private readonly TCSEntities db;
        public DashbroadBussiness(TCSEntities db)
        {
            this.db = db;
        }

        public List<Menu> GetBottomMenu(long langId)
        {
            var menuTypes = db.jos_menu_types.AsNoTracking().Where(s => Constants.MENU_BOTTOM.Equals(s.menutype)).Select(s => s.id).ToList();
            var menus = db.jos_menu.AsNoTracking().Where(s => s.lang_id == langId && s.published == 1 && menuTypes.Contains(s.id_menutype ?? 0)).Select(s => new Menu()
            {
                Id = s.id,
                ParentId = s.parent,
                LangId = s.lang_id,
                Name = s.name,
                Url = s.link
            }).ToList();

            return menus;
        }

        public List<Menu> GetTopMenu(long langId)
        {
            var menuTypes = db.jos_menu_types.AsNoTracking().Where(s => Constants.MENU_TOP.Equals(s.menutype)).Select(s => s.id).ToList();
            var menus = db.jos_menu.AsNoTracking().Where(s => s.lang_id == langId && s.published == 1 && menuTypes.Contains(s.id_menutype ?? 0)).Select(s => new Menu()
            {
                Id = s.id,
                ParentId = s.parent,
                LangId = s.lang_id,
                Name = s.name,
                Url = s.link
            }).ToList();
            List<Menu> menuModel = new List<Menu>();
            var parentMenu = menus.Where(s => s.ParentId == 0).ToList();
            if (parentMenu.Any())
            {
                foreach (var level1 in parentMenu)
                {
                    var menu = new Menu();
                    menu.Name = level1.Name;
                    menu.Url = level1.Url;
                    var subMenu = menus.Where(s => s.ParentId == level1.Id).ToList();
                    if (subMenu.Any())
                    {
                        menu.Childrens = new List<Menu>();
                        foreach (var level2 in subMenu)
                        {
                            var menuLv2 = new Menu();
                            menuLv2.Name = level2.Name;
                            menuLv2.Url = level2.Url;

                            var subMenu2 = menus.Where(s => s.ParentId == level2.Id).ToList();
                            if (subMenu2.Any())
                            {
                                menuLv2.Childrens = new List<Menu>();
                                foreach (var level3 in subMenu)
                                {
                                    var menuLv3 = new Menu();
                                    menuLv3.Name = level3.Name;
                                    menuLv3.Url = level3.Url;
                                    menu.Childrens.Add(menuLv3);
                                }
                            }
                            menu.Childrens.Add(menuLv2);
                        }
                    }

                    menuModel.Add(menu);
                }
            }

            return menuModel;
        }
    }
}
