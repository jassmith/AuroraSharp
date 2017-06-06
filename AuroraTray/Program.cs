using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AuroraSharp;
using AuroraTray.Properties;
using Zeroconf;

namespace AuroraTray
{
	static class Program
	{
		private static AuroraTray _auroraTray;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			using (_auroraTray = new AuroraTray())
			{
				Application.Idle += ApplicationOnIdle;
				Application.Run();	
			}
		}

		private static void ApplicationOnIdle(object sender, EventArgs eventArgs)
		{
			Application.Idle -= ApplicationOnIdle;
			_auroraTray.Display();
		}
	}

	public class AuroraTray : IDisposable
	{
		private readonly NotifyIcon _notify;
		private Aurora _aurora;

		public AuroraTray()
		{
			_notify = new NotifyIcon {Icon = Resources.mainIcon};
		}

		public async Task<IZeroconfHost> ProbeForAuroras()
		{
			IReadOnlyList<IZeroconfHost> results = await
				ZeroconfResolver.ResolveAsync("_nanoleafapi._tcp.local.").ConfigureAwait(false);
			
			return results.FirstOrDefault();
		}

		public async void Display()
		{
			var result = await ProbeForAuroras();

			_aurora = new Aurora(result.IPAddress, "Qxs6JjRa4P2nANiZAYhClZmcslQfVl1t");

			var menu = new ContextMenuStrip();

			var menuItems = await GetEffectMenuItems();
			menu.Items.AddRange(menuItems.ToArray());
			
			menu.Items.Add(new ToolStripSeparator());

			menu.Items.Add(GetToggleItem());

			_notify.ContextMenuStrip = menu;
			_notify.Visible = true;	
		}

		public ToolStripItem GetToggleItem()
		{
			var result = new ToolStripMenuItem("Toggle On/Off");

			result.Click += async (sender, args) =>
			{
				var isOn = await _aurora.IsEnabled();
				if (isOn)
					await _aurora.Disable();
				else
					await _aurora.Enable();
			};

			return result;
		}

		public async Task<IList<ToolStripItem>> GetEffectMenuItems()
		{
			var result = new List<ToolStripItem>();

			var effects = await _aurora.ListEffects();

			foreach (var e in effects)
			{
				var effect = e;
				var mi = new ToolStripMenuItem(e);
				mi.Click += async (sender, args) =>
				{
					await _aurora.SetEffect(effect);
				};
				result.Add(mi);
			}

			return result;
		}

		public void Dispose()
		{
			_notify.Dispose();
		}
	}
}
