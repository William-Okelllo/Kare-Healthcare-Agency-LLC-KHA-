using Ishop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ishop.Startup))]
namespace Ishop
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
          
            ConfigureAuth(app);
            createRolesandUsers();

        }



        // In this method we will create default User roles and Admin user for login    
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);


                role.Name = "System";
                roleManager.Create(role);

                //huyu ni bazeng                 

                var user = new ApplicationUser();
                user.UserName = "Muru";
                user.Email = "murucharls@gmail.com";

                string userPWD = "Ch@rlz007";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }
          

          
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Tickets_Approval"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Tickets_Approval";
                roleManager.Create(role);

            }
            
            if (!roleManager.RoleExists("DashBoard"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "DashBoard";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Tickets_Invoicing"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Tickets_Invoicing";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Leaves_Approval"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Leaves_Approval";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Report_Viewer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Report_Viewer";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Finance"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Finance";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Advance_Approvals"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Advance_Approvals";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Ticket_View"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Ticket_View";
                roleManager.Create(role);

            }
        }
    }
}