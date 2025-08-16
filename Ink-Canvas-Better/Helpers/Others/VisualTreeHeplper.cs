using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ink_Canvas_Better.Helpers.Others
{
    static class VisualTreeHeplper
    {
        // Find the first parent of the specified type
        public static DependencyObject FindFirstParent(this DependencyObject child, params Type[] types)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                foreach (var item in types)
                {
                    if (parent.GetType() == item)
                    {
                        return parent;
                    }
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }
    }
}
