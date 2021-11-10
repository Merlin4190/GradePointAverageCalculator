using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IHistory
    {
        void Archive(string output);

        Task<string[]> RetrieveAsync();
    }
}
