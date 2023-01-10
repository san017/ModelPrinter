using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelPrinter
{
    public partial class ModelForm : Form
    {
        bool checkRequest = true;
        readonly CPU cpu = new CPU();
        readonly ClassPrinter printer = new ClassPrinter();
        //  ResultForm resultForm = new ResultForm();
        int a = 0;
        DateTime time;
        public ModelForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            requestPictureBox.Visible = false;
            DoubleBuffered = true;
            label1.Invalidate();
            timer5.Start();
            time = DateTime.Now;
            StartToolStripMenuItem.Enabled = false;
            ResetToolStripMenuItem.Enabled = false;
            button2.Visible = false;
            button1.Visible = false;
            dataGridView1.ScrollBars = ScrollBars.Horizontal;
        }

        //Формирование команд в ЦП
        public void Command(int commOut)
        {
            var commCPU = cpu.commandProc(commOut);
            for (int i = 0; i < commCPU + commOut; i++)
            {
                dataGridView1.Columns.Add("Name " + i.ToString(), "");
                dataGridView1.Columns["Name " + i.ToString()].Width = 80;
                dataGridView1.RowTemplate.Height = dataGridView1.Height;
                if (i % 2 == 0)
                {
                    dataGridView1.Rows[0].Cells[i].Value = "Команда процессора" + "\n" + "Кол-во команд: " + cpu.commandInBlocks(i, ParamInit.SumProcCommands, commCPU);
                }
                if (i % 2 == 1)
                {
                    dataGridView1.Rows[0].Cells[i].Value = "Вывод на печать";
                    dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.LightCyan;
                }
                dataGridView1.FirstDisplayedScrollingColumnIndex = i;
            }
            cpu.timeWorkCPUblock(ParamInit.SumProcCommands, commCPU).ToString();
        }
        //непрерывный режим
        public void NotModelCommand(int commOut)
        {
            var commCPU = cpu.commandProc(commOut);
            for (int i = 0; i < commCPU + commOut; i++)
            {
                if (i % 2 == 0)
                {
                    cpu.commandInBlocks(i, ParamInit.SumProcCommands, commCPU);
                }

            }
            cpu.timeWorkCPUblock(ParamInit.SumProcCommands, commCPU);
        }
        //путь от процессора до ОП
        public void RoadCPUInRAM()
        {
            CPUandMemoryTimer.Enabled = true;
            label2.Visible = false;
            CPUpictureBox.Image = Properties.Resources.CPU_green_;
            RAMpictureBox.Image = Properties.Resources.Memory_green_;
            angleTirePictureBox.Image = Properties.Resources.TireCPU_green_;
            GMCHpictureBox.Image = Properties.Resources.GMCH_green_;
            horizontalTirePictureBox.Image = Properties.Resources.HorizontalTire_green_;
            printerPictureBox.Image = Properties.Resources.Printer;

        }
        //путь от ОП до принтера
        public void RoadRAMInPrinter()
        {
            CPUpictureBox.Image = Properties.Resources.CPU;
            RAMpictureBox.Image = Properties.Resources.Memory_green_;
            angleTirePictureBox.Image = Properties.Resources.TireCPU;
            GMCHpictureBox.Image = Properties.Resources.GMCH_green_;
            horizontalTirePictureBox.Image = Properties.Resources.HorizontalTire_green_;
            printerPictureBox.Image = Properties.Resources.Printer_green_;
            verticalTirePictureBox.Image = Properties.Resources.VerticalTire_green_;
            ICHpictureBox.Image = Properties.Resources.ICH_green_;
            horizontalTirePictureBox1.Image = Properties.Resources.HorizontalTire_green_;
            requestPictureBox.Visible = false;
            timer2.Start();
        }
        //запрос на прерывание от процессора
        public void InterruptRequestInCPU()
        {
            RAMpictureBox.Image = Properties.Resources.Memory;
            angleTirePictureBox.Image = Properties.Resources.TireCPU;
            GMCHpictureBox.Image = Properties.Resources.GMCH;
            horizontalTirePictureBox.Image = Properties.Resources.HorizontalTire;
            requestPictureBox.Visible = true;
            requestPictureBox.Image = Properties.Resources.requestArrow2;
            printerPictureBox.Image = Properties.Resources.Printer_green_;
            timerRequest.Start();
            button1.Enabled = false;
            checkRequest = false;
        }

        //Принтер работает
        public void workPrinter()
        {
            GMCHpictureBox.Image = Properties.Resources.GMCH;
            horizontalTirePictureBox.Image = Properties.Resources.HorizontalTire;
            printerPictureBox.Image = Properties.Resources.Printer_green_;
            verticalTirePictureBox.Image = Properties.Resources.VerticalTire;
            ICHpictureBox.Image = Properties.Resources.ICH;
            horizontalTirePictureBox1.Image = Properties.Resources.HorizontalTire;
            RAMpictureBox.Image = Properties.Resources.Memory;
        }
        public void InterruptRequestInPrinter()
        {
            printerPictureBox.Image = Properties.Resources.Printer_green_;
            CPUpictureBox.Image = Properties.Resources.CPU_green_;
        }
        //чтобы не выделялись ячейки
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
        //очередь команд справа налево
        private void button1_Click(object sender, EventArgs e)
        {//Переменная чтобы удалялось по одному блоку
            bool deleteOneBlock = true;
            if (requestPictureBox.Visible)
            {
                requestPictureBox.Visible = false;
            }
            for (int i = dataGridView1.Columns.Count - 1; i >= 0; i--)
            {

                bool blockCPU = cpu.typeBlockCPU(i);
                {
                    if (deleteOneBlock)
                    {
                        if (blockCPU)
                        {
                            if (i == 0)
                            {
                                dataGridView1.Columns.Remove(dataGridView1.Columns[i]);
                                //label7.Text = "VSE";
                                CPUpictureBox.Image = Properties.Resources.CPU;
                                printerPictureBox.Image = Properties.Resources.Printer;
                                button1.Visible = false;
                            }
                            else
                            {
                                RoadCPUInRAM();
                                dataGridView1.Columns.Remove(dataGridView1.Columns[i]);
                            }
                        }
                        if (!blockCPU)
                        {
                            if (checkRequest)
                            {
                                InterruptRequestInCPU();
                            }
                            else
                            {
                                if (label3.Visible)
                                {
                                    label3.Visible = false;
                                    label4.Visible = true;
                                    button1.Enabled = false;
                                    workPrinter();
                                    timerRequestPrinter.Start();
                                    dataGridView1.Columns.Remove(dataGridView1.Columns[i]);
                                    checkRequest = true;
                                }
                                else
                                {
                                    RoadRAMInPrinter();
                                }
                            }
                        }
                        deleteOneBlock = false;

                    }
                }

            }


        }
        //по кнопке открыть окно результатов
        private void button2_Click(object sender, EventArgs e)
        {
            ResultForm f = new ResultForm();
            f.Show();
            f.FormClosed += F_FormClosed;
        }
        //закрытие формы результатов
        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = dataGridView1.Columns.Count - 1; i >= 0; i--)
            {
                dataGridView1.Columns.Remove(dataGridView1.Columns[i]);
            }
            StartToolStripMenuItem.Enabled = false;
            ResetToolStripMenuItem.Enabled = false;
            ParamInit.QuantityCommandProccesorInOutPut = 0;
            ParamInit.SumProcCommands = 0;
            ParamInit.VolumeFile = 0;
            ParamInit.PerfomanceCPU = 0;
            ParamInit.PerfomPrinter = 0;
            ParamInit.NumSymbolInPagePrinter = 0;
            ParamInit.TimeOP = 0;
            ParamInit.TimeCPU = 0;
            ParamInit.TimePrinter = 0;
            ParametersForms.Enabled = true;
            TaсtToolStripMenuItem.Checked = false;
            NotTactToolStripMenuItem.Checked = false;
        }
        //меню параметров
        private void ParametersForms_Click(object sender, EventArgs e)
        {
            ParametersForm pm = new ParametersForm();
            pm.Show();
            ParametersForms.Enabled = false;
            if (TaсtToolStripMenuItem.Checked || NotTactToolStripMenuItem.Checked)
            {
                StartToolStripMenuItem.Enabled = true;
                ResetToolStripMenuItem.Enabled = true;
            }
        }
        //запуск программы
        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (TaсtToolStripMenuItem.Checked)
            {
                button2.Visible = true;
                button1.Visible = true;

                Command(ParamInit.QuantityCommandProccesorInOutPut);
                printer.TimePrinter(ParamInit.NumSymbolInPagePrinter, ParamInit.VolumeFile,
                    ParamInit.PerfomPrinter, ParamInit.QuantityCommandProccesorInOutPut).ToString();

            }
            if (NotTactToolStripMenuItem.Checked)
            {
                NotModelCommand(ParamInit.QuantityCommandProccesorInOutPut);
                printer.TimePrinter(ParamInit.NumSymbolInPagePrinter, ParamInit.VolumeFile,
                ParamInit.PerfomPrinter, ParamInit.QuantityCommandProccesorInOutPut);
                ResultForm f = new ResultForm();
                f.Show();
                f.FormClosed += F_FormClosed;
            }
        }
        //сброс настроек
        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartToolStripMenuItem.Enabled = false;
            ResetToolStripMenuItem.Enabled = false;
            ParamInit.QuantityCommandProccesorInOutPut = 0;
            ParamInit.SumProcCommands = 0;
            ParamInit.VolumeFile = 0;
            ParamInit.PerfomanceCPU = 0;
            ParamInit.PerfomPrinter = 0;
            ParamInit.NumSymbolInPagePrinter = 0;
            ParamInit.TimeOP = 0;
            ParamInit.TimeCPU = 0;
            ParamInit.TimePrinter = 0;
            ParametersForms.Enabled = true;
            TaсtToolStripMenuItem.Checked = false;
            NotTactToolStripMenuItem.Checked = false;
            MessageBox.Show("Сброшено");
        }
        private void NotTactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaсtToolStripMenuItem.Checked = false;
            if (!ParametersForms.Enabled)
            {
                StartToolStripMenuItem.Enabled = true;
                ResetToolStripMenuItem.Enabled = true;
            }
        }
        private void TactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotTactToolStripMenuItem.Checked = false;
            if (!ParametersForms.Enabled)
            {
                StartToolStripMenuItem.Enabled = true;
                ResetToolStripMenuItem.Enabled = true;
            }
        }
        //перемещение данных из ЦП в ОП
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Visible = false;
            label1.Invalidate();
            button1.Enabled = false;
            int stepMove = 10;
            label1.Text = "Данные: " + ParamInit.VolumeFile + " КБ";
            label1.Visible = true;

            if (label1.Right <= angleTirePictureBox.Right)
            {
                label1.Left += stepMove;
            }
            else
            {
                if (label1.Bottom <= GMCHpictureBox.Bottom - 50)
                {
                    label1.Top += stepMove;
                }
                else
                {
                    if (label1.Right <= RAMpictureBox.Left - 10)
                    {
                        label1.Left += stepMove;
                    }
                    else
                    {
                        label1.Location = new Point(417, 177);
                        label1.Visible = false;
                        label2.Text = "Данные: " + ParamInit.VolumeFile + " КБ";
                        label2.Visible = true;
                        button1.Enabled = true;
                        CPUandMemoryTimer.Enabled = false;
                    }
                }

            }
        }


        //перемещение данных из ЦП в ОП
        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Invalidate();
            button1.Enabled = false;
            int a = 10;
            label2.Text = "Данные: " + ParamInit.VolumeFile + " КБ";
            label3.Text = "Данные: " + ParamInit.VolumeFile + " КБ";
            if (label2.Top > RAMpictureBox.Top + 40 && label2.Left > RAMpictureBox.Left)
            {
                label2.Top -= a;
            }
            else
            {
                if (GMCHpictureBox.Left + 60 < label2.Left)
                {
                    label2.Left -= a;
                }
                else
                {
                    if (label2.Bottom < ICHpictureBox.Bottom - 40)
                    {
                        label2.Top += a;
                    }
                    else
                    {
                        if (horizontalTirePictureBox1.Left < label2.Left)
                        {
                            label2.Left -= a;
                        }
                        else
                        {
                            label2.Location = new Point(1046, 534);
                            label2.Visible = false;
                            label3.Visible = true;
                            button1.Enabled = true;
                            timer2.Stop();
                        }
                    }
                }
            }
        }

        private void timerRequest_Tick(object sender, EventArgs e)
        {
            requestPictureBox.Image = Properties.Resources.requestArrow4;
            button1.Enabled = true;
            timerRequest.Stop();
        }

        private void timerRequestPrinter_Tick(object sender, EventArgs e)
        {
            InterruptRequestInPrinter();
            requestPictureBox.Visible = true;
            label4.Visible = false;
            requestPictureBox.Image = Properties.Resources.requestArrow6;
            if (a >= 1)
            {
                requestPictureBox.Image = Properties.Resources.requestArrow5;
                a = 0;
                button1.Enabled = true;
                timerRequestPrinter.Stop();

            }
            else { a++; }


        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks - time.Ticks;
            DateTime stop = new DateTime();
            stop = stop.AddTicks(tick);
            //label5.Text = String.Format("{0:HH:mm:ss}", stop);
        }
    }
}



