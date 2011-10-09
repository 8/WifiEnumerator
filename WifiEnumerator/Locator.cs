using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiEnumerator
{
  public class Locator
  {
    public SSIDListViewModel SSIDListViewModel { get; private set; }

    public Locator()
    {
      this.SSIDListViewModel = new SSIDListViewModel();
    }
  }
}
