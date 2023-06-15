namespace Make.Domain.Entities;

/// <summary>
/// Доменный объект.
/// </summary>
public interface IEntity
{
	/// <summary>
	/// Идентификатор объекта.
	/// </summary>
	public long Id { get; set; }
}