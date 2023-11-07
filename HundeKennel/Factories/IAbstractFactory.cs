namespace HundeKennel.Factories
{
	public interface IAbstractFactory<T>
	{
		T Create();
	}
}