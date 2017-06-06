using Newtonsoft.Json;

namespace AuroraSharp
{
	public class Color
	{
		public Color()
		{
			
		}

		public Color(int hue, int saturation, int brightness)
		{
			Hue = hue;
			Saturation = saturation;
			Brightness = brightness;
		}

		[JsonProperty("hue")]
		public int Hue { get; set; }

		[JsonProperty("saturation")]
		public int Saturation { get; set; }

		[JsonProperty("brightness")]
		public int Brightness { get; set; }

		public override string ToString()
		{
			return $"{nameof(Hue)}: {Hue}, {nameof(Saturation)}: {Saturation}, {nameof(Brightness)}: {Brightness}";
		}
	}
}