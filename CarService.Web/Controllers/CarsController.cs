using Microsoft.AspNetCore.Mvc;
using CarService.Web.Services;
using CarService.Web.Models;

namespace CarService.Web.Controllers;
public class CarsController : Controller
{
    public static CarServices service = new CarServices();
    [HttpGet("")]
    public IActionResult Index()
    {
        return View(service.GetAllCars());
    }
    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost("add")]
    public IActionResult Add(Car car)
    {
        if (!ModelState.IsValid)
            return View(car);

        if (!service.AddCar(car))
            return View(car);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet("details/{id:int}")]
    public IActionResult Details(int id)
    {
        return View(service.GetCarById(id));
    }

}
