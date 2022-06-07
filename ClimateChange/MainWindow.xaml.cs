using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLib;

namespace ClimateChange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            //laden van datagridCountries 
            DataBewerking dataBewerking = new DataBewerking();
            DataGridCountries.ItemsSource = dataBewerking.GetCountriesDataView();
        
            ComboBoxOpvullen();
        }

        private void ComboBoxCountries_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBox)sender;
            var selecedItem = item.SelectedItem;

            DataBewerking dataBewerking = new DataBewerking();
            var country = dataBewerking.GetCountries().First(x => x.CountryName == selecedItem.ToString());
            
            Uri uri = new Uri($"/images/flags/{country.ImageFilePath}", UriKind.Relative);
            ImageSource imgSource = new BitmapImage(uri);
            ImageCountryFlag.Source = imgSource;
            TextBlockCountryName.Text = country.CountryName;
            TextBlockCountryRegion.Text = country.Region;
            TextBlockCountrySubRegion.Text = country.Subregion;

            var listTempChages = dataBewerking.GetTempChangeByCountry(country);
            ListBoxTempChange.ItemsSource = listTempChages;

        }


        private void ComboBoxOpvullen()
        {
            DataBewerking dataBewerking = new DataBewerking();
            var listCountries = dataBewerking.GetCountries();
            ComboBoxCountries.ItemsSource = listCountries;
        }
    }
}