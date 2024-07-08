using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BinanceRebateReport.Model;
using BinanceRebateReport.ViewModel;
using CsvHelper;
using Serilog;

namespace BinanceRebateReport.View
{
    /// <summary>
    /// WndReport.xaml 的交互逻辑
    /// </summary>
    public partial class WndReport
    {
        private ReportViewModel _context;
        private Dictionary<string, UidNameItem> _uidNames = new Dictionary<string, UidNameItem>();

        public WndReport()
        {
            InitializeComponent();
            _context = new ReportViewModel();
            DataContext = _context;
        }

        private void ReportView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_context.StartTime == null || _context.EndTime == null || _context.StartTime >= _context.EndTime)
                {
                    MessageBox.Show("Please input correct start time and end time first.");
                    return;
                }

                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".csv";
                dlg.Filter = "CSV Files (*.csv)|*.csv";
                if (dlg.ShowDialog() == true)
                {
                    if (File.Exists(dlg.FileName) == false)
                    {
                        MessageBox.Show("Csv file is not exist.");
                        return;
                    }

                    int count = 0;
                    List<RebateDataItem> dataItems = new List<RebateDataItem>();
                    //读取csv文件
                    using (var reader = new StreamReader(dlg.FileName))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read())
                        {
                            var record = csv.GetRecord<RebateDataItem>();
                            count++;
                            if (record.CommissionTime < _context.StartTime || record.CommissionTime >= _context.EndTime)
                            {
                                continue;
                            }

                            dataItems.Add(record);
                        }
                    }

                    if (dataItems.Count > 0)
                    {
                        //统计数据并根据UID合并统计到StatItems
                        _context.StatItems.Clear();
                        _context.TotalBnb = 0;
                        _context.TotalUsdt = 0;
                        foreach (var dataItem in dataItems)
                        {
                            if (_uidNames.ContainsKey(dataItem.Uid) == false)
                            {
                                _uidNames[dataItem.Uid] = new UidNameItem() { Uid = dataItem.Uid };
                            }

                            var uidName = _uidNames[dataItem.Uid];
                            var statItem = _context.StatItems.FirstOrDefault(x => x.Uid == dataItem.Uid);
                            if (statItem == null)
                            {
                                statItem = new RebateStatItem()
                                {
                                    Uid = dataItem.Uid,
                                    Name = uidName.Name,
                                    Start = _context.StartTime,
                                    End = _context.EndTime
                                };
                                _context.StatItems.Add(statItem);
                            }

                            if (dataItem.OrderType.Contains("spot"))
                            {
                                statItem.Count++;
                                _context.TotalCount++;
                                if (dataItem.CommissionAsset == "BNB")
                                {
                                    statItem.SpotBnb += dataItem.CommissionEarned;
                                    _context.TotalBnb += dataItem.CommissionEarned;
                                }
                                else if (dataItem.CommissionAsset == "USDT")
                                {
                                    statItem.SpotUsdt += dataItem.CommissionEarned;
                                    _context.TotalUsdt += dataItem.CommissionEarned;
                                }
                            }
                            else if (dataItem.OrderType.Contains("USDT-futures"))
                            {
                                statItem.Count++;
                                _context.TotalCount++;
                                if (dataItem.CommissionAsset == "BNB")
                                {
                                    statItem.UFutureBnb += dataItem.CommissionEarned;
                                    _context.TotalBnb += dataItem.CommissionEarned;
                                }
                                else if (dataItem.CommissionAsset == "USDT")
                                {
                                    statItem.UFutureUsdt += dataItem.CommissionEarned;
                                    _context.TotalUsdt += dataItem.CommissionEarned;
                                }
                            }
                        }
                    }

                    MessageBox.Show($"File has {count} records. Import {dataItems.Count} records.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Import Error");
                MessageBox.Show($"Import Error:{ex.Message}");
            }
            
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //导出统计数据到csv文件
            try
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.DefaultExt = ".csv";
                dlg.Filter = "CSV Files (*.csv)|*.csv";
                if (dlg.ShowDialog() == true)
                {
                    using (var writer = new StreamWriter(dlg.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(_context.StatItems);
                    }
                    MessageBox.Show("Export Success.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Export Error");
                MessageBox.Show($"Export Error:{ex.Message}");
            }
        }

        
    }
}
