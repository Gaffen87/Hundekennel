using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Factories;

public static class ServiceExtensions
{
	public static void AddViewFactory<TView>(this IServiceCollection services)
		where TView : class
	{
		services.AddTransient<TView>();
		services.AddSingleton<Func<TView>>(x => () => x.GetService<TView>()!);
		services.AddSingleton<IAbstractFactory<TView>, AbstractFactory<TView>>();
	}
}
