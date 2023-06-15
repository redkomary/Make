namespace Make.Utilities;

/// <summary>
/// Методы расширения для <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
	/// <summary>
	/// Выполняет над каждым элементом исходной последовательности указанное действие.
	/// </summary>
	/// <param name="source">Исходная последовательность.</param>
	/// <param name="action">Действие, которое будет выполняться над каждым элементом последовательности.</param>
	/// <typeparam name="T">Тип элементов в последовательности.</typeparam>
	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (T item in source)
		{
			action(item);
		}
	}
}