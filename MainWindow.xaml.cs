using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace Event_Organiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer AnimationTimer = new System.Timers.Timer();
        private bool AppClosing;
        private bool TimerDisposed;
        private byte WrongFormatCounter;
        private bool WrongFormatWarning;
        private long AnimationMitigator;

       
        public MainWindow()
        {
           
            AnimationTimer.Interval = 500;
            AnimationTimer.Disposed += AnimationTimer_Disposed;
            AnimationTimer.Elapsed += AnimationTimer_Elapsed;
            AnimationTimer.Start();

            InitializeComponent();
        }

        private void AnimationTimer_Disposed(object sender, EventArgs e)
        {
            TimerDisposed = true;
        }

        private void AnimationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            AnimationMitigator++;

            switch(AppClosing)
            {
                case true:
                    try
                    {
                        AnimationTimer.Stop();
                    }
                    catch { }
                    goto ApplicationClosing;
            }

            switch(Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:
                    try
                    {
                        AnimationTimer.Stop();
                    }
                    catch { }
                    goto ApplicationClosing;
            }

            Thread FileVerification = new Thread(() =>
            {
                try
                {
                    string FullPath1 = System.IO.Path.GetFullPath(@"SavedEvent.json");
                    string Date_Time1 = File.ReadAllText(FullPath1);
                    dynamic date_time1 = JsonConvert.DeserializeObject(Date_Time1);

                    long HourVerification;
                    long MinuteVerification;
                    long DayVerification;
                    long MonthVerification;
                    long YearVerification;

                    try
                    {
                        HourVerification = Convert.ToInt32(date_time1["EventHour"]);
                        MinuteVerification = Convert.ToInt32(date_time1["EventMinute"]);
                        DayVerification = Convert.ToInt32(date_time1["EventDay"]);
                        MonthVerification = Convert.ToInt32(date_time1["EventMonth"]);
                        YearVerification = Convert.ToInt32(date_time1["EventYear"]);

                        if((HourVerification == DateTime.Now.Hour) && (MinuteVerification == DateTime.Now.Minute) && (DayVerification == DateTime.Now.Day) && (MonthVerification == DateTime.Now.Month) && (YearVerification == DateTime.Now.Year))
                        {
                            var task  = Task.Run(() =>
                            {
                                
                                    date_time1["EventHour"] = "Empty";
                                    date_time1["EventMinute"] = "Empty";
                                    date_time1["EventDay"] = "Empty";
                                    date_time1["EventMonth"] = "Empty";
                                    date_time1["EventYear"] = "Empty";

                                    string output = JsonConvert.SerializeObject(date_time1, Formatting.Indented);
                                    File.WriteAllText(@"SavedEvent.json", output);
                            });

                            task.Wait();

                          
                            Thread SmartFeaturesThread = new Thread(() =>
                            {
                                Process.Start("www.google.com/maps/dir/" + date_time1["EventStartLocation"] + App.StartPostcodeSeparator + date_time1["EventStartPostcode"] + "/" + date_time1["EventLocation"] + App.DestinationPostcodeSeparator + date_time1["EventPostcode"]);

                            });

                            SmartFeaturesThread.Priority = ThreadPriority.AboveNormal;
                            SmartFeaturesThread.SetApartmentState(ApartmentState.STA);
                            SmartFeaturesThread.Start();               

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                AlarmNotificationWindow notification = new AlarmNotificationWindow();
                                notification.ShowDialog();
                            });

                        }
                    }
                    catch { }
                }
                catch { }
            });

            FileVerification.Priority = ThreadPriority.AboveNormal;
            FileVerification.SetApartmentState(ApartmentState.STA);
            FileVerification.Start();
           

            Application.Current.Dispatcher.Invoke(() =>
            {
                switch(Application.Current.MainWindow)
                {
                    case null:
                        try
                        {
                            AnimationTimer.Stop();
                        }
                        catch { }
                        goto ApplicationWindowIsNull;

                }

                switch(WrongFormatWarning)
                {
                    case true:

                        WrongFormatCounter++;

                        switch(WrongFormatCounter)
                        {
                            case 4:
                               
                                WrongFormatCounter = 0;
                                WrongFormatWarning = false;

                                DataEventHandlingPanel.SetValue(Grid.ColumnSpanProperty, 1);
                                InfoLabel.FontSize = 10;

                                InfoLabel.Foreground = new SolidColorBrush(Colors.White);
                                InfoLabel.FontFamily = new FontFamily("Consolas");
                                InfoLabel.Text = "Save the\nevent";

                                break;
                        }

                        break;
                }

                switch(AnimationMitigator)
                {
                    case 1:
                        AnimationContext.Text = "Setting the event";
                        break;

                    case 2:
                        AnimationContext.Text = "Setting the event . _";
                        break;

                    case 3:
                        AnimationContext.Text = "Setting the event . .";
                        break;

                    case 4:
                        AnimationContext.Text = "Setting the event . . . _";
                        break;

                    case 5:
                        AnimationMitigator = 0;
                        break;
                }

            ApplicationWindowIsNull:;
            });

            ApplicationClosing:;
        }

        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            switch (AppClosing)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch (Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:
                    goto ApplicationClosing;
            }

            switch(Application.Current.MainWindow)
            {
                case null:
                    goto ApplicationClosing;
            }

            this.DragMove();

            ApplicationClosing:;
        }

        private void CloseTheApp(object sender, RoutedEventArgs e)
        {
            switch(TimerDisposed)
            {
                case true:
                    goto TimerIsDisposed;
            }

            AnimationTimer.Dispose();

        TimerIsDisposed:

            switch (Application.Current.MainWindow)
            {
                case null:
                    goto ApplicationAlreadyClosed;
            }

            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.Collect();

            this.Close();

        ApplicationAlreadyClosed:;

        }

        private void ApplicationClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AppClosing = true;
            App.AppClosing = true;

            try
            {
                string FullPath = System.IO.Path.GetFullPath(@"DateTime.json");
                string Date_Time = File.ReadAllText(FullPath);
                dynamic date_time = JsonConvert.DeserializeObject(Date_Time);

                date_time["Hour"] = "Empty";
                date_time["Minute"] = "Empty";

                date_time["Day"] = "Empty";
                date_time["Month"] = "Empty";
                date_time["Year"] = "Empty";

                date_time["StartLocation"] = String.Empty;
                date_time["StartPostcode"] = String.Empty;
                date_time["Location"] = String.Empty;
                date_time["Postcode"] = String.Empty;

                string output = JsonConvert.SerializeObject(date_time, Formatting.Indented);
                File.WriteAllText(@"DateTime.json", output);
            }
            catch { }


            switch (TimerDisposed)
            {
                case true:
                    goto ApplicationClosed;
            }

            try
            {
                AnimationTimer.Dispose();
            }
            catch { }

        ApplicationClosed:

            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.Collect();
        }

        private void AppWindowLoaded(object sender, RoutedEventArgs e)
        {
            FunctionNavigationMainframe.Navigate(new TimeSetting());

            BrushConverter bc = new BrushConverter();

            TimeButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");
            TimeButton.Background = new SolidColorBrush(Colors.White);

            try
            {
                string FullPath = System.IO.Path.GetFullPath(@"DateTime.json");
                string Date_Time = File.ReadAllText(FullPath);
                dynamic date_time = JsonConvert.DeserializeObject(Date_Time);

                date_time["Hour"] = DateTime.Now.Hour.ToString();
                date_time["Minute"] = DateTime.Now.Minute.ToString();

                date_time["Day"] = DateTime.Now.Day.ToString();
                date_time["Month"] = DateTime.Now.Month.ToString();
                date_time["Year"] = DateTime.Now.Year.ToString();

                string output = JsonConvert.SerializeObject(date_time, Formatting.Indented);
                File.WriteAllText(@"DateTime.json", output);
            }
            catch { }

        }

        private void TimeSetting(object sender, RoutedEventArgs e)
        {
            switch (AppClosing)
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

      

            FunctionNavigationMainframe.Navigate(new TimeSetting());

            BrushConverter bc = new BrushConverter();

            TimeButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");
            TimeButton.Background = new SolidColorBrush(Colors.White);

            DateButton.Foreground = new SolidColorBrush(Colors.White);
            DateButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            LocationButton.Foreground = new SolidColorBrush(Colors.White);
            LocationButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

        ApplicationClosing:;
        }

        private void DateSetting(object sender, RoutedEventArgs e)
        {
            switch (AppClosing)
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


            FunctionNavigationMainframe.Navigate(new DateSetting());

            BrushConverter bc = new BrushConverter();

            DateButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");
            DateButton.Background = new SolidColorBrush(Colors.White);

            TimeButton.Foreground = new SolidColorBrush(Colors.White);
            TimeButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            LocationButton.Foreground = new SolidColorBrush(Colors.White);
            LocationButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            ApplicationClosing:;
        }

        private void LocationSetting(object sender, RoutedEventArgs e)
        {
            switch (AppClosing)
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


            FunctionNavigationMainframe.Navigate(new LocationSettings());

            BrushConverter bc = new BrushConverter();

            LocationButton.Foreground = (Brush)bc.ConvertFrom("#FF0E4A80");
            LocationButton.Background = new SolidColorBrush(Colors.White);

            TimeButton.Foreground = new SolidColorBrush(Colors.White);
            TimeButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            DateButton.Foreground = new SolidColorBrush(Colors.White);
            DateButton.Background = (Brush)bc.ConvertFrom("#FF0E4A80");

            ApplicationClosing:;
        }

        ~MainWindow()
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.Collect();
        }

        private void SaveTheEvent(object sender, RoutedEventArgs e)
        {
            switch (AppClosing)
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
                string FullPath1 = System.IO.Path.GetFullPath(@"SavedEvent.json");
                string Date_Time1 = File.ReadAllText(FullPath1);
                dynamic date_time1 = JsonConvert.DeserializeObject(Date_Time1);

                string FullPath2 = System.IO.Path.GetFullPath(@"DateTime.json");
                string Date_Time2 = File.ReadAllText(FullPath2);
                dynamic date_time2 = JsonConvert.DeserializeObject(Date_Time2);

                long HourVerification;
                long MinuteVerification;
                long DayVerification;
                long MonthVerification;
                long YearVerification;

                try
                {
                    HourVerification = Convert.ToInt32(date_time2["Hour"]);
                    MinuteVerification =  Convert.ToInt32(date_time2["Minute"]);
                    DayVerification = Convert.ToInt32(date_time2["Day"]);
                    MonthVerification = Convert.ToInt32(date_time2["Month"]);
                    YearVerification = Convert.ToInt32(date_time2["Year"]);
                }
                catch
                {
                    goto NonConvertable;
                }

                if(YearVerification < DateTime.Now.Year)
                {
                    WrongFormatWarning = true;
                    DataEventHandlingPanel.SetValue(Grid.ColumnSpanProperty, 4);
                    InfoLabel.FontFamily = new FontFamily("Impact");
                    InfoLabel.FontSize = 15;
                    InfoLabel.Foreground = new SolidColorBrush(Colors.Red);
                    InfoLabel.Text = "Events cannot be held in the past. Please set another date and time !";
                    goto NonConvertable;
                }
                else if((YearVerification == DateTime.Now.Year) && (MonthVerification < DateTime.Now.Month))
                {
                    WrongFormatWarning = true;
                    DataEventHandlingPanel.SetValue(Grid.ColumnSpanProperty, 4);
                    InfoLabel.FontFamily = new FontFamily("Impact");
                    InfoLabel.FontSize = 15;
                    InfoLabel.Foreground = new SolidColorBrush(Colors.Red);
                    InfoLabel.Text = "Events cannot be held in the past. Please set another date and time !";
                    goto NonConvertable;
                }
                else if((YearVerification == DateTime.Now.Year) && (MonthVerification == DateTime.Now.Month) && (DayVerification < DateTime.Now.Day))
                {
                    WrongFormatWarning = true;
                    DataEventHandlingPanel.SetValue(Grid.ColumnSpanProperty, 4);
                    InfoLabel.FontFamily = new FontFamily("Impact");
                    InfoLabel.FontSize = 15;
                    InfoLabel.Foreground = new SolidColorBrush(Colors.Red);
                    InfoLabel.Text = "Events cannot be held in the past. Please set another date and time !";
                    goto NonConvertable;
                }
                else if((YearVerification == DateTime.Now.Year) && (MonthVerification == DateTime.Now.Month) && (DayVerification == DateTime.Now.Day) && (HourVerification < DateTime.Now.Hour))
                {
                    WrongFormatWarning = true;
                    DataEventHandlingPanel.SetValue(Grid.ColumnSpanProperty, 4);
                    InfoLabel.FontFamily = new FontFamily("Impact");
                    InfoLabel.FontSize = 15;
                    InfoLabel.Foreground = new SolidColorBrush(Colors.Red);
                    InfoLabel.Text = "Events cannot be held in the past. Please set another date and time !";
                    goto NonConvertable;
                }
                else if((YearVerification == DateTime.Now.Year) && (MonthVerification == DateTime.Now.Month) && (DayVerification == DateTime.Now.Day) && (HourVerification == DateTime.Now.Hour) && (MinuteVerification < DateTime.Now.Minute))
                {
                    WrongFormatWarning = true;
                    DataEventHandlingPanel.SetValue(Grid.ColumnSpanProperty, 4);
                    InfoLabel.FontFamily = new FontFamily("Impact");
                    InfoLabel.FontSize = 15;
                    InfoLabel.Foreground = new SolidColorBrush(Colors.Red);
                    InfoLabel.Text = "Events cannot be held in the past. Please set another date and time !";
                    goto NonConvertable;
                }
                else if((date_time2["Location"] == String.Empty) && (date_time2["Postcode"] == String.Empty))
                {
                    WrongFormatWarning = true;
                    DataEventHandlingPanel.SetValue(Grid.ColumnSpanProperty, 4);
                    InfoLabel.FontFamily = new FontFamily("Impact");
                    InfoLabel.FontSize = 15;
                    InfoLabel.Foreground = new SolidColorBrush(Colors.Red);
                    InfoLabel.Text = "Please choose a destination !";
                    goto NonConvertable;
                }

                InfoLabel.Foreground = new SolidColorBrush(Colors.White);
                InfoLabel.Text = "Save the\nevent";

                date_time1["EventMinute"] = date_time2["Minute"];
                date_time1["EventHour"] = date_time2["Hour"];
                date_time1["EventDay"] = date_time2["Day"];
                date_time1["EventMonth"] = date_time2["Month"];
                date_time1["EventYear"] = date_time2["Year"];
                date_time1["EventStartLocation"] = date_time2["StartLocation"];
                date_time1["EventStartPostcode"] = date_time2["StartPostcode"];
                date_time1["EventLocation"] = date_time2["Location"];
                date_time1["EventPostcode"] = date_time2["Postcode"];


                string output = JsonConvert.SerializeObject(date_time1, Formatting.Indented);
                File.WriteAllText(@"SavedEvent.json" ,output);


            NonConvertable:;
            }
            catch { }

            ApplicationClosing:;
        }

        private void MinimiseTheApp(object sender, RoutedEventArgs e)
        {
            switch (AppClosing)
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

            Application.Current.MainWindow.WindowState = WindowState.Minimized;

        ApplicationClosing:;
        }
    }
}
