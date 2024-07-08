using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BinanceRebateReport.ViewModel
{
    public class NotifyViewModel : INotifyPropertyChanged
    {
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "none passed")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChangedAll()
        {
            PropertyInfo[] props = this.GetType().GetProperties();
            if (props.Length > 0)
            {
                foreach (var p in props)
                {
                    OnPropertyChanged(p.Name);
                }
            }
        }
    }
}
