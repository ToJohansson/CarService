using System.ComponentModel.DataAnnotations;

namespace CarService.Web.Views.Cars;

public class AddItemVM
{

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name can be at most 100 characters.")]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "Description can be at most 500 characters.")]
    public string Description { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Km interval must be a positive number.")]
    public int? KmInterval { get; set; }

    [Range(0, 50, ErrorMessage = "Time interval in years must be between 0 and 50.")]
    public int? TimeIntervalMonths { get; set; }

    [Required(ErrorMessage = "Last service date is required.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime LastService { get; set; }

    [Range(0, int.MaxValue)]
    public int TripMeterWhenService { get; set; }

}
