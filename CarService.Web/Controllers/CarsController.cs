using Microsoft.AspNetCore.Mvc;
using CarService.Web.Services;
using CarService.Web.Models;
using CarService.Web.Views.Cars;
using static CarService.Web.Views.Cars.DetailsVM;
using CarService.Web.Controllers.Loggers;

namespace CarService.Web.Controllers;
public class CarsController(CarServices service) : Controller
{
    [ServiceFilter(typeof(LogFilter))]
    [HttpGet("")]
    public IActionResult Index()
    {
        var model = service.GetAllCars();
        var viewModel = new IndexVM
        {
            Cars = model
            .Select(o => new IndexVM.CarVM
            {
                Id = o.Id,
                Brand = o.Brand,
                Model = o.Model,
            }).ToArray()
        };
        return View(viewModel);
    }

    [ServiceFilter(typeof(LogFilter))]
    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }

    [ServiceFilter(typeof(LogFilter))]
    [HttpPost("add")]
    public IActionResult Add(AddVM carVM)
    {
        if (!ModelState.IsValid)
            return View(carVM);

        var car = new Car
        {
            Brand = carVM.Brand,
            Model = carVM.Model,
            Year = carVM.Year,
            EngineType = carVM.EngineType,
        };

        if (!service.AddCar(car))
            return View(carVM); // returnera popup som säger om det var bu eller bä

        return RedirectToAction(nameof(Index));
    }

    [ServiceFilter(typeof(LogFilter))]
    [HttpGet("details/{id:int}")]
    public IActionResult Details(int id)
    {
        var model = service.GetCarById(id);
        return View(ServiceMapper.CheckServiceItemStatuses(model));
    }

    [ServiceFilter(typeof(LogFilter))]
    [HttpPut("/update/car/{id}")]
    public IActionResult UpdateCar(int id)
    {

    }

    [ServiceFilter(typeof(LogFilter))]
    [HttpGet("additem/{id:int}")]
    public IActionResult AddItem(int id)
    {
        ViewBag.CarId = id;
        return View();
    }

    [ServiceFilter(typeof(LogFilter))]

    [HttpPost("additem/{id:int}")]
    public IActionResult AddItem(int id, AddItemVM itemVM)
    {
        if (!ModelState.IsValid)
            return View(itemVM);

        var item = new ServiceItem
        {
            Name = itemVM.Name,
            Description = itemVM.Description,
            KmInterval = itemVM.KmInterval,
            TimeIntervalMonths = itemVM.TimeIntervalMonths,
            LastService = itemVM.LastService,
            TripMeterWhenService = itemVM.TripMeterWhenService,
        };

        if (!service.AddServiceItem(id, item))
            return View(itemVM); // returnera popup som säger om det var bu eller bä

        return RedirectToAction(nameof(Details), new { id = id });
    }

}
