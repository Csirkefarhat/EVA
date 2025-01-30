using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aszteroidák.Persistence
{
    public interface IPersistence
    {
        Game Load(string path);

        void Save(String path, Game values);
    }
}
