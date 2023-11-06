using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Z.Dapper.Plus;
using Z.BulkOperations;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace HundeKennel.Services
{
	public class DBAccess : IDBAccess
	{
		private readonly string? connectionString;

		public DBAccess()
		{
			IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			this.connectionString = config.GetConnectionString("MyDBConnection");
		}

		public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters)
		{
			using IDbConnection conn = new SqlConnection(connectionString);

			return await conn.QueryAsync<T>(sql, parameters);
		}

		public async Task SaveData<T>(string sql, T parameters)
		{
			using IDbConnection conn = new SqlConnection(connectionString);

			await conn.ExecuteAsync(sql, parameters);
		}
		
		public async Task SaveBulk<T>(T parameters)
		{
			using IDbConnection conn = new SqlConnection(connectionString);
			try
			{
				await conn.BulkActionAsync(x => x.BulkInsert(parameters));
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}
