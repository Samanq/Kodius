using Kodius.Presentation.WebApp.Models;
using Kodius.Presentation.WebApp.Services.Interfaces;
using Kodius.Presentation.WebApp.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Kodius.Presentation.WebApp.Controllers;

public class FlightsController : Controller
{
    private readonly IAmadeusService _amadeusService;

    public FlightsController(IAmadeusService amadeusService)
    {
        _amadeusService = amadeusService;
    }

    public IActionResult Index()
    {
        ViewBag.Cities = Iata.Cities();

        return View(new FlightSearchModel
        {
            Date = DateTime.Now.Date
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index([Bind("Origin,Destination,Date")] FlightSearchModel model)
    {
        ViewBag.Cities = Iata.Cities();

        if (ModelState.IsValid)
        {
            try
            {
                ViewBag.Flights = _amadeusService.GetFlightOffers(model.Date,model.Origin,model.Destination);
            }
            catch (Exception e)
            {
            }
        }

        return View(model);
    }
}
