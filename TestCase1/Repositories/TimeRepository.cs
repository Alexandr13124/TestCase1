using System;
using System.Collections.Generic;
using System.Text;
using TestCase1.FileHandling;
using TestCase1.Models;

namespace TestCase1.Repositories
{
    class TimeRepository : IReadable<Time>
    {
        IFileHandler fileHandler;

        public TimeRepository(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public List<Time> GetAll()
        {
            List<Time> times = new List<Time>();

            var stringTimes = fileHandler.ReadFile(FileConfig.TimesFile);

            foreach (var element in stringTimes)
            {
                times.Add(new Time(element));
            }

            return times;
        }
    }
}
