using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence
{
    public interface IPersistence
    {
        Task<Game> Load(Stream path);

        void Save(Stream path, Game values);
    }
}
