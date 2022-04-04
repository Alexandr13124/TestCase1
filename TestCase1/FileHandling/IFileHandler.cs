using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase1.Models
{
    interface IFileHandler
    {
        public List<List<string>> ReadFile(string fileName);

        public void WriteToFile<T>(List<T> convertable, string[] headers, string fileName) where T : IConvertable;

    }
}
