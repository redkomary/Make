using Make.Domain.Entities;


namespace Make.DataAccess.InMemory;

/// <summary>
/// Репозиторий, который реализует хранение данных в памяти исполняемого приложения.
/// </summary>
public class InMemoryRepository<TEntity> : IRepository<TEntity>
	where TEntity : IEntity
{
	private readonly List<TEntity> _list = new();


	/// <inheritdoc />
	public IEnumerable<TEntity> GetAll()
	{
		return _list;
	}

	/// <inheritdoc />
	public TEntity Get(long id)
	{
		return _list.FirstOrDefault(entity => entity.Id == id) ??
		       throw new KeyNotFoundException($"Объект \"{typeof(TEntity).FullName}\" с идентификатором {id} не найден.");
	}

	/// <inheritdoc />
	public void Create(TEntity entity)
	{
		entity.Id = _list.Count + 1;
		_list.Add(entity);
	}

	/// <inheritdoc />
	public void Update(TEntity entity)
	{
		// ignore
	}

	/// <inheritdoc />
	public void Delete(TEntity entity)
	{
		_list.Remove(entity);
	}
}