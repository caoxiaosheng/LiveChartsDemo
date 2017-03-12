using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Brushes = System.Windows.Media.Brushes;

namespace LiveChartsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //先定义好默认X、Y轴样式,避免多Y轴时问题
            myChart.AxisY.Add(new Axis()
            {
                Foreground = Brushes.Black,
                Title = ckb_BigPoint.Text,
                Position = AxisPosition.LeftBottom
            });
            myChart.AxisX.Add(new Axis
            {
                LabelFormatter = value =>
                {
                    long tick = (long)(value * TimeSpan.FromHours(1).Ticks);
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
        }

        private readonly Random random = new Random();

        private LineSeries BigLineSeries { get; set; }
        private LineSeries SmallLineSeries { get; set; }
        private Axis SmallAxis { get; set; }
        private List<DateTime> DateTimes { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var dayConfig = Mappers.Xy<DateModel>()
            //    .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
            //    .Y(dayModel => dayModel.Value);
            //myChart.Series = new SeriesCollection(dayConfig);
            DateTimes = CreateDateTime();
            
        }

        private List<DateTimePoint> CreateBigPoint()
        {
            return CreatePoint(1000, 2000);
        }

        private List<DateTimePoint> CreateSmallPoint()
        {
            return CreatePoint(1, 10);
        }

        private List<DateTimePoint> CreatePoint(int minInt,int maxInt)
        {
            List<DateTimePoint> dateModels = new List<DateTimePoint>();
            foreach (var dateTime in DateTimes)
            {
                dateModels.Add(new DateTimePoint()
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
            myChart.Refresh();
            if (ckb_BigPoint.Checked)
            {
                var chartValues = new ChartValues<DateTimePoint>();
                chartValues.AddRange(CreateBigPoint());
                BigLineSeries = new LineSeries { Values = chartValues, ScalesYAt = 0 };
                myChart.Series.Add(BigLineSeries);
            }
            else
            {
                myChart.Series.Remove(BigLineSeries);
            }
        }

        private void ckb_SmallPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_SmallPoint.Checked)
            {
                var chartValues = new ChartValues<DateTimePoint>();
                chartValues.AddRange(CreateSmallPoint());
                SmallLineSeries = new LineSeries { Values = chartValues, ScalesYAt =1 };
                myChart.Series.Add(SmallLineSeries);
                SmallAxis= new Axis() { Foreground = Brushes.BlueViolet, Title = ckb_SmallPoint.Text,Position = AxisPosition.RightTop};
                myChart.AxisY.Add(SmallAxis);
            }
            else
            {
                myChart.Series.Remove(SmallLineSeries);
                myChart.AxisY.Remove(SmallAxis);
            }
        }

        #region 导出图片
        private void btn_OutPutPng_Click(object sender, EventArgs e)
        {
            OutPutPng("mychart.png");
        }

        private void OutPutPng(string fileName)
        {
            using (Bitmap bitmap = new Bitmap(myChart.Width, myChart.Height))
            {
                myChart.DrawToBitmap(bitmap, new Rectangle(0, 0, myChart.Width, myChart.Height));
                bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        } 
        #endregion
    }

    //public class DateModel
    //{
    //    public DateTime DateTime { get; set; }
    //    public double Value { get; set; }
    //}
}
