using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelPrinter
{
    public partial class ParametersForm : Form
    {
        ModelForm form1;

        public ParametersForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void ParametersForm_Load(object sender, EventArgs e)
        {
            OperationInOutput.Text = trackBar1.Value.ToString();
            textBox1.Text = trackBar2.Value.ToString();
            textBox2.Text = trackBar5.Value.ToString();
            textBox3.Text = (trackBar3.Value * 1024).ToString();
            textBox4.Text = ((double)trackBar4.Value / 10).ToString();
            textBox5.Text = trackBar6.Value.ToString();
            textBox6.Text = trackBar7.Value.ToString();
            textBox7.Text = trackBar8.Value.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ParamInit.QuantityCommandProccesorInOutPut = trackBar1.Value;
            ParamInit.SumProcCommands = trackBar2.Value;
            ParamInit.VolumeFile = trackBar5.Value;
            ParamInit.NumSymbolInPagePrinter = trackBar3.Value * 1024;
            ParamInit.PerfomPrinter = (double)trackBar4.Value / 10.0;
            ParamInit.PerfomanceCPU = trackBar6.Value;
            this.Close();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            OperationInOutput.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = (trackBar3.Value * 1024).ToString();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = ((double)trackBar4.Value / 10).ToString();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar5.Value.ToString();
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            textBox5.Text = trackBar6.Value.ToString();
        }

        private void ParametersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParamInit.QuantityCommandProccesorInOutPut = trackBar1.Value;
            ParamInit.SumProcCommands = trackBar2.Value;
            ParamInit.VolumeFile = trackBar5.Value;
            ParamInit.NumSymbolInPagePrinter = trackBar3.Value * 1024;
            ParamInit.PerfomPrinter = (double)trackBar4.Value / 10.0;
            ParamInit.PerfomanceCPU = trackBar6.Value;
        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            textBox7.Text = trackBar8.Value.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            textBox6.Text = trackBar7.Value.ToString();
        }
    }
}
