using HundeKennel.Models;
using HundeKennel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DataService dataService;
		public MainWindow()
		{
			InitializeComponent();

			dataService = new DataService();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			ProgressBar.IsIndeterminate = true;
			List<Dog> dogs = ExcelService.Excel(@"C:\Users\askel\Downloads\HundeData.xlsx");
			await dataService.InsertListAsync(dogs);
			ProgressBar.IsIndeterminate = false;
		}
	}
}
