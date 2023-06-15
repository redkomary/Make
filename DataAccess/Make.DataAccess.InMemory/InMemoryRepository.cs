using Make.Domain.Entities;

namespace Make.DataAccess.InMemory;

public class InMemoryRepository<TEntity> : IRepository<TEntity>
	where TEntity : IEntity
{
	private readonly List<TEntity> _list = new();


	public IEnumerable<TEntity> GetAll()
	{
		return _list;
	}

	public TEntity Get(long id)
	{
		return _list.FirstOrDefault(entity => entity.Id == id) ??
		       throw new KeyNotFoundException($"Объект \"{typeof(TEntity).FullName}\" с идентификатором {id} не найден.");
	}

	public void Create(TEntity entity)
	{
		entity.Id = _list.Count + 1;
		_list.Add(entity);
	}

	public void Update(TEntity entity)
	{
		// ignore
	}

	public void Delete(TEntity entity)
	{
		_list.Remove(entity);
	}
}