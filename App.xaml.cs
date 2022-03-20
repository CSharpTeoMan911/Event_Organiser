using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Event_Organiser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static bool AppClosing;

        internal static string StartPostcodeSeparator = String.Empty;
        internal static string DestinationPostcodeSeparator = String.Empty;



        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        }

        ~App()
        {

            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.Collect();
        }
    }
}
