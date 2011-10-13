using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WifiEnumerator.ViewModel;

namespace WifiEnumerator
{
  public class Locator
  {
    public SSIDListViewModel SSIDListViewModel { get; private set; }
    public RefreshViewModel RefreshViewModel { get; private set; }

    public Locator()
    {
      var ssidListViewModel = new SSIDListViewModel();
      this.SSIDListViewModel = ssidListViewModel;
      this.RefreshViewModel  = new RefreshViewModel(ssidListViewModel);
    }
  }
}
