# Crystal-Task
A class file is used to create tasks on a scheduled basis and execute tasks and commands at specific times (for example at a specific time)

<h3>instructions</h3>
First add the class file to your project

 ```csharp
    using CrystalSceduler;
 ```
 
 
 Create an instance of the CrystalTask class
  ```csharp
   CrystalTask taskMgr = new CrystalTask();
 ```
 
 
 Subscribe to required events
  ```csharp
taskMgr.ScedulerStart += Task_ScedulerStart;
taskMgr.ScedulerStop += Task_ScedulerStop;
taskMgr.TaskExecuted += Task_TaskExecuted;
taskMgr.TaskScedulated += Task_TaskScedulated;
 ```
 
<h2>Schedule a Task</h2>

  ```csharp
    var t = new CrystalSceduler.TaskFactory()
{
    ExcuteTime = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(5),
    TaskName = "Open notepad",
    Task = () => System.Diagnostics.Process.Start("notepad.exe")
    
};

taskMgr.AddTask(t);
 ```
 You can also leave the task blank
Because the result of the execution of the schedule is sent to the <strong>TaskExecuted</strong> event and from there it can also be sent to Tess


<h2>Other methods and events</h2>

| Name                         | Type   | Output            | Usage                                                                                                      |
|------------------------------|--------|-------------------|------------------------------------------------------------------------------------------------------------|
| Stop()                       | Method | void              | To end the scheduling process                                                                              |
| RemoveTask(TaskFactory task) | Method | void              | To Remove a Task from scheduling process                                                                   |
| Get Tasks()                  | Method | List<TaskFactory> | To get all the tasks created                                                                               |
| TaskExecuted                 | Event  |                   | When a task is executed, the executed task is passed to this event as a Factory task class as a parameter. |
| TaskScedulated               | Event  |                   | When a new task is defined                                                                                 |
| ScedulerStart                | Event  |                   | When the scheduling process starts                                                                         |
| ScedulerStop                 | Event  |                   | When the scheduling process is stopped                                                                     |
 
 
