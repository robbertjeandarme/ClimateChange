using System.Data;
using System.Globalization;
using System.Net;

namespace ClassLib;

public class DataBewerking
{
    public DataSet ClimateChangeDataSet;


    public List<Country> GetCountries()
    {
        var countryDataTable = InitializeCountriesDataTable();

        var listCountry = countryDataTable.AsEnumerable().Select(x => new Country()
        {
            CountryCode = x.Field<string>("Alpha3"),
            CountryName = x.Field<string>("Country"),
            ImageFilePath = x.Field<string>("ImagesFileName"),
            Region = x.Field<string>("Region"),
            Subregion = x.Field<string>("Subregion")
        }).ToList();

        return listCountry;
    }

    public DataView GetCountriesDataView()
    {
        var dt = InitializeCountriesDataTable();
        return dt.DefaultView;
    }

    public DataView GetTempChangeDataView()
    {
        var dt = InitializeTempChangeDataTable();
        return dt.DefaultView;
    }

    public List<TempChange> GetTempChangeByCountry(Country country)
    {
        //CountryCode,CountryName,Year,TempChange
        var dt = InitializeTempChangeDataTable();
        var listOfTempChange = dt.AsEnumerable().Select(x => new TempChange()
        {
            CountryCode = x.Field<string>("CountryCode"),
            CountryName = x.Field<string>("CountryName"),
            Year = x.Field<int>("Year"),
            Chagne = x.Field<double>("TempChange")
        }).ToList().Where(p => p.CountryName == country.CountryName).ToList();
        
        return listOfTempChange;
    }

    public List<TempChange> GetWorstYearsAfter20000()
    {
        var dt = InitializeTempChangeDataTable();
        var listOfTempChange = dt.AsEnumerable().Select(x => new TempChange()
        {
            CountryCode = x.Field<string>("CountryCode"),
            CountryName = x.Field<string>("CountryName"),
            Year = x.Field<int>("Year"),
            Chagne = x.Field<double>("TempChange")
        }).ToList();

        var result = listOfTempChange.Where(t => t.Year > 2000 && t.Chagne > 1.2).OrderByDescending(t => t.Chagne).ToList();
        return result;
    }

    private void AddRowsToDataTableFromFile(DataTable dt, string fileName)
    {
        
    }

    public void InitializeDataSet()
    {
        ClimateChangeDataSet = new DataSet();
        ClimateChangeDataSet.Tables.Add(InitializeCountriesDataTable());
        ClimateChangeDataSet.Tables.Add(InitializeTempChangeDataTable());
    }

    public DataTable InitializeCountriesDataTable()
    {
        DataTable countriesDataTable = new DataTable("Countries");

        DataColumn country = new DataColumn("Country", typeof(string));
        DataColumn imagesFileName = new DataColumn("ImagesFileName", typeof(string));
        DataColumn alpha3 = new DataColumn("Alpha3", typeof(string));
        DataColumn region = new DataColumn("Region", typeof(string));
        DataColumn subRegion = new DataColumn("SubRegion", typeof(string));

        countriesDataTable.Columns.Add(country);
        countriesDataTable.Columns.Add(imagesFileName);
        countriesDataTable.Columns.Add(alpha3);
        countriesDataTable.Columns.Add(region);
        countriesDataTable.Columns.Add(subRegion);

        DataColumn[] primaryKey = new DataColumn[] { country };

        countriesDataTable.PrimaryKey = primaryKey;
        using (StreamReader sr = new StreamReader("Country_codes_and_flags.csv"))
        {
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] csv = sr.ReadLine().Split(',');
                countriesDataTable.Rows.Add(csv);
            }
        }

        return countriesDataTable;
    }

    public DataTable InitializeTempChangeDataTable()
    {
        //CountryCode,CountryName,Year,TempChange
        DataTable tempChangeDataTable = new DataTable("TempChange");

        DataColumn countryCode = new DataColumn("CountryCode", typeof(string));
        DataColumn countryName = new DataColumn("CountryName", typeof(string));
        DataColumn year = new DataColumn("Year", typeof(int));
        DataColumn tempChange = new DataColumn("TempChange", typeof(double));
        tempChange.AllowDBNull = true;
        year.AllowDBNull = true;
        countryCode.AllowDBNull = true;
        countryName.AllowDBNull = true;
        tempChangeDataTable.Columns.Add(countryCode);
        tempChangeDataTable.Columns.Add(countryName);
        tempChangeDataTable.Columns.Add(year);
        tempChangeDataTable.Columns.Add(tempChange);
        // DataColumn[] primaryKey = new DataColumn[] { countryCode };
        //
        // tempChangeDataTable.PrimaryKey = primaryKey;

        using (StreamReader sr = new StreamReader("Temperature_change.csv"))
        {
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] csv = sr.ReadLine().Split(',');
                tempChangeDataTable.Rows.Add(csv);
            }
        }

        return tempChangeDataTable;
    }
}