namespace ClassLib;

public class Country
{
    public string CountryCode { get; set; }
    public string CountryName { get; set; }
    public string ImageFilePath { get; set; }
    public string Region { get; set; }
    public string Subregion { get; set; }


    public Country()
    {
        
    }
    public Country(string countryCode, string countryName, string imageFilePath, string region, string subregion)
    {
        CountryCode = countryCode;
        CountryName = countryName;
        ImageFilePath = imageFilePath;
        Region = region;
        Subregion = subregion;
    }

    public override string ToString()
    {
        return CountryName;
    }
}