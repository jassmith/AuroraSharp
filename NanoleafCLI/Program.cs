using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Options;
using Nito.AsyncEx;
using IniParser;
using IniParser.Model;
using AuroraSharp;

namespace NanoleafCLI
{
	class Program
	{
		static int Main(string[] args)
		{
			try
			{
				return AsyncContext.Run(() => MainAsync(args));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex);
				return -1;
			}
		}

		static async Task<int> MainAsync(string[] args)
		{
			string address;
			string authToken;

			var parser = new FileIniDataParser();
			try
			{
				IniData data = parser.ReadFile("Configuration.ini");
				address = data["General"]["ipaddr"];
				authToken = data["General"]["authToken"];
			}
			catch
			{
				IniData data = new IniData();
				data["General"]["ipaddr"] = "0.0.0.0";
				data["General"]["authToken"] = "<INSERT KEY HERE>";
				parser.WriteFile("Configuration.ini", data);

				Console.WriteLine("Please make sure Configuration.ini is properly configured.");
				return 1;
			}
			
			//var aurora = new Aurora("172.20.8.50", "Qxs6JjRa4P2nANiZAYhClZmcslQfVl1t");
			var aurora = new Aurora(address, authToken);
			
			var shouldShowHelp = false;
			var dumpInfo = false;
			var listEffects = false;
			string newEffect = null;
			string mode = null;
			string details = null;
			string brightness = null;
			string hue = null;
			string saturation = null;
			string temperature = null;

			var options = new OptionSet
			{
				{ "i|info", "Dump all accessible data from the Nanoleaf Aurora", i => dumpInfo = true },
				{ "h|help", "Show this message and exit", h => shouldShowHelp = true },
				{ "l|list", "List all effects", _l => listEffects = true },
				{ "e|setEffect=", "Set active effect", _e => newEffect = _e },
				{ "m|mode=", "Turn on/off the Aurora", _m => mode = _m },
				{ "d|details=", "Get effect details", _d => details = _d },
				{ "setBrightness=", "Set brightness of Aurora", _b => brightness = _b },
				{ "setHue=", "Set the hue of Aurora", _h => hue = _h },
				{ "setSaturation=", _s => saturation = _s },
				{ "setColorTemp=", _t => temperature = _t }
			};

			List<string> extra;
			try
			{
				extra = options.Parse(args);
			}
			catch (OptionException ex)
			{
				Console.Write("aurora: ");
				Console.WriteLine(ex.Message);
				Console.WriteLine("Try `aurora --help` for more information.");
				return 1;
			}

			if (shouldShowHelp)
			{
				Console.WriteLine("Options:");
				options.WriteOptionDescriptions(Console.Out);
				return 0;
			}

			if (dumpInfo)
			{
				var info = await aurora.GetInfo();
				Console.WriteLine(info);
				return 0;
			}

			if (listEffects)
			{
				var effects = await aurora.ListEffects();
				foreach (var e in effects)
					Console.WriteLine(e);
				return 0;
			}

			if (!string.IsNullOrEmpty(mode))
			{
				switch (mode.ToLowerInvariant())
				{
					case "on":
						await aurora.Enable();
						break;
					case "off":
						await aurora.Disable();
						break;
					default:
						Console.WriteLine("Mode not understood");
						return 1;
				}
			}

			if (!string.IsNullOrEmpty(newEffect))
			{
				await aurora.SetEffect(newEffect);
			}

			if (!string.IsNullOrEmpty(details))
			{
				var effectDetails = await aurora.GetEffectDetails(details);
				Console.WriteLine(effectDetails);
			}

			if (!string.IsNullOrEmpty(brightness))
			{
				int bright;
				if (!int.TryParse(brightness, out bright))
				{
					Console.WriteLine("Could not parse brightness");
					return 1;
				}
				await aurora.SetBrightness(bright);
			}

			if (!string.IsNullOrEmpty(hue))
			{
				int h;
				if (!int.TryParse(hue, out h))
				{
					Console.WriteLine("Could not parse hue: " + hue);
					return 1;
				}
				await aurora.SetHue(h);
			}

			if (!string.IsNullOrEmpty(saturation))
			{
				int sat;
				if (!int.TryParse(saturation, out sat))
				{
					Console.WriteLine("Could not parse saturation: " + saturation);
					return 1;
				}
				await aurora.SetSaturation(sat);
			}

			if (!string.IsNullOrEmpty(temperature))
			{
				int temp;
				if (!int.TryParse(temperature, out temp))
				{
					Console.WriteLine("Could not parse temperature: " + temperature);
					return 1;
				}
				await aurora.SetColorTemperature(temp);
			}

			return 0;
		}
	}
}
