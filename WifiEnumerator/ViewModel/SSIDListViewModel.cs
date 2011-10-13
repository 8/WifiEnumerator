using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using NativeWifi;

namespace WifiEnumerator
{
  public class SSIDListViewModel : SynchronizationContextObject
  {
    public ObservableCollection<SSIDViewModel> SSIDViewModels { get; private set; }

    #region RefreshCommand
    private ICommand _RefreshCommand;
    public ICommand RefreshCommand
    {
      get
      {
        if (_RefreshCommand == null)
          _RefreshCommand = new RelayCommand(call => Refresh());
        return _RefreshCommand;
      }
    }
    #endregion
    
    public SSIDListViewModel( )
    {
      this.SSIDViewModels = new ObservableCollection<SSIDViewModel>();

      this.Refresh();
    }

    #region Methods

    private IEnumerable<SSIDViewModel> GetVMs()
    {
      var vms = new List<SSIDViewModel>();
      try
      {
        var client = new WlanClient();
        foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
        {
          Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
          foreach (Wlan.WlanAvailableNetwork network in networks)
            vms.Add(new SSIDViewModel() { SSID = network.dot11Ssid.ToText(), SignalStrength = (int)network.wlanSignalQuality });
        }
      }
      catch { }
      return vms;
    }

    public void Refresh()
    {
      var vms = GetVMs();

      this.Post(() =>
        {
          this.SSIDViewModels.Clear();
          foreach (var vm in vms)
            this.SSIDViewModels.Add(vm);
        });
    }

    #endregion
  }
}
