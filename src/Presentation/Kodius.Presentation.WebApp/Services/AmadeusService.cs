using amadeus;
using amadeus.resources;
using Kodius.Presentation.WebApp.Services.Interfaces;

namespace Kodius.Presentation.WebApp.Services;

public class AmadeusService : IAmadeusService
{
    public Amadeus AmadeusApi { get; set; }

    public AmadeusService(IConfiguration configuration)
    {
        var apiKey = configuration["Amadeus:ApiKey"];
        var apiSecret = configuration["Amadeus:ApiSecret"];

        amadeus.Configuration amadeusconfig = Amadeus.builder(apiKey, apiSecret);
        amadeusconfig.setLoglevel("debug");
        AmadeusApi = amadeusconfig.build();
    }
    public FlightOffer[] GetFlightOffers(DateTime date, string origin, string destination)
    {
        var searchDate = date.Date.ToString("yyyy-MM-dd");

        FlightOffer[] flights = AmadeusApi.shopping.flightOffersSearch.getFlightOffers(Params
            .with("originLocationCode", origin)
            .and("destinationLocationCode", destination)
            .and("departureDate", searchDate)
            .and("adults", "1")
            .and("nonStop", "false")
            .and("max", "5"));

        return flights;
    }
}
