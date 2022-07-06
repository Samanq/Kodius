namespace Kodius.Presentation.WebApp.Models;

public class FlightSearchModel
{
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
