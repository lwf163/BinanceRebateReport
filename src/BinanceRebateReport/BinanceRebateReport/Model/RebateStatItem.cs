using System;
using CsvHelper.Configuration.Attributes;

namespace BinanceRebateReport.Model
{
    public class RebateStatItem : NotifyModel
    {
        private string _name;
        private string _uid;
        private decimal _spotBnb;
        private decimal _spotUsdt;
        private decimal _uFutureBnb;
        private decimal _uFutureUsdt;
        private int _count;
        private DateTime _start;
        private DateTime _end;

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

        [Name("Spot(BNB)")]
        public decimal SpotBnb
        {
            get => _spotBnb;
            set
            {
                if (value == _spotBnb) return;
                _spotBnb = value;
                OnPropertyChanged();
            }
        }

        [Name("Spot(USDT)")]
        public decimal SpotUsdt
        {
            get => _spotUsdt;
            set
            {
                if (value == _spotUsdt) return;
                _spotUsdt = value;
                OnPropertyChanged();
            }
        }

        [Name("USDT-Futures(BNB)")]
        public decimal UFutureBnb
        {
            get => _uFutureBnb;
            set
            {
                if (value == _uFutureBnb) return;
                _uFutureBnb = value;
                OnPropertyChanged();
            }
        }

        [Name("USDT-Futures(USDT)")]
        public decimal UFutureUsdt
        {
            get => _uFutureUsdt;
            set
            {
                if (value == _uFutureUsdt) return;
                _uFutureUsdt = value;
                OnPropertyChanged();
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                if (value == _count) return;
                _count = value;
                OnPropertyChanged();
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                if (value.Equals(_start)) return;
                _start = value;
                OnPropertyChanged();
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                if (value.Equals(_end)) return;
                _end = value;
                OnPropertyChanged();
            }
        }
    }
}