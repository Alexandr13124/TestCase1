using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase1.Models
{
    internal class Time: IConvertable
    {
        public int MachineToolId { get; set; }
        public int NomenclatureId { get; set; }
        public int OperationTime { get; set; }

        public string[] ToArray()
        {
            return new string[] { MachineToolId.ToString(), NomenclatureId.ToString(), OperationTime.ToString() };
        }

        public Time(List<string> data)
        {
            try
            {
                MachineToolId = Int32.Parse(data[0]);
                NomenclatureId = Int32.Parse(data[1]);
                OperationTime = Int32.Parse(data[2]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи в модели");
            }
        }
    }
}
