using ExcelMapper;
using HundeKennel.Models;
using HundeKennel.Services.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HundeKennel.Services;

public class ExcelService
{
	/// <summary>
	/// Opens an excel file and tries to read all rows into a List<ExcelDogHelper> and then converts that list into a List<Dog>
	/// </summary>
	/// <param name="path"> The filepath of the excel file </param>
	/// <returns></returns>
	public List<Dog> ImportFromExcelFile(string path)
	{
		List<Dog> dogs;

		var Edogs = ImportIntoList(path);

		dogs = ParseDogs(Edogs);

		return dogs;
	}

	private List<ExcelDogHelper> ImportIntoList(string path)
	{
		using var stream = File.OpenRead(path);
		using var importer = new ExcelImporter(stream);

		ExcelSheet sheet = importer.ReadSheet();
		List<ExcelDogHelper> Edogs = sheet.ReadRows<ExcelDogHelper>().ToList();

		return Edogs;
	}

	private List<Dog> ParseDogs(List<ExcelDogHelper> Edogs)
	{
		List<Dog> dogs = new();

		foreach (var Edog in Edogs)
		{
			Dog dog = new()
			{
				Pedigree = Edog.Pedigree.Trim(),
				Chip = Edog.Chip,
				Name = Edog.Name,
				Sex = Edog.Sex,
				Color = Edog.Color,
				DKTitles = Edog.DKTitles,
				Titles = Edog.Titles,
				HD = Edog.HD,
				AD = Edog.AD,
				HZ =Edog.HZ,
				SP = Edog.SP,
				HDIndex = Edog.HDIndex,
				AK = Edog.AK,
				BreedingStatus = Edog.BreedingStatus,
				MB = Edog.MB,
				Dad = Edog.Dad,
				Mom = Edog.Mom
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

			// tjek om der allerede er en hund med samme Pedigree og spring over hvis true
			if (dogs.Any(x => x.Pedigree == dog.Pedigree))
			{
				continue;
			}
			dogs.Add(dog);
		}

		return dogs;
	}
}
