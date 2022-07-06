using amadeus;
using amadeus.resources;

namespace Kodius.Presentation.WebApp.Services.Interfaces
{
    public interface IAmadeusService
    {
        public Amadeus AmadeusApi { get; set; }
        FlightOffer[] GetFlightOffers(DateTime date, string origin, string destination);
    }
}