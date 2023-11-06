using HundeKennel.Models;
using HundeKennel.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace HundeKennel
{
	public partial class MainWindow : Window
	{
		readonly ExcelService excelService;
		readonly DataService dataService;
		public MainWindow(ExcelService excelService, DataService dataService)
		{
			InitializeComponent();

			this.dataService = dataService;
			this.excelService = excelService;
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			((Button)sender!).IsEnabled = false;
			ProgressBar.IsIndeterminate = true;

			OpenFileDialog openFileDialog = new()
			{
				Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
			};
			openFileDialog.ShowDialog();
			if (openFileDialog.FileName != "")
			{
				string path = openFileDialog.FileName;
				List<Dog> dogs = excelService.Excel(path);

				//foreach (Dog dog in dogs)
				//{
				//	try
				//	{
				//		await dataService.InsertDog(dog);
				//	}
				//	catch (Exception ex)
				//	{
				//		Debug.WriteLine(ex.Message);
				//		continue;
				//	}
				//}
				Stopwatch stopwatch = new();
				stopwatch.Start();
				await dataService.InsertList(dogs);
				stopwatch.Stop();
				Debug.WriteLine("Done in "+ stopwatch.ElapsedMilliseconds + " ms");
				((Button)sender!).IsEnabled = true;
				ProgressBar.IsIndeterminate = false;
			}
			((Button)sender!).IsEnabled = true;
			ProgressBar.IsIndeterminate = false;
		}
	}
}
