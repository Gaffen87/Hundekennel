using HundeKennel.Factories;
using HundeKennel.Models;
using HundeKennel.Services;
using HundeKennel.ViewModels;
using HundeKennel.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace HundeKennel;

public partial class App : Application
{
	public static IHost? AppHost { get; private set; }
	
	public App()
	{
		// Encoding for reading Excel files
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		// License key to use SyncFusion controls
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH9ceXVdR2NYWEJ3XEM=");

		// Setup services
		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices((hostContext, services) =>
			{
				// Add MainWindow and its viewmodel to services
				services.AddSingleton<MainWindow>();
				services.AddSingleton<MainViewModel>();

				// Add DetailsView and its viewmodel to services, + factory that creates them
				services.AddTransient<DetailsView>();
				services.AddTransient<DetailsViewModel>();
				services.AddViewFactory<DetailsView>();

				// 
				services.AddTransient<PedigreeView>();
				services.AddTransient<PedigreeViewModel>();
				services.AddViewFactory<PedigreeView>();

				// Add Database services and excel reader class to services
				services.AddTransient<DataService>();
				services.AddTransient<ExcelService>();
				services.AddTransient<IDBAccess, DBAccess>();
			})
			.Build();
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		// Start up AppHost before rest of application
		await AppHost!.StartAsync();

		// Initializes MainViewModel
		await AppHost.Services.GetRequiredService<MainViewModel>().InitializeAsync();

		// Opens MainWindow
		AppHost.Services.GetRequiredService<MainWindow>().Show();

		// starts application
		base.OnStartup(e);
	}

	protected override async void OnExit(ExitEventArgs e)
	{
		await AppHost!.StopAsync();
		base.OnExit(e);
	}

}
