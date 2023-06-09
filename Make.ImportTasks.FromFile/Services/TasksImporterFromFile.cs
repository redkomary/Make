using Domain.Entities;

namespace Make.ImportTasks.FromFile.Services;

public class TasksImporterFromFile
{
	private readonly FileReader _fileReader = new();
	private readonly TaskInfoParser _parser = new();
	private readonly TasksCreator _creator = new();


	public IEnumerable<ITask> Import(string filePath)
	{
		IEnumerable<IReadOnlyList<string>> blocks = _fileReader.Read(filePath);
		IEnumerable<TaskInfo> taskInfos = blocks.Select(_parser.Parse);
		return _creator.Create(taskInfos);
	}
}