﻿namespace LiveChartsDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Media.SolidColorBrush solidColorBrush1 = new System.Windows.Media.SolidColorBrush();
            this.ckb_BigPoint = new System.Windows.Forms.CheckBox();
            this.ckb_SmallPoint = new System.Windows.Forms.CheckBox();
            this.btn_OutPutPng = new System.Windows.Forms.Button();
            this.myChart = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();
            // 
            // ckb_BigPoint
            // 
            this.ckb_BigPoint.AutoSize = true;
            this.ckb_BigPoint.Location = new System.Drawing.Point(676, 65);
            this.ckb_BigPoint.Name = "ckb_BigPoint";
            this.ckb_BigPoint.Size = new System.Drawing.Size(60, 16);
            this.ckb_BigPoint.TabIndex = 1;
            this.ckb_BigPoint.Text = "大数据";
            this.ckb_BigPoint.UseVisualStyleBackColor = true;
            this.ckb_BigPoint.CheckedChanged += new System.EventHandler(this.ckb_BigPoint_CheckedChanged);
            // 
            // ckb_SmallPoint
            // 
            this.ckb_SmallPoint.AutoSize = true;
            this.ckb_SmallPoint.Location = new System.Drawing.Point(676, 104);
            this.ckb_SmallPoint.Name = "ckb_SmallPoint";
            this.ckb_SmallPoint.Size = new System.Drawing.Size(60, 16);
            this.ckb_SmallPoint.TabIndex = 2;
            this.ckb_SmallPoint.Text = "小数据";
            this.ckb_SmallPoint.UseVisualStyleBackColor = true;
            this.ckb_SmallPoint.CheckedChanged += new System.EventHandler(this.ckb_SmallPoint_CheckedChanged);
            // 
            // btn_OutPutPng
            // 
            this.btn_OutPutPng.Location = new System.Drawing.Point(676, 180);
            this.btn_OutPutPng.Name = "btn_OutPutPng";
            this.btn_OutPutPng.Size = new System.Drawing.Size(75, 23);
            this.btn_OutPutPng.TabIndex = 3;
            this.btn_OutPutPng.Text = "导出图片";
            this.btn_OutPutPng.UseVisualStyleBackColor = true;
            this.btn_OutPutPng.Click += new System.EventHandler(this.btn_OutPutPng_Click);
            // 
            // myChart
            // 
            this.myChart.Hoverable = true;
            this.myChart.Location = new System.Drawing.Point(12, 20);
            this.myChart.Name = "myChart";
            solidColorBrush1.Color = System.Windows.Media.Color.FromArgb(((byte)(30)), ((byte)(30)), ((byte)(30)), ((byte)(30)));
            this.myChart.ScrollBarFill = solidColorBrush1;
            this.myChart.ScrollHorizontalFrom = 0D;
            this.myChart.ScrollHorizontalTo = 0D;
            this.myChart.ScrollMode = LiveCharts.ScrollMode.None;
            this.myChart.ScrollVerticalFrom = 0D;
            this.myChart.ScrollVerticalTo = 0D;
            this.myChart.Size = new System.Drawing.Size(643, 399);
            this.myChart.TabIndex = 4;
            this.myChart.Text = "cartesianChart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 425);
            this.Controls.Add(this.myChart);
            this.Controls.Add(this.btn_OutPutPng);
            this.Controls.Add(this.ckb_SmallPoint);
            this.Controls.Add(this.ckb_BigPoint);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox ckb_BigPoint;
        private System.Windows.Forms.CheckBox ckb_SmallPoint;
        private System.Windows.Forms.Button btn_OutPutPng;
        private LiveCharts.WinForms.CartesianChart myChart;
    }
}

