using System.ComponentModel.DataAnnotations;

namespace CarService.Web.Views.Cars;

public class IndexVM
{
    public CarVM[] Cars { get; set; }
    public AddCarVM AddCarVm { get; set; }
    public class CarVM
    {
        public required int Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
    }
    public class AddCarVM
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
        [Display(Name = "Trip")]
        public int TripMeter { get; set; }
    }
}
