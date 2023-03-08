using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;

namespace AeroScreenCapture
{
    public class ColorBoxDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules => base.SelectionRules & ~SelectionRules.AllSizeable;
    }
}
