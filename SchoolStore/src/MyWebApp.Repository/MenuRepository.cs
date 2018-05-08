using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Repository
{
    public interface IMenuRepository
    {
        MenuModel[] Menus { get; }
        MenuModel Menu(int menuId);
    }

    public class MenuModel
    {
        public int Id { get; set; }
        public string DisplayText { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }

    class MenuRepository : IMenuRepository
    {
        public MenuModel[] Menus
        {
            get
            {
                return DatabaseAccessor.Instance.Menus
                    .Select(m => new MenuModel { Id = m.MenuId, Name = m.MenuName, DisplayText = m.MenuDisplayText, Description = m.MenuDescription })
                    .ToArray();
            }
        }

        public MenuModel Menu(int menuId)
        {
            var menu = DatabaseAccessor.Instance.Menus
                .Where(m => m.MenuId == menuId)
                .Select(m => new MenuModel { Id = m.MenuId, Name = m.MenuName, DisplayText = m.MenuDisplayText, Description = m.MenuDescription })
                .First();

            return menu;
        }
    }
}
