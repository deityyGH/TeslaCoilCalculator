using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using SGTC.ViewModels;
using SGTC.Models;

namespace SGTC.Views
{
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IUnitConverter, UnitConverter>();
                    services.AddSingleton<ICoilDataService, CoilDataService>();
                    services.AddSingleton<ICoilCalculator, CoilCalculator>();

                    // Register MainViewModel and Window
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                    // Register ViewModels
                    services.AddTransient<PrimaryCircuitViewModel>();
                    services.AddTransient<SecondaryCircuitViewModel>();
                    services.AddTransient<TopLoadViewModel>();
                    services.AddTransient<ResultViewModel>();

                    // Register Views (optional, only if you're using them outside of DataTemplates)
                    services.AddTransient<PrimaryCircuit>();
                    services.AddTransient<SecondaryCircuit>();
                    services.AddTransient<TopLoad>();
                    services.AddTransient<Result>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine("Starting");
            AppHost?.Start();

            try
            {
                var startupForm = AppHost?.Services.GetRequiredService<MainWindow>();
                if (startupForm != null)
                {
                    MainWindow = startupForm;
                    MainWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            Console.WriteLine("Exitting");
            if (AppHost != null)
            {
                await AppHost.StopAsync();
                AppHost.Dispose();
            }
            base.OnExit(e);
        }
    }
}