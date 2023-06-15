using Make.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Make.DataAccess.InMemory.DependencyInjection;

/// <summary>
/// Сервис внедрения зависимостей.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Добавляет зависимости модуля.
	/// </summary>
	public static IServiceCollection AddDataAccess(this IServiceCollection services)
	{
		return services
			.AddRepository<IJob>()
			.AddRepository<IOperation>();
	}
}