using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace Canban.View.Windows
{
    /// <summary>
    /// Interakční logika pro GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<double> Dates { get; set; }  // Store dates as numeric OLE automation dates
        public Func<double, string> DateFormatter { get; set; }  // Formatter to display real dates on the axis
        public double MinDate { get; set; }  // Minimum date for the X-axis
        public double MaxDate { get; set; }  // Maximum date for the X-axis

        public GraphWindow()
        {
            InitializeComponent();
            var dateTimes = new List<DateTime>
        {
            new DateTime(2023, 5, 1),
            new DateTime(2023, 5, 2),
            new DateTime(2023, 5, 3),
            new DateTime(2023, 5, 4),
            new DateTime(2023, 5, 5)
        };

            // Convert DateTime to OLE Automation dates (double)
            Dates = new List<double>();
            foreach (var date in dateTimes)
            {
                Dates.Add(date.ToOADate());
            }

            // Sample cycle times for the corresponding dates
            var cycleTimes = new ChartValues<int> { 2, 4, 3, 5, 6 };

            SeriesCollection = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Cycle Time",
                Values = cycleTimes
            }
        };

            // Formatter to display the dates in a readable format (e.g., "MMM dd")
            DateFormatter = Dates => DateTime.FromOADate(Dates).ToString("MMM dd");
            DataContext = this;
            Graph.Series = SeriesCollection;
            XAxis.LabelFormatter = DateFormatter;
            
            
        }

    }
}