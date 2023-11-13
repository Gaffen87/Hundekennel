using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HundeKennel.Factories;
using HundeKennel.Models;
using HundeKennel.Services;
using HundeKennel.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HundeKennel.ViewModels;

public partial class MainViewModel : ObservableObject
{
	private readonly DataService data;
	private readonly ExcelService excel;
	private readonly IAbstractFactory<DetailsView> detailsFactory;
	private readonly IAbstractFactory<PedigreeView> pedigreeFactory;

	public MainViewModel(DataService data, ExcelService excel, IAbstractFactory<DetailsView> detailsFactory, IAbstractFactory<PedigreeView> pedigreeFactory)
	{
		this.data = data;
		this.excel = excel;
		this.detailsFactory = detailsFactory;
		this.pedigreeFactory = pedigreeFactory;
	}

	public ObservableCollection<Dog>? Dogs { get; private set; }
	public ICollectionView? DogsView { get; set; }

	[ObservableProperty]
	private Dog? selectedDog;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ShowTreeViewCommand))]
	private Dog? leftCompare;
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ShowTreeViewCommand))]
	private Dog? rightCompare;

	[RelayCommand(CanExecute = nameof(CanOpenTreeView))]
	public void ShowTreeView()
	{
		PedigreeView pedigreeView = pedigreeFactory.Create();
		pedigreeView.Show();
	}
	private bool CanOpenTreeView()
	{ 
		return LeftCompare != null && RightCompare != null;
	}

	[RelayCommand]
	public void SetCompareDogs(string position)
	{
		if (position == "Left")
			LeftCompare = SelectedDog;

		if (position == "Right")
			RightCompare = SelectedDog;
	}

	[RelayCommand]
	public void OpenDetails()
	{
		DetailsView details = detailsFactory.Create();
		details.Show();
	}

	public async Task InitializeAsync()
	{
		Dogs = new();
		//await data.InsertList(excel.Excel(@"C:\Users\askel\Downloads\HundeData.xlsx"));
		var dogslist = await data.RetrieveAllDogs();
		foreach (var dogItem in dogslist)
		{
			Dogs.Add(dogItem);
		}
		foreach (Dog dog in Dogs)
		{
			dog.Parents = new()
			{
				Dogs.FirstOrDefault(x => x.Pedigree == dog.Dad) ?? new Dog() { Name = "Ingen Data" },
				Dogs.FirstOrDefault(x => x.Pedigree == dog.Mom) ?? new Dog() { Name = "Ingen Data" }
			};
		}
		DogsView = CollectionViewSource.GetDefaultView(Dogs);
		DogsView.Filter = new Predicate<object>(Filter);
	}

	#region ListView Filtering properties/methods
	private bool Filter(object item)
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



	#region indsæt billede
	[ObservableProperty]
	private byte[]? byteimage;

	[RelayCommand]
	public async Task imageconverter() 
	{
		OpenFileDialog file = new OpenFileDialog();
		file.InitialDirectory = @"C:\Users\askel\Pictures\Screenshots";
		if (file.ShowDialog() == true)
		{
			Byteimage = File.ReadAllBytes(file.FileName);

			MemoryStream ms = new(Byteimage);
			BitmapImage tmp = new();
			tmp.BeginInit();
			tmp.StreamSource = ms;
			tmp.CacheOption = BitmapCacheOption.OnLoad;
			tmp.EndInit();

			SelectedDog.Image = Byteimage;
			await data.UpdateDog(SelectedDog);
		}
	}
	#endregion
}
