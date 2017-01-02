using System;
using System.Collections.Generic;
using System.Text;

namespace MagicNumber.Pages
{
    public partial class Home
    {
        public Home ( )
        {
            InitializeComponent ( );
        }
    }

    public abstract class HomeBase : Utilis.UI.Win.BasePage<Core.ViewModel.Home>
    {

    }
}
