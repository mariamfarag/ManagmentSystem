using DataAccess.User;
using Microsoft.AspNetCore.Identity;
using static DataAccess.Enums.UserRoleList;

namespace Application_UI.Utility
{
    public class IdentityHelper
    {
        protected IdentityHelper()
        {
        }

        public static async Task CreateRolesIfNotExistAsync(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();

            UserRole[] allRoles = (UserRole[])Enum.GetValues(typeof(UserRole));
            var tr = allRoles[0].ToString();
            var roles = allRoles; 
            var usersWithRoles = new List<(string Username, string Password, string[] Roles)>
            {
                ("SuperAdmin@SuperAdmin.com", "SuperAdmin123!", new[] { allRoles[0].ToString()}),
                ("admin@admin.com", "Admin123!", new[] { allRoles[1].ToString() }),
                ("TestUser@TestCustomer.com", "TestUser123!", new[] { allRoles[2].ToString() })
            };
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role.ToString());
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }
            await CreateUsersWithRolesAsync(userManager, usersWithRoles);
        }

        public static async Task CreateUsersWithRolesAsync(UserManager<Users> userManager, IEnumerable<(string Username, string Password, string[] Roles)> usersWithRoles)
        {
            foreach (var userData in usersWithRoles)
            {
                var user = new Users { UserName = userData.Username, Email = userData.Username, EmailConfirmed = true,FirstName=" ",LastName= " " };
                var result = await userManager.CreateAsync(user, userData.Password);

                if (result.Succeeded)
                {
                    foreach (var role in userData.Roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }

    }
}
