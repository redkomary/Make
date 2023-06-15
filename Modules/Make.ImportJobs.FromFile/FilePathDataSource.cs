namespace Make.ImportJobs.FromFile;

/// <summary>
/// Источник данных, предоставляющий путь к файлу.
/// </summary>
public class FilePathDataSource : IDataSource
{
	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="filePath">Путь к файлу.</param>
	public FilePathDataSource(string filePath)
	{
		FilePath = filePath;
	}


	/// <summary>
	/// Путь к файлу.
	/// </summary>
	public string FilePath { get; }
}