# Htmx.Net.Toast - Notifications For ASP.NET HTMX Applications

ToastNotification is a Minimal & Elegant Toast Notification Package for ASP.NET Core Web Applications that can be invoked via C#. Compatible with ASP.NET 7+ and HTMX.

## Features

- 📱 Elegant & Responsive
- 🐣 Global Configuration to Set the Toast Position, Duration.
- 🎸 Easily integration with ASP.NET 7+ Applications.
- 🎃 Support to render custom HTML content within the toasts
- 🐣 Simple and Customizable. Create your own custom toast with your favorite color and icons with ease!
- 👴🏽 Works with TempData internally.
- 📱 Currently Supports Notyf JS Library.
- 📱 Supports HTMX out of the box.


## Installation

```
Install-Package Htmx.Net.Toast
```
Or

```
dotnet add package Htmx.Net.Toast
```

Or

Get it directly from NuGet - https://www.nuget.org/packages/Htmx.Net.Toast/

Follow the guide below for each of the toast notification library. Cheers!

# Notyf

## Usage - Notyf

Once the package is installed, open your Startup.cs and add in the following to the ConfigureServices method.

```csharp
builder.Services.AddNotyf( config => { 
			config.Duration = 2000;
			config.Dismissable = true;
			config.Position = NotyfPosition.BottomRight;
			config.Ripple = true;
			config.CustomTypes = new List<NotyfNotificationOptions>
			{
				// Create a new notification type called "rawr" with some sensible purple defaults - Icons by HeroIcons
				new NotyfNotificationOptions
				{
					Type = ToastNotificationType.Custom("rawr"),
					BackgroundColor = "#5928a7",
					Dismissible = true,
					Duration = 5000,
					Icon = "<svg xmlns=\"http://www.w3.org/2000/svg\" fill=\"none\" viewBox=\"0 0 24 24\" stroke-width=\"1.5\" stroke=\"currentColor\" style=\"width: 1.25em; height: 1.25em;\">\r\n  <path stroke-linecap=\"round\" stroke-linejoin=\"round\" d=\"M15.59 14.37a6 6 0 01-5.84 7.38v-4.8m5.84-2.58a14.98 14.98 0 006.16-12.12A14.98 14.98 0 009.631 8.41m5.96 5.96a14.926 14.926 0 01-5.841 2.58m-.119-8.54a6 6 0 00-7.381 5.84h4.8m2.581-5.84a14.927 14.927 0 00-2.58 5.84m2.699 2.7c-.103.021-.207.041-.311.06a15.09 15.09 0 01-2.448-2.448 14.9 14.9 0 01.06-.312m-2.24 2.39a4.493 4.493 0 00-1.757 4.306 4.493 4.493 0 004.306-1.758M16.5 9a1.5 1.5 0 11-3 0 1.5 1.5 0 013 0z\" />\r\n</svg>\r\n",
					Message = "This is a default message",
					Ripple = true
				}
			};
	});
```

> Available Positions are TopRight,BottomRight,BottomLeft,TopLeft,TopCenter,BottomCenter.
Set the Dismissible bool to false, to remove the close icon from your toasts! Pretty handy.

### Enable HTMX middleware

To enable toast notification while working with HTMX, you will have to add the middleware into the Service Container. Open up Program.cs and add the following line of code under the builder.Build() method.

```csharp
app.UseNotyf();
```

> More settings will be added in the upcoming releases

Next, open up your _Layout.cshml file and add in the following to the head section. Note that you can use a bundler with this CSS if you like. It is not included by the Htmx.Net.Toast component out of the box.

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/notyf@3/notyf.min.css">
```

In the foot of your page add a reference to the notyf.min.js script and a call to invoke the Notyf component.  Note that while the example requires jQuery, Htmx.Net.Toast has no jQuery dependency:

```html
<script src="https://cdn.jsdelivr.net/npm/notyf@3/notyf.min.js"></script>
    
@await Component.InvokeAsync("Notyf")
```

Let's add the Constructor Injection. Add the following in your controllers / razor classes to invoke the toast notifications as required.

```csharp
public INotyfService _notyf { get; }
public HomeController( INotyfService notifyService)
{
    _notyf = notifyService;
}
```
Once the Injection is done, you can call the toast notification as you need. Currently 5 Types are supported.

### Success
```csharp
_notyf.Success("This is a Success Notification");
```

### Error
```csharp
_notyf.Error("This is an Error Notification");
```

### Warning
```csharp
_notyf.Warning("This is a Warning Notification");
```

### Information
```csharp
_notyf.Information("This is an Information Notification");
```

#### Set Toast Duration
By default, the toast gets dismissed in 5 seconds. You can set the duration(in milliseconds) after which the toast will be dismissed.
```csharp
_notyf.Success("This toast will be dismissed in 10 seconds.", 10000);
```

## Custom
You can customize toasts but you will need to add the custom types using config.CustomTypes when adding the Notyf service via the DI container.

```csharp
_notyf.Custom(ToastNotificationType.Custom("rawr"), "Custom Notification - closes in 5 seconds.", 5000, new NotyfIcon { Color = "indigo", ClassName = "fa fa-gear" });
_notyf.Custom(ToastNotificationType.Custom("rawr"), "Custom Notification - closes in 5 seconds.", 5000);
_notyf.Custom(ToastNotificationType.Custom("rawr"), "Custom Notification - closes in 5 seconds.", 5000, false);  // No icon
_notyf.Custom(ToastNotificationType.Custom("rawr"), "Custom Notification - closes in 5 seconds.", 5000, "<svg>....</svg>");
            
```

Here, you add the class of the icon as required. Font Awesome icons are supported if you include the Font Awesome icons. You would just have to pass the icon class name. 

## Demo - Notyf

A Demo Implementation using ASP.NET 7 MVC can be found here - https://github.com/JalexSocial/Htmx.Net.Toast/tree/master/Htmx.Net.Toast.Demo

# Mentions

The Javascript library used in this project is https://github.com/caroso1222/notyf


# About the Author
Originally based on the work of Mukesh Murugan at [codewithmukesh.com](https://www.codewithmukesh.com)

Updated 2023 by Michael Tanczos
