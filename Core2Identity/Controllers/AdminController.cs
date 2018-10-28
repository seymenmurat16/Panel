using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core2Identity.Controllers
{
    public class AdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IDuyuruRepository repository;
        private IYorumRepository YorumRepository;
        public AdminController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, IDuyuruRepository duyrepo,RoleManager<IdentityRole> _roleManager, IYorumRepository _YorumRepository)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            repository = duyrepo;
            roleManager = _roleManager;
            YorumRepository = _YorumRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Kayit(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

                return View(model);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password,false,false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/Admin/Panel");
                    }
                }
                ModelState.AddModelError("UserName", "Invalid Email or Password");
            }

            return View(model);
        }

        [Authorize(Roles="Admin")]
        public IActionResult Panel()
        {
            return View(repository.Duyuru);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Duyuru duyuru)
        {
            if (ModelState.IsValid)
            {
                repository.CreateDuyuru(duyuru);
                return RedirectToAction("Panel");
            }
            return View(duyuru);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            return View(repository.GetDuyuruId(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(Duyuru duyuru)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateDuyuru(duyuru);
                return RedirectToAction("Panel");
            }
            return View(duyuru);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            repository.DeleteDuyuru(id);
            return RedirectToAction("Panel");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(userManager.Users);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Roles()
        {
            return View(roleManager.Roles);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(name);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string id)
        {

            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return RedirectToAction("Roles");
                    }
                }
            }
            return RedirectToAction("Roles");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            var members = new List<ApplicationUser>();
            var nonmembers = new List<ApplicationUser>();

            foreach (var user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name)?members:nonmembers;
                list.Add(user);
            }

            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(RoleEditModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await userManager.FindByIdAsync(userId);

                    if(user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                return RedirectToAction("EditRole", model.RoleId);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Yorumlar()
        {
            return View(YorumRepository.Yorum);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteYorum(int id)
        {
            YorumRepository.DeleteYorum(id);
            return RedirectToAction("Yorumlar");
        }
    }
}