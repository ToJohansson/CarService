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
            return View(car); // returnera popup som säger om det var bu eller bä

        return RedirectToAction(nameof(Index));
    }
    [HttpGet("details/{id:int}")]
    public IActionResult Details(int id)
    {
        return View(service.GetCarById(id));
    }
    [HttpGet("additem/{id:int}")]
    public IActionResult AddItem(int id)
    {
        ViewBag.CarId = id;
        return View();
    }

    [HttpPost("additem/{id:int}")]
    public IActionResult AddItem(int id, ServiceItem item)
    {
        if (!ModelState.IsValid)
            return View(item);

        if (!service.AddServiceItem(id, item))
            return View(item); // returnera popup som säger om det var bu eller bä

        return RedirectToAction(nameof(Details), new { id = id });
    }

}
