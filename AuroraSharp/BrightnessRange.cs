using Newtonsoft.Json;

namespace AuroraSharp
{
	public class BrightnessRange
	{

		[JsonProperty("maxValue")]
		public int MaxValue { get; set; }

		[JsonProperty("minValue")]
		public int MinValue { get; set; }

		public override string ToString()
		{
			return $"{nameof(MaxValue)}: {MaxValue}, {nameof(MinValue)}: {MinValue}";
		}
	}
}