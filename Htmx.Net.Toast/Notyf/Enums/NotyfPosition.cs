using System.ComponentModel;

namespace Htmx.Net.Toast.Notyf.Enums;

public enum NotyfPosition
{
	[Description("right-top")]
	TopRight,

	[Description("right-bottom")]
	BottomRight,

	[Description("left-bottom")]
	BottomLeft,

	[Description("left-top")]
	TopLeft,

	[Description("center-top")]
	TopCenter,

	[Description("center-bottom")]
	BottomCenter
}