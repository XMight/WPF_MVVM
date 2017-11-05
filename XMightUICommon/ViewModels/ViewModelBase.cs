using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMightUICommon
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T pStorage, T pValue, String pPropertyName = null)
        {
            bool result = false;
            if (!object.Equals(pStorage, pValue))
            {
                pStorage = pValue;
                this.OnPropertyChanged(pPropertyName);
                result = true;
            }

            return result;
        }

        protected void OnPropertyChanged(String pPropertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(pPropertyName));
            }
        }
    }
}
