using System;
using System.Collections.Generic;

namespace POIRE.MusicDB;

public partial class TaskList
{
    public int IdTaskList { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
