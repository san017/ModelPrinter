using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace ModelPrinter
{
    public partial class ResultForm : Form
    {
        CPU cpu = new CPU();
        ClassPrinter printer = new ClassPrinter();
        ModelForm f;
        int pet = 0;
        int kek = 0;
        int a = 0;
        int b = 0;
        int c = 0;
        bool g = true;
        public ResultForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            ParamInit.TimeOP = cpu.commandProc(ParamInit.QuantityCommandProccesorInOutPut) + ParamInit.QuantityCommandProccesorInOutPut - 1;
            timeWorkCPULabel.Text = ParamInit.TimeCPU + " сек.";
            timeWorkPrinterLabel.Text = ParamInit.TimePrinter + " сек.";
            timeWorkRAMLabel.Text = ParamInit.TimeOP + " сек.";
            var allTimeWork = ParamInit.TimeCPU + ParamInit.TimePrinter + ParamInit.TimeOP;
            AllTimeLabel.Text = allTimeWork.ToString() + " сек.";
            //Загрузки
            loadCPULabel.Text = Math.Round((ParamInit.TimeCPU * 100 / allTimeWork), 1).ToString() + " %";
            loadRAMLabel.Text = Math.Round((ParamInit.TimeOP * 100 / allTimeWork), 1).ToString() + " %";
            loadPrinterLabel.Text = Math.Round((ParamInit.TimePrinter * 100 / allTimeWork), 1).ToString() + " %";
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.Series["Series1"]["PixelPointWidth"] = "45";
            chart1.ChartAreas[0].AxisY.Maximum = 1;
            chart1.ChartAreas[0].AxisY.Interval = 1;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            // chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Maximum = Math.Round(allTimeWork, 0);
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.ScaleView.Size = 10;//размер скрола
            chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            List<double> points = cpu.timeWorkCPUblockList(ParamInit.SumProcCommands, cpu.commandProc(ParamInit.QuantityCommandProccesorInOutPut));
            List<double> points2 = printer.TimePrinterList(ParamInit.NumSymbolInPagePrinter, ParamInit.VolumeFile,
                    ParamInit.PerfomPrinter, ParamInit.QuantityCommandProccesorInOutPut);
            int coll = cpu.commandProc(ParamInit.QuantityCommandProccesorInOutPut);
            //-----------------------------
            chart2.Series[0].IsVisibleInLegend = false;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.Series["Series1"]["PixelPointWidth"] = "45";
            chart2.ChartAreas[0].AxisY.Maximum = 1;
            chart2.ChartAreas[0].AxisY.Interval = 1;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            // chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisX.Maximum = Math.Round(allTimeWork, 0);
            chart2.ChartAreas[0].AxisX.Interval = 1;
            chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart2.ChartAreas[0].AxisX.ScaleView.Size = 10;//размер скрола
            chart2.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            //----------------------------------
            chart3.Series[0].IsVisibleInLegend = false;
            chart3.ChartAreas[0].AxisY.Minimum = 0;
            chart3.Series["Series1"]["PixelPointWidth"] = "45";
            chart3.ChartAreas[0].AxisY.Maximum = 1;
            chart3.ChartAreas[0].AxisY.Interval = 1;
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //  chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false
            chart3.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart3.ChartAreas[0].AxisX.Maximum = allTimeWork;
            chart3.ChartAreas[0].AxisX.Interval = 1;
            chart3.ChartAreas[0].AxisX.ScaleView.Size = 10;//размер скрола
            chart3.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            for (int i = 0; i < coll + ParamInit.QuantityCommandProccesorInOutPut; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < (int)points[kek]; j++)
                    {
                        chart1.Series[0].Points.AddXY(j + 1 + a + b, 1);
                    }
                    a += (int)points[kek];
                    kek++;
                }
                if (c < 2)
                {
                    chart2.Series[0].Points.AddXY(1 + a + b, 1);
                    c++;
                    a += 1;
                }
                if (i % 2 == 1)
                {
                    for (int j = 0; j < (int)points2[pet]; j++)
                    {
                        chart3.Series[0].Points.AddXY(1 + a + j + b, 1);
                    }
                    b += (int)points2[0];
                    c = 0;
                    pet++;
                }
                chart1.Series[0].Points.AddXY((int)(allTimeWork - 4 + i), 1);
                if (coll + ParamInit.QuantityCommandProccesorInOutPut - 1 == i)
                {
                    kek = 0;
                    pet = 0;
                    a = 0;
                    b = 0;
                }
            }

        }


    }

}
