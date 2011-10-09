using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiEnumerator
{
  public static class Dot11SsidExtensions
  {
    public static string ToText(this NativeWifi.Wlan.Dot11Ssid ssid)
    {
      return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
    }
  }
}
