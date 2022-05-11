using Microsoft.Build.Framework;
using SqlUserTypeGenerator.Helpers;

// ReSharper disable UnusedType.Global

namespace SqlUserTypeGenerator.BuildTasks
{
	/// <summary> Task for encode any text to base64 </summary>
	public class ArgumentEncodeTask : ITask
	{
		/// <summary> Output of value encoded to base64 </summary>
		[Output]
		public string EncodedArgument { get; set; }

		/// <summary> Input string value </summary>
		public string Value { get; set; }

		/// <inheritdoc />
		public bool Execute()
		{
			EncodedArgument = !string.IsNullOrEmpty(Value) ? StringHelper.EncodeArgument(Value) : null;
			return true;
		}

		/// <inheritdoc />
		public IBuildEngine BuildEngine { get; set; }

		/// <inheritdoc />
		public ITaskHost HostObject { get; set; }
	}
}
