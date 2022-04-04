using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase1.Models
{
    class Nomenclature
    {
        public int NomenclatureId { get; set; }
        public string Name { get; set; }
        public Nomenclature(List<string> data)
        {
            try
            {
                NomenclatureId = Int32.Parse(data[0]);
                Name = data[1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи в модели");
            }

        }
    }
}
