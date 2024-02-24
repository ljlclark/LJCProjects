using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFilesDAL
{
  public class ProjectFile
  {
    /// <summary>Gets or sets the Target Solution name.</summary>
    public string TargetSolution { get; set; }

    /// <summary>Gets or sets the Target Project name.</summary>
    public string TargetProject { get; set; }

    /// <summary>Gets or sets the Source file name.</summary>
    public string SourceFileName { get; set; }

    /// <summary>Gets or sets the Source Project name.</summary>
    public string SourceProject { get; set; }

    /// <summary>Gets or sets the Source file spec.</summary>
    public string SourceFileSpec { get; set; }

    /// <summary>Gets or sets the Target file spec.</summary>
    public string TargetFileSpec { get; set; }
  }
}
