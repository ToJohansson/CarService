using System.ComponentModel.DataAnnotations;

namespace CarService.Web.Views.Cars;

public class UpdateServiceItemVM
{
    public required int CarId { get; set; }
    public required int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100)]
    public required string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, int.MaxValue)]
    public int? KmInterval { get; set; }

    [Range(0, 50)]
    public int? TimeIntervalMonths { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public required DateTime LastService { get; set; }

    [Range(0, int.MaxValue)]
    public required int TripMeterWhenService { get; set; }
}
