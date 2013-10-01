using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using System.IO;
using System.Diagnostics;
using CommandLine;

namespace TrackTask
{
  

  public class TaskDescription
  {
    public DateTime DateTime
    {
      get;
      private set;
    }
    public String FilePath
    {
      get;
      private set;
    }
    public String Description
    {
      get;
      private set;
    }
    public override string ToString()
    {
      return String.Format("{0},{1}", this.DateTime.ToString(), this.Description);
    }
    public static TaskDescription From(CommandLineArgs args)
    {
      //parse the date
      var datetext = args.Date;
      var timetext = args.Time;
      var datetimetext = new StringBuilder(datetext.Length + timetext.Length + 1)
                           .Append(datetext)
                           .Append(' ')
                           .Append(timetext);
      var taskDescription = new TaskDescription();

      taskDescription.DateTime = DateTime.Parse(datetimetext.ToString());
      taskDescription.Description = args.Task.Aggregate("", (acc, s) => new StringBuilder(acc).Append(" ").Append(s).ToString().Trim());
      taskDescription.FilePath = args.FilePath;

      return taskDescription;
    }

  }

  public class TaskFileInfo
  {
    public TaskFileInfo(CommandLineArgs args)
    {
      _fileInfo = new FileInfo(args.FilePath);
    }

    public bool Exists
    {
      get
      {
        return _fileInfo.Exists;
      }
    }

    public DirectoryInfo Directory { get { return this._fileInfo.Directory; } }

    public String DirectoryName
    {
      get
      {
        return this._fileInfo.DirectoryName;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this._fileInfo.IsReadOnly;
      }
      set
      {
        this._fileInfo.IsReadOnly = value;
      }
    }

    public long Length
    {
      get
      {
        return this._fileInfo.Length;
      }
    }

    public String Name
    {
      get
      {
        return _fileInfo.Name;
      }
    }

    public FileStream Open(FileMode fileMode, FileAccess fileAccess)
    {
      return _fileInfo.Open(fileMode, fileAccess);
    }

    private FileInfo _fileInfo;
  }

  class Program
  {
    static void Main(string[] args)
    {

      var taskArgs = new CommandLineArgs();
      var cmdLineParserSettings = new CommandLineParserSettings(Console.Error);
      cmdLineParserSettings.CaseSensitive = false;

      var parser = new CommandLineParser(new CommandLineParserSettings(Console.Error));
      if (!parser.ParseArguments(args, taskArgs))
      {
        Environment.Exit(1);
      }

      
      var taskDescription = TaskDescription.From(taskArgs);


      var taskFileInfo = new TaskFileInfo(taskArgs);
      // Open the file 
      FileMode theFileMode = default(FileMode);
      FileAccess theFileAccess = default(FileAccess);
      if (taskFileInfo.Exists)
      {
        // open a file stream
        theFileMode = FileMode.Append;
        theFileAccess = FileAccess.Write;
      }
      else
      {
        // create a file 
        theFileMode = FileMode.Create;
        theFileAccess = FileAccess.Write;
      }

      //Create a writer, using the stream

      using (FileStream fs = taskFileInfo.Open(theFileMode, theFileAccess))
      {
        // now create a text writer using a file stream object
        using (var textWriter = new StreamWriter(fs))
        {
          textWriter.WriteLine(taskDescription.ToString());
        }
        fs.Close();
      }

    }
  }
}
