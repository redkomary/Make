namespace Make.ImportJobs.FromFile;

public class FilePathDataSource : IDataSource
{
	public FilePathDataSource(string filePath)
	{
		FilePath = filePath;
	}

	public string FilePath { get; }
}