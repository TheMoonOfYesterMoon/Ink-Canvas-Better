using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Ink_Canvas_Better.Model;

namespace Ink_Canvas_Better.ViewModel
{
    public class SettingsViewModel : ObservableRecipient
    {
        private Settings _settings = App.GetService<Settings>();
    }
}
