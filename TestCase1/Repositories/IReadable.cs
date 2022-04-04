using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase1.Repositories
{
    interface IReadable<T> where T : class
    {
        public List<T> GetAll();
    }
}
