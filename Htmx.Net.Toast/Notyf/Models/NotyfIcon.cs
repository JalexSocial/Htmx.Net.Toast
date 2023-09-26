using System.Text.Json.Serialization;

namespace Htmx.Net.Toast.Notyf.Models;

public class NotyfIcon
{
	[JsonPropertyName("className")]
	public string? ClassName { get; set; } 
	[JsonPropertyName("tagName")]
	public string? TagName { get; set; } 
	[JsonPropertyName("text")]
	public string? Text { get; set; } 
	[JsonPropertyName("color")]
	public string? Color { get; set; }
}