/*
MICROSOFT LIMITED PUBLIC LICENSE version 1.1

This license governs use of code marked as “sample” or “example” available on this web site without a license agreement, as provided under the section above titled “NOTICE SPECIFIC TO SOFTWARE AVAILABLE ON THIS WEB SITE.” If you use such code (the “software”), you accept this license. If you do not accept the license, do not use the software.

1. Definitions

The terms “reproduce,” “reproduction,” “derivative works,” and “distribution” have the same meaning here as under U.S. copyright law.

A “contribution” is the original software, or any additions or changes to the software.

A “contributor” is any person that distributes its contribution under this license.

“Licensed patents” are a contributor’s patent claims that read directly on its contribution.

2. Grant of Rights

(A) Copyright Grant - Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.

(B) Patent Grant - Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations

(A) No Trademark License- This license does not grant you rights to use any contributors’ name, logo, or trademarks.

(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.

(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.

(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.

(E) The software is licensed “as-is.” You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.

(F) Platform Limitation - The licenses granted in sections 2(A) and 2(B) extend only to the software or derivative works that you create that run directly on a Microsoft Windows operating system product, Microsoft run-time technology (such as the .NET Framework or Silverlight), or Microsoft application platform (such as Microsoft Office or Microsoft Dynamics).
*/

//-------------------------------------------------------------------------- 
//  
//  Copyright (c) Microsoft Corporation.  All rights reserved.  
//  
//  File: StaTaskScheduler.cs 
// 
//-------------------------------------------------------------------------- 

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace System.Threading.Tasks.Schedulers
{
  /// <summary>Provides a scheduler that uses STA threads.</summary> 
  public sealed class StaTaskScheduler : TaskScheduler, IDisposable
  {
    /// <summary>Stores the queued tasks to be executed by our pool of STA threads.</summary> 
    private BlockingCollection<Task> _tasks;
    /// <summary>The STA threads used by the scheduler.</summary> 
    private readonly List<Thread> _threads;

    /// <summary>Initializes a new instance of the StaTaskScheduler class with the specified concurrency level.</summary> 
    /// <param name="numberOfThreads">The number of threads that should be created and used by this scheduler.</param> 
    public StaTaskScheduler(int numberOfThreads)
    {
      // Validate arguments 
      if (numberOfThreads < 1) throw new ArgumentOutOfRangeException("numberOfThreads");

      // Initialize the tasks collection 
      _tasks = new BlockingCollection<Task>();

      // Create the threads to be used by this scheduler 
      _threads = Enumerable.Range(0, numberOfThreads).Select(i =>
      {
        var thread = new Thread(() =>
        {
          // Continually get the next task and try to execute it. 
          // This will continue until the scheduler is disposed and no more tasks remain. 
          foreach (var t in _tasks.GetConsumingEnumerable())
          {
            TryExecuteTask(t);
          }
        });
        thread.IsBackground = true;
        thread.SetApartmentState(ApartmentState.STA);
        return thread;
      }).ToList();

      // Start all of the threads 
      _threads.ForEach(t => t.Start());
    }

    /// <summary>Queues a Task to be executed by this scheduler.</summary> 
    /// <param name="task">The task to be executed.</param> 
    protected override void QueueTask(Task task)
    {
      // Push it into the blocking collection of tasks 
      _tasks.Add(task);
    }

    /// <summary>Provides a list of the scheduled tasks for the debugger to consume.</summary> 
    /// <returns>An enumerable of all tasks currently scheduled.</returns> 
    protected override IEnumerable<Task> GetScheduledTasks()
    {
      // Serialize the contents of the blocking collection of tasks for the debugger 
      return _tasks.ToArray();
    }

    /// <summary>Determines whether a Task may be inlined.</summary> 
    /// <param name="task">The task to be executed.</param> 
    /// <param name="taskWasPreviouslyQueued">Whether the task was previously queued.</param> 
    /// <returns>true if the task was successfully inlined; otherwise, false.</returns> 
    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
      // Try to inline if the current thread is STA 
      return
          Thread.CurrentThread.GetApartmentState() == ApartmentState.STA &&
          TryExecuteTask(task);
    }

    /// <summary>Gets the maximum concurrency level supported by this scheduler.</summary> 
    public override int MaximumConcurrencyLevel
    {
      get { return _threads.Count; }
    }

    /// <summary> 
    /// Cleans up the scheduler by indicating that no more tasks will be queued. 
    /// This method blocks until all threads successfully shutdown. 
    /// </summary> 
    public void Dispose()
    {
      if (_tasks != null)
      {
        // Indicate that no new tasks will be coming in 
        _tasks.CompleteAdding();

        // Wait for all threads to finish processing tasks 
        foreach (var thread in _threads) thread.Join();

        // Cleanup 
        _tasks.Dispose();
        _tasks = null;
      }
    }
  }
}