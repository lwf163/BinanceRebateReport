namespace BinanceRebateReport.Model
{
    public class UidNameItem : NotifyModel
    {
        private string _uid;
        private string _name;

        public string Uid
        {
            get => _uid;
            set
            {
                if (value == _uid) return;
                _uid = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}