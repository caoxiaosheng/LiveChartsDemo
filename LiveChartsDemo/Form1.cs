using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Brushes = System.Windows.Media.Brushes;

namespace LiveChartsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly Random random = new Random();

        private LineSeries BigLineSeries { get; set; }
        private LineSeries SmallLineSeries { get; set; }
        private Axis BigAxis { get; set; }
        private Axis SmallAxis { get; set; }
        private List<DateTime> DateTimes { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            myChart.Series.Configuration = Mappers.Xy<DateModel>()
                .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
                .Y(dayModel => dayModel.Value);
            DateTimes = CreateDateTime();
            //myChart.Series = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Values = new ChartValues<double>() {3,5,double.NaN, 4, 7},
            //        Stroke = Brushes.Red,
            //        Fill = Brushes.Transparent,
            //        StrokeThickness = 10
            //    },
            //    new ColumnSeries
            //    {
            //        Values = new ChartValues<decimal> {4, 3,  3, 1},
            //        Stroke = Brushes.Blue,
            //        Fill = Brushes.Chartreuse
            //    }
            //};
        }

        private List<DateModel> CreateBigPoint()
        {
            return CreatePoint(1000, 2000);
        }

        private List<DateModel> CreateSmallPoint()
        {
            return CreatePoint(1, 10);
        }

        private List<DateModel> CreatePoint(int minInt,int maxInt)
        {
            List<DateModel> dateModels = new List<DateModel>();
            foreach (var dateTime in DateTimes)
            {
                dateModels.Add(new DateModel()
                {
                    DateTime = dateTime,
                    Value = random.NextDouble() + random.Next(minInt, maxInt)
                });
            }
            return dateModels;
        }

        private  List<DateTime> CreateDateTime()
        {
            List<DateTime> dateTimes=new List<DateTime>();
            for (int i = 0; i < 500; i++)
            {
                dateTimes.Add(DateTime.Now.AddMonths(i).AddDays(random.Next(20)));
            }
            return dateTimes;
        }

        private void ckb_BigPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_BigPoint.Checked)
            {
                var chartValues = new ChartValues<DateModel>();
                chartValues.AddRange(CreateBigPoint());
                BigLineSeries = new LineSeries { Values = chartValues, ScalesYAt = 0 };
                myChart.Series.Add(BigLineSeries);
                BigAxis = new Axis() { Foreground = Brushes.Blue, Title = ckb_BigPoint.Text };
                myChart.AxisY.Add(BigAxis);
            }
            else
            {
                myChart.Series.Remove(BigLineSeries);
                myChart.AxisY.Remove(BigAxis);
            }
            myChart.AxisX.Clear();
            myChart.AxisX.Add(new Axis
            {
                LabelFormatter = value =>
                {
                    long tick = (long) (value * TimeSpan.FromHours(1).Ticks);
                    if (tick > DateTime.MinValue.Ticks && tick < DateTime.MaxValue.Ticks)
                    {
                        return new System.DateTime(tick).ToString("yyyy-MM-dd hh");
                    }
                    else
                    {
                        return DateTime.Now.ToString("yyyy-MM-dd hh");
                    }
                }
            });
            //myChart.Series.Add(new LineSeries
            //{
            //    Values = new ChartValues<double> { 1, 5, 3, 5, 3 },
            //    ScalesYAt = 0
            //});
            //myChart.Series.Add(new LineSeries
            //{
            //    Values = new ChartValues<double> { 20, 30, 70, 20, 10 },
            //    ScalesYAt = 1
            //});
            //myChart.Series.Add(new LineSeries
            //{
            //    Values = new ChartValues<double> { 600, 300, 200, 600, 800 },
            //    ScalesYAt = 2
            //});

            ////now we add the 3 axes

            //myChart.AxisY.Add(new Axis
            //{
            //    Foreground = System.Windows.Media.Brushes.DodgerBlue,
            //    Title = "Blue Axis"
            //});
            //myChart.AxisY.Add(new Axis
            //{
            //    Foreground = System.Windows.Media.Brushes.IndianRed,
            //    Title = "Red Axis",
            //    Position = AxisPosition.RightTop
            //});
            //myChart.AxisY.Add(new Axis
            //{
            //    Foreground = System.Windows.Media.Brushes.DarkOliveGreen,
            //    Title = "Green Axis",
            //    Position = AxisPosition.RightTop
            //});

        }

        private void ckb_SmallPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_BigPoint.Checked)
            {
                var chartValues = new ChartValues<DateModel>();
                chartValues.AddRange(CreateSmallPoint());
                SmallLineSeries = new LineSeries { Values = chartValues, ScalesYAt =1 };
                myChart.Series.Add(SmallLineSeries);
                SmallAxis= new Axis() { Foreground = Brushes.Black, Title = ckb_SmallPoint.Text };
                myChart.AxisY.Add(SmallAxis);
            }
            else
            {
                myChart.Series.Remove(SmallLineSeries);
                myChart.AxisY.Remove(SmallAxis);
            }
        }
    }

    public class DateModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
