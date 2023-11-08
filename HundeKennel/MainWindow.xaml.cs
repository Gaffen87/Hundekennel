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
}