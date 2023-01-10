using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelPrinter
{
    internal class CPU
    {
        public CPU()
        {
        }
        //метод для подсчёта кол-ва команд в блоке команд операций
        public int commandProc(int commOutPut)
        {
            int numTeamsInProccesor = 0;
            for (int i = 0; i <= commOutPut + 1; i++)
            {
                numTeamsInProccesor = i;
            }
            return numTeamsInProccesor;
        }
        //проверка на блок команд или ввода-вывода
        public bool typeBlockCPU(int c)
        {
            return c % 2 == 0 ? true : false;
        }
        // расчёт количества команд на каждый блок(рандомно)
        public List<int> commandsInBlockCPU(int quantCommands, int quantBlockCPU)
        {
            List<int> comList = new List<int>();
            Random rnd = new Random();
            int sumComList = 0;
            for (int i = 0; i < quantBlockCPU; i++)
            {
                comList.Add(rnd.Next(7, quantCommands / quantBlockCPU));
                sumComList += comList[comList.Count - 1];
            }
            if (sumComList != quantCommands)
            {
                var differnce = quantCommands - sumComList;
                int comm = differnce / quantBlockCPU;
                sumComList = 0;
                for (int i = 0; i < comList.Count; i++)
                {
                    comList[i] += comm;
                    sumComList += comList[i];
                }
                var remainsCommands = quantCommands - sumComList;
                comList[0] += remainsCommands;
            }
            return comList;
        }
        // добавление в каждый блок определённое кол-во команд
        public int commandInBlocks(int numBlock, int quantCommands, int quantBlockCPU)
        {
            List<int> quantCommandInOneBlock = commandsInBlockCPU(quantCommands, quantBlockCPU);
            return quantCommandInOneBlock[numBlock / 2];
        }
        //Время работы всех блоков в процессора ( не вывода)
        public double timeWorkCPUblock(int quantCommands, int quantBlockCPU)
        {
            List<int> quantCommandInOneBlock = commandsInBlockCPU(quantCommands, quantBlockCPU);
            for (int i = 0; i < quantCommandInOneBlock.Count; i++)
            {
                ParamInit.TimeCPU += (Math.Round((double)quantCommandInOneBlock[i] / ParamInit.PerfomanceCPU, 2));
            }
            return ParamInit.TimeCPU;
        }
        public List<double> timeWorkCPUblockList(int quantCommands, int quantBlockCPU)
        {
            List<int> quantCommandInOneBlock = commandsInBlockCPU(quantCommands, quantBlockCPU);
            List<double> timeWorkCPUList = new List<double>();
            for (int i = 0; i < quantCommandInOneBlock.Count; i++)
            {
                timeWorkCPUList.Add((Math.Round((double)quantCommandInOneBlock[i] / ParamInit.PerfomanceCPU, 2)));
            }
            return timeWorkCPUList;
        }
    }
}
