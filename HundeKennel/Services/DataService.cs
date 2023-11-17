using HundeKennel.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HundeKennel.Services;

public class DataService
{
	readonly IDBAccess db;

	public DataService(IDBAccess dBAccess)
	{
		this.db = dBAccess;
	}

	/// <summary>
	/// Retrieves 1 dog from the db
	/// </summary>
	/// <param name="pedigree"> Pedigree that is used as search parameter in the db </param>
	/// <returns> An IEnumerable<Dog> containing 1 dog if found </returns>
	public async Task<IEnumerable<Dog>> RetrieveDog(string pedigree)
	{
		return await db.LoadData<Dog, dynamic>("spRetrieveDog", new { Pedigree = pedigree });
	}

	/// <summary>
	/// Retrieves all dogs from the db
	/// </summary>
	/// <returns> An IEnumerable<Dog> containing all dogs in the db </returns>
	public async Task<IEnumerable<Dog>> RetrieveAllDogs()
	{
		return await db.LoadData<Dog, dynamic>("spRetrieveAllDogs", new { } );
	}

	/// <summary>
	/// Used to insert 1 dog in the db
	/// </summary>
	/// <param name="dog"> The dog object you want to insert </param>
	/// <returns></returns>
	public Task InsertDog(Dog dog) =>
		db.SaveData("spInsertDog", new
		{
			dog.Pedigree,
			dog.HD,
			dog.HZ,
			dog.AD,
			dog.Name,
			dog.SP,
			dog.AK,
			dog.BirthDate,
			dog.BreederID,
			dog.BreedingStatus,
			dog.Chip,
			dog.Color,
			dog.Dad,
			dog.Dead,
			dog.MB,
			dog.Mom,
			dog.Image,
			dog.IndexDate,
			dog.HDIndex,
			dog.Sex,
			dog.DKTitles,
			dog.Titles,
			dog.OwnerID
		});

	/// <summary>
	/// Used to insert multiple dogs from a list
	/// </summary>
	/// <param name="dogs"> The list of dog objects you want to insert </param>
	/// <returns></returns>
	public Task InsertList(List<Dog> dogs) =>
		db.SaveBulk(dogs);

	/// <summary>
	/// Used to delete a dog from the db
	/// </summary>
	/// <param name="pedigree"> The pedigree string of the dog object you want to delete </param>
	/// <returns></returns>
	public Task DeleteDog(string pedigree) =>
		db.SaveData("spDeleteDog", new { pedigree });

	/// <summary>
	/// Used to update a dog in the db
	/// </summary>
	/// <param name="dog"> The updated dog object you want to update in the db</param>
	/// <returns></returns>
	public Task UpdateDog(Dog dog) =>
		db.SaveData("spUpdateDog", new 
		{
			dog.Pedigree,
			dog.HD,
			dog.HZ,
			dog.AD,
			dog.Name,
			dog.SP,
			dog.AK,
			dog.BirthDate,
			dog.BreederID,
			dog.BreedingStatus,
			dog.Chip,
			dog.Color,
			dog.Dad,
			dog.Dead,
			dog.MB,
			dog.Mom,
			dog.Image,
			dog.IndexDate,
			dog.HDIndex,
			dog.Sex,
			dog.DKTitles,
			dog.Titles,
			dog.OwnerID
		});
}
