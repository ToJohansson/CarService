using Microsoft.AspNetCore.Mvc;
using CarService.Web.Services;

namespace CarService.Web.Controllers;
public class CarsController : Controller
{
    public static CarServices service = new CarServices();
    public IActionResult Index()
    {
        return View();
    }
}
