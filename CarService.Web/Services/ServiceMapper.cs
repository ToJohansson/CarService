using CarService.Web.Views.Cars;
using static CarService.Web.Views.Cars.DetailsVM;
using System.Reflection;
using CarService.Web.Models;
namespace CarService.Web.Services;

public static class ServiceMapper
{
    public static DetailsVM CheckServiceItemStatuses(Car model)
    {
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
        return viewModel;
    }
}
