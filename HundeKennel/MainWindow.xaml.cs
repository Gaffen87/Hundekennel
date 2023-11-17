using HundeKennel.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace HundeKennel;

public partial class MainWindow : Window
{
	readonly MainViewModel viewModel;
	public MainWindow(MainViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		DataContext = viewModel;
	}

	private void DogListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		viewModel.OpenDetails();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		viewModel.SelectedDog = new();
		InsertDogView.Visibility = Visibility.Visible;
    }

	private void DogListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	{
		DogListView.ScrollIntoView(viewModel.SelectedDog);
	}
}