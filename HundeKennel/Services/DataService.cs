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

	// Returns a list of 1 dog from db
	public async Task<IEnumerable<Dog>> RetrieveDog(string pedigree)
	{
		return await db.LoadData<Dog, dynamic>("spRetrieveDog", new { Pedigree = pedigree });
	}

	// returns a list of all dogs from db
	public async Task<IEnumerable<Dog>> RetrieveAllDogs()
	{
		return await db.LoadData<Dog, dynamic>("spRetrieveAllDogs", new { } );
	}

	// Inserts a dog in db
	public Task InsertDog(Dog dog) =>
		db.SaveData("spInsertDog", dog);

	// Inserts a list of dogs in db
	public Task InsertList(List<Dog> dogs) =>
		db.SaveBulk(dogs);

	// deletes a dog with matching pedigree from db
	public Task DeleteDog(string pedigree) =>
		db.SaveData("spDeleteDog", pedigree);

	// Updates dog with matching pedigree in db
	public Task UpdateDog(Dog dog) =>
		db.SaveData("spUpdateDog", dog);
}
