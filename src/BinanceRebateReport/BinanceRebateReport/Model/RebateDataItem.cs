using System;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace BinanceRebateReport.Model
{
    [Serializable]
    public class RebateDataItem
    {
        [Index(0)]
        public string OrderType { get; set; }

        [Index(1)]
        public string Uid { get; set; }

        [Index(3)]
        public string CommissionAsset { get; set; }

        [Index(4)]
        [TypeConverter(typeof(DecimalEConverter))]
        public decimal CommissionEarned { get; set; }

        [Index(6)]
        public DateTime CommissionTime { get; set; }
    }

    public class DecimalEConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return Decimal.Parse(text, NumberStyles.Float);
        }
    }
}