using System.Text.Json;
using System.Text.Json.Serialization;

namespace Htmx.Net.Toast.Helpers;

public static class JsonSerialization
{
	public static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
	};

	public static string ToJson(this object obj)
	{
		return JsonSerializer.Serialize(obj, JsonSerializerOptions);
	}
}