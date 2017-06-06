using System.Collections.Generic;
using Newtonsoft.Json;

namespace AuroraSharp
{
	public class ExplodeEffect : Effect
	{
		public ExplodeEffect()
		{
			AnimName = "explode";
		}

		[JsonProperty("explodeFactor")]
		public double ExplodeFactor { get; set; }
	}
}