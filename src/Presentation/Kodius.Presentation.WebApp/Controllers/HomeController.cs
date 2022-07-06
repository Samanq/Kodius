using amadeus;
using amadeus.resources;
using Kodius.Presentation.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kodius.Presentation.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {

                var apikey = "FTbCzBAZuZjTw7wB0GQGGGV0MGpBLmZM";
                var apisecret = "9vws6SPp2Ofb25GG";

                Configuration amadeusconfig = Amadeus.builder(apikey, apisecret);
                amadeusconfig.setLoglevel("debug");
                Amadeus amadeus = amadeusconfig.build();


                FlightOffer[] flights = amadeus.shopping.flightOffersSearch.getFlightOffers(Params
                    .with("originLocationCode", "ZAG")
                    .and("destinationLocationCode", "ZAD")
                    .and("departureDate", "2022-07-15")
                    .and("adults", "1")
                    .and("nonStop", "false")
                    .and("max", "5"));

                ViewBag.Flights = flights;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.ToString());
            }
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}