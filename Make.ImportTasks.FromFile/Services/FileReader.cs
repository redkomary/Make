namespace Make.ImportTasks.FromFile.Services;

public class FileReader
{
	public IEnumerable<IReadOnlyList<string>> Read(string filePath)
	{
		return ReadInternal(filePath);
	}

	private IEnumerable<IReadOnlyList<string>> ReadInternal(string filePath)
	{
		IEnumerable<string> lines = ReadLinesFromFile(filePath);
		int i = 0;

		var currentBlock = new List<string>();
		foreach (string line in lines)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				throw new FormatException($"Ошибка чтения данных из файла \"{filePath}\", строка {i + 1}: " +
				                          $"Строка не может быть пустой.");
			}

			bool isNewBlockStarting = !char.IsWhiteSpace(line[0]);
			if (isNewBlockStarting)
			{
				if (currentBlock.Any())
					yield return currentBlock.AsReadOnly();

				currentBlock = new List<string> { line };
			}
			else
			{
				currentBlock.Add(line);
			}

			i++;
		}

		yield return currentBlock.AsReadOnly();
	}

	private IEnumerable<string> ReadLinesFromFile(string filePath)
	{
		if (!File.Exists(filePath))
			throw new FileNotFoundException("Файл не существует.", filePath);

		try
		{
			return File.ReadLines(filePath);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Не удалось прочитать содержимое файла \"{filePath}\".", ex);
		}
	}
}