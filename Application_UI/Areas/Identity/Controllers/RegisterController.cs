using BusinessLogic.ViewModels;
using DataAccess.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application_UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class RegisterController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public RegisterController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager = null)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        { 
            return View();
        }

        public async Task<IActionResult> RegisterNewUser([Bind("Email,PasswordHash,FirstName,LastName")] UserViewModel aspNetUsers, string returnUrl = null)
        {
            try
            {
                returnUrl = returnUrl ?? Url.Content("~/User/Home/Index");
                await addAdmin();
                if (ModelState.IsValid)
                {
                    var user = new Users { UserName = aspNetUsers.Email, Email = aspNetUsers.Email, EmailConfirmed = true, UserType = 2 };
                    var result = await _userManager.CreateAsync(user, aspNetUsers.PasswordHash);
                    if (result.Succeeded)
                    {
                        aspNetUsers.Id = user.Id.ToString();
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ViewBag.Errors = result.Errors.Select(s => s.Description).ToList();
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch
            {
                return LocalRedirect(returnUrl);
            }
        }
        private async Task<IdentityResult> addAdmin()
        {
            Users user;
            var existingUser = await _userManager.FindByEmailAsync("Admin@Admin.com");
            string AddEmail = "Admin@Admin.com";
            if (existingUser == null)
            {
                user = new Users()
                {
                    Email = AddEmail,
                    UserName = AddEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "root",
                };
                await _userManager.CreateAsync(user, "123456789aA@");
            }
            var resultAddAdminRole = await addAdminRole(AddEmail);
            return resultAddAdminRole;
        }
        private async Task<IdentityResult> addAdminRole(string AddEmail)
        {
            string RoleName = "";
            string AddRoleName = "Admin";
            Users UserModel;
            IEnumerable<IdentityRole> ListRole;
            ListRole = await roleManager.Roles.ToListAsync();
            if (!(ListRole.Any(x => x.Name == AddRoleName)))
            {
                IdentityRole roles = new IdentityRole()
                {
                    Name = AddRoleName
                };
                await roleManager.CreateAsync(roles);
                RoleName = roles.Name;
            }
            else
            {
                var ListRoleFirstOrDefault = ListRole.FirstOrDefault(x => x.Name == AddRoleName);
                if (ListRoleFirstOrDefault != null)
                {

                    RoleName = ListRoleFirstOrDefault.Name;
                }
            }
            if (await _userManager.FindByEmailAsync(AddEmail) != null)
            {
                UserModel = await _userManager.FindByEmailAsync(AddEmail);
            }
            else
            {
                UserModel = await _userManager.FindByEmailAsync(AddEmail);
            }
            if (!string.IsNullOrEmpty(RoleName))
            {
                var result = await _userManager.AddToRoleAsync(UserModel, RoleName);
                return result;
            }
            else
            {
                return new IdentityResult();
            }
        }

    }
}
