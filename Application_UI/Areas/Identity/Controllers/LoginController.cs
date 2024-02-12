using BusinessLogic.ViewModels;
using DataAccess.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application_UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class LoginController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public string ReturnUrl { get; set; }

        public LoginController(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.roleManager = roleManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]//OnPostAsync//loginmethoud
        public async Task<IActionResult> loginmethoud([Bind("Email,PasswordHash")] UserViewModel aspNetUsers, string returnUrl = null)
        {
            try
            {
                //http://localhost:63671/Admin/home/Index
                returnUrl = returnUrl ?? Url.Content("~/Admin/Home/Index");
                await addAdmin();
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(aspNetUsers.Email, aspNetUsers.PasswordHash, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var Usertype = _userManager.Users.FirstOrDefault(e => e.Email == aspNetUsers.Email);
                        if (Usertype != null)
                        {

                            var uUsertype = Usertype.UserType;
                            await GetUserRoles(aspNetUsers.Email);
                            if (uUsertype == 1)
                            {
                                return LocalRedirect(returnUrl);
                            }
                            else
                            {
                                var user = _userManager.Users.FirstOrDefault(e => e.Email == aspNetUsers.Email);
                                if (user != null)
                                {
                                    var uUserID = user.Id;
                                    TempData["uUserID"] = uUserID;
                                }
                                List<string> Roles = await GetUserRoles(aspNetUsers.Email);
                                returnUrl = Url.Content("/");
                                if (Roles.Contains("Admin"))
                                {
                                    returnUrl = Url.Content("~/Admin/Home/Index");
                                }
                                return LocalRedirect(returnUrl);
                            }
                        }
                        else
                        {
                            return View("index", aspNetUsers);
                        }

                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = false });
                    }
                    if (result.IsLockedOut)
                    {
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        return View("index", aspNetUsers);
                    }
                }

                // If we got this far, something failed, redisplay form
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        public async Task<IActionResult> LogoutMethoud(string returnUrl = null)
        {
            try
            {
                await _signInManager.SignOutAsync();
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    returnUrl = Url.Content("~/User/Home/Index");
                    return Redirect("/");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View("LogoutM");
            }
        }

        public async Task<List<string>> GetUserRoles(string Email)
        {
            List<string> roles = new List<string>();
            if (Email != null)
            {
                var user = await _userManager.FindByEmailAsync(Email);

                if (user != null)
                {
                    roles = _userManager.GetRolesAsync(user).Result.ToList();

                    ViewBag.UserRoles = roles;
                }
            }
            return roles;
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
                var firstRoleName = ListRole.FirstOrDefault(x => x.Name == AddRoleName);
                if (firstRoleName != null)
                {
                    RoleName = firstRoleName.Name;
                }
            }
            Users UserModel;

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
