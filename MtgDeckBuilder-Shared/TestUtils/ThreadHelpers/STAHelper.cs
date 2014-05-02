using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;

namespace Utils.ThreadHelpers
{
  public static class STAHelper
  {
    private static readonly StaTaskScheduler taskScheduler = new StaTaskScheduler(1);

    public static void RunSTACode(this Action staDependantAction)
    {
      var newTask = new Task(staDependantAction);
      newTask.Start(taskScheduler);
      newTask.Wait();

      if (newTask.IsFaulted && newTask.Exception != null) throw newTask.Exception.Flatten();
    }
  }
}
