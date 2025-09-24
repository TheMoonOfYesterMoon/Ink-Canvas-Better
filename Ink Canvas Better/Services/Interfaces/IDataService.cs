using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink_Canvas_Better.Services.Interfaces
{
    internal interface IDataService
    {
        public void LoadData();
        public void SaveData();
        public void ResetData();
        public void DeleteData();
        public object GetData();
    }
}
