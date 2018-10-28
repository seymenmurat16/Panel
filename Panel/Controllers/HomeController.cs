using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panel.Models;

namespace Panel.Controllers
{

    public class HomeController : Controller
    {
        private UserManager<Kisi> userManager;
        private IKisiReprository reprository;
        private IDuyuruRepository duyrep;
        public HomeController(IKisiReprository repo, IDuyuruRepository duyrepro, UserManager<Kisi> _userManager)
        {
            reprository = repo;
            duyrep = duyrepro;
            userManager = _userManager;
        }



        public IActionResult Index()
        {
            return View(duyrep.Duyuru);
        }
        [HttpGet]
        public IActionResult Panel()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Panel(Kisi kisi)
        {           
            if (ModelState.IsValid)
            {
                if (reprository.SearchUser(kisi))
                {
                    return View("ControlPanel", duyrep.Duyuru);
                }
                return View();
            }
            else
            {
                return View();
            }   
        }

        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Kayit(Kisi kisi)
        {
            if (ModelState.IsValid)
            {
                Kisi user = new Kisi();
                user.Email = kisi.Email;
                user.Id = kisi.Id;
                user.Password = kisi.Password;

                var result = await userManager.CreateAsync(user, kisi.Password);

                if (result.Succeeded)
                {
                    //reprository.NewUser(kisi);
                    return RedirectToAction("Panel");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

                return View(kisi);

                
                
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Duyuru duyuru)
        {
            duyrep.CreateDuyuru(duyuru);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(duyrep.GetDuyuruId(id));
        }

        [HttpPost]
        public IActionResult Details(Duyuru duyuru)
        {
            duyrep.UpdateDuyuru(duyuru);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            duyrep.DeleteDuyuru(id);
            return RedirectToAction("Panel");
        }

    }
}