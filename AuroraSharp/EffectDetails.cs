using System.Collections.Generic;
using Newtonsoft.Json;

namespace AuroraSharp
{
	public class EffectDetails
	{

		[JsonProperty("animName")]
		public string AnimName { get; set; }

		[JsonProperty("loop")]
		public bool Loop { get; set; }

		[JsonProperty("palette")]
		public IList<Color> Palette { get; } = new List<Color>();

		[JsonProperty("transTime")]
		public TransTime TransTime { get; set; }

		[JsonProperty("windowSize")]
		public int WindowSize { get; set; }

		[JsonProperty("flowFactor")]
		public int FlowFactor { get; set; }

		[JsonProperty("delayTime")]
		public DelayTime DelayTime { get; set; }

		[JsonProperty("colorType")]
		public string ColorType { get; set; }

		[JsonProperty("animType")]
		public string AnimType { get; set; }

		[JsonProperty("explodeFactor")]
		public int ExplodeFactor { get; set; }

		[JsonProperty("brightnessRange")]
		public BrightnessRange BrightnessRange { get; set; }

		[JsonProperty("direction")]
		public string Direction { get; set; }

		public override string ToString()
		{
			var colorStr = "";
			foreach (var color in Palette)
				colorStr += "\t" + color + "\n";
			return $@"{nameof(AnimName)}: {AnimName}, 
{nameof(Loop)}: {Loop}, 
{nameof(Palette)}:
{colorStr}
{nameof(TransTime)}: {TransTime}, 
{nameof(WindowSize)}: {WindowSize}, 
{nameof(FlowFactor)}: {FlowFactor}, 
{nameof(DelayTime)}: {DelayTime}, 
{nameof(ColorType)}: {ColorType}, 
{nameof(AnimType)}: {AnimType}, 
{nameof(ExplodeFactor)}: {ExplodeFactor}, 
{nameof(BrightnessRange)}: {BrightnessRange}, 
{nameof(Direction)}: {Direction}";
		}
	}
}