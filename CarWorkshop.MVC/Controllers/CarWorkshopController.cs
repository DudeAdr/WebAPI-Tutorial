using Microsoft.AspNetCore.Mvc;
using CarWorkshop.Application.Services;
using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Application.CarWorkshop;

namespace CarWorkshop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly ICarWorkshopService _carWorkshopService;

        public CarWorkshopController(ICarWorkshopService carWorkshopService)
        {
            _carWorkshopService = carWorkshopService;
        }

        public async Task<IActionResult> Index()
        {
            var carWorkshop = await _carWorkshopService.GetAll();
            return View(carWorkshop);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarWorkshopDto carWorkshop)
        {
            if(!ModelState.IsValid)
            {
                return View(carWorkshop);
            }

            await _carWorkshopService.Create(carWorkshop);
            return RedirectToAction(nameof(Index));
        }
    }
}
