using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HundeKennel.Factories;
using HundeKennel.Models;
using HundeKennel.Services;
using HundeKennel.Views;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HundeKennel.ViewModels;

public partial class MainViewModel : ObservableObject
{
	/*
	 * 
	 *				Constructor
	 * 
	 */
	public MainViewModel(DataService data, ExcelService excel, IAbstractFactory<DetailsView> detailsFactory, IAbstractFactory<PedigreeView> pedigreeFactory, PedigreeViewModel pvm)
	{
		this.data = data;
		this.excel = excel;
		this.detailsFactory = detailsFactory;
		this.pedigreeFactory = pedigreeFactory;
		pedigreeViewModel = pvm;
	}

	/*
	 * 
	 *		Fields and Properties
	 * 
	 */
	private readonly DataService data;
	private readonly ExcelService excel;
	private readonly IAbstractFactory<DetailsView> detailsFactory;
	private readonly IAbstractFactory<PedigreeView> pedigreeFactory;
	private readonly PedigreeViewModel pedigreeViewModel;

	// A list of all dogs in the db
	public ObservableCollection<Dog>? Dogs { get; private set; }
	// The CollectionView used to data bind to a ListView
	public ICollectionView? DogsView { get; set; }

	// Bound to SelectedItem in a ListView
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(SetCompareDogsCommand))]
	[NotifyCanExecuteChangedFor(nameof(DeleteDogCommand))]
	private Dog? selectedDog;

	// Used to store the objects the user wants to compare
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ShowTreeViewCommand))]
	private Dog? leftCompare;
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ShowTreeViewCommand))]
	private Dog? rightCompare;

	// Bound to a button to disable it when InsertFromExcelFile is running
	[ObservableProperty]
	private bool isNotBusy = true;

	// Bound to an animation to show if InsertFromExcelFile is running
	[ObservableProperty]
	private bool loading = false;

	/*
	 * 
	 *		Methods and Commands
	 * 
	 */
	/// <summary>
	/// Used to initialize the ViewModel
	/// </summary>
	/// <returns></returns>
	public async Task InitializeAsync()
	{
		Dogs = new();
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

	[RelayCommand(CanExecute = nameof(CanOpenTreeView))]
	public void ShowTreeView()
	{
		PedigreeView pedigreeView = pedigreeFactory.Create();
		InitPedigreeViewModel();
		pedigreeView.DataContext = pedigreeViewModel;
		pedigreeView.Show();
	}
	private bool CanOpenTreeView()
	{ 
		return LeftCompare != null && RightCompare != null;
	}
	private void InitPedigreeViewModel()
	{
		pedigreeViewModel.Init(LeftCompare!, RightCompare!);
	}

	[RelayCommand]
	public void InsertDog(Dog dog)
	{
		try
		{
			data.InsertDog(dog);
			Dogs!.Add(dog);
		}
		catch (ConstraintException)
		{
			MessageBox.Show("En hund med samme stambogs nr findes allerede i databasen", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		catch (SqlException ex)
		{
			MessageBox.Show($"Der skete en fejl: {ex.Message}", "Ukendt Fejl");
		}
	}

	[RelayCommand(CanExecute = nameof(CanSetCompare))]
	public void SetCompareDogs(string position)
	{
		if (position == "Left")
			LeftCompare = SelectedDog;

		if (position == "Right")
			RightCompare = SelectedDog;

		SelectedDog = null;
	}
	private bool CanSetCompare()
	{
		return SelectedDog != null;
	}

	[RelayCommand(CanExecute = nameof(CanDeleteDog))]
	public void DeleteDog(Dog dog)
	{
		var result = MessageBox.Show("Er du sikker?", "Slet?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
		if (result == MessageBoxResult.OK)
		{
			MessageBox.Show("Hund Slettet!", "Success!");
			data.DeleteDog(SelectedDog!.Pedigree!);
			Dogs!.Remove(dog);
			DogsView!.Refresh();
		}
	}
	private bool CanDeleteDog()
	{
		return SelectedDog != null;
	}

	[RelayCommand]
	public async Task InsertFromExcelFile()
	{
		OpenFileDialog fileDialog = new();
		fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
		if ((bool)fileDialog.ShowDialog()!)
		{
			IsNotBusy = false;
			Loading = true;
			var dogs = excel.Excel(fileDialog.FileName);
			foreach (var dog in dogs)
			{
				//if (Dogs!.Any(x => x.Pedigree == dog.Pedigree))
				//{
				//	continue;
				//}
				try
				{
					await data.InsertDog(dog);
				}
				catch (SqlException)
				{
					continue;
				}
			}
		}
		IsNotBusy = true;
		Loading = false;
	}

	[RelayCommand]
	public void OpenDetails()
	{
		DetailsView details = detailsFactory.Create();
		details.Show();
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
	public void Imageconverter() 
	{
		OpenFileDialog file = new OpenFileDialog();
		if (file.ShowDialog() == true)
		{
			Byteimage = File.ReadAllBytes(file.FileName);

			MemoryStream ms = new(Byteimage);
			BitmapImage tmp = new();
			tmp.BeginInit();
			tmp.StreamSource = ms;
			tmp.CacheOption = BitmapCacheOption.OnLoad;
			tmp.EndInit();

			SelectedDog!.Image = Byteimage;
			//await data.UpdateDog(SelectedDog);
		}
	}
	#endregion
}
