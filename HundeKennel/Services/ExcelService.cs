using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelMapper;
using HundeKennel.Models;
using HundeKennel.Services.Helpers;

namespace HundeKennel.Services;

public static class ExcelService
{
	public static List<Dog> Excel(string path)
	{
		List<Dog> dogs;

		using var stream = File.OpenRead(path);
		using var importer = new ExcelImporter(stream);

		ExcelSheet sheet = importer.ReadSheet();
		List<ExcelDogHelper> Edogs = sheet.ReadRows<ExcelDogHelper>().ToList();

		dogs = ParseDogs(Edogs);



		return dogs;
	}

	static List<Dog> ParseDogs(List<ExcelDogHelper> Edogs)
	{
		List<Dog> dogs = new();

		foreach (var Edog in Edogs)
		{
			Dog dog = new Dog()
			{
				Pedigree = Edog.Pedigree,
				Chip = Edog.Chip,
				Name = Edog.Name,
				Sex = Edog.Sex,
				Color = Edog.Color,
				DKTitles = Edog.DKTitles,
				Titles = Edog.DKTitles,
				HD = Edog.HD,
				AD = Edog.AD,
				HZ = Edog.HZ,
				SP = Edog.SP,
				HDIndex = Edog.HDIndex,
				AK = Edog.AK,
				BreedingStatus = Edog.BreedingStatus,
				MB = Edog.MB,
				DadString = Edog.Dad,
				MomString = Edog.Mom
			};

			bool convertSucces = DateTime.TryParse(Edog.BirthDate, out DateTime date);
			if (convertSucces)
			{
				dog.BirthDate = date;
			}
			convertSucces = DateTime.TryParse(Edog.IndexDate, out date);
			if (convertSucces)
			{
				dog.IndexDate = date;
			}
			_ = Edog.Dead == "1" ? dog.Dead = true : dog.Dead = false;

			dogs.Add(dog);
		}

		return FindParents(dogs);
	}

	static List<Dog> FindParents(List<Dog> dogs)
	{
		foreach (Dog dog in dogs)
		{
			dog.Parents = new()
			{
				dogs.FirstOrDefault(x => x.Pedigree == dog.DadString) ?? new Dog() { Name = "Ingen Data" },
				dogs.FirstOrDefault(x => x.Pedigree == dog.MomString) ?? new Dog() { Name = "Ingen Data" }
			};
		}

		return dogs;
	}
}
