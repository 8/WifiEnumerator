using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WifiEnumerator
{
  [Serializable]
  public class SynchronizationContextObject
  {
    [NonSerialized]
    private SynchronizationContext SynchronizationContext;

    public SynchronizationContextObject()
    {
      this.SynchronizationContext = SynchronizationContext.Current;
    }

    #region Methods

    public void Post(Action action)
    {
      if (SynchronizationContext != null)
        SynchronizationContext.Post(o => action(), null);
      else
        action();
    }

    public void Send(Action action)
    {
      if (SynchronizationContext != null)
        SynchronizationContext.Send(o => action(), null);
      else
        action();
    }

    #endregion

  }
}
