using AspNetRoleAuthorization.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNetRoleAuthorization.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
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
        public ActionResult List()
        {
            ViewBag.Message = "Your contact page.";
            ListItemView newList = new ListItemView();
            newList.items = new List<ListItem>();
            newList.items.Add(new ListItem { Id= 1, Text= "test" });
            newList.items.Add(new ListItem { Id = 2, Text = "test2" });
            return View(newList);
        }
        [HttpPost]
        public ActionResult List(ListItemView newList)
        {
            if (newList.items == null) return View();
            ViewBag.Message = "Your contact page.";
            //List<string> myList = newList.ToList<string>();
            foreach (ListItem item in newList.items)
            {
                string i = item.Text;
            }
           
            return View(newList);
        }
        [HttpPost]
        public ActionResult AddNewItemList(ListItemView newList)
        {
            newList.items.Add(new ListItem { Id = 0, Text = "new Item" });
            return View("List",newList);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult AddRole()
        {
           List< Microsoft.AspNet.Identity.EntityFramework.IdentityRole> roles = _context.Roles.ToList< Microsoft.AspNet.Identity.EntityFramework.IdentityRole>();
            Microsoft.AspNet.Identity.EntityFramework.IdentityRole newRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            newRole.Name = "new role";
            roles.Add(newRole);
            return View(roles);
        }
        [Authorize(Roles = "Admin"), HttpPost]
        public async Task<ActionResult> AddRole(List<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> modelList)
        {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            foreach(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role in modelList)
            {
                if (!await roleManager.RoleExistsAsync(role.Name)) await roleManager.CreateAsync(role);
            }
            return View(modelList);
        }
        public ActionResult AddUserToRole()
        {
            return View();
        }
        [Authorize(Roles = "Admin"), HttpPost]
        public ActionResult AddUserToRole(ApplicationUser user, string roleName)
        {

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
                var result1 = UserManager.AddToRoles(user.Id, "Admin");

            return View();
        }


    }
}