using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PersonApp.Models;

[assembly: OwinStartupAttribute(typeof(PersonApp.Startup))]
namespace PersonApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("admin"))
            {
                //create super admin role
                var role = new IdentityRole("admin");
                roleManager.Create(role);

                //create default user
                var user = new ApplicationUser();
                user.UserName = "support@personal.com";
                user.Email = "support@personal.com";
                string pwd = "Password@2018";
                var newuser = userManager.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "admin");
                }
            }
            if (!roleManager.RoleExists("normal"))
            {
                //create super admin role
                var role = new IdentityRole("normal");
                roleManager.Create(role);
            }
        }
    }
}
