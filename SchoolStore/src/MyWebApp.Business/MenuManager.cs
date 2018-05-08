using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebApp.Repository;

namespace MyWebApp.Business
{
    public interface IMenuManager
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

        public MenuModel(int id, string name, string displayText, string description)
        {
            Id = id;
            Name = name;
            DisplayText = displayText;
            Description = description;
        }
    }

    class MenuManager : IMenuManager
    {
        private readonly IMenuRepository menuRepository;

        public MenuManager(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
        }

        public MenuModel[] Menus
        {
            get
            {
                return menuRepository.Menus
                    .Select(m => new MenuModel(m.Id, m.Name, m.DisplayText, m.Description))
                    .ToArray();
            }
        }

        public MenuModel Menu(int menuId)
        {
            var menuModel = menuRepository.Menu(menuId);
            return new MenuModel(menuModel.Id, menuModel.Name, menuModel.DisplayText, menuModel.Description);
        }
    }
}
