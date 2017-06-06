using Newtonsoft.Json;

namespace AuroraSharp
{
	public class DelayTime
	{

		[JsonProperty("maxValue")]
		public int MaxValue { get; set; }

		[JsonProperty("minValue")]
		public int MinValue { get; set; }

		public static implicit operator DelayTime(int value)
		{
			return new DelayTime {MaxValue = value, MinValue = value};
		}

		public override string ToString()
		{
			return $"{nameof(MaxValue)}: {MaxValue}, {nameof(MinValue)}: {MinValue}";
		}
	}
}