using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AuroraSharp
{
	public class Aurora
	{
		private readonly string _address;
		private readonly string _authToken;
		private readonly string _baseUrl;
		private readonly HttpClient _client;

		public Aurora(string address, string authToken)
		{
			if (address == null) throw new ArgumentNullException(nameof(address));
			if (authToken == null) throw new ArgumentNullException(nameof(authToken));

			_address = address;
			_authToken = authToken;
			_baseUrl = "http://" + _address + ":16021/api/v1/" + _authToken + "/";
			_client = new HttpClient
			{
				BaseAddress = new Uri(_baseUrl),
				Timeout = TimeSpan.FromSeconds(10)
			};
		}

		public override string ToString()
		{
			return "<Aurora(" + _address + ")>";
		}

		private Task<string> Put(string endpoint, object data)
		{
			return Put(endpoint, JsonConvert.SerializeObject(data));
		}

		private Task<string> Put(string endpoint, IDirectJson data)
		{
			return Put(endpoint, data.GetJson());
		}

		private async Task<string> Put(string endpoint, string data)
		{
			var result = await _client.PutAsync(endpoint, new StringContent(data)).ConfigureAwait(false);
			return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
		}

		private async Task<string> Get(string endpoint = "")
		{
			var result = await _client.GetAsync(endpoint).ConfigureAwait(false);
			return await result.Content.ReadAsStringAsync();
		}

		private async Task<int> GetInt(string endpoint = "")
		{
			var result = await Get(endpoint).ConfigureAwait(false);
			return int.Parse(result);
		}

		private Task Delete(string endpoint)
		{
			return _client.DeleteAsync(endpoint);
		}

		public Task<string> GetInfo()
		{
			return Get();
		}

		public Task<string> GetColorMode() => Get("state/colorMode");

		public Task Identify()
		{
			return Put("identify", new Dictionary<string, string>());
		}

		public Task<string> GetFirmware() => Get("firmwareVersion");

		public Task<string> GetModel() => Get("model");

		public Task<string> GetSerialNumber() => Get("serialNo");

		public Task DeleteUser()
		{
			return Delete("");
		}

		//   ###########################################
		//   # On / Off methods
		//   ###########################################

		public async Task<bool> IsEnabled()
		{
			var result = await Get("state/on/value").ConfigureAwait(false);

			if (string.Compare(result, "true", StringComparison.InvariantCultureIgnoreCase) == 0)
				return true;
			return false;
		}

		public Task Enable()
		{
			return Put("state", new StateSetter("on", "true"));
		}

		public Task Disable()
		{
			return Put("state", new StateSetter("on", "false"));
		}

		//   ###########################################
		//   # Brightness methods
		//   ###########################################

		public Task<int> GetBrightness() => GetInt("state/brightness/value");
		public Task<int> GetBrightnessMin() => GetInt("state/brightness/min");
		public Task<int> GetBrightnessMax() => GetInt("state/brightness/max");

		public Task SetBrightness(int level)
		{
			if (level < 0 || level > 100)
				throw new ArgumentOutOfRangeException("level");
			return Put("state", new StateSetter("brightness", level.ToString()));
		}

		public Task IncrementBrightness(int level)
		{
			return Put("state", new StateSetter("brightness", level.ToString(), "increment"));
		}

		//   ###########################################
		//   # Hue methods
		//   ###########################################

		public Task<int> GetHue() => GetInt("state/hue/value");

		public Task<int> GetHueMin() => GetInt("state/hue/min");

		public Task<int> GetHueMax() => GetInt("state/hue/max");

		public Task SetHue(int hue)
		{
			if (hue < 0 || hue > 360)
				throw new ArgumentOutOfRangeException("hue");
			return Put("state", new StateSetter("hue", hue.ToString()));
		}

		public Task IncrementHue(int level)
		{
			return Put("state", new StateSetter("hue", level.ToString(), "increment"));
		}

		//   ###########################################
		//   # Saturation methods
		//   ###########################################

		public Task<int> GetSaturation() => GetInt("state/saturation/value");

		public Task<int> GetSaturationMin() => GetInt("state/saturation/min");

		public Task<int> GetSaturationMax() => GetInt("state/saturation/max");

		public Task SetSaturation(int saturation)
		{
			if (saturation < 0 || saturation > 100)
				throw new ArgumentOutOfRangeException("sat");
			return Put("state", new StateSetter("sat", saturation.ToString()));
		}

		public Task IncrementSaturation(int level)
		{
			return Put("state", new StateSetter("saturation", level.ToString(), "increment"));
		}

		//   ###########################################
		//   # Color Temperature methods
		//   ###########################################

		public Task<int> GetColorTemperature() => GetInt("state/ct/value");

		public Task<int> GetColorTemperatureMin() => GetInt("state/ct/min");

		public Task<int> GetColorTemperatureMax() => GetInt("state/ct/max");

		public Task SetColorTemperature(int temp)
		{
			if (temp < 1200 || temp > 6500)
				throw new ArgumentOutOfRangeException("temp");
			return Put("state", new StateSetter("ct", temp.ToString()));
		}

		public Task IncrementColorTemperature(int level)
		{
			return Put("state", new StateSetter("ct", level.ToString(), "increment"));
		}


		//   ###########################################
		//   # Layout methods
		//   ###########################################

		public Task<int> GetOrientation() => GetInt("panelLayout/globalOrientation/value");

		public Task<int> GetOrientationMin() => GetInt("panelLayout/globalOrientation/min");

		public Task<int> GetOrientationMax() => GetInt("panelLayout/globalOrientation/max");

		public Task<int> GetPanelCount() => GetInt("panelLayout/layout/numPanels");

		public Task<int> GetPanelLength() => GetInt("panelLayout/layout/sideLength");

		public async Task<IList<PanelPositionData>> GetPanelPositions()
		{
			var json = await Get("panelLayout/layout/positionData").ConfigureAwait(false);

			return JsonConvert.DeserializeObject<List<PanelPositionData>>(json);
		}

		//   ###########################################
		//   # Effect methods
		//   ###########################################

		//   _reserved_effect_names = ["*Static*", "*Dynamic*", "*Solid*"]
		
		public Task<string> GetSelectedEffect() => Get("effects/select");
		
		public Task SetEffect(string effect) => Put("effects", new Dictionary<string, string>() {{"select", effect}});

		public async Task<IList<string>> ListEffects()
		{
			var json = await Get("effects/effectsList").ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<IList<string>>(json);
			return result;
		}

		//   def effect_set_raw(self, effect_data: dict):
		//       """Sends a raw dict containing effect data to the device.
		//       The dict given must match the json structure specified in the API docs."""
		//       data = {"write": effect_data}
		//       self.__put("effects", data)

		public async Task<EffectDetails> GetEffectDetails(string effect)
		{
			var data = "{\"write\" : {\"command\" : \"request\", \"animName\" : \"" + effect + "\"}}";

			var json = await Put("effects", data).ConfigureAwait(false);

			return JsonConvert.DeserializeObject<EffectDetails>(json);
		}

		public async Task<IList<EffectDetails>> GetAllEffectDetails()
		{
			var data = "{\"write\" : {\"command\" : \"requestAll\"}}";

			var json = await Put("effects", data).ConfigureAwait(false);

			return JsonConvert.DeserializeObject<List<EffectDetails>>(json);
		}

		public Task DeleteEffect(string effect)
		{
			var data = "{\"write\": {\"command\": \"delete\", \"animName\": " + effect + "}}";
			return Put("effects", data);
		}

		public Task RenameEffect(string effect, string newName)
		{
			var data = "{\"write\": {\"command\": \"rename\", \"animName\": " + effect + ", \"newName\": " + newName + "}}";
			return Put("effects", data);
		}
	}
}
