namespace Kodius.Presentation.WebApp.Tools;

public static class Iata
{
    private static Dictionary<string, string> _cities = new Dictionary<string, string>()
    {
        {"zagreb","ZAG"},
        {"zadar","ZAD"},
        {"madrid","MAD"},

    };
    private static Dictionary<string, string> _airports = new Dictionary<string, string>()
    {
        {"","" }
    };

    public static Dictionary<string, string> Cities()
    {
        return _cities;
    }
    public static Dictionary<string, string> Airports()
    {
        return _airports;
    }
}
