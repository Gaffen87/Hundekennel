using System.Collections.Generic;
using System.Threading.Tasks;

namespace HundeKennel.Services
{
	public interface IDBAccess
	{
		Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters);
		Task SaveData<T>(string sql, T parameters);
		Task SaveBulk<T>(T parameters);
	}
}