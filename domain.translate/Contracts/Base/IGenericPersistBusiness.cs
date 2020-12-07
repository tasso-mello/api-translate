namespace domain.translate.Contracts.Base
{
	public interface IGenericPersistBusiness<TEntity> where TEntity : class
	{
		object Save(TEntity obj, long idUser);
		object Update(TEntity obj, long idUser);
		object Delete(TEntity obj);
	}
}
