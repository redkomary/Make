using Microsoft.Extensions.DependencyInjection;

namespace Make.ImportTasks.FromFile.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddImporter(this IServiceCollection services)
	{
		return services
			.AddTransient<ITasksImporter, TasksImporterFromFile>();
	}
}