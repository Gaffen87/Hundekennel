using HundeKennel.ViewModels;
using Syncfusion.Windows.PropertyGrid;
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
using System.Windows.Shapes;

namespace HundeKennel.Views
{
	/// <summary>
	/// Interaction logic for DetailsView.xaml
	/// </summary>
	public partial class DetailsView : Window
	{
		public DetailsView(DetailsViewModel viewModel, MainViewModel mvm)
		{
			InitializeComponent();
			DataContext = viewModel;
			viewModel.SelectedDog = mvm.SelectedDog;
		}

		private void DogDetailsGrid_AutoGeneratingPropertyGridItem(object sender, Syncfusion.Windows.PropertyGrid.AutoGeneratingPropertyGridItemEventArgs e)
		{
			if (e.DisplayName == "Parents" || e.DisplayName == "BreederID" || e.DisplayName == "OwnerID" || e.DisplayName == "BitImage" || e.DisplayName == "Image")
				e.Cancel = true;
        }
    }
}
