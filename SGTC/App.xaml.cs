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
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IUnitConverter, UnitConverter>();
                    services.AddSingleton<ICoilDataService, CoilDataService>();
                    services.AddTransient<ICoilCalculator, CoilCalculator>();
                    services.AddTransient<IAutoCalculatorService, AutoCalculatorService>();
                    // Register MainViewModel and Window
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                    // Register ViewModels
                    services.AddTransient<PrimaryCircuitViewModel>();
                    services.AddTransient<SecondaryCircuitViewModel>();
                    services.AddTransient<TopLoadViewModel>();
                    services.AddTransient<ResultViewModel>();
                    services.AddTransient<ResultGraphViewModel>();

                    // Register Views (optional, only if you're using them outside of DataTemplates)
                    services.AddTransient<PrimaryCircuit>();
                    services.AddTransient<SecondaryCircuit>();
                    services.AddTransient<TopLoad>();
                    services.AddTransient<Result>();
                    services.AddTransient<ResultGraph>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync(); // Start the host

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync(); // Stop the host
            base.OnExit(e);
        }
    }
}