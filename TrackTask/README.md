# Simple Task logging tool for the command line #

----------  

This is a simple task/time logging tool that I wrote so that I can track my time spent at work. It uses a simple text file to store data and appends log entries to the end of the file. 

Example contents of the file.
  
	...
	10/26/2010 3:00:00 PM,project meeting client team
	10/26/2010 4:00:00 PM,environment setup and debugging
	10/26/2010 5:35:00 PM,
	10/27/2010 8:15:00 AM,administrative email and time sheet
	10/27/2010 8:25:00 AM,environment setup and debugging
	...
	
The tool does not specify any particular protocol for entering log items.
It simply records them. Its up to other tools on how they want to process data.  

Command line parameters for the tool

	TaskTrackerApp Help

	-d, --date        date of the task (defaults to the current date)
	-t, --time        time of the task (defaults to the time right now)
    -k, --task        Description of the task (defaults to empty string)
 	-f, --filepath    Required. Complete path of the data store
    --help            Display this help screen.

Example usage

	tracktask -f task.log -k Task 1 did foo 
A `task.log` file will be created with the following entry
	
	10/26/2010 3:00:00 PM,Task 1 did foo

If no date or time is specified, it will default the current date and time. 

Example usage with date and time,

	tracktask -f task.log -k task 2: did bar -t 15:00 -d 10/25/2013
	
## Design ##
tracktask depends on the rather excellent commnand-line parsing library from  [gsscoder/commandline](https://github.com/gsscoder/commandline "commandline"). I am using the stable-1.9.71.2 branch as a linked dependency in Git

To get the code in the references folder, please do the following 

	git submodule init
	git submodule update
 

 

