using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiEnumerator
{
  /// <summary>
  /// Calls the supplied callback as long as the timers IsEnabled property is true.
  /// - Use IsEnabled to start or stop the Timer anytime
  /// - Use Interval to change the timing interval at anytime
  /// - Supports long running callbacks, because the timer starts only ticking when the callback returns, preventing multiple callback threads to be spawned.
  /// - if the timer is disabled and enabled or the interval is changed, the interval starts from the beginning
  /// </summary>
  public class SimplePeriodicTimer : IDisposable
  {
    #region Properties

    private System.Threading.Timer Timer;
    public Action Callback { get; set; }

    private object IsEnabledLock = new object();

    #region Interval
    /// <summary>Interval of the timer in ms</summary>
    private int _Interval = -1;
    public int Interval
    {
      get { return _Interval; }
      set
      {
        if (_Interval != value)
        {
          _Interval = value;
          UpdateTimer();
        }
      }
    }
    #endregion

    #region IsEnabled
    private bool _IsEnabled = false;
    public bool IsEnabled
    {
      get { return _IsEnabled; }
      set
      {
        if (_IsEnabled != value)
        {
          _IsEnabled = value;
          UpdateTimer();
        }
      }
    }
    #endregion

    #endregion

    public SimplePeriodicTimer()
      : base()
    {
      Timer = new System.Threading.Timer(TimerCallback);
    }

    #region Methods

    private void TimerCallback(object state)
    {
      Action callback = Callback;
      if (callback != null)
        callback();

      UpdateTimer();
    }

    private void UpdateTimer()
    {
      // Lock guarantees that IsEnabled and Intervall are fresh
      lock (IsEnabledLock)
      {
        Timer.Change(this.IsEnabled ? this.Interval : System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
      }
    }

    #region IDisposable Members

    public void Dispose()
    {
      var timer = this.Timer;
      if (timer != null)
      {
        timer.Dispose();
        this.Timer = null;
      }
    }

    #endregion

    #endregion
  }

}
