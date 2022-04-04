using System;
using System.Collections.Generic;
using System.Text;
using TestCase1.FileHandling;
using TestCase1.Models;

namespace TestCase1.Repositories
{
    class PartyRepository : IReadable<Party>
    {
        IFileHandler fileHandler;

        public PartyRepository(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public List<Party> GetAll()
        {
            List<Party> parties = new List<Party>();

            var stringParties = fileHandler.ReadFile(FileConfig.PartiesFile);

            foreach (var element in stringParties)
            {
                parties.Add(new Party(element));
            }

            return parties;
        }
    }
}
