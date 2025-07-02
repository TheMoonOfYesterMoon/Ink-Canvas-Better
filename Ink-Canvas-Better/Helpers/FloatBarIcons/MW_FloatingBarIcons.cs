using Ink_Canvas_Better.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {

        // TODO:这个功能仍需打磨
        public void SwitchButtonStatus(String mode)
        {
            CursorIcon.IsStatusEnable = false;
            EraserIcon.IsStatusEnable = false;
            // HighlighterIcon.IsStatusEnable = false;
            PenIcon.IsStatusEnable = false;
            // RetraceIcon.IsStatusEnable = false;
            PickIcon.IsStatusEnable = false;
            // ShapeIcon.IsStatusEnable = false;
            // ToolsIcon.IsStatusEnable = false;
            // TouchSettingIcon.IsStatusEnable = false;

            ICB_Button foundControl = (ICB_Button)FindName(mode);
            if (foundControl == null)
            {
                Console.WriteLine($"未找到控件:{mode}");
            }
            else
            {
                foundControl.IsStatusEnable = true;
            }
        }

    }
}
