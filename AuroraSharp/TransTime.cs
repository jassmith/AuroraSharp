using Newtonsoft.Json;

namespace AuroraSharp
{
	public class TransTime
	{

		[JsonProperty("maxValue")]
		public int MaxValue { get; set; }

		[JsonProperty("minValue")]
		public int MinValue { get; set; }

		public static implicit operator TransTime(int value)
		{
			return new TransTime {MaxValue = value, MinValue = value};
		}

		public override string ToString()
		{
			return $"{nameof(MaxValue)}: {MaxValue}, {nameof(MinValue)}: {MinValue}";
		}
	}
}