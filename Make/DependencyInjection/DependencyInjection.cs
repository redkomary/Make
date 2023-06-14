using Make.Application.DependencyInjection;
using Make.DataAccess.InMemory.DependencyInjection;
using Make.ImportTasks.FromFile.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Make.ConsoleApp.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection RegisterDependencies(this IServiceCollection services)
	{
		return services
			.AddDataAccess()
			.AddApplication()
			.AddImporter()
			.AddSingleton<Application>();
	}
}