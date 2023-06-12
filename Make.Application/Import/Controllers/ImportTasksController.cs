using Make.Domain.Entities;
using Make.ImportTasks;

namespace Make.Application.Import.Controllers;

public class ImportTasksController
{
	private readonly ITasksImporter _importer;


	public ImportTasksController(ITasksImporter importer)
	{
		_importer = importer;
	}


	public IEnumerable<ITask> Import(string filePath)
	{
		return _importer.Import(filePath);
	}
}