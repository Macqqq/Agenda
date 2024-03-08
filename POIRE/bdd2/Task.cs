using System;
using System.Collections.Generic;

namespace POIRE.MusicDB;

public partial class Task
{
    public int IdTask { get; set; }

    public string TaskContent { get; set; } = null!;

    public DateTime? CreationDate { get; set; }

    public bool? IsCompleted { get; set; }

    public int TaskListIdTaskList { get; set; }

    public virtual TaskList TaskListIdTaskListNavigation { get; set; } = null!;
}
