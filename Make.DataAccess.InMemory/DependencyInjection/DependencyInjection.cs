using Make.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Make.DataAccess.InMemory.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services)
	{
		return services
			.AddRepository<ITask>()
			.AddRepository<IOperation>();
	}
}