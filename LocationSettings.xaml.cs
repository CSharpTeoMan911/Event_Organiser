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
using System.IO;
using Newtonsoft.Json;

namespace Event_Organiser
{
    /// <summary>
    /// Interaction logic for LocationSettings.xaml
    /// </summary>
    public partial class LocationSettings : Page
    {
        
        public LocationSettings()
        {
            InitializeComponent();
        }

        private bool LocationValue;
        private bool PostcodeValue;
        private bool StartLocationValue;
        private bool StartPostcodeValue;

        private void LocationSettingsPageLoaded(object sender, RoutedEventArgs e)
        {
            StartLocationButton.FontSize = LocationButton.FontSize / 1.25;
            LocationButton.FontSize = LocationButton.FontSize / 1.25;
            StartPostcodeButton.FontSize = StartPostcodeButton.FontSize / 1.25;
            PostcodeButton.FontSize = PostcodeButton.FontSize / 1.25;

            StartLocation.FontSize = StartLocation.FontSize / 1.25;
            Location.FontSize = Location.FontSize / 1.25;
            InfoTextBox.FontSize = InfoTextBox.FontSize / 1.25;
            StartPostcode.FontSize = StartPostcode.FontSize / 1.25;
            Postcode.FontSize = Postcode.FontSize / 1.25;

            Thickness StartLocationButtonThickness = new Thickness();
            StartLocationButtonThickness.Left = StartLocationButton.Margin.Left / 1.25;
            StartLocationButtonThickness.Right = StartLocationButton.Margin.Right / 2.25;
            StartLocationButtonThickness.Top = StartLocationButton.Margin.Top / 2.25;
            StartLocationButtonThickness.Bottom = StartLocationButton.Margin.Bottom / 2.25;

            StartLocationButton.Margin = StartLocationButtonThickness;

            Thickness LocationButtonThickness = new Thickness();
            LocationButtonThickness.Left = LocationButton.Margin.Left / 1.25;
            LocationButtonThickness.Right = LocationButton.Margin.Right / 2.25;
            LocationButtonThickness.Top = LocationButton.Margin.Top / 2.25;
            LocationButtonThickness.Bottom = LocationButton.Margin.Bottom / 2.25;

            LocationButton.Margin = LocationButtonThickness;

            Thickness StartPostcodeButtonThickness = new Thickness();
            StartPostcodeButtonThickness.Left = StartPostcodeButton.Margin.Left / 1.25;
            StartPostcodeButtonThickness.Right = StartPostcodeButton.Margin.Right / 2.25;
            StartPostcodeButtonThickness.Top = StartPostcodeButton.Margin.Top / 2.25;
            StartPostcodeButtonThickness.Bottom = StartPostcodeButton.Margin.Bottom / 2.25;

            StartPostcodeButton.Margin = StartPostcodeButtonThickness;

            Thickness PostcodeButtonThickness = new Thickness();
            PostcodeButtonThickness.Left = PostcodeButton.Margin.Left / 1.25;
            PostcodeButtonThickness.Right = PostcodeButton.Margin.Right / 2.25;
            PostcodeButtonThickness.Top = PostcodeButton.Margin.Top / 2.25;
            PostcodeButtonThickness.Bottom = PostcodeButton.Margin.Bottom / 2.25;

            PostcodeButton.Margin = PostcodeButtonThickness;

            Thickness StartLocationText = new Thickness();
            StartLocationText.Left = StartLocation.Margin.Left / 1.25;
            StartLocationText.Right = StartLocation.Margin.Right / 2.25;
            StartLocationText.Top = StartLocation.Margin.Top / 2.25;
            StartLocationText.Bottom = StartLocation.Margin.Bottom / 2.25;

            StartLocation.Margin = StartLocationText;

            Thickness LocationText = new Thickness();
            LocationText.Left = Location.Margin.Left / 1.25;
            LocationText.Right = Location.Margin.Right / 2.25;
            LocationText.Top = Location.Margin.Top / 2.25;
            LocationText.Bottom = Location.Margin.Bottom / 2.25;

            Location.Margin = LocationText;

            Thickness StartPostcodeText = new Thickness();
            StartPostcodeText.Left = StartPostcode.Margin.Left / 1.25;
            StartPostcodeText.Right = StartPostcode.Margin.Right / 2.25;
            StartPostcodeText.Top = StartPostcode.Margin.Top / 2.25;
            StartPostcodeText.Bottom = StartPostcode.Margin.Bottom / 2.25;

            StartPostcode.Margin = StartPostcodeText;

            Thickness PostcodeText = new Thickness();
            PostcodeText.Left = Postcode.Margin.Left / 1.25;
            PostcodeText.Right = Postcode.Margin.Right / 2.25;
            PostcodeText.Top = Postcode.Margin.Top / 2.25;
            PostcodeText.Bottom = Postcode.Margin.Bottom / 2.25;

            Postcode.Margin = PostcodeText;



          


            try
            {
                string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                string Getter = File.ReadAllText(Location);
                dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);
                InfoTextBox.Text = JsonFileConverter["StartLocation"];
            }
            catch { }


