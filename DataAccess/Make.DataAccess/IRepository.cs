using Make.Domain.Entities;

namespace Make.DataAccess;

/// <summary>
/// Репозиторий.
/// </summary>
/// <typeparam name="TEntity">Тип хранимых объектов.</typeparam>
public interface IRepository<TEntity>
	where TEntity : IEntity
{
	/// <summary>
	/// Возвращает все имеющиеся объекты.
	/// </summary>
	public IEnumerable<TEntity> GetAll();

	/// <summary>
	/// Возвращает объект по его идентификатору.
	/// </summary>
	/// <param name="id">Идентификатор объекта.</param>
	public TEntity Get(long id);

	/// <summary>
	/// Сохраняет новый объект.
	/// </summary>
	/// <param name="entity">Сохраняемый объект.</param>
	public void Create(TEntity entity);

	/// <summary>
	/// Обновляет объект.
	/// </summary>
	/// <param name="entity">Обновляемый объект.</param>
	public void Update(TEntity entity);

	/// <summary>
	/// Удаляет объект.
	/// </summary>
	/// <param name="entity">Удаляемый объект.</param>
	public void Delete(TEntity entity);
}