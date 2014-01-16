using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace ComputeDayTasks
{
  class ProgramOptions
  {
/// <summary>
/// TODO Comments
/// </summary>
    [Option('i', "input",
      Required = false, HelpText = "The input file to use")]
    public string InputFilePath
    {
      get;
      set;
    }

    [Option('o', "output",
      Required = false, HelpText = "the output file where the xml output will be written. If empty, the output will be written to s")]
    public string OutputFilePath
    {
      get;
      set;
    }

    [Option('h', "help",
      Required = false, HelpText = "Displays the Help")]
    public bool Help
    {
      get;
      set;
    }
  

/// <summary>
/// TODO Comments
/// </summary>
/// <returns></returns>
    [HelpOption(HelpText="Display this help screen")]
    public string GetUsage()
    {
      var help = new HelpText("ComputeDayTask Help");
      help.AdditionalNewLineAfterOption = true;
      help.Copyright = new CopyrightInfo("Haroon Khan", 2010, 2020);
      help.AddOptions(this);
      return help;
    }
  }
}
