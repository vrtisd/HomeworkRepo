using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebApp.Business;
using MyWebApp.WebSite.Models;

namespace MyWebApp.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuManager menuManager;
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;

        public HomeController(IMenuManager menuManager, IClassManager classManager, IUserManager userManager)
        {
            this.menuManager = menuManager;
            this.classManager = classManager;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            var menus = menuManager.Menus
                .Select(m => new MyWebApp.WebSite.Models.MenuModel(m.Id, m.Name, m.DisplayText, m.Description))
                .ToArray();
            var model = new IndexModel { Menus = menus };
            return View(model);
        }

        public ActionResult Classes()
        {
            var classes = classManager.GetAll()
                .Select(c => new MyWebApp.WebSite.Models.ClassModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price
                }).ToArray();

            var model = new ClassViewModel
            {
                Classes = classes
            };
            return View(model);
        }

        //public ActionResult Student(int userId)
        //There is already a Controller.User, so I am using Student instead
        public ActionResult Student()
        {
            if (Session["User"] != null)
            {
                var userId = ((Models.UserModel)Session["User"]).Id;
                var user = userManager.User(userId);
                var classes = classManager
                    .ForUser(userId)
                    .Select(c => new MyWebApp.WebSite.Models.ClassModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Price = c.Price
                    }).ToArray();

                var model = new UserViewModel
                {
                    User = new MyWebApp.WebSite.Models.UserModel(user.Id, user.Name, user.Email, user.Password, user.IsAdmin),
                    Classes = classes
                };

                return View(model);
            }
            else
            {
                return Redirect("~/");
            }
        }

        public ActionResult Enroll()
        {
            if (Session["User"] != null)
            {
                var classes = classManager.GetAll()
                .Select(c => new MyWebApp.WebSite.Models.ClassModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price
                }).ToArray();

                var model = new ClassViewModel
                {
                    Classes = classes
                };

                model.ClassSelectList = new SelectList(classes, "Id", "Name");

                return View(model);
            }
            else
            {
                return Redirect("~/");
            }
        }

        [HttpPost]
        public ActionResult Enroll(ClassViewModel classViewModel)
        {
            if (ModelState.IsValid)
            {
                string selectedClass = classViewModel.SelectedClass;
                var userId = ((Models.UserModel)Session["User"]).Id;
                int classId = int.Parse(selectedClass);
                classManager.AddClass(userId, classId);
                return Redirect("~/Home/Student");
            }

            var classes = classManager.GetAll()
                .Select(c => new MyWebApp.WebSite.Models.ClassModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Price = c.Price
                }).ToArray();

            var model = new ClassViewModel
            {
                Classes = classes
            };

            model.ClassSelectList = new SelectList(classes, "Id", "Name");

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if(ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.Email, registerModel.Password);

                return Redirect("~/");
            }

            return View(registerModel);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //var user = userManager.LogIn(loginModel.UserEmail, loginModel.Password);
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                    //ModelState.AddModelError("", "User email and password do not match.");
                    ModelState.AddModelError("", "User email and password do not match.");
                else
                {
                    //Session["User"] = new MyWebApp.WebSite.Models.UserModel { Id = user.Id, Name = user.Name, Email = user.Email, Password = user.Password, IsAdmin = user.IsAdmin };
                    Session["User"] = new MyWebApp.WebSite.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}