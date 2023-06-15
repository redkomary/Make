using Make.Application.DependencyInjection;
using Make.DataAccess.InMemory.DependencyInjection;
using Make.ImportJobs.FromFile.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;


namespace Make.ConsoleApp.DependencyInjection;

/// <summary>
/// Сервис внедрения зависимостей.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Добавляет зависимости.
	/// </summary>
	public static IServiceCollection RegisterDependencies(this IServiceCollection services)
	{
		return services
			.AddDataAccess()
			.AddApplication()
			.AddImporter()
			.AddSingleton<Application>();
	}
}