using HundeKennel.ViewModels;
using System;
using System.Collections.Generic;
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

namespace HundeKennel.Views
{
	/// <summary>
	/// Interaction logic for InsertDogControl.xaml
	/// </summary>
	public partial class InsertDogControl : UserControl
	{
		public InsertDogControl()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Er du sikker?", "Bekræft", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.OK)
			{
				MessageBox.Show("Hund Gemt", "Success");
				this.Visibility = Visibility.Collapsed;
			}
        }

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.Visibility = Visibility.Collapsed;
		}
	}
}
