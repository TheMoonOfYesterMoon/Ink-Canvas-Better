using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink_Canvas_Better.Services.Interfaces
{
    internal interface IDataService
    {
        void LoadData();
        void SaveData();
        void ResetData();
        void DeleteData();
    }
}
