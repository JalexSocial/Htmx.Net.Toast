using System.Text.Json.Serialization;

namespace Htmx.Net.Toast.Notyf.Models;

public class Position
{
	[JsonPropertyName("x")]
	public string X { get; set; } = string.Empty;
	[JsonPropertyName("y")]
	public string Y { get; set; } = string.Empty;
}