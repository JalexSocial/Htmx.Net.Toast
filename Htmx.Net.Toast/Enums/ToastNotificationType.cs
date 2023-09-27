namespace Htmx.Net.Toast.Enums;

public class ToastNotificationType
{
	private readonly string _type;

	private ToastNotificationType(string type)
	{
		_type = type;
	}

	public static ToastNotificationType Success => new("success");
	public static ToastNotificationType Error => new("error");
	public static ToastNotificationType Warning => new("warning");
	public static ToastNotificationType Information => new("information");
	public static ToastNotificationType Custom(string type) => new(type);

	public override string ToString()
	{
		return _type;
	}
}

