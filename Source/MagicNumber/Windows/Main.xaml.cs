﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MagicNumber.Windows
{
    public partial class Main
    {
        public Main ( )
        {
            InitializeComponent ( );
        }

        public override System.Windows.Controls.Frame GetFrame ( )
        {
            return MainFrame;
        }
    }

    public abstract class FrameContainerWindow : System.Windows.Window
    {
        public abstract System.Windows.Controls.Frame GetFrame ( );
    }
}
