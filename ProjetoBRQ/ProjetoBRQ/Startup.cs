using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System.Web;

[assembly: OwinStartupAttribute(typeof(ProjetoBRQ.Startup))]
namespace ProjetoBRQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private async void createRolesandUsers()
        {
            var Db = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Db));

            if (!roleManager.RoleExists("ADMIN"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "ADMIN";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin.admin@admin.com";
                user.Email = "admin.admin@admin.com";

                string userPWD = "admin@admin123";

                var ckUser = await UserManager.CreateAsync(user, userPWD);
                if (ckUser.Succeeded)
                    UserManager.AddToRole(user.Id,role.Name);
                                          
            }
        }
    }
}
