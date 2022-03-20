using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Event_Organiser
{
    /// <summary>
    /// Interaction logic for TimeSetting.xaml
    /// </summary>
    public partial class TimeSetting : Page
    {
        long MinutesParse;
        long HourParse;
      

        private System.Timers.Timer TextFormat = new System.Timers.Timer();
        private bool TimerDisposed;
        public TimeSetting()
        {
            TextFormat.Interval = 100;
            TextFormat.Elapsed += TextFormat_Elapsed;
            TextFormat.Disposed += TextFormat_Disposed;
            TextFormat.Start();

           
                InitializeComponent();
            
        }

        private void TextFormat_Disposed(object sender, EventArgs e)
        {
            TimerDisposed = true;
        }

        private void TextFormat_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch(App.AppClosing)
            {
                case true:

                    switch (TimerDisposed)
                    {
                        case true:
                            goto TimerIsDisposed;
                    }

                    try
                    {
                        TextFormat.Stop();
                        TextFormat.Dispose();
                    }
                    catch { }

                    TimerIsDisposed:

                    goto ApplicationIsClosing;
            }

            switch(Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:

                    switch (TimerDisposed)
                    {
                        case true:
                            goto TimerIsDisposed;
                    }

                    try
                    {
                        TextFormat.Stop();
                        TextFormat.Dispose();
                    }
                    catch { }

                    TimerIsDisposed:

                    goto ApplicationIsClosing;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                switch(Application.Current.MainWindow)
                {
                    case null:

                        switch (TimerDisposed)
                        {
                            case true:
                                goto TimerIsDisposed;
                        }

                        try
                        {
                            TextFormat.Stop();
                            TextFormat.Dispose();
                        }
                        catch { }

                        TimerIsDisposed:

                        goto ApplicationClosing;
                }

                if (HourContent.Text != String.Empty)
                {
                    RuntimeHelpers.EnsureSufficientExecutionStack();

                    try
                    {
                        HourParse = Convert.ToInt32(HourContent.Text);
                    }
                    catch
                    {
                        HourContent.Text = String.Empty;
                    }

                    BrushConverter bc = new BrushConverter();
                    HourContent.Background = (Brush)bc.ConvertFrom("#FF03182B");
                    HourContent.UpdateLayout();
                    Separator.Text = ":";

                  
                 

                    try
                    {
                        if(HourParse > 23)
                        {
                            HourContent.Text = "23";
                        }
                        else if(HourParse < 0)
                        {
                            HourContent.Text = "0";
                        }
                    }
                    catch { }


                
                    HourParse = 0;

                }
                else 
                {
                    BrushConverter bc = new BrushConverter();
                    HourContent.Background = (Brush)bc.ConvertFrom("#FF06355F");
                    HourContent.UpdateLayout();
                    Separator.Text = "|";

                    HourParse = 0;
                }

                if(MinutesContent.Text != String.Empty)
                {
                    RuntimeHelpers.EnsureSufficientExecutionStack();

                    try
                    {
                        MinutesParse = Convert.ToInt32(MinutesContent.Text);
                    }
                    catch
                    {
                        MinutesContent.Text = String.Empty;
                    }

                    BrushConverter bc = new BrushConverter();
                    MinutesContent.Background = (Brush)bc.ConvertFrom("#FF03182B");
                    MinutesContent.UpdateLayout();

                    if (HourContent.Text == String.Empty)
                    {
                        Separator.Text = ":";
                    }
                 

                    try
                    {
                        if (MinutesParse > 60)
                        {
                            MinutesContent.Text = "60";
                        }
                        else if (MinutesParse < 0)
                        {
                            MinutesContent.Text = "0";
                        }                    
                    }
                    catch { }

               
                    MinutesParse = 0;
                }
                else
                {
                    BrushConverter bc = new BrushConverter();
                    MinutesContent.Background = (Brush)bc.ConvertFrom("#FF06355F");
                    MinutesContent.UpdateLayout();

                    if(HourContent.Text == String.Empty)
                    {
                        Separator.Text = "|";
                    }

                    MinutesParse = 0;
                }

            ApplicationClosing:;
            });

        ApplicationIsClosing:;
        }

        private void TimeSettingsPageLoaded(object sender, RoutedEventArgs e)
        {
            Hour.FontSize = Hour.FontSize / 1.25;
            Minutes.FontSize = Minutes.FontSize / 1.25;
            Separator.FontSize = Separator.FontSize / 1.25;

            Thickness HourThickness = new Thickness();
            HourThickness.Left = Hour.Margin.Left / 1.25;
            HourThickness.Right = Hour.Margin.Right / 1.25;
            HourThickness.Top = Hour.Margin.Top / 1.25;
            HourThickness.Bottom = Hour.Margin.Bottom / 1.25;

            Hour.Margin = HourThickness;

            Thickness MinutesThickness = new Thickness();
            MinutesThickness.Left = Minutes.Margin.Left / 1.25;
            MinutesThickness.Right = Minutes.Margin.Right / 1.25;
            MinutesThickness.Top = Minutes.Margin.Top / 1.25;
            MinutesThickness.Bottom = Minutes.Margin.Bottom / 1.25;

            Minutes.Margin = MinutesThickness;

            Thickness SeparatorThickness = new Thickness();
            SeparatorThickness.Left = Separator.Margin.Left / 1.25;
            SeparatorThickness.Right = Separator.Margin.Right / 1.25;
            SeparatorThickness.Top = Separator.Margin.Top / 1.25;
            SeparatorThickness.Bottom = Separator.Margin.Bottom / 1.25;

            Separator.Margin = SeparatorThickness;

            Thickness HourContentThickness = new Thickness();
            HourContentThickness.Left = HourContent.Margin.Left / 1.25;
            HourContentThickness.Right = HourContent.Margin.Right / 1.25;
            HourContentThickness.Top = HourContent.Margin.Top / 1.25;
            HourContentThickness.Bottom = HourContent.Margin.Bottom / 1.25;

            HourContent.Margin = HourContentThickness;


            Thickness MinuteContentThickness = new Thickness();
            MinuteContentThickness.Left = MinutesContent.Margin.Left / 1.25;
            MinuteContentThickness.Right = MinutesContent.Margin.Right / 1.25;
            MinuteContentThickness.Top = MinutesContent.Margin.Top / 1.25;
            MinuteContentThickness.Bottom = MinutesContent.Margin.Bottom / 1.25;

            MinutesContent.Margin = MinuteContentThickness;

            try
            {

                string FullPath = System.IO.Path.GetFullPath("DateTime.json");
                string Date_Time = File.ReadAllText(FullPath);
                dynamic date_time = JsonConvert.DeserializeObject(Date_Time);
                HourContent.Text = date_time["Hour"];
                MinutesContent.Text = date_time["Minute"];
                try
                {
                    Convert.ToInt32(date_time["Hour"]);
                    Convert.ToInt32(date_time["Minute"]);
                  
                }
                catch
                {
                    goto NonConvertable;
                }

                HourContent.Text = date_time["Hour"];
                MinutesContent.Text = date_time["Minute"];


                goto Convertable;

                NonConvertable:

                MinutesContent.Text = DateTime.Now.Minute.ToString();
                HourContent.Text = DateTime.Now.Hour.ToString();
            }
            catch { }

        Convertable:;

        }

        ~TimeSetting()
        {
            switch(TimerDisposed)
            {
                case true:
                    goto TimerIsDisposed;
            }

            try
            {
                TextFormat.Dispose();
            }
            catch { }

           TimerIsDisposed:

            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.Collect();
        }

        private void HourText(object sender, TextChangedEventArgs e)
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

            try
            {
                Convert.ToInt32(HourContent.Text);
            }
            catch 
            {
                goto NonCovertable;
            }

            try
            {
                if ((HourContent.Text != String.Empty) || (HourContent.Text != null))
                {
                    string FullPath = System.IO.Path.GetFullPath(@"DateTime.json");
                    string Date_Time = File.ReadAllText(FullPath);
                    dynamic date_time = JsonConvert.DeserializeObject(Date_Time);
                    date_time["Hour"] = HourContent.Text;
                    string output = JsonConvert.SerializeObject(date_time, Formatting.Indented);
                    File.WriteAllText(@"DateTime.json", output);
                }
            }
            catch { }

        NonCovertable:;
        ApplicationClosing:;
        }

        private void MinutesText(object sender, TextChangedEventArgs e)
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

            try
            {
                Convert.ToInt32(HourContent.Text);
            }
            catch
            {
                goto NonCovertable;
            }

            try
            {
                if ((HourContent.Text != String.Empty) || (HourContent.Text != null))
                {
                    string FullPath = System.IO.Path.GetFullPath(@"DateTime.json");
                    string Date_Time = File.ReadAllText(FullPath);
                    dynamic date_time = JsonConvert.DeserializeObject(Date_Time);
                    date_time["Minute"] = MinutesContent.Text;
                    string output = JsonConvert.SerializeObject(date_time, Formatting.Indented);
                    File.WriteAllText(@"DateTime.json", output);
                }
            }
            catch { }

        NonCovertable:;
        ApplicationClosing:;
        }
    }
}
