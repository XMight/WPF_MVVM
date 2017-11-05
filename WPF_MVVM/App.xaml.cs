using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class WpfMvvmExampleApp : Application
    {
        private static readonly String ThisIdentifier = "WpfMvvmExampleApp";

        protected override void OnStartup(StartupEventArgs e)
        {
            // catches all exceptions on the UI thread. 
            // This seems to work reliably, and will replace the AppDomain.CurrentDomain.UnhandledException handler on the UI thread (takes priority). 
            // Use e.Handled = true to keep the application running.
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            base.OnStartup(e);
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Log the exception or do something with it
            // Logger.Initialize(this.GetType(), "log4netConfigFile.xml");
            // Logger.LogInfo(ThisIdentifier, "Logger initialized in WpfMvvmExampleApp constructor");
        }

        public WpfMvvmExampleApp()
        {
            // This is to avoid localized exception messages in the app, if the machine is not english
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            // Here we can init logger
        }

        /// <summary>
        /// We start the WPF application manually
        /// </summary>
        /// <param name="parameters"></param>
        [STAThread]
        public static void Main(String[] parameters)
        {
            WpfMvvmExampleApp app = new WpfMvvmExampleApp();
            app.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            app.InitializeComponent();
            app.Run();
        }
    }
}
