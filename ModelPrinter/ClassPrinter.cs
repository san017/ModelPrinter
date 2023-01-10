using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPrinter
{
    internal class ClassPrinter
    {
        public double TimePrinter(int numSymbol, int VolumeFile, double perfomPrinter, int commandOutPut)
        {//объём одной страницы
            double quantityPage = 0.0;
            double sizePage = Math.Round((double)numSymbol / 1024, 1);
            var step = sizePage;
            int numberPage = VolumeFile / (int)sizePage;
            for (int u = 0; u < commandOutPut; u++)
            {
                for (int j = 0; j < numberPage; j++)
                {
                    if (step <= VolumeFile)
                    {
                        quantityPage++;
                        step += sizePage;
                    }               
                }
                ParamInit.TimePrinter += Math.Round((double)quantityPage / perfomPrinter,2);
            }
            return Math.Round(ParamInit.TimePrinter, 2);
        }
        public List<double> TimePrinterList(int numSymbol, int VolumeFile, double perfomPrinter, int commandOutPut)
        {//объём одной страницы
            double quantityPage = 0.0;
            double sizePage = Math.Round((double)numSymbol / 1024, 1);
            var step = sizePage;
            int numberPage = VolumeFile / (int)sizePage;
            List<double> timeWorkPrinter = new List<double>(); 
            for (int u = 0; u < commandOutPut; u++)
            {
                for (int j = 0; j < numberPage; j++)
                {
                    if (step <= VolumeFile)
                    {
                        quantityPage++;
                        step += sizePage;
                    }
                }
                timeWorkPrinter.Add(Math.Round((double)quantityPage / perfomPrinter, 2));
            }
            return timeWorkPrinter;
        }
        public double timeUp(double perfomPrinter, double difference)
        {
            var step = 1 / (perfomPrinter * 10);
            var paceStep = step;
            var perf = perfomPrinter;
            List<double> times = new List<double>();
            List<double> pathList = new List<double>();
            for (int i = 0; i < perfomPrinter * 10; i++)
            {
                times.Add(Math.Round(paceStep, 2));
                pathList.Add(Math.Round(perf, 2));
                paceStep += step;
                perf -= 0.1;

                if (pathList[i] == Math.Round(difference, 1))
                {
                    pathList.Sort();
                    times.Reverse();
                    return times[i-1];
                }
            }
            return 0;
        }
     /*    //создаёт список из каждой десятой части страницы и сколько вней хранится КБ
         public List<double> pepega(double perfomPrinter, double sizePage)
         {
             List<double> pepe = new List<double>();
             var step = sizePage / 10;
             for (int i = 0; i < 10; i++)
             {
                 if (i == 0)
                 {
                     pepe.Add(sizePage);
                 }
                 else
                 {
                     sizePage -= step;
                     pepe.Add(Math.Round(sizePage, 1));

                 }
             }
             return pepe;
         }*/

    }
}
