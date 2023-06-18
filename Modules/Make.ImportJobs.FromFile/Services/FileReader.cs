namespace Make.ImportJobs.FromFile.Services;

internal class FileReader
{
	public IEnumerable<IReadOnlyList<string>> Read(string filePath, CancellationToken cancellationToken)
	{
		return ReadInternal(filePath, cancellationToken);
	}


	private static IEnumerable<IReadOnlyList<string>> ReadInternal(string filePath, CancellationToken cancellationToken)
	{
		IEnumerable<string> lines = ReadLinesFromFile(filePath);
		int i = 0;

		var currentBlock = new List<string>();
		foreach (string line in lines)
		{
			if (cancellationToken.IsCancellationRequested)
				throw new OperationCanceledException();

			if (string.IsNullOrWhiteSpace(line))
			{
				throw new InvalidOperationException(
					$"Ошибка чтения данных из файла \"{filePath}\", строка {i + 1}: " +
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
				if (!currentBlock.Any())
				{
					throw new InvalidOperationException(
						$"Ошибка чтения данных из файла \"{filePath}\", строка {i + 1}: " +
						$"Описание действий должно следовать за заголовком задачи.");
				}

				currentBlock.Add(line);
			}

			i++;
		}

		yield return currentBlock.AsReadOnly();
	}

	private static IEnumerable<string> ReadLinesFromFile(string filePath)
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