using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;
using Core2Identity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Core2Identity.Controllers
{
    public class HomeController : Controller
    {
        private IDuyuruRepository repository;
        private IYorumRepository yorumRepository;
        public HomeController(IDuyuruRepository duyrepo, IYorumRepository _yorumRepository)
        {
            repository = duyrepo;
            yorumRepository = _yorumRepository;
        }

        public IActionResult Index()
        {
            return View(repository.Duyuru);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Duyuru duyuru = repository.GetDuyuruId(id);
            IQueryable<Yorum> yorumlar = yorumRepository.DuyuruYorumlar(id);
            var viewmodel = new DuyuruYorumViewModel();
            viewmodel.Duyuru = duyuru;
            viewmodel.Yorumlar = yorumlar;
            viewmodel.Yorum = null;
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult Detail(Yorum model)
        {
            if (ModelState.IsValid)
            {
                yorumRepository.CreateYorum(model);
                return RedirectToAction("Detail", model.YorumId);
            }
            return RedirectToAction("Detail", model.YorumId);
        }
    }
}