using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Brushes = System.Windows.Media.Brushes;

namespace LiveChartsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DateTimes = CreateDateTime();
            myChart.DisableAnimations = true;
            myChart.Hoverable = false;
            //开启缩放
            myChart.Zoom=ZoomingOptions.X;
        }

        private readonly Random random = new Random();

        private LineSeries BigLineSeries { get; set; }
        private LineSeries SmallLineSeries { get; set; }
        private LineSeries NormalLineSeries { get; set; }
        private Axis SmallAxis { get; set; }
        private List<DateTime> DateTimes { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var dayConfig = Mappers.Xy<DateModel>()
            //    .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
            //    .Y(dayModel => dayModel.Value);
            //myChart.Series = new SeriesCollection(dayConfig);
            //btn_Refresh_Click(null,null);
        }


        private List<DateTimePoint> CreateNormalPoint()
        {
            var a=CreatePoint(500, 600);
            a[480].Value = double.NaN;
            a[481].Value = double.NaN;
            a[483].Value = double.NaN;
            return a;
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
            if (ckb_BigPoint.Checked)
            {
                var chartValues = new ChartValues<DateTimePoint>();
                chartValues.AddRange(CreateBigPoint());
                BigLineSeries = new LineSeries {
                    Values = new ChartValues<DateTimePoint>(CreateBigPoint()),
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 4,
                    DataLabels = false,
                    Stroke = Brushes.Blue,
                    Title = "大数据"
                };
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
                SmallLineSeries = new LineSeries
                {
                    Values = new ChartValues<DateTimePoint>(CreateSmallPoint()),
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 4,
                    DataLabels = false,
                    Stroke = Brushes.Red,
                    Title = "小数据"
                };
                myChart.Series.Add(SmallLineSeries);
            }
            else
            {
                myChart.Series.Remove(SmallLineSeries);
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

        #region 取消缩放
        private void btn_CancelZoom_Click(object sender, EventArgs e)
        {
            myChart.AxisX[0].MinValue = double.NaN;
            myChart.AxisX[0].MaxValue = double.NaN;
        }
        #endregion

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            //var cv = new ChartValues<DateTimePoint>();
            //cv.AddRange(CreateNormalPoint());

            //NormalLineSeries = new LineSeries
            //{
            //    Values = cv,
            //    StrokeThickness = 2,
            //    Fill = Brushes.Transparent,
            //    PointGeometry = null,
            //    DataLabels = false,
            //    Stroke = Brushes.Black,
            //    Title = "基础数据"
            //};

            //var a = new StepLineSeries()
            //{
            //    Values = cv,
            //    StrokeThickness = 2,
            //    Fill = Brushes.Transparent,
            //    PointGeometry = null,
            //    DataLabels = false,
            //    Stroke = Brushes.Black,
            //    Title = "基础数据"
            //};
            //myChart.Series = new SeriesCollection { a };
            MyChartViewModel viewModel = new MyChartViewModel();
            myChart.Series = viewModel.Series;

            //var axisY = myChart.AxisY.FirstOrDefault();
            //if (axisY != null)
            //{
            //    axisY.Foreground = Brushes.Black;
            //    axisY.Title = "基础数据";
            //    axisY.Position=AxisPosition.LeftBottom;
            //}
            myChart.AxisY.Clear();
            myChart.AxisY.Add(new Axis
            {
                Foreground = Brushes.Black,
                Title = "基础数据",
                Position = AxisPosition.LeftBottom
            });
            myChart.AxisX.Clear();
            myChart.AxisX.Add(new Axis
            {
                Foreground = Brushes.Black,
                LabelFormatter = val =>
                {
                    //必须判断是否在正常日期范围内，否则报错
                    if (val < DateTime.MinValue.Ticks)
                    {
                        return DateTime.MinValue.ToString("yyyy/MM/dd hh:mm");
                    }
                    else if (val > DateTime.MaxValue.Ticks)
                    {
                        return DateTime.MaxValue.ToString("yyyy/MM/dd hh:mm");
                    }
                    else
                    {
                        return new DateTime((long)val).ToString("yyyy/MM/dd hh:mm");
                    }
                }
            });
        }

        private void cbx_MultiAxis_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_MultiAxis.Checked)
            {
                for (int i = 1; i < myChart.Series.Count; i++)
                {
                    LineSeries lineSeries= (LineSeries)myChart.Series[i];
                    myChart.AxisY.Add(new Axis(){Title = lineSeries.Title,Foreground = lineSeries.Stroke,Position = AxisPosition.RightTop});
                    lineSeries.ScalesYAt = i;
                }
            }
            else
            {
                //必须先重置series，否则删除axis报错
                for (int i = 1; i < myChart.Series.Count; i++)
                {
                    LineSeries lineSeries = (LineSeries)myChart.Series[i];
                    lineSeries.ScalesYAt = 0;
                }
                int count = myChart.AxisY.Count;
                for (int i = count - 1; i > 0; i--)
                {
                    myChart.AxisY.RemoveAt(i);
                }
            }
        }
    }

    public class MyChartViewModel
    {
        public MyChartViewModel()
        {
            //Series = new SeriesCollection();
            //Random random=new Random();
            //List<DateTime> dateTimes = new List<DateTime>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    dateTimes.Add(DateTime.Now.AddMonths(i).AddDays(random.Next(20)));
            //}
            //var points=new DateTimePoint[500];
            //for (int i = 0; i < 500; i++)
            //{
            //    points[i]=new DateTimePoint(dateTimes[i],random.Next(0, 10));
            //}
            //var cv = new ChartValues<DateTimePoint>();
            //cv.AddRange(points);
            //var series = new LineSeries
            //{
            //    Values = cv,
            //    StrokeThickness = 2,
            //    Fill = Brushes.Transparent,
            //    PointGeometry = null,
            //    DataLabels = false,
            //    Stroke = Brushes.Black,
            //    Title = "基础数据"
            //};
            //Series.Add(series);
            //for (int i = 0; i < 500; i++)
            //{
            //    points[i] = new DateTimePoint(dateTimes[i], random.Next(0, 10));
            //}
            //var cv2 = new ChartValues<DateTimePoint>();
            //cv2.AddRange(points);
            //var series2 = new LineSeries
            //{
            //    Values = cv2,
            //    StrokeThickness = 2,
            //    Fill = Brushes.Transparent,
            //    PointGeometry = null,
            //    DataLabels = false,
            //    Stroke = Brushes.Red,
            //    Title = "基础数据"
            //};
            //Series.Add(series2);
            //for (int i = 0; i < 500; i++)
            //{
            //    points[i] = new DateTimePoint(dateTimes[i], random.Next(0, 10));
            //}
            //var cv3 = new ChartValues<DateTimePoint>();
            //cv3.AddRange(points);
            //var series3 = new LineSeries
            //{
            //    Values = cv3,
            //    StrokeThickness = 2,
            //    Fill = Brushes.Transparent,
            //    PointGeometry = null,
            //    DataLabels = false,
            //    Stroke = Brushes.Aqua,
            //    Title = "基础数据"
            //};
            //Series.Add(series3);

            Series = new SeriesCollection();
            Random random = new Random();
            List<DateTime> dateTimes = new List<DateTime>();
            for (int i = 0; i < 100000; i++)
            {
                dateTimes.Add(DateTime.Now.AddDays(i).AddMinutes(random.Next(10)));
            }
            var points = new DateTimePoint[100000];
            for (int i = 0; i < 100000; i++)
            {
                points[i] = new DateTimePoint(dateTimes[i], random.Next(0, 10));
            }
            var cv = new GearedValues<DateTimePoint>();
            cv.AddRange(points);
            var series = new GLineSeries
            {
                Values = cv,
                StrokeThickness = 2,
                Fill = Brushes.Transparent,
                PointGeometry = null,
                DataLabels = false,
                Stroke = Brushes.Black,
                Title = "基础数据"
            };
            Series.Add(series);
            for (int i = 0; i < 100000; i++)
            {
                points[i] = new DateTimePoint(dateTimes[i], random.Next(20, 40));
            }
            var cv2 = new GearedValues<DateTimePoint>();
            cv2.AddRange(points);
            var series2 = new GLineSeries
            {
                Values = cv2,
                StrokeThickness = 2,
                Fill = Brushes.Transparent,
                PointGeometry = null,
                DataLabels = false,
                Stroke = Brushes.Red,
                Title = "基础数据"
            };
            Series.Add(series2);
            for (int i = 0; i < 100000; i++)
            {
                points[i] = new DateTimePoint(dateTimes[i], random.Next(10, 100));
            }
            var cv3 = new GearedValues<DateTimePoint>();
            cv3.AddRange(points);
            var series3 = new GLineSeries
            {
                Values = cv3,
                StrokeThickness = 2,
                Fill = Brushes.Transparent,
                PointGeometry = null,
                DataLabels = false,
                Stroke = Brushes.Aqua,
                Title = "基础数据"
            };
            Series.Add(series3);
        }

        public SeriesCollection Series { get;}
    }

    //public class DateModel
    //{
    //    public DateTime DateTime { get; set; }
    //    public double Value { get; set; }
    //}
}
