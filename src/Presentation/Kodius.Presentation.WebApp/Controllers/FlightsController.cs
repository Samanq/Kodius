using amadeus;
using amadeus.resources;
using Kodius.Presentation.WebApp.Models;
using Kodius.Presentation.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace Kodius.Presentation.WebApp.Controllers
{
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
                    //var apikey = "FTbCzBAZuZjTw7wB0GQGGGV0MGpBLmZM";
                    //var apisecret = "9vws6SPp2Ofb25GG";

                    //amadeus.Configuration amadeusconfig = Amadeus.builder(apikey, apisecret);
                    //amadeusconfig.setLoglevel("debug");
                    //Amadeus amadeus = amadeusconfig.build();

                    //var searchDate = model.Date.ToString("yyyy-MM-dd");

                    //FlightOffer[] flights = amadeus.shopping.flightOffersSearch.getFlightOffers(Params
                    //    .with("originLocationCode", model.Origin)
                    //    .and("destinationLocationCode", model.Destination)
                    //    .and("departureDate", searchDate)
                    //    .and("adults", "1")
                    //    .and("nonStop", "false")
                    //    .and("max", "5"));

                    //ViewBag.Flights = flights;
                    ViewBag.Flights = _amadeusService.GetFlightOffers(model.Date,model.Origin,model.Destination);
                }
                catch (Exception e)
                {
                }
            }

            return View(model);
        }
    }
}
