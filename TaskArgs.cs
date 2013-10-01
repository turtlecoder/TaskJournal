using CommandLine;
using System;
using CommandLine.Text;
using System.Text;
using System.Linq;
namespace TrackTask
{
/// <summary>
/// TODO comments
/// </summary>
  public sealed class CommandLineArgs
  {
/// <summary>
/// TODO Comments
/// </summary>
    [Option("d", "date", Required = false, HelpText = "Date of the Task")]
    public String Date = String.Format("{0}-{1}-{2}", CommandLineArgs._now.Year, CommandLineArgs._now.Month, CommandLineArgs._now.Day);

    /// <summary>
    /// TODO comments
    /// </summary>
    [Option
      ("t",
      "time",
      Required = false,
      HelpText = "Time of the task")]
    public String Time = String.Format("{0}:{1}:{2}", CommandLineArgs._now.Hour, CommandLineArgs._now.Minute, CommandLineArgs._now.Second);

    /// <summary>
    /// TODO Comments
    /// </summary>
    [OptionArray("k", "task",
      Required = false, HelpText = "Description of the task")]
    public String[] Task = new string[] { "" };

    /// <summary>
    /// TODO Comments
    /// </summary>
    [Option("f", "filepath",
      Required = true, HelpText = "Complete path of the data store")]
    public String FilePath;

    /// <summary>
    /// TODO Comments
    /// </summary>
    /// <returns></returns>
    [HelpOption(HelpText = "Display this help screen.")]
    public string GetUsage()
    {
      var help = new HelpText("TaskTrackerApp Help");
      help.AdditionalNewLineAfterOption = true;
      help.Copyright = new CopyrightInfo("Haroon Khan", 2010, 2011);
      help.AddOptions(this);
      return help;
    }
    /// <summary>
    /// TODO comments
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return new StringBuilder(Date.Length + Time.Length + FilePath.Length + Task.Aggregate(0, (acc, s) => acc + s.Length))
               .Append(String.Format("( Date: {0}, ", this.Date.ToString()))
               .Append(String.Format("Time: {0}, ", this.Time))
               .Append(String.Format("FilePath: {0}, ", this.FilePath))
               .Append(String.Format("Task: {0} )", this.Task.Aggregate("", (acc, s) => acc + " " + s)))
               .ToString();
    }

    private static DateTime _now = DateTime.Now;

  }
}