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
using System.Windows.Shapes;

namespace Event_Organiser
{
    /// <summary>
    /// Interaction logic for AlarmNotificationWindow.xaml
    /// </summary>
    public partial class AlarmNotificationWindow : Window
    {
        private System.Media.SoundPlayer Alarm = new System.Media.SoundPlayer("AlarmSound.wav");
        private System.Timers.Timer ClockShakingAnimation = new System.Timers.Timer();
        private short AnimationAngleCounter;
        private byte SoundWavesPropagation;
        private bool DirectionPointer;
        private bool NotificationWindowClosing;
        private bool ClockDisposed;
        private bool AlarmDisposed;
        public AlarmNotificationWindow()
        {
            string FullPath1 = System.IO.Path.GetFullPath(@"SavedEvent.json");
            string Date_Time1 = File.ReadAllText(FullPath1);
            dynamic date_time1 = JsonConvert.DeserializeObject(Date_Time1);

            date_time1["EventStartLocation"] = "Empty";
            date_time1["EventLocation"] = "Empty";
            date_time1["EventStartPostcode"] = "Empty";
            date_time1["EventPostcode"] = "Empty";

            string output = JsonConvert.SerializeObject(date_time1, Formatting.Indented);
            File.WriteAllText(@"SavedEvent.json", output);

            ClockShakingAnimation.Interval = 10;
            ClockShakingAnimation.Disposed += ClockShakingAnimation_Disposed;
            ClockShakingAnimation.Elapsed += ClockShakingAnimation_Elapsed;
            ClockShakingAnimation.Start();
            Alarm.Disposed += Alarm_Disposed;
            Alarm.PlayLooping();
            InitializeComponent();
        }

        private void Alarm_Disposed(object sender, EventArgs e)
        {
            AlarmDisposed = true;
        }

        private void ClockShakingAnimation_Disposed(object sender, EventArgs e)
        {
            ClockDisposed = true;
        }

        private void ClockShakingAnimation_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SoundWavesPropagation++;

            switch(NotificationWindowClosing)
            {
                case true:

                    try
                    {
                        ClockShakingAnimation.Stop();
                    }
                    catch { }

                    goto NotificationWindowClosing;
            }


            switch(Application.Current.Dispatcher.HasShutdownStarted)
            {
                case true:
                    try
                    {
                        ClockShakingAnimation.Stop();
                    }
                    catch { }

                    goto NotificationWindowClosing;
            }

            switch(SoundWavesPropagation)
            {
                case 40:
                    SoundWavesPropagation = 0;
                    break;
            }

            switch(DirectionPointer)
            {
                case false:
                    AnimationAngleCounter++;
                    AnimationAngleCounter++;
                    AnimationAngleCounter++;
                    AnimationAngleCounter++;
                    AnimationAngleCounter++;                              
                    break;

                case true:
                    AnimationAngleCounter--;
                    AnimationAngleCounter--;
                    AnimationAngleCounter--;
                    AnimationAngleCounter--;
                    AnimationAngleCounter--;
                    break;
            }
           
            switch(AnimationAngleCounter)
            {
                case 30:
                    DirectionPointer = true;
                    break;

                case -30:
                    DirectionPointer = false;
                    break;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                switch(Application.Current.MainWindow)
                {
                    case null:
                        goto NotificationWindowClosed;
                }
                RotateTransform rotate = new RotateTransform();

                rotate.Angle = AnimationAngleCounter;
                Clock.RenderTransform = rotate;

                NotificationWindowClosed:;
            });

        NotificationWindowClosing:;
        }

        private void MoveNotificationWindow(object sender, MouseButtonEventArgs e)
        {
            switch(App.AppClosing)
            {
                case true:
                    goto ApplicationClosing;
            }

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

            this.DragMove();

        ApplicationClosing:;
        }

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            NotificationWindowClosing = true;

            switch(ClockDisposed)
            {
                case true:
                    goto ClockISDisposed;
            }

            try
            {
                ClockShakingAnimation.Dispose();
            }
            catch { }

        ClockISDisposed:

            switch (AlarmDisposed)
            {
                case true:
                    goto AlarmIsDisposed;
            }

            try
            {
                Alarm.Stop();
                Alarm.Dispose();
            }
            catch { }

        AlarmIsDisposed:

            switch (Application.Current.MainWindow)
            {
                case null:
                    goto ApplicationWindowClosed;
            }

        ApplicationWindowClosed:

            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.Collect();

            this.Close();

        }

        private void NotificationWindowLoaded(object sender, RoutedEventArgs e)
        {

            this.Height = this.ActualHeight / 2.5;
            this.Width = this.ActualWidth / 2.5;
        
            AcceptButtonControl.FontSize = AcceptButtonControl.FontSize / 2.5;
           
            Clock.FontSize = Clock.FontSize / 2.5;

            EventWarning.FontSize = EventWarning.FontSize / 2.5;

            Thickness Border = new Thickness();
            Border.Left = AcceptButtonControl.BorderThickness.Left / 2.5;
            Border.Right = AcceptButtonControl.BorderThickness.Right / 2.5;
            Border.Top = AcceptButtonControl.BorderThickness.Top / 2.5;
            Border.Bottom = AcceptButtonControl.BorderThickness.Bottom / 2.5;

            AcceptButtonControl.BorderThickness = Border;

            Thickness ClockThickness = new Thickness();
            ClockThickness.Left = Clock.Margin.Left / 2.5;
            ClockThickness.Right = Clock.Margin.Right / 2.5;
            ClockThickness.Top = Clock.Margin.Top / 2.5;
            ClockThickness.Bottom = Clock.Margin.Bottom / 2.5;

            Clock.Margin = ClockThickness;

            Thickness ButtonThickness = new Thickness();
            ButtonThickness.Left = AcceptButtonControl.Margin.Left / 2.5;
            ButtonThickness.Right = AcceptButtonControl.Margin.Right / 2.5;
            ButtonThickness.Top = AcceptButtonControl.Margin.Top / 2.5;
            ButtonThickness.Bottom = AcceptButtonControl.Margin.Bottom / 2.5;

            AcceptButtonControl.Margin = ButtonThickness;
        }
    }
}
