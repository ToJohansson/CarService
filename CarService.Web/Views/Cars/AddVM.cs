using CarService.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CarService.Web.Views.Cars;

public class AddVM
{
    [Display(Name = "Brand")]
    [Required(ErrorMessage = "Brand is required!")]
    public string Brand { get; set; }

    [Display(Name = "Model")]
    [Required(ErrorMessage = "Model is required!")]
    public string Model { get; set; }

    [Display(Name = "Manufacturing Year")]
    [Required(ErrorMessage = "Year is required!")]
    [Range(1886, int.MaxValue, ErrorMessage = "Please enter a valid year.")]
    public int Year { get; set; }

    [Display(Name = "Engine Type")]
    [Required(ErrorMessage = "Engine type is required!")]
    public string EngineType { get; set; }


    [Range(0, int.MaxValue)]
    public int TripMeter { get; set; }
}
