using System;
using System.Diagnostics;
using TestCase1.FileHandling;
using TestCase1.Models;
using TestCase1.Repositories;



namespace TestCase1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFileHandler excelHandler = ExcelHandler.GetInstance();

            PartyRepository partyRepository = new PartyRepository(excelHandler);
            NomenclatureRepository nomenclatureRepository = new NomenclatureRepository(excelHandler);
            TimeRepository timeRepository = new TimeRepository(excelHandler);
            ScheduleElementRepository elementRepository = new ScheduleElementRepository(excelHandler);
            MachineToolRepository machineToolRepository  = new MachineToolRepository(excelHandler);

            ScheduleConstructor scheduleConstructor = new ScheduleConstructor(partyRepository,nomenclatureRepository,machineToolRepository,
                timeRepository,elementRepository);

            scheduleConstructor.CreateSchedule();

            Console.Write("Операция завершена, файл schedule.xlsx находится в папке с exe");

           

        }
    }
}
