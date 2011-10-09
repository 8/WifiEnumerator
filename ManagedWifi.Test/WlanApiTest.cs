using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NativeWifi;

namespace ManagedWifi.Test
{
  [TestClass]
  public class WlanApiTest
  {
    [TestMethod]
    public void WlanApiTest_Ctor()
    {
      var client = new WlanClient();
      var interfaces = client.Interfaces;
    }

    [TestMethod]
    public void WlanApiTest_WifiStrength()
    {
      WlanClient client = new WlanClient();
      foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
      {
        Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
        foreach (Wlan.WlanAvailableNetwork network in networks)
        {          
          System.Diagnostics.Debug.WriteLine("Found network with SSID {0} and Siqnal Quality {1}.", Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, (int)network.dot11Ssid.SSIDLength), network.wlanSignalQuality);
        }
      }

    }
  }
}
