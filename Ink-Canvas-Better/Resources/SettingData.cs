using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink_Canvas_Better.Resources
{
    public class SettingData
    {
        public bool ShowWelcomeScreen { get; set; } = true;

    }

    public enum OptionalOperation
    {
        Yes,
        No,
        Ask
    }
}
