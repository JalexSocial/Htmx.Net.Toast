using System;
using System.ComponentModel;

namespace Htmx.Net.Toast.Extensions;

public static class EnumExtensions
{
	public static string ToDescriptionString<T>(this T source) where T : Enum
	{
		var fi = source.GetType().GetField(source.ToString());
		var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
			typeof(DescriptionAttribute), false);
		if (attributes != null && attributes.Length > 0) return attributes[0].Description;
		return source.ToString();
	}
}