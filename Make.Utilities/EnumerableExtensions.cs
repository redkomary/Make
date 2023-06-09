namespace Make.Utilities;

public static class EnumerableExtensions
{
	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (T item in source)
		{
			action(item);
		}
	}

	public static TValue? GetValueOrDefault<TKey, TValue>(
		this IDictionary<TKey, TValue> dictionary,
		TKey key,
		TValue? defaultValue = default)
	{
		return dictionary.TryGetValue(key, out TValue? value)
			? value
			: defaultValue;
	}
}