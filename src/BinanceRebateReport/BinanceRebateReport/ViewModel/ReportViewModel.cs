using System;
using System.Collections.ObjectModel;
using BinanceRebateReport.Model;

namespace BinanceRebateReport.ViewModel
{
    public class ReportViewModel : NotifyViewModel
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private decimal _totalBnb;
        private decimal _totalUsdt;
        private int _totalCount;

        public ObservableCollection<RebateStatItem> StatItems { get; set; } =
            new ObservableCollection<RebateStatItem>();

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value.Equals(_startTime)) return;
                _startTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (value.Equals(_endTime)) return;
                _endTime = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalBnb
        {
            get => _totalBnb;
            set
            {
                if (value == _totalBnb) return;
                _totalBnb = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalUsdt
        {
            get => _totalUsdt;
            set
            {
                if (value == _totalUsdt) return;
                _totalUsdt = value;
                OnPropertyChanged();
            }
        }

        public int TotalCount
        {
            get => _totalCount;
            set
            {
                if (value == _totalCount) return;
                _totalCount = value;
                OnPropertyChanged();
            }
        }

        public ReportViewModel()
        {
            StartTime = DateTime.Now.Date;
            EndTime = DateTime.Now.Date;
        }


    }
}