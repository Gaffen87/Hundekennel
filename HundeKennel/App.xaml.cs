using HundeKennel.Models;
using HundeKennel.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace HundeKennel
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH9ceXVdR2NYWEJ3XEM=");
		}

	}
}
