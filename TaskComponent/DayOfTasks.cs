using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagement
{
  public class Task
  {
    public DateTime StartDate {get;set;}
    public TimeSpan TimeSpan {get;set;}
    public string Description {get;set;}
    public override string ToString()
    {
      return new StringBuilder(100)
             .Append(StartDate.ToString()).Append(",")
             .Append(TimeSpan.ToString()).Append(",")
             .Append(Description)
             .ToString()
             ;
    }
  }
  public class DayOfTasks
  {
    public DateTime DateTime { get; set; }
    public IEnumerable<Task> TaskList {get;set;}
    public override string ToString()
    {
      return null;
    }
  }
}
