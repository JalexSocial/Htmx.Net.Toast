using System.Text.Json;
using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Htmx.Net.Toast.Services;

public class TempDataService : ITempDataService
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly JsonSerializerOptions _serializerOptions;
	private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

	public TempDataService(ITempDataDictionaryFactory tempDataDictionaryFactory,
		IHttpContextAccessor httpContextAccessor)
	{
		_tempDataDictionaryFactory = tempDataDictionaryFactory;
		_httpContextAccessor = httpContextAccessor;
		_serializerOptions = GetSerializerSettings();
	}

	/// <summary>
	///     Gets or sets <see cref="ITempDataDictionary" />/>.
	/// </summary>
	private ITempDataDictionary TempData =>
		_tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);

	public T Get<T>(string key) where T : class
	{
		if (TempData.ContainsKey(key) && TempData[key] is string json)
			return JsonSerializer.Deserialize<T>(json, _serializerOptions);
		return null;
	}

	public T Peek<T>(string key) where T : class
	{
		if (TempData.ContainsKey(key) && TempData.Peek(key) is string json)
			return JsonSerializer.Deserialize<T>(json, _serializerOptions);
		return null;
	}

	public void Add(string key, object value)
	{
		TempData[key] = value.ToJson();
	}

	public bool Remove(string key)
	{
		return TempData.ContainsKey(key) && TempData.Remove(key);
	}

	private JsonSerializerOptions GetSerializerSettings()
	{
		return new JsonSerializerOptions
		{
			IncludeFields = true
		};
	}
}