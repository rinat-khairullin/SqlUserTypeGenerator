using Microsoft.Build.Framework;

namespace SqlUserTypeGenerator.BuildTasks
{
    public class BuildDestinationFolderTask : ITask
    {
	    public IBuildEngine BuildEngine { get; set; }
	    public ITaskHost HostObject { get; set; }

		[Output]
		public string Path { get; set; }
		
	    public string Value { get; set; }
	    public string ProjectDir { get; set; }

		public bool Execute()
		{
			Path = System.IO.Path.Combine(ProjectDir, "GeneratedSqlTypes");
			if (!string.IsNullOrEmpty(Value))
			{
				var trimmed = Value.TrimEnd('\\');
				Path = System.IO.Path.GetFullPath(trimmed);
			}
			
			return true;
		}

	    
    }
}
