using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase1.Models
{
    internal class MachineTool
    {
        public int MachineToolId { get; set; }
        public string Name { get; set; }
        public MachineTool(List<string> data)
        {
            try
            {
                MachineToolId = Int32.Parse(data[0]);
                Name = data[1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи в модели");
            }

        }
    }
}
