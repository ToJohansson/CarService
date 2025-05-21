using Microsoft.AspNetCore.Mvc;
using CarService.Web.Services;
using CarService.Web.Models;
using CarService.Web.Views.Cars;
using static CarService.Web.Views.Cars.DetailsVM;

namespace CarService.Web.Controllers;
public class CarsController(CarServices service) : Controller
{

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
    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }
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
    [HttpGet("details/{id:int}")]
    public IActionResult Details(int id)
    {
        var model = service.GetCarById(id);
        var currentDate = DateTime.Now;
        var currentTripMeter = model.TripMeter;

        var viewModel = new DetailsVM
        {
            Id = model.Id,
            Brand = model.Brand,
            Model = model.Model,
            Year = model.Year,
            EngineType = model.EngineType,
            TripMeter = model.TripMeter,
            serviceItemsVM = model.ServiceItems
            .Select(m =>
            {
                var kmPassed = currentTripMeter - m.TripMeterWhenService;
                var monthsPassed = ((currentDate.Year - m.LastService.Year) * 12) + currentDate.Month - m.LastService.Month;

                bool isKmOverdue = m.KmInterval.HasValue && kmPassed >= m.KmInterval.Value;
                bool isTimeOverdue = m.TimeIntervalMonths.HasValue && monthsPassed >= m.TimeIntervalMonths.Value;

                bool isKmDue = m.KmInterval.HasValue && kmPassed >= (m.KmInterval.Value - 500);
                bool isTimeDue = m.TimeIntervalMonths.HasValue && monthsPassed >= (m.TimeIntervalMonths.Value - 1);

                var status = ServiceStatusVM.Ok;

                if (isKmOverdue || isTimeOverdue)
                    status = ServiceStatusVM.Overdue;
                else if (isKmDue || isTimeDue)
                    status = ServiceStatusVM.Due;

                return new ServiceItemsVM
                {
                    Name = m.Name,
                    Description = m.Description,
                    KmInterval = m.KmInterval,
                    TimeIntervalMonths = m.TimeIntervalMonths,
                    TripMeterWhenService = m.TripMeterWhenService,
                    LastService = m.LastService,
                    Status = status
                };
            })
            .OrderByDescending(m => m.Status)
            .ToArray()
        };

        return View(viewModel);
    }

    [HttpGet("additem/{id:int}")]
    public IActionResult AddItem(int id)
    {
        ViewBag.CarId = id;
        return View();
    }

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
