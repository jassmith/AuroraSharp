using Newtonsoft.Json;

namespace AuroraSharp
{
	public class BrightnessRange
	{

		[JsonProperty("maxValue")]
		public int MaxValue { get; set; }

		[JsonProperty("minValue")]
		public int MinValue { get; set; }

		public static implicit operator BrightnessRange(int value)
		{
			return new BrightnessRange {MaxValue = value, MinValue = value};
		}

		public override string ToString()
		{
			return $"{nameof(MaxValue)}: {MaxValue}, {nameof(MinValue)}: {MinValue}";
		}
	}
}