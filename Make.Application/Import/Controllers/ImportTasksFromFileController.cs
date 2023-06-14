using Make.Application.API;
using Make.DataAccess;
using Make.Domain.Entities;
using Make.ImportTasks;
using Make.ImportTasks.FromFile;
using Make.Utilities;

namespace Make.Application.Import.Controllers;

public class ImportTasksFromFileController : IImportTasksFromFileController
{
	private readonly ITasksImporter<FilePathDataSource> _importer;
	private readonly IRepository<ITask> _taskRs;
	private readonly IRepository<IOperation> _operationRs;


	public ImportTasksFromFileController(
		ITasksImporter<FilePathDataSource> importer,
		IRepository<ITask> taskRs,
		IRepository<IOperation> operationRs)
	{
		_importer = importer;
		_taskRs = taskRs;
		_operationRs = operationRs;
	}


	public void Import(string filePath)
	{
		var dataSource = new FilePathDataSource(filePath);
		IEnumerable<ITask> tasks = _importer.ImportFrom(dataSource);
		foreach (ITask task in tasks)
		{
			_taskRs.Create(task);
			task.Operations.ForEach(_operationRs.Create);
		}
	}
}