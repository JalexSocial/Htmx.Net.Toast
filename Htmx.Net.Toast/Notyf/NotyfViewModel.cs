using Htmx.Net.Toast.Notyf.Models;
using System.Collections.Generic;

namespace Htmx.Net.Toast.Notyf;

public class NotyfViewModel
{
	public string Configuration { get; set; }
	public IEnumerable<NotyfNotification> Notifications { get; set; }
}