using System;
using Tyler.Courtroom.DomainPresentationFramework;
using Utils.ThreadHelpers;

namespace Utils.UIDependencyFactories
{
  public class OperationNodeFactory
  {
    /// <summary>
    /// <para>This will allow you to create a DpfOperationNode without using the BackgroundWorkerSyncContextHelper or creating your own STA based dispatcher.</para>
    /// <para>If you supply a non-null activityName, then this will create an activityNode based on the 3 parameter constructor (name,label,isBatchable).</para>
    /// <para>If you supply an activityID, then this will be set for you</para>
    /// </summary>
    /// <param name="activityName"></param>
    /// <param name="activityID"></param>
    /// <param name="label"></param>
    /// <param name="isBatchable"></param>
    /// <returns></returns>
    public static DpfOperationNode CreateDpfOperationNode()
    {
      DpfOperationNode node = null;

      STAHelper.RunSTACode
      (
        () => node = new DpfOperationNode()
      );

      return node;
    }

    public static DpfOperationNode CreateDpfOperationNodeFromAction(Func<DpfOperationNode> action)
    {
      DpfOperationNode node = null;

      STAHelper.RunSTACode
      (
        () => node = action()
      );

      return node;
    }
  }
}
