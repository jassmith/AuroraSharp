using Newtonsoft.Json;

namespace AuroraSharp
{
	public class Color
	{

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