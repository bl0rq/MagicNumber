using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber.Core.ViewModel
{
    public class Home : Utilis.UI.ViewModel.Base
    {
        private readonly Model.Server m_server;

        public Home ( Model.Server server )
        {
            m_server = server;
        }
    }

    public class HomeDesign : Home
    {
        public HomeDesign ( ) : base ( new Model.Server ( "magicNumber.blorq.com:15613" ) )
        {

        }
    }
}
