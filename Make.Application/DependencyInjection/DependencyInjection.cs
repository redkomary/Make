using Make.Application.API;
using Make.Application.Import.Controllers;
using Make.Application.RunTask.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Make.Application.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		return services
			.AddTransient<IImportTasksController, ImportTasksController >()
			.AddTransient<IRunTaskController ,    RunTaskController>();
	}
}