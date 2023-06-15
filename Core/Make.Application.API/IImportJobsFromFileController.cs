namespace Make.Application.API;

/// <summary>
/// Контроллер, предоставляющий методы для инструмента импорта задач из файла.
/// </summary>
public interface IImportJobsFromFileController
{
	/// <summary>
	/// Загрузить задачи из файла.
	/// </summary>
	/// <param name="filePath">Путь к файлу импорта.</param>
	public void Import(string filePath);
}