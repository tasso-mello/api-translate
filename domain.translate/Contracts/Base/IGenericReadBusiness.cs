namespace domain.translate.Contracts.Base
{
	public interface IGenericReadBusiness<TEntity> where TEntity : class
	{
		object Get();
		object GetById(long id);
		object GetByName(string name);
	}
}
