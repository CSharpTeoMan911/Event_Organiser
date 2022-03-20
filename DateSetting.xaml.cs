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
    /// Interaction logic for DateSetting.xaml
    /// </summary>
    public partial class DateSetting : Page
    {
        private long DayParse;

        private long MonthParse;

        private long YearParse;

        private System.Timers.Timer FormatTimer = new System.Timers.Timer();
        private bool TimerDisposed;

        public DateSetting()
        {
            FormatTimer.Interval = 100;
            FormatTimer.Elapsed += FormatTimer_Elapsed;
            FormatTimer.Disposed += FormatTimer_Disposed;
            FormatTimer.Start();

            InitializeComponent();
        }

        private void FormatTimer_Disposed(object sender, EventArgs e)
        {
            TimerDisposed = true;
        }

        private void FormatTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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
                        FormatTimer.Stop();
                        FormatTimer.Dispose();
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
                        FormatTimer.Stop();
                        FormatTimer.Dispose();
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
                            FormatTimer.Stop();
                            FormatTimer.Dispose();
                        }
                        catch { }

                        TimerIsDisposed:

                        goto ApplicationClosing;
                }

                if (DayNumber.Text != String.Empty)
                {
                    RuntimeHelpers.EnsureSufficientExecutionStack();

                    try
                    {
                        DayParse = Convert.ToInt32(DayNumber.Text);
                    }
                    catch
                    {
                        DayNumber.Text = String.Empty;
                    }

                    BrushConverter bc = new BrushConverter();
                    DayNumber.Background = (Brush)bc.ConvertFrom("#FF03182B");
                    DayNumber.UpdateLayout();
                    Separator1.Text = "/";

                    try
                    {
                        if ((DayNumber.Text.Length > 2) || (DayParse > 31))
                        {
                            DayNumber.Text = "31";
                        }
                        else if(DayParse <= 0)
                        {
                            DayNumber.Text = "1";
                        }
                      
                        else if ((MonthNumber.Text != String.Empty) && (Convert.ToInt32(MonthNumber.Text) <= 31))
                        {
                            if ((Convert.ToInt32(MonthNumber.Text) == 2) && (DayParse > 29) && (Month.Text != String.Empty))
                            {
                                if ((YearNumber.Text != String.Empty) && (DateTime.IsLeapYear(Convert.ToInt32(YearNumber.Text)) == true))
                                {
                                    DayNumber.Text = "29";
                                }
                                else
                                {
                                    DayNumber.Text = "28";
                                }
                            }
                            else if (((Convert.ToInt32(MonthNumber.Text) == 4) || (Convert.ToInt32(MonthNumber.Text) == 6) || (Convert.ToInt32(MonthNumber.Text) == 9) || (Convert.ToInt32(MonthNumber.Text) == 11)) && (DayParse > 30) && (MonthNumber.Text != String.Empty) && (DayNumber.Text != String.Empty))
                            {
                                DayNumber.Text = "30";
                            }
                        }
                    }
                    catch { }

                
                    DayParse = 0;
                }
                else
                {
                    BrushConverter bc = new BrushConverter();
                    DayNumber.Background = (Brush)bc.ConvertFrom("#FF06355F");
                    DayNumber.UpdateLayout();
                    Separator1.Text = "|";
                    DayParse = 0;
                }

                if (MonthNumber.Text != String.Empty)
                {
                    try
                    {
                        MonthParse = Convert.ToInt32(MonthNumber.Text);
                    }
                    catch
                    {
                        MonthNumber.Text = String.Empty;

                    }

                    BrushConverter bc = new BrushConverter();
                    MonthNumber.Background = (Brush)bc.ConvertFrom("#FF03182B");
                    MonthNumber.UpdateLayout();
                    Separator2.Text = "/";

                    try
                    {
                        if ((MonthNumber.Text.Length > 2) || (MonthParse > 12))
                        {
                            MonthNumber.Text = "12";
                        }
                        else if(MonthParse <= 0)
                        {
                            MonthNumber.Text = "1";
                        }
                        else if ((MonthNumber.Text != String.Empty) && (MonthParse <= 31))
                        {
                            if ((MonthParse == 2) && (Convert.ToInt32(DayNumber.Text) > 29) && (DayNumber.Text != String.Empty))
                            {
                                if ((YearNumber.Text != String.Empty) && (DateTime.IsLeapYear(Convert.ToInt32(YearNumber.Text)) == true))
                                {
                                    DayNumber.Text = "29";
                                }
                                else
                                {
                                    DayNumber.Text = "28";
                                }
                            }
                            else if (((MonthParse == 4) || (MonthParse == 6) || (MonthParse == 9) || (MonthParse == 11)) && (Convert.ToInt32(DayNumber.Text) > 30) && (MonthNumber.Text != String.Empty) && (DayNumber.Text != String.Empty))
                            {
                                DayNumber.Text = "30";
                            }
                        }
                    }
                    catch { }

                   
                    MonthParse = 0;
                }
                else
                {
                    BrushConverter bc = new BrushConverter();
                    MonthNumber.Background = (Brush)bc.ConvertFrom("#FF06355F");
                    MonthNumber.UpdateLayout();
                    Separator2.Text = "|";
                    MonthParse = 0;
                }

                if (YearNumber.Text != String.Empty)
                {
                    try
                    {
                        YearParse = Convert.ToInt32(YearNumber.Text);
                    }
                    catch
                    {
                        YearNumber.Text = String.Empty;

                    }

                    BrushConverter bc = new BrushConverter();
                    YearNumber.Background = (Brush)bc.ConvertFrom("#FF03182B");
                    YearNumber.UpdateLayout();

                    try
                    {
                        if ((YearNumber.Text.Length > 4) || (YearParse > 3000))
                        {
                            YearNumber.Text = DateTime.Now.Year.ToString();
                        }
                        else if ((YearParse < DateTime.Now.Year) && (YearNumber.Text.Length > 3))
                        {
                            YearNumber.Text = DateTime.Now.Year.ToString();
                        }
                        else if (YearNumber.Text != String.Empty)
                        {
                            if ((Convert.ToInt32(MonthNumber.Text) == 2) && (Convert.ToInt32(DayNumber.Text) >= 29) && (DayNumber.Text != String.Empty) && (MonthNumber.Text != String.Empty))
                            {
                                if ((YearNumber.Text != String.Empty) && (DateTime.IsLeapYear((int)YearParse) == true))
                                {
                                    DayNumber.Text = "29";
                                }
                                else
                                {
                                    DayNumber.Text = "28";
                                }
                            }
                            else if (((Convert.ToInt32(MonthNumber.Text) == 4) || (Convert.ToInt32(MonthNumber.Text) == 6) || (Convert.ToInt32(MonthNumber.Text) == 9) || (Convert.ToInt32(MonthNumber.Text) == 11)) && (Convert.ToInt32(DayNumber.Text) > 30) && (MonthNumber.Text != String.Empty) && (DayNumber.Text != String.Empty))
                            {
                                DayNumber.Text = "30";
                            }
                        }
                    }
                    catch { }

                   
                    YearParse = 0;
                }
                else
                {
                    BrushConverter bc = new BrushConverter();
                    YearNumber.Background = (Brush)bc.ConvertFrom("#FF06355F");
                    YearNumber.UpdateLayout();
                    YearParse = 0;
                }

            ApplicationClosing:;

            });

        ApplicationIsClosing:;

        }

      


        private void DateSettingsPageLoaded(object sender, RoutedEventArgs e)
        {

            Day.FontSize = Day.FontSize / 1.25;

            Month.FontSize = Month.FontSize / 1.25;

            Year.FontSize = Year.FontSize / 1.25;

            Separator1.FontSize = Separator1.FontSize / 1.25;

            Separator2.FontSize = Separator2.FontSize / 1.25;

            DayNumber.FontSize = DayNumber.FontSize / 1.25;

            MonthNumber.FontSize = MonthNumber.FontSize / 1.25;

            YearNumber.FontSize = YearNumber.FontSize / 1.25;

            Thickness DayTextblock = new Thickness();
            DayTextblock.Left = Day.Margin.Left / 1.25;
            DayTextblock.Right = Day.Margin.Right / 1.25;
            DayTextblock.Top = Day.Margin.Bottom / 1.25;
            DayTextblock.Bottom = Day.Margin.Top / 1.25;

            Day.Margin = DayTextblock;

            Thickness MonthTextblock = new Thickness();
            MonthTextblock.Left = Month.Margin.Left / 1.25;
            MonthTextblock.Right = Month.Margin.Right / 1.25;
            MonthTextblock.Top = Month.Margin.Bottom / 1.25;
            MonthTextblock.Bottom = Month.Margin.Top / 1.25;

            Month.Margin = DayTextblock;

            Thickness YearTextblock = new Thickness();
            YearTextblock.Left = Year.Margin.Left / 1.25;
            YearTextblock.Right = Year.Margin.Right / 1.25;
            YearTextblock.Top = Year.Margin.Bottom / 1.25;
            YearTextblock.Bottom = Year.Margin.Top / 1.25;

            Year.Margin = DayTextblock;

            try
            {         
                    string FullPath = System.IO.Path.GetFullPath(@"DateTime.json");
                    string Date_Time = File.ReadAllText(FullPath);
                    dynamic date_time = JsonConvert.DeserializeObject(Date_Time);

                try
                {
                    Convert.ToInt32(date_time["Day"]);
                    Convert.ToInt32(date_time["Month"]);
                    Convert.ToInt32(date_time["Year"]);
                }
                catch 
                {
                    goto NonConvertable;
                }

                    DayNumber.Text = date_time["Day"];
                    MonthNumber.Text = date_time["Month"];
                    YearNumber.Text = date_time["Year"];

                goto Convertable;

            NonConvertable:

                

                DayNumber.Text = DateTime.Now.Day.ToString();
                MonthNumber.Text = DateTime.Now.Month.ToString();
                YearNumber.Text = DateTime.Now.Year.ToString();
            }
            catch { }

        Convertable:;
        }     

        ~DateSetting()
        {
            switch(TimerDisposed)
            {
                case true:
                    goto TimerIsDisposed;
            }

            try
            {
                FormatTimer.Dispose();
            }
            catch { }

            TimerIsDisposed:

            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.Collect();
        }

        private void DayText(object sender, TextChangedEventArgs e)
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
                Convert.ToInt32(DayNumber.Text);
            }
            catch
            {
                goto NonCovertable;
            }

            try
            {
                if ((DayNumber.Text != String.Empty) || (DayNumber.Text != null))
                {
                    string FullPath = System.IO.Path.GetFullPath(@"DateTime.json");
                    string Date_Time = File.ReadAllText(FullPath);
                    dynamic date_time = JsonConvert.DeserializeObject(Date_Time);
                    date_time["Day"] = DayNumber.Text;
                    string output = JsonConvert.SerializeObject(date_time, Formatting.Indented);
                    File.WriteAllText(@"DateTime.json", output);
                }
            }
            catch { }

        NonCovertable:;
        ApplicationClosing:;
        }

        private void MonthText(object sender, TextChangedEventArgs e)
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
                Convert.ToInt32(MonthNumber.Text);
            }
            catch
            {
                goto NonCovertable;
            }

            try
            {
                if ((MonthNumber.Text != String.Empty) || (MonthNumber.Text != null))
                {
                    string FullPath = System.IO.Path.GetFullPath(@"DateTime.json");
                    string Date_Time = File.ReadAllText(FullPath);
                    dynamic date_time = JsonConvert.DeserializeObject(Date_Time);
                    date_time["Month"] = MonthNumber.Text;
                    string output = JsonConvert.SerializeObject(date_time, Formatting.Indented);
                    File.WriteAllText(@"DateTime.json", output);
                }
            }
            catch { }

        NonCovertable:;
        ApplicationClosing:;
        }

        private void YearText(object sender, TextChangedEventArgs e)
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
                Convert.ToInt32(YearNumber.Text);
            }
            catch
            {
                goto NonCovertable;
            }

            try
            {
                if ((YearNumber.Text != String.Empty) || (YearNumber.Text != null))
                {
                    string FullPath = System.IO.Path.GetFullPath(@"DateTime.json");
                    string Date_Time = File.ReadAllText(FullPath);
                    dynamic date_time = JsonConvert.DeserializeObject(Date_Time);
                    date_time["Year"] = YearNumber.Text;
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
