using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HundeKennel.Factories;
using HundeKennel.Models;
using HundeKennel.Services;
using HundeKennel.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace HundeKennel.ViewModels;

public partial class MainViewModel : ObservableObject
{
	readonly DataService data;
	readonly ExcelService excel;
	private readonly IAbstractFactory<DetailsView> factory;

	public MainViewModel(DataService data, 
		ExcelService excel,
		IAbstractFactory<DetailsView> factory)
	{
		this.data = data;
		this.excel = excel;
		this.factory = factory;
		_ = Init();
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

    async Task Init()
	{
		Dogs = new();

		var dogslist = await data.RetrieveAllDogs();
		foreach (var dogItem in dogslist)
		{
			Dogs.Add(dogItem);
		}
	}

}
