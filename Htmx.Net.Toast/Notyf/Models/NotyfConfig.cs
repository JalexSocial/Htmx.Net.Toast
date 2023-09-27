using Htmx.Net.Toast.Notyf.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Htmx.Net.Toast.Enums;

namespace Htmx.Net.Toast.Notyf.Models;

public class NotyfConfig
{
	private Position _position = new Position();

	public NotyfConfig(NotyfPosition position = NotyfPosition.BottomRight, List<NotyfNotificationOptions>? customTypes = null)
	{
		Duration = 5000;
		Dismissible = true;
		Ripple = true;
		try
		{
			var description = ToDescriptionString(position);
			var positionArray = description.Split('-');
			Position = new Position
			{
				X = positionArray.Length == 0 ? "right" : positionArray[0],
				Y = positionArray.Length == 0 ? "bottom" : positionArray[1]
			};
		}
		catch
		{
			Position = new Position
			{
				X = "right",
				Y = "bottom"
			};
		}

		Types = new List<NotyfNotificationOptions>
		{
			/*
			Icons courtesy of HeroIcons
			https://heroicons.com/
			MIT License
			   
			Copyright (c) 2020 Refactoring UI Inc.

			Permission is hereby granted, free of charge, to any person obtaining a copy
			of this software and associated documentation files (the "Software"), to deal
			in the Software without restriction, including without limitation the rights
			to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
			copies of the Software, and to permit persons to whom the Software is
			furnished to do so, subject to the following conditions:

			The above copyright notice and this permission notice shall be included in all
			copies or substantial portions of the Software.

			THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
			IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
			FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
			AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
			LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
			OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
			SOFTWARE.
			*/ 
			new NotyfNotificationOptions
			{
				Type = ToastNotificationType.Success,
				Icon = "<svg xmlns=\"http://www.w3.org/2000/svg\" fill=\"none\" viewBox=\"0 0 24 24\" stroke-width=\"1.5\" stroke=\"currentColor\" style=\"width: 1.25em; height: 1.25em\">\r\n  <path stroke-linecap=\"round\" stroke-linejoin=\"round\" d=\"M9 12.75L11.25 15 15 9.75M21 12a9 9 0 11-18 0 9 9 0 0118 0z\" />\r\n</svg>\r\n",
				BackgroundColor = "#28a745"
			},
			new NotyfNotificationOptions
			{
				Type = ToastNotificationType.Error,
				Icon = "<svg xmlns=\"http://www.w3.org/2000/svg\" fill=\"none\" viewBox=\"0 0 24 24\" stroke-width=\"1.5\" stroke=\"currentColor\"  style=\"width: 1.25em; height: 1.25em\">\r\n  <path stroke-linecap=\"round\" stroke-linejoin=\"round\" d=\"M12 9v3.75m9-.75a9 9 0 11-18 0 9 9 0 0118 0zm-9 3.75h.008v.008H12v-.008z\" />\r\n</svg>\r\n",
				BackgroundColor = "#dc3545"
			},
			new NotyfNotificationOptions
			{
				Type = ToastNotificationType.Warning,
				BackgroundColor = "orange",
				ClassName = "Text-dark",
				Icon = "<svg xmlns=\"http://www.w3.org/2000/svg\" fill=\"none\" viewBox=\"0 0 24 24\" stroke-width=\"1.5\" stroke=\"currentColor\" style=\"width: 1.25em; height: 1.25em;\">\r\n  <path stroke-linecap=\"round\" stroke-linejoin=\"round\" d=\"M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126zM12 15.75h.007v.008H12v-.008z\" />\r\n</svg>"
			},
			new NotyfNotificationOptions
			{
				Type = ToastNotificationType.Information,
				BackgroundColor = "#17a2b8",
				Icon = "<svg xmlns=\"http://www.w3.org/2000/svg\" fill=\"none\" viewBox=\"0 0 24 24\" stroke-width=\"1.5\" stroke=\"currentColor\" style=\"width: 1.25em; height: 1.25em;\">\r\n  <path stroke-linecap=\"round\" stroke-linejoin=\"round\" d=\"M11.25 11.25l.041-.02a.75.75 0 011.063.852l-.708 2.836a.75.75 0 001.063.853l.041-.021M21 12a9 9 0 11-18 0 9 9 0 0118 0zm-9-3.75h.008v.008H12V8.25z\" />\r\n</svg>\r\n"
			}
		};

		if (customTypes != null)
		{
			Types.AddRange(customTypes);
		}
	}

	[JsonPropertyName("duration")]
	public int Duration { get; set; }
	[JsonPropertyName("position")]
	public Position Position { get; set; }

	[JsonPropertyName("dismissible")]
	public bool Dismissible { get; set; } = true;
	[JsonPropertyName("ripple")]
	public bool Ripple { get; set; } = true;
	[JsonPropertyName("types")]
	public List<NotyfNotificationOptions> Types { get; set; }

	private static string ToDescriptionString(NotyfPosition val)
	{
		var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString())
			.GetCustomAttributes(typeof(DescriptionAttribute), false);
		return attributes.Length > 0 ? attributes[0].Description : "right-bottom";
	}
}