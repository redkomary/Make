using Make.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Make.DataAccess.InMemory;

public static class DependencyInjectionExtensions
{
	public static IServiceCollection AddRepository<TEntity>(this IServiceCollection services)
		where TEntity : IEntity
	{
		return services
			.AddSingleton<IRepository<TEntity>, InMemoryRepository<TEntity>>();
	}
}