using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase1.Models
{
    internal class Party
    {
        public int PartyId { get; set; }
        public int NomenclatureId { get; set; }
        public Nomenclature Nomenclature { get; set; }

        public Party(List<string> data)
        {
            try
            {
                PartyId = Int32.Parse(data[0]);
                NomenclatureId = Int32.Parse(data[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи в модели");
            }
        }
    }
}
