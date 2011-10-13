using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using NativeWifi;

namespace WifiEnumerator
{
  public class SSIDListViewModel
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

    public void Refresh()
    {
      this.SSIDViewModels.Clear();

      try
      {
        var client = new WlanClient();
        foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
        {
          Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
          foreach (Wlan.WlanAvailableNetwork network in networks)
          {
            this.SSIDViewModels.Add(new SSIDViewModel() { SSID = network.dot11Ssid.ToText(), SignalStrength = (int)network.wlanSignalQuality });
          }
        }
      }
      catch { }
    }

  }
}
