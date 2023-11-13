using HundeKennel.ViewModels;
using System.Windows;

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
