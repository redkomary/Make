using Make.Application.Import.Controllers;
using Make.Application.RunTask.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Make.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		return services
			.AddTransient<ImportTasksController>()
			.AddTransient<RunTaskController>();
	}
}