using ExcelDataReader.Log;
using HundeKennel.Models;
using HundeKennel.Services;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace HundeKennelTest;

[TestClass]
public class UnitTest1
{
	DBAccess dB;
	DataService dataService;
	ExcelService excelService;
	Dog TestDog;

	[TestInitialize]
	public void InitializeTest() 
	{
		dB = new DBAccess();
		dataService = new(dB);
		excelService = new ExcelService();
		TestDog = new()
		{
			Pedigree = "ABC123",
			Name = "Test"
		};
	}

	[TestMethod]
	public void InsertDogsFromExcelFileTest()
	{
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		// sti navnet skal selvfølgelig ændres til at passe til ens egen computer
		var dogs = excelService.ImportFromExcelFile(@"C:\Users\askel\Downloads\HundeData.xlsx");
		Assert.IsNotNull(dogs);
		Assert.AreEqual("Bibi", dogs[1000].Name);
	}

	[TestMethod]
	public async Task InsertIntoDBTest()
	{
		Dog dog = new();
		try
		{
			await dataService.InsertDog(TestDog);
			var doglist = await dataService.RetrieveDog(TestDog.Pedigree!);
			foreach (var item in doglist)
			{
				dog.Name = item.Name;
			}
		}
		finally
		{
			await dataService.DeleteDog(TestDog.Pedigree!);
		}

		Assert.AreEqual("Test", dog.Name);
	}

	[TestMethod]
	public async Task DeleteFromDBTest()
	{
		Dog dog = new();

		await dataService.InsertDog(TestDog);
		await dataService.DeleteDog(TestDog.Pedigree!);
		var doglist = await dataService.RetrieveDog(TestDog.Pedigree!);
		foreach (var item in doglist)
		{
			dog.Name = item.Name;
		}

		Assert.IsNull(dog.Name);
	}

	[TestMethod]
	public async Task TryInsertSameDogInDBTest()
	{
		string result = "";
		await dataService.InsertDog(TestDog);
		try
		{
			await dataService.InsertDog(TestDog);
		}
		catch (SqlException e)
		{
			result = e.Message;
		}
		finally
		{
			await dataService.DeleteDog(TestDog.Pedigree!);
		}
		Assert.AreEqual("Overtrædelse af PRIMARY KEY-begrænsningen 'PK_Dogs'. Dubletnøgle kan ikke indsættes i objektet 'dbo.Dogs'. Dubletnøgleværdien er (ABC123).\r\nSætningen blev afsluttet.", result);
	}
}