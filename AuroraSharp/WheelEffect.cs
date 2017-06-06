using System.Collections.Generic;
using Newtonsoft.Json;

namespace AuroraSharp
{
	public class WheelEffect : Effect
	{
		public WheelEffect()
		{
			AnimName = "wheel";
		}

		[JsonProperty("windowSize")]
		public int WindowSize { get; set; }
	}
}