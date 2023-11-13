using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeExtensions
{
	public static class AgeExtension
	{
		public static int Age(this DateTime? dt, DateTime date = new())
		{
			if (date == DateTime.MinValue)
				date = DateTime.Now;

			if (date < dt)
				return 0;

			if (date.Month < dt?.Month || (date.Month == dt?.Month && date.Day < dt?.Day))
			{
				return date.Year - dt?.Year - 1 ?? 0;
			}
			return date.Year - dt?.Year ?? 0;
		}
	}
}
