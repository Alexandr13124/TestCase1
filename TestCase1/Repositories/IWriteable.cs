using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase1.Repositories
{
    interface IWriteable<T> where T : class
    {
        public void Write(List<T> scheduleElements);
    }
}
