using System;
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

        private void MainFrame_Navigated ( object sender, System.Windows.Navigation.NavigationEventArgs e )
        {
            Pages.Home home = MainFrame.Content as Pages.Home;
            if (home != null)
            {
                if (home.ViewModel.Sets.Count > 0)
                {
                    this.Width = home.ViewModel.Sets.Count * 146 + 20;
                    this.Height = 225;
                    //this.Height = 200;
                }
            }
        }
    }

    public abstract class FrameContainerWindow : System.Windows.Window
    {
        public abstract System.Windows.Controls.Frame GetFrame ( );
    }
}
