using HundeKennel.Factories;
using HundeKennel.Services;
using HundeKennel.ViewModels;
using HundeKennel.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Windows;


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
				services.AddSingleton<MainWindow>();
				services.AddSingleton<MainViewModel>();
				services.AddTransient<DataService>();
				services.AddTransient<ExcelService>();
				services.AddTransient<IDBAccess, DBAccess>();

				services.AddDetailsFactory<DetailsView>();

				services.AddTransient<DetailsViewModel>();
				services.AddTransient<DetailsView>();
			})
			.Build();
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		await AppHost!.StartAsync();

		var startupWindow = AppHost.Services.GetRequiredService<MainWindow>();
		startupWindow.Show(); // Opens MainWindow on startup

		base.OnStartup(e);
	}

	protected override async void OnExit(ExitEventArgs e)
	{
		await AppHost!.StopAsync();
		base.OnExit(e);
	}

}
