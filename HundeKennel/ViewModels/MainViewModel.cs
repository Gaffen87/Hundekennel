using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HundeKennel.Factories;
using HundeKennel.Models;
using HundeKennel.Services;
using HundeKennel.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace HundeKennel.ViewModels;

public partial class MainViewModel : ObservableObject
{
	readonly DataService data;
	readonly ExcelService excel;
	readonly IAbstractFactory<DetailsView> factory;

	public MainViewModel(DataService data, ExcelService excel, IAbstractFactory<DetailsView> factory)
	{
		this.data = data;
		this.excel = excel;
		this.factory = factory;
	}

	public ObservableCollection<Dog>? Dogs { get; private set; }
    public ICollectionView? DogsView { get; set; }

	[ObservableProperty]
	private Dog? selectedDog;

	[RelayCommand]
	public void OpenDetails()
	{
		DetailsView details = factory.Create();
		details.Show();
	}

    public async Task InitializeAsync()
	{
		Dogs = new();

		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		var dogslist = await data.RetrieveAllDogs();
		foreach (var dogItem in dogslist)
		{
			Dogs.Add(dogItem);
		}
		DogsView = CollectionViewSource.GetDefaultView(Dogs);
		DogsView.Filter = new System.Predicate<object>(Contains);
		stopwatch.Stop();
		Debug.WriteLine(stopwatch.ElapsedMilliseconds);
	}

	#region ListView Filtering properties/methods
	private bool Contains(object item)
	{
		Dog? dog = item as Dog;
		if (dog == null)
			return false;

		if (dog.Name == null) 
			return false;

		if (!string.IsNullOrEmpty(NameFilter) && dog.Name != null && !dog.Name!.Contains(NameFilter, System.StringComparison.OrdinalIgnoreCase))
			return false;
		
		if (!string.IsNullOrEmpty(ColorFilter) && dog.Color != null && !dog.Color!.Contains(ColorFilter, System.StringComparison.OrdinalIgnoreCase))
			return false;

		if (ShowOwned && dog.BreederID != 1)
			return false;

		if (!ShowDead)
			if (dog.Dead == true || dog.Age > 15) 
				return false;

		if (OnlyMale && dog.Sex != "H")
			return false;

		if (OnlyFemale && dog.Sex != "T")
			return false;

		return true;
	}

	[ObservableProperty]
	private bool onlyMale = false;
	[ObservableProperty]
	private bool onlyFemale = false;
	[ObservableProperty]
	private bool showDead = true;
	[ObservableProperty]
	private bool showOwned = false;
	[ObservableProperty]
	private string? colorFilter;
	[ObservableProperty]
	private string? nameFilter;

	partial void OnOnlyMaleChanged(bool value)
	{
		if (OnlyFemale && OnlyMale)
			OnlyFemale = false;
		DogsView!.Refresh();
	}
	partial void OnOnlyFemaleChanged(bool value)
	{
		if (OnlyFemale && OnlyMale)
			OnlyMale = false;
		DogsView!.Refresh();
	}
	partial void OnShowDeadChanged(bool value)
	{
		DogsView!.Refresh();
	}
	partial void OnShowOwnedChanged(bool value)
	{
		DogsView!.Refresh();
	}
	partial void OnColorFilterChanged(string? value)
	{
		DogsView!.Refresh();
	}
	partial void OnNameFilterChanged(string? value)
	{
		DogsView!.Refresh();
	}
	#endregion




	[ObservableProperty]
	private BitmapImage? test;

	[ObservableProperty]
	private byte[]? byteimage;

	[RelayCommand]
	public async Task imageconverter()
	{
		string path = @"C:\Users\askel\Downloads\Uthyla.jpg";
		Byteimage = File.ReadAllBytes(path);

		MemoryStream ms = new(Byteimage);
		BitmapImage tmp = new();
		tmp.BeginInit();
		tmp.StreamSource = ms;
		tmp.CacheOption = BitmapCacheOption.OnLoad;
		tmp.EndInit();

		SelectedDog.Image = Byteimage;
		await data.UpdateDog(SelectedDog);

		Test = tmp;
	}

}
