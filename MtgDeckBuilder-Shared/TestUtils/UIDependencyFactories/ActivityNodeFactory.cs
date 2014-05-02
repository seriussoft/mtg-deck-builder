using Tyler.Courtroom.Domain.Core;
using Utils.ThreadHelpers;

namespace Utils.UIDependencyFactories
{
  public class ActivityNodeFactory
  {
    /// <summary>
    /// <para>This will allow you to create a DpfActivityNode without using the BackgroundWorkerSyncContextHelper or creating your own STA based dispatcher.</para>
    /// <para>If you supply a non-null activityName, then this will create an activityNode based on the 3 parameter constructor (name,label,isBatchable).</para>
    /// <para>If you supply an activityID, then this will be set for you</para>
    /// </summary>
    /// <param name="activityName"></param>
    /// <param name="activityID"></param>
    /// <param name="label"></param>
    /// <param name="isBatchable"></param>
    /// <returns></returns>
    public static DpfActivityNode CreateDpfActivityNode(string activityName = null, string activityID = null, string label = null, bool isBatchable=false)
    {
      
      DpfActivityNode node = null;

      STAHelper.RunSTACode
        (
          () =>
            {
              node = activityName != null
                        ? new DpfActivityNode(activityName, label, isBatchable) { Id = activityID }
                        : new DpfActivityNode();

              if (activityID != null)
              {
                node.Id = activityID;
              }
            }
        );

      return node;
      
    }
  }
}
