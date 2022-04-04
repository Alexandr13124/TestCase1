using System;
using System.Collections.Generic;
using System.Text;
using TestCase1.FileHandling;
using TestCase1.Models;

namespace TestCase1.Repositories
{
    class NomenclatureRepository : IReadable<Nomenclature>
    {
        IFileHandler fileHandler;

        public NomenclatureRepository(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public List<Nomenclature> GetAll()
        {
            List<Nomenclature> nomenclatures = new List<Nomenclature>();

            var stringNomenclatures = fileHandler.ReadFile(FileConfig.NomenclatureFile);

            foreach (var element in stringNomenclatures)
            {
                nomenclatures.Add(new Nomenclature(element));
            }

            return nomenclatures;
        }
    }
}
