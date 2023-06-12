using Make.Application.Import.Controllers;
using Make.Application.RunTask.Controllers;
using Make.Domain.Entities;
using Make.ImportTasks.FromFile;

using TTask = System.Threading.Tasks.Task;

namespace Make.ConsoleApp;

internal class Program
{
	public static async TTask Main(string[] args)
	{
		try
		{
			var importTasksController = new ImportTasksController(new TasksImporterFromFile());
			IEnumerable<ITask> tasks = importTasksController.Import("makefile.txt");

			ITask targetTask = tasks.First(task => task.Name == "Target_05");

			var executor = new RunTaskController();
			await executor.Run(targetTask, CancellationToken.None);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		Console.ReadKey();
	}
}