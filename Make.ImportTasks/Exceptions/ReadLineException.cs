namespace Make.ImportTasks.FromFile;

public class ReadLineException : FormatException
{
	public ReadLineException(string message, string filePath, int lineNumber)
		: base($"Ошибка чтения данных из файла \"{filePath}\", {lineNumber}: {message}.")
	{
	}
}