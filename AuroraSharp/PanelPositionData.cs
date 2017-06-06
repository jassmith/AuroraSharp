using Newtonsoft.Json;

namespace AuroraSharp
{
	public class PanelPositionData
	{

		[JsonProperty("panelId")]
		public int PanelId { get; set; }

		[JsonProperty("x")]
		public int X { get; set; }

		[JsonProperty("y")]
		public int Y { get; set; }

		[JsonProperty("o")]
		public int Orientation { get; set; }

		public override string ToString()
		{
			return $"{nameof(PanelId)}: {PanelId}, {nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Orientation)}: {Orientation}";
		}
	}
}