using Microsoft.AspNetCore.Mvc;
using CarService.Web.Services;
using CarService.Web.Models;
using CarService.Web.Views.Cars;
using static CarService.Web.Views.Cars.DetailsVM;
using CarService.Web.Controllers.Loggers;
using static CarService.Web.Views.Cars.IndexVM;

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

    [HttpPost("")]
    public IActionResult CreateCar(IndexVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", vm);
        }

        var car = new Car
        {
            Brand = vm.AddCarVm.Brand,
            Model = vm.AddCarVm.Model,
            Year = vm.AddCarVm.Year,
            EngineType = vm.AddCarVm.EngineType,
            TripMeter = vm.AddCarVm.TripMeter
        };

        if (!service.AddCar(car))
        {
            return View("Index", vm);
        }

        return RedirectToAction(nameof(Index));
    }


    [HttpGet("cars/delete/{carId}")]
    public IActionResult DeleteCar(int carId)
    {
        if (!service.DeleteCarById(carId))
            return RedirectToAction(nameof(Index)); // visa upp felmeddelande? 

        return RedirectToAction(nameof(Index));
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
    [HttpGet("/update/car/{id}")]
    public IActionResult UpdateCar(int id)
    {
        var model = service.GetCarById(id);

        var viewModel = new UpdateCarVM
        {
            CarId = model.Id,
            Brand = model.Brand,
            Model = model.Model,
            Year = model.Year,
            EngineType = model.EngineType,
            TripMeter = model.TripMeter,
        };

        return View(viewModel);
    }

    [HttpPost("/update/car/{id}")]
    [ServiceFilter(typeof(LogFilter))]
    public IActionResult UpdateCar(int id, UpdateCarVM updateCarVM)
    {
        if (!ModelState.IsValid)
        {
            return View(updateCarVM);
        }

        service.UpdateCar(updateCarVM);

        return RedirectToAction(nameof(Details), new { id });
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

        return RedirectToAction(nameof(Details), new { id });
    }
    [HttpGet("/cars/{carId}/serviceitem/edit/{itemId}")]
    public IActionResult UpdateServiceItem(int carId, int itemId)
    {
        var car = service.GetCarById(carId);
        if (car == null)
            return NotFound();

        var item = car.ServiceItems.FirstOrDefault(x => x.Id == itemId);
        if (item == null)
            return NotFound();

        var vm = new UpdateServiceItemVM
        {
            CarId = carId,
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            KmInterval = item.KmInterval,
            TimeIntervalMonths = item.TimeIntervalMonths,
            LastService = item.LastService,
            TripMeterWhenService = item.TripMeterWhenService
        };

        return View(vm);
    }

    [HttpPost("/cars/{carId}/serviceitem/edit/{itemId}")]
    public IActionResult UpdateServiceItem(UpdateServiceItemVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var car = service.GetCarById(vm.CarId);
        if (car == null)
            return NotFound();

        var item = car.ServiceItems.FirstOrDefault(x => x.Id == vm.Id);
        if (item == null)
            return NotFound();

        item.Name = vm.Name;
        item.Description = vm.Description;
        item.KmInterval = vm.KmInterval;
        item.TimeIntervalMonths = vm.TimeIntervalMonths;
        item.LastService = vm.LastService;
        item.TripMeterWhenService = vm.TripMeterWhenService;

        return RedirectToAction("Details", new { id = vm.CarId });
    }

    [HttpGet("/cars/{carId}/serviceitem/delete/{itemId}")]
    public IActionResult DeleteServiceItem(int carId, int itemId)
    {
        if (!service.RemoveServiceItem(carId, itemId))
            return RedirectToAction("Details", new { id = carId }); // visa upp felmeddelande? 

        return RedirectToAction("Details", new { id = carId });
    }

}
