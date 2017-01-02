using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MagicNumber.Pages
{
    public partial class TryConnect
    {
        public TryConnect ( )
        {
            InitializeComponent ( );
        }
    }

    public abstract class TryConnectBase : Utilis.UI.Win.BasePage<Core.ViewModel.TryConnect>
    {

    }
}
