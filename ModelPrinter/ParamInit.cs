using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPrinter
{
    internal static class ParamInit
    {
        //количество команд вывода
        public static int QuantityCommandProccesorInOutPut { get; set; }
        //Общее количество команд
        public static int SumProcCommands { get; set; }
        //Размер файла в МБ
        public static int VolumeFile { get; set; }
        //быстродействие процессора
        public static int PerfomanceCPU { get; set; }
      
        //-----------------------------------
        //Количество символом на одной странице
        public static int NumSymbolInPagePrinter { get; set; }
        //Быстродействие принтера( печатаемые страницы в секунду)
        public static double PerfomPrinter { get; set; }
        //объём памяти контроллера принтера
        public static int VolumeMemoryPrinter { get; set; }
        // объём памяти ОЗУ
        public static int VolumeMemoryRam { get; set; }
        //-------------------------------------
        // время работы Принтера
        public  static double TimePrinter { get; set; }
        //время работы ОП
        public static double TimeOP { get; set; }
        //Время работы ЦП
        public static double TimeCPU { get; set; }
    }
}
