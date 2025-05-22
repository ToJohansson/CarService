using Microsoft.AspNetCore.Mvc;

namespace CarService.Web.Controllers;
public class ErrorController : Controller
{
    [HttpGet("error/exception")]
    public IActionResult ServerError()
    {
        return View();
    }
    [HttpGet("error/http/{status}")]
    public IActionResult HttpError(int status)
    {
        return View(status);
    }
}
