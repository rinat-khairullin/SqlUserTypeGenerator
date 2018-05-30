using Microsoft.Build.Framework;

namespace SqlUserTypeGenerator.BuildTasks
{
	public class BuildDefaultValueTask: ITask
	{
		public IBuildEngine BuildEngine { get; set; }
		public ITaskHost HostObject { get; set; }

		[Output]
		public string Result { get; set; }

		public string InputValue { get; set; }
		public string DefaultValue { get; set; }

		public bool Execute()
		{
			Result = !string.IsNullOrEmpty(InputValue) ? InputValue : DefaultValue;
			return true;
		}
	}
}