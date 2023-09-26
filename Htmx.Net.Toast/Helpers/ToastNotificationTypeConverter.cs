using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Htmx.Net.Toast.Enums;

namespace Htmx.Net.Toast.Helpers;
internal class ToastNotificationTypeConverter : JsonConverter<ToastNotificationType>
{
	public override ToastNotificationType Read(ref Utf8JsonReader reader, Type typeToConvert,
		JsonSerializerOptions options) => ToastNotificationType.Custom(reader.GetString()!);

	public override void Write(Utf8JsonWriter writer, ToastNotificationType value, JsonSerializerOptions options) =>
		writer.WriteStringValue(value.ToString());
}
