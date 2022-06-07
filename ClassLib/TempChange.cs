namespace ClassLib;

public class TempChange
{
    public double? Chagne { get; set; }
    public string CountryCode { get; set; }
    public string CountryName { get; set; }
    public int Id { get; set; }
    public int Year { get; set; }

    public TempChange()
    {
        
    }
    public TempChange(double? chagne, string countryCode, string countryName, int id, int year)
    {
        Chagne = chagne;
        CountryCode = countryCode;
        CountryName = countryName;
        Id = id;
        Year = year;
    }
}