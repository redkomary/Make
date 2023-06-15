using Make.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Make.DataAccess.InMemory.DependencyInjection;

/// <summary>
/// Вспомогательные методы расширения класса <see cref="IServiceCollection"/>.
/// </summary>
public static class DependencyInjectionExtensions
{
	/// <summary>
	/// Добавляет репозиторий в зависимости.
	/// </summary>
	public static IServiceCollection AddRepository<TEntity>(this IServiceCollection services)
		where TEntity : IEntity
	{
		return services
			.AddSingleton<IRepository<TEntity>, InMemoryRepository<TEntity>>();
	}
}