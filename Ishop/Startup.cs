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
            if (!roleManager.RoleExists("Admisions"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admisions";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Education"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Education";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("New_Americans"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "New_Americans";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Timesheets"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Timesheets";
                roleManager.Create(role);

            }

            
            if (!roleManager.RoleExists("Beneficiaries"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Beneficiaries";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Expenses"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Expenses";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Workplan"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Workplan";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Budget"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Budget";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Reports"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Reports";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("MasterData"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "MasterData";
                roleManager.Create(role);

            }
        }


    }

}
