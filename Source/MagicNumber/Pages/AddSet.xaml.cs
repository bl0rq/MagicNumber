using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MagicNumber.Pages
{
    public partial class AddSet
    {
        public AddSet ( )
        {
            InitializeComponent ( );
        }
    }

    public abstract class AddSetBase : Utilis.UI.Win.BasePage<Core.ViewModel.AddSet>
    {

    }
}
