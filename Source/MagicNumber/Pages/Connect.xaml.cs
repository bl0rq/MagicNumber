using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MagicNumber.Pages
{
    public partial class Connect
    {
        public Connect ( )
        {
            InitializeComponent ( );
        }
    }

    public abstract class ConnectBase : Utilis.UI.Win.BasePage<Core.ViewModel.Connect>
    {
        
    }
}
