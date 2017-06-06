using System.Collections.Generic;
using Newtonsoft.Json;

namespace AuroraSharp
{
	public class Effect
	{
		[JsonProperty("command")]
		public string Command { get; internal set; }

		[JsonProperty("animName")]
		public string AnimName { get; set; }

		[JsonProperty("animType")]
		public string AnimType { get; protected set; }

		[JsonProperty("version")]
		public string Version { get; set; } = "1.0";

		[JsonProperty("colorType")]
		public string ColorType { get; protected set; } = "HSB";

		[JsonProperty("animData")]
		public object AnimData { get; protected set; }

		[JsonProperty("palette")]
		public IList<Color> Palette { get; } = new List<Color>();

		[JsonProperty("transTime")]
		public TransTime TransTime { get; set; } = 20;

		[JsonProperty("delayTime")]
		public DelayTime DelayTime { get; set; } = 20;

		[JsonProperty("direction")]
		public string Direction { get; set; } = "left";

		[JsonProperty("loop")]
		public bool Loop { get; set; } = true;
	}
}