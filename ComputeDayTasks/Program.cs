using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using System.IO;
using System.Diagnostics;
using TaskManagement;
using System.Xml.Linq;
using System.Xml;

namespace ComputeDayTasks
{
  class Program
  {
    /// <summary>
    /// main entry point
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
      // create the program options object
      var programOptions = new ProgramOptions();
      //var cmdLineArgs = new CommandLineParserSettings(Console.Error);
      //var parser = new CommandLineParser(new CommandLineParserSettings(Console.Error));
      var parser = new CommandLine.Parser(with => with.HelpWriter = Console.Error);
      if (!parser.ParseArguments(args, programOptions))
      {
        Environment.Exit(1);
      }

      if (programOptions.Help)
      {
        Console.Write(programOptions.GetUsage());
        Environment.Exit(0);
      }

      // if the program options. file name is null then read from standard input
      Stream inputStream;
      FileInfo fileInfo;

      if (null == programOptions.InputFilePath)
      {
        inputStream = Console.OpenStandardInput();

      }
      // else if program options file name is not null then open the file and create a stream
      else
      {
        fileInfo = new FileInfo(programOptions.InputFilePath);
        inputStream = fileInfo.Open(FileMode.Open, FileAccess.Read);
      }



      // Now open a text reader for the input stream
      StreamReader reader = new StreamReader(inputStream);
      List<String> stringList = new List<string>();

      while (!reader.EndOfStream)
      {
        string s = reader.ReadLine();
        stringList.Add(s);
      }

      reader.Close();

      /*
      var taskTextList = from line in
                           from line in stringList select line.Split(',')
                         select line;
      */
      var taskTextList = from line in stringList where line.Length!=0
                         select line.Split(',');
                           
                           

      var taskList = from taskText in taskTextList
                     select new { StartDate = DateTime.Parse(taskText[0]), Descrption = taskText[1] };

      var groupedTaskList = from task in
                              from sortedTask in taskList
                              orderby sortedTask.StartDate
                              select sortedTask
                            group task by task.StartDate.Date
                              into dateAndTaskList
                              select dateAndTaskList;


      var dayTaskList = from tuple in groupedTaskList
                        select new { StartDate = tuple.Key, TaskList = tuple.AsEnumerable() };


      var computedDayTaskList = from item in dayTaskList
                                select new
                                {
                                  StartDate = item.StartDate,
                                  TaskList2 = item.TaskList.Skip(1),
                                  TaskList1 = item.TaskList.Take(item.TaskList.Count() - 1)
                                };


      var computedDayTaskList2 =
        (from item in computedDayTaskList
         select new
         {
           StartDate = item.StartDate,
           TaskList = from indexedTask in
                        item.TaskList1.Select((task, i) => new { Task = task, Index = i })
                      select new
                      {
                        Description = indexedTask.Task.Descrption,
                        StartDate = indexedTask.Task.StartDate,
                        Duration = item.TaskList2.ElementAt(indexedTask.Index).StartDate.Subtract(indexedTask.Task.StartDate)
                      }
         }).Select((daytask) => new
         {
           StartDate = daytask.StartDate,
           TaskList = daytask.TaskList,
           TotalDuration = daytask.TaskList.Aggregate(default(TimeSpan),
(acc, task) => acc + task.Duration)
         });




      // now serialize into xml data
      XDocument doc = new XDocument(
        new XDeclaration("1.0", "utf-8", "yes"),
        new XComment(""),
        new XElement("DayTaskLog",
          (from computedDayTask in computedDayTaskList2
           select new XElement("DayTask",
             new XElement("Date", computedDayTask.StartDate.ToString()),
             new XElement("Duration", computedDayTask.TotalDuration.ToString()),
             new XElement("TaskList",
               (from task in computedDayTask.TaskList
                select new XElement("Task",
                  new XAttribute("StartDate", task.StartDate.ToString()),
                  new XAttribute("Duration", task.Duration.ToString()),
                  new XAttribute("Description", task.Description))))))));

      // Now write it to an output stream either file or standard output
      Stream outputStream = null;
      if (null == programOptions.OutputFilePath)
      {
        // Open the standard output stream
        outputStream = Console.OpenStandardOutput();
      }
      else
      {
        FileInfo fi = new FileInfo(programOptions.OutputFilePath);
        outputStream = fi.Open(FileMode.Create, FileAccess.Write);
      }

      // write the data to the output stream
      StreamWriter writer = new StreamWriter(outputStream);
      writer.Write(doc.ToString());
      writer.Close();
    }
  }
}