            BrushConverter bc = new BrushConverter();
            StartLocationButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");
            StartLocationButton.Background = new SolidColorBrush(Colors.White);
            StartLocationValue = true;
        }

        ~LocationSettings()
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.Collect();
        }


        private void StartLocationSetting(object sender, RoutedEventArgs e)
        {

            switch (App.AppClosing)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.MainWindow)
            {
                case null:
                    goto ApplicationClosing;
            }



            LocationValue = false;
            PostcodeValue = false;
            StartLocationValue = true;
            StartPostcodeValue = false;

            BrushConverter bc = new BrushConverter();

            InfoTextBox.SetValue(Grid.RowProperty, 0);

            LocationButton.Foreground = new SolidColorBrush(Colors.White);
            LocationButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            StartPostcodeButton.Foreground = new SolidColorBrush(Colors.White);
            StartPostcodeButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            StartLocationButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");
            StartLocationButton.Background = new SolidColorBrush(Colors.White);

            PostcodeButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");
            PostcodeButton.Foreground = new SolidColorBrush(Colors.White);

            try
            {
                string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                string Getter = File.ReadAllText(Location);
                dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);
                InfoTextBox.Text = JsonFileConverter["StartLocation"];
            }
            catch { }

        ApplicationClosing:;
        }

        private void StartPostcodeSetting(object sender, RoutedEventArgs e)
        {
            switch (App.AppClosing)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.MainWindow)
            {
                case null:
                    goto ApplicationClosing;
            }

            LocationValue = false;
            PostcodeValue = false;
            StartLocationValue = false;
            StartPostcodeValue = true;

            BrushConverter bc = new BrushConverter();

            InfoTextBox.SetValue(Grid.RowProperty, 3);

            LocationButton.Foreground = new SolidColorBrush(Colors.White);
            LocationButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            StartPostcodeButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");
            StartPostcodeButton.Background = new SolidColorBrush(Colors.White);

            StartLocationButton.Foreground = new SolidColorBrush(Colors.White);
            StartLocationButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            PostcodeButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");
            PostcodeButton.Foreground = new SolidColorBrush(Colors.White);

            try
            {
                string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                string Getter = File.ReadAllText(Location);
                dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);
                InfoTextBox.Text = JsonFileConverter["StartPostcode"];
            }
            catch { }

        ApplicationClosing:;
        }

        private void LocationSetting(object sender, RoutedEventArgs e)
        {
            switch (App.AppClosing)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.MainWindow)
            {
                case null:
                    goto ApplicationClosing;
            }


            LocationValue = true;
            PostcodeValue = false;
            StartLocationValue = false;
            StartPostcodeValue = false;

            BrushConverter bc = new BrushConverter();

            InfoTextBox.SetValue(Grid.RowProperty, 6);

            LocationButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");
            LocationButton.Background = new SolidColorBrush(Colors.White);

            StartPostcodeButton.Foreground = new SolidColorBrush(Colors.White);
            StartPostcodeButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            StartLocationButton.Foreground = new SolidColorBrush(Colors.White);
            StartLocationButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            PostcodeButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");
            PostcodeButton.Foreground = new SolidColorBrush(Colors.White);

            try
            {
                string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                string Getter = File.ReadAllText(Location);
                dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);
                InfoTextBox.Text = JsonFileConverter["Location"];
            }
            catch { }

        ApplicationClosing:;
        }
    
        private void PostcodeSetting(object sender, RoutedEventArgs e)
        {

            switch (App.AppClosing)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.MainWindow)
            {
                case null:
                    goto ApplicationClosing;
            }

            LocationValue = false;
            PostcodeValue = true;
            StartLocationValue = false;
            StartPostcodeValue = false;

            BrushConverter bc = new BrushConverter();

            InfoTextBox.SetValue(Grid.RowProperty, 9);


            LocationButton.Foreground = new SolidColorBrush(Colors.White);
            LocationButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            StartPostcodeButton.Foreground = new SolidColorBrush(Colors.White);
            StartPostcodeButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            StartLocationButton.Foreground = new SolidColorBrush(Colors.White);
            StartLocationButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            PostcodeButton.Background = new SolidColorBrush(Colors.White);
            PostcodeButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");


            try
            {
                string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                string Getter = File.ReadAllText(Location);
                dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);
                InfoTextBox.Text = JsonFileConverter["Postcode"];
            }
            catch { }

        ApplicationClosing:;
        }

        private void ContentChanged(object sender, TextChangedEventArgs e)
        {
            switch (App.AppClosing)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.MainWindow)
            {
                case null:
                    goto ApplicationClosing;
            }


            switch (StartLocationValue)
            {
                case true:

                    try
                    {
                        string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                        string Getter = File.ReadAllText(Location);
                        dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);

                      

                        JsonFileConverter["StartLocation"] = InfoTextBox.Text;
                        string output = JsonConvert.SerializeObject(JsonFileConverter, Formatting.Indented);
                        File.WriteAllText(Location, output);
                    }
                    catch { }

                    break;
            }



            switch(StartPostcodeValue)
            {
                case true:

                    try
                    {
                        string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                        string Getter = File.ReadAllText(Location);
                        dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);

                        if (InfoTextBox.Text == String.Empty)
                        {
                            App.StartPostcodeSeparator = String.Empty;

                        }
                        else if ((InfoTextBox.Text != String.Empty) && (JsonFileConverter["StartLocation"] != String.Empty))
                        {
                            App.StartPostcodeSeparator = ",";
                        }

                        JsonFileConverter["StartPostcode"] = InfoTextBox.Text;
                        string output = JsonConvert.SerializeObject(JsonFileConverter, Formatting.Indented);
                        File.WriteAllText(Location, output);
                    }
                    catch { }

                    break;
            }



            switch(LocationValue)
            {
                case true:

                    try
                    {
                        string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                        string Getter = File.ReadAllText(Location);
                        dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);

                      

                        JsonFileConverter["Location"] = InfoTextBox.Text;
                        string output = JsonConvert.SerializeObject(JsonFileConverter, Formatting.Indented);
                        File.WriteAllText(Location, output);
                    }
                    catch { }
                 
                    break;
            }

          

            switch(PostcodeValue)
            {

                case true:

                    try
                    {
                        string Location = System.IO.Path.GetFullPath(@"DateTime.json");
                        string Getter = File.ReadAllText(Location);
                        dynamic JsonFileConverter = JsonConvert.DeserializeObject(Getter);
                        JsonFileConverter["Postcode"] = InfoTextBox.Text;

                        if (InfoTextBox.Text == String.Empty)
                        {
                            App.DestinationPostcodeSeparator = String.Empty;

                        }
                        else if((InfoTextBox.Text != String.Empty) && (JsonFileConverter["Location"] != String.Empty))
                        {
                            App.DestinationPostcodeSeparator = ",";
                        }

                        string output = JsonConvert.SerializeObject(JsonFileConverter, Formatting.Indented);
                        File.WriteAllText(Location, output);
                    }
                    catch { }

                    break;
            }

        ApplicationClosing:;
        }

      
    }
}
