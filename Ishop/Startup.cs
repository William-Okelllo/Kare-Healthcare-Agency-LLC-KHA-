using Ishop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;


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
            if (!roleManager.RoleExists("Reports_Access"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Reports_Access";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Master_Data"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Master_Data";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Leaves_Management"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Leaves_Management";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Timesheet_Approvals"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Timesheet_Approvals";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("leave_Approvals"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "leave_Approvals";
                roleManager.Create(role);

            }
        }


    }

}
