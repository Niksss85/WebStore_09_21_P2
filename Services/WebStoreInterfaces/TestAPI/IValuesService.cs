using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Interfaces.TestAPI
{
    public interface IValuesService
    {
        IEnumerable<string> GetAll();
        string GertByIndex(int index);
        void Add(string Value);
        void Edit(int index, string str);
        bool Delete(int index);

    }
}
