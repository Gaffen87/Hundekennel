using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace HundeKennel.Services;

public class DBAccess : IDBAccess
{
	private readonly string? connectionString;

	public DBAccess()
	{
		IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		this.connectionString = config.GetConnectionString("MyDBConnection");
	}

	public async Task<IEnumerable<T>> LoadData<T, U>(string sqlStatement, U parameters)
	{
		using IDbConnection conn = new SqlConnection(connectionString);

		return await conn.QueryAsync<T>(sqlStatement, parameters, commandType: CommandType.StoredProcedure);
	}

	public async Task SaveData<T>(string sqlStatement, T parameters)
	{
		using IDbConnection conn = new SqlConnection(connectionString);

		await conn.ExecuteAsync(sqlStatement, parameters, commandType: CommandType.StoredProcedure);
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
