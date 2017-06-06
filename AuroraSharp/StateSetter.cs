using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraSharp
{
	public class StateSetter : IDirectJson
	{
		public string State { get; }
		public string Value { get; }
		public string UpdateMode { get; }

		public StateSetter(string state, string value, string updateMode)
		{
			State = state;
			Value = value;
			UpdateMode = updateMode;
		}

		public StateSetter(string state, string value) : this(state, value, "value")
		{
		}

		public string GetJson()
		{
			return "{\"" + State + "\" : {\"" + UpdateMode + "\":" + Value + "}}";
		}
	}
}
