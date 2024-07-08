using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BinanceRebateReport.Model
{
    [Serializable]
    public class NotifyModel : INotifyPropertyChanged, ICloneable
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}