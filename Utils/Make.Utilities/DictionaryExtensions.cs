namespace Make.Utilities;

/// <summary>
/// Методы расширения для <see cref="IDictionary{TKey, TValue}"/>.
/// </summary>
public static class DictionaryExtensions
{
	/// <summary>
	/// Возвращает значение, записанное в словарь с указанным ключом, или значение по умолчанию, если ключ отсутствует в словаре.
	/// </summary>
	/// <param name="source">Словарь.</param>
	/// <param name="key">Ключ.</param>
	/// <param name="defaultValue">Значение по умолчанию.</param>
	/// <typeparam name="TKey">Тип ключа.</typeparam>
	/// <typeparam name="TValue">Тип значения.</typeparam>
	/// <returns>Значение по указанному ключу или значение по умолчанию.</returns>
	public static TValue? GetValueOrDefault<TKey, TValue>(
		this IDictionary<TKey, TValue> source,
		TKey key,
		TValue? defaultValue = default)
	{
		return source.TryGetValue(key, out TValue? value)
			? value
			: defaultValue;
	}
}