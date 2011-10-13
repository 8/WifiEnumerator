using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WifiEnumerator.ViewModel
{
  public class RefreshViewModel : BasePropertyChanged
  {
    #region Properties

    #region IsAutoRefreshEnabled
    public const string IsAutoRefreshEnabledPropertyName = "IsAutoRefreshEnabled";
    private bool _IsAutoRefreshEnabled;
    public bool IsAutoRefreshEnabled
    {
      get { return _IsAutoRefreshEnabled; }
      set
      {
        if (_IsAutoRefreshEnabled != value)
        {
          _IsAutoRefreshEnabled = value;
          OnPropertyChanged(IsAutoRefreshEnabledPropertyName);
        }
      }
    }
    #endregion

    #region RefreshInterval
    public const string RefreshIntervalPropertyName = "RefreshInterval";
    private int _RefreshInterval;
    public int RefreshInterval
    {
      get { return _RefreshInterval; }
      set
      {
        if (_RefreshInterval != value)
        {
          _RefreshInterval = value;
          OnPropertyChanged(RefreshIntervalPropertyName);
        }
      }
    }
    #endregion

    private SimplePeriodicTimer Timer;

    #endregion

    public RefreshViewModel(SSIDListViewModel ssidListViewModel)
    {
      var timer = new SimplePeriodicTimer();
      timer.Callback = () => ssidListViewModel.Refresh(); 
      this.Timer = timer;
      
      this.PropertyChanged += this_PropertyChanged;

      this.RefreshInterval      = 5000;      
      this.IsAutoRefreshEnabled = true;
    }

    private void this_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case RefreshIntervalPropertyName:
          this.Timer.Interval = this.RefreshInterval;
          break;
        case IsAutoRefreshEnabledPropertyName:
          this.Timer.IsEnabled = this.IsAutoRefreshEnabled;
          break;
      }
    }
  }
}
