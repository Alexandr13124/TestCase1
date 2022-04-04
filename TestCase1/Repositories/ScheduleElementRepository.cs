using System;
using System.Collections.Generic;
using System.Text;
using TestCase1.FileHandling;
using TestCase1.Models;

namespace TestCase1.Repositories
{
    class ScheduleElementRepository : IWriteable<ScheduleElement>
    {
        IFileHandler fileHandler;

        private static string[] headers = new string[] { "Ид партии", "Ид оборудования", "Номенклатура", "Имя оборудывания",
            "Время начала выполнения операции на оборудовании", "Время окончания выполнения операции на оборудовании" };

        public ScheduleElementRepository(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public void Write(List<ScheduleElement> scheduleElements)
        {
            fileHandler.WriteToFile(scheduleElements, headers, FileConfig.ScheduleFile);
        }
    }
}
