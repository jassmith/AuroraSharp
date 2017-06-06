using System.Collections.Generic;
using Newtonsoft.Json;

namespace AuroraSharp
{
	public class RandomEffect : Effect
	{
		public RandomEffect()
		{
			AnimType = "random";
		}

		[JsonProperty("brightnessRange")]
		public BrightnessRange BrightnessRange { get; set; }
	}
}