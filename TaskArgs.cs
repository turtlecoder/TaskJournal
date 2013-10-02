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
    
    public String _date = String.Format("{0}-{1}-{2}", CommandLineArgs._now.Year, CommandLineArgs._now.Month, CommandLineArgs._now.Day);

    [Option('d', "date", Required = false, HelpText = "_date of the _task")]
    public String Date
    {
      get { return _date; }
      set { _date = value; }
    }

    /// <summary>
    /// TODO comments
    /// </summary>
    public String _time = String.Format("{0}:{1}:{2}", CommandLineArgs._now.Hour, CommandLineArgs._now.Minute, CommandLineArgs._now.Second);

    [Option
      ('t',
      "time",
      Required = false,
      HelpText = "_time of the task")]
    public String Time
    {
      get { return _time; }
      set { _time = value; }
    }

    /// <summary>
    /// TODO Comments
    /// </summary>
    public String[] _task = new string[] { "" };

    [OptionArray('k', "task",
      Required = false, HelpText = "Description of the task")]
    public String[] Task
    {
      get { return _task; }
      set { _task = value; }
    }

    /// <summary>
    /// TODO Comments
    /// </summary>
    public String _filePath;

    [Option('f', "filepath",
      Required = true, HelpText = "Complete path of the data store")]
    public String FilePath
    {
      get { return _filePath; }
      set { _filePath = value; }
    }

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
      return new StringBuilder(_date.Length + _time.Length + _filePath.Length + _task.Aggregate(0, (acc, s) => acc + s.Length))
               .Append(String.Format("( _date: {0}, ", this._date.ToString()))
               .Append(String.Format("_time: {0}, ", this._time))
               .Append(String.Format("_filePath: {0}, ", this._filePath))
               .Append(String.Format("_task: {0} )", this._task.Aggregate("", (acc, s) => acc + " " + s)))
               .ToString();
    }

    private static DateTime _now = DateTime.Now;

  }
}