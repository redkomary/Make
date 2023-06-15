using Make.Domain.Entities;

namespace Make.DataAccess;

public interface IRepository<TEntity>
	where TEntity : IEntity
{
	public IEnumerable<TEntity> GetAll();

	public TEntity Get(long id);

	public void Create(TEntity entity);

	public void Update(TEntity entity);

	public void Delete(TEntity entity);
}