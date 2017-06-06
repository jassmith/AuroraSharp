using System.Collections.Generic;
using Newtonsoft.Json;

namespace AuroraSharp
{
	public class FlowEffect : Effect
	{
		[JsonProperty("flowFactor")]
		public double FlowFactor { get; set; } = 2.5;
	}
}