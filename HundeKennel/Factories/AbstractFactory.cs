using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace HundeKennel.Factories;

public class AbstractFactory<T> : IAbstractFactory<T>
{
	private readonly Func<T> factory;

	public AbstractFactory(Func<T> factory) => this.factory = factory;

	public T Create() => factory();
}
