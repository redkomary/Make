using Domain.Entities;
using Action = Domain.Entities.Action;
using Task = Domain.Entities.Task;

namespace Make.ImportTasks.FromFile;

public class TasksImporterFromFile_Backup //: ITasksImporter
{
	private readonly string _filePath;
	private readonly IDictionary<string, TaskInfo> _cache = new Dictionary<string, TaskInfo>(StringComparer.InvariantCultureIgnoreCase);


	public TasksImporterFromFile_Backup(string filePath)
	{
		_filePath = filePath;
	}


	public IEnumerable<ITask> Import(CancellationToken cancellationToken)
	{
		return ImportTasks(cancellationToken).Select(CreateTask);
	}

	private IEnumerable<TaskInfo> ImportTasks(CancellationToken cancellationToken)
	{
		IEnumerable<string> lines = ReadLinesFromFile();
		int i = 0;

		TaskInfo? currentTask = null;
		foreach (string line in lines)
		{
			if (string.IsNullOrWhiteSpace(line))
				throw new ReadLineException("Строка не может быть пустой.", _filePath, i + 1);

			bool isActionLine = char.IsWhiteSpace(line[0]);
			if (isActionLine)
			{
				if (currentTask == null)
					throw new ReadLineException("Список действий должен следовать после описания заголовка задачи.", _filePath, i + 1);

				ActionInfo action = ParseAction(line);
				currentTask.AddAction(action);
			}
			else
			{
				yield return currentTask = ParseTaskHeader(line, i + 1);
				if (!_cache.TryAdd(currentTask.Name, currentTask))
					throw new InvalidOperationException($"Задача с наименованием \"{currentTask.Name}\" уже была загружена. Наименование задачи должно быть уникальным.");
			}

			i++;
		}
	}

	private IEnumerable<string> ReadLinesFromFile()
	{
		if (!File.Exists(_filePath))
			throw new FileNotFoundException("Файл не существует.", _filePath);

		try
		{
			return File.ReadLines(_filePath);
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Не удалось прочитать содержимое файла \"{_filePath}\".", e);
		}
	}

	private TaskInfo ParseTaskHeader(string line, int lineNumber)
	{
		string[] parts = line.Split(':', StringSplitOptions.TrimEntries);

		if (parts.Length is 0 or > 2)
			throw new ReadLineException("Не удалось прочитать заголовок задачи.", _filePath, lineNumber);

		string name = parts[0];

		IEnumerable<string> dependencies = parts.Length == 2
			? parts[1].Split(Array.Empty<string>(), StringSplitOptions.RemoveEmptyEntries)
			: Enumerable.Empty<string>();
		
		return new TaskInfo(name, dependencies);
	}

	private ActionInfo ParseAction(string line)
	{
		line = line.Trim();
		return new ActionInfo(line);
	}


	private Task CreateTask(TaskInfo taskInfo)
	{
		return new Task(taskInfo.Name)
		{
			Actions = taskInfo.Actions
				.Select(CreateAction),

			Children = taskInfo.DependencyTaskNames
				.Select(GetTaskInfo)
				.Select(CreateTask) // TODO: неправильно!!!!!!! 100 раз одну задачу будем создавать
		};
	}

	private TaskInfo GetTaskInfo(string name)
	{
		return _cache.TryGetValue(name, out TaskInfo? task)
			? task
			: throw new KeyNotFoundException($"Задача \"{name}\" не найдена.");
	}

	private Action CreateAction(ActionInfo actionInfo)
	{
		return new Action(actionInfo.Name);
	}
}