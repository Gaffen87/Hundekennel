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

namespace HundeKennel.Services
{
	public class DataService
	{
		//private string? connectionString;
		readonly IDBAccess db;

		public DataService(IDBAccess dBAccess)
		{
			this.db = dBAccess;
		}

		string sql = "INSERT INTO dogs VALUES (@Pedigree, @Chip, @Name, @BirthDate, @Sex, @Color, @Dad, @Mom, @DkTitles, @Titles, @HD, @AD, @HZ, @SP, @IndexDate, @HDIndex, @Dead, @AK, @BreedingStatus, @MB, @Image, @BreederID, @OwnerID)";

		public Task InsertDog(Dog dog) =>
			db.SaveData(sql, dog);

		public Task InsertList(List<Dog> dogs) =>
			db.SaveBulk(dogs);

		//public DataService()
		//{
		//	IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		//	connectionString = config.GetConnectionString("MyDBConnection");
		//}

		//public async Task InsertListAsync(List<Dog> dogs)
		//{
		//	string sqlstring = "INSERT INTO dogs VALUES (@Pedigree, @Chip, @Name, @BirthDate, @Sex, @Color, @Dad, @Mom, @DkTitles, @Titles, @HD, @AD, @HZ, @SP, @IndexDate, @HDIndex, @Dead, @AK, @BreedingStatus, @MB, @Image, @OwnerID, @BreederID)";
		//	using SqlConnection conn = new(connectionString);
		//	try
		//	{
		//		conn.Open();
		//	}
		//	catch (Exception)
		//	{
		//		return;
		//	}
		//	SqlCommand cmd = new(sqlstring, conn);
		//	foreach (Dog dog in dogs)
		//	{
		//		try
		//		{
		//			cmd = new(sqlstring, conn);
		//			cmd.Parameters.Add("@Pedigree", SqlDbType.NVarChar).Value = dog.Pedigree;
		//			cmd.Parameters.Add("@Chip", SqlDbType.NVarChar).Value = dog.Chip != null ? dog.Chip : DBNull.Value;
		//			cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = dog.Name != null ? dog.Name : DBNull.Value;
		//			cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = dog.BirthDate != null ? dog.BirthDate : DBNull.Value;
		//			cmd.Parameters.Add("@Sex", SqlDbType.NVarChar).Value = dog.Sex != null ? dog.Sex : DBNull.Value;
		//			cmd.Parameters.Add("@Color", SqlDbType.NVarChar).Value = dog.Color != null ? dog.Color : DBNull.Value;
		//			cmd.Parameters.Add("@Dad", SqlDbType.NVarChar).Value = dog.DadString != null ? dog.DadString : DBNull.Value;
		//			cmd.Parameters.Add("@Mom", SqlDbType.NVarChar).Value = dog.MomString != null ? dog.MomString : DBNull.Value;
		//			cmd.Parameters.Add("@DKTitles", SqlDbType.NVarChar).Value = dog.DKTitles != null ? dog.DKTitles : DBNull.Value;
		//			cmd.Parameters.Add("@Titles", SqlDbType.NVarChar).Value = dog.Titles != null ? dog.Titles : DBNull.Value;
		//			cmd.Parameters.Add("@HD", SqlDbType.NVarChar).Value = dog.HD != null ? dog.HD : DBNull.Value;
		//			cmd.Parameters.Add("@AD", SqlDbType.NVarChar).Value = dog.AD != null ? dog.AD : DBNull.Value;
		//			cmd.Parameters.Add("@HZ", SqlDbType.NVarChar).Value = dog.HZ != null ? dog.HZ : DBNull.Value;
		//			cmd.Parameters.Add("@SP", SqlDbType.NVarChar).Value = dog.SP != null ? dog.SP : DBNull.Value;
		//			cmd.Parameters.Add("@IndexDate", SqlDbType.Date).Value = dog.IndexDate != null ? dog.IndexDate : DBNull.Value;
		//			cmd.Parameters.Add("@HDIndex", SqlDbType.NVarChar).Value = dog.HDIndex != null ? dog.HDIndex : DBNull.Value;
		//			cmd.Parameters.Add("@Dead", SqlDbType.Bit).Value = dog.Dead;
		//			cmd.Parameters.Add("@AK", SqlDbType.NVarChar).Value = dog.AK != null ? dog.AK : DBNull.Value;
		//			cmd.Parameters.Add("@BreedingStatus", SqlDbType.NVarChar).Value = dog.BreedingStatus != null ? dog.BreedingStatus : DBNull.Value;
		//			cmd.Parameters.Add("@MB", SqlDbType.NVarChar).Value = dog.MB != null ? dog.MB : DBNull.Value;
		//			cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = dog.Image != null ? dog.Image : DBNull.Value;
		//			cmd.Parameters.Add("@OwnerID", SqlDbType.Int).Value = dog.Owner != null ? dog.Owner.ID : DBNull.Value;
		//			cmd.Parameters.Add("@BreederID", SqlDbType.Int).Value = dog.Breeder != null ? dog.Breeder.ID : DBNull.Value;
		//			await cmd.ExecuteNonQueryAsync();
		//			cmd.Parameters.Clear();
		//		}
		//		catch (SqlException)
		//		{
		//			continue;
		//		}
		//	}
		//}

		//void Insert(Dog dog) 
		//{

		//}

		//void Update(Dog dog, string[] column) 
		//{

		//}

		//void Retrieve() { }

		//void Delete(string pedigree) { }
	}
}
