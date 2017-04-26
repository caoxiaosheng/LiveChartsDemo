using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LiveCharts.Defaults;

namespace LiveChartsDemo
{
    public partial class MsChartDemo : Form
    {
        public MsChartDemo()
        {
            InitializeComponent();
        }

        private void MsChartDemo_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            foreach (var pair in points)
            {
                chart1.Series[0].Points.AddXY(pair.DateTime, pair.Value);
            }
            for (int i = 0; i < 100000; i++)
            {
                points[i] = new DateTimePoint(dateTimes[i], random.Next(20, 40));
            }
            foreach (var pair in points)
            {
                chart1.Series[1].Points.AddXY(pair.DateTime, pair.Value);
            }
            for (int i = 0; i < 100000; i++)
            {
                points[i] = new DateTimePoint(dateTimes[i], random.Next(20, 40));
            }
            foreach (var pair in points)
            {
                chart1.Series[2].Points.AddXY(pair.DateTime, pair.Value);
            }
        }
    }
}
