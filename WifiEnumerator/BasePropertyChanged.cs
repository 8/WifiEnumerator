using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace WifiEnumerator
{
  [Serializable]
  public class BasePropertyChanged : SynchronizationContextObject, INotifyPropertyChanged
  {
    [field: NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged != null)
        propertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged<T>(Expression<Func<T>> property)
    {
      OnPropertyChanged((property.Body as MemberExpression).Member.Name);
    }
  }

}
