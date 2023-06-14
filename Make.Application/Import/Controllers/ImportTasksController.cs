using Make.Application.API;
using Make.DataAccess;
using Make.Domain.Entities;
using Make.ImportTasks;
using Make.Utilities;

namespace Make.Application.Import.Controllers;

public class ImportTasksController : IImportTasksController
{
	private readonly ITasksImporter _importer;
	private readonly IRepository<ITask> _taskRs;
	private readonly IRepository<IAction> _actionRs;


	public ImportTasksController(
		ITasksImporter importer,
		IRepository<ITask> taskRs,
		IRepository<IAction> actionRs)
	{
		_importer = importer;
		_taskRs = taskRs;
		_actionRs = actionRs;
	}


	public void Import(string filePath)
	{
		IEnumerable<ITask> tasks = _importer.Import(filePath);
		foreach (ITask task in tasks)
		{
			_taskRs.Create(task);
			task.Actions.ForEach(_actionRs.Create);
		}
	}
}