using Microsoft.Extensions.DependencyInjection;

namespace Make.ImportTasks.FromFile;

public static class DependencyInjection
{
	public static IServiceCollection AddImporter(this IServiceCollection services)
	{
		return services
			.AddTransient<ITasksImporter, TasksImporterFromFile>();
	}
}