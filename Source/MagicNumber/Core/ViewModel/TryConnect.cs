using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber.Core.ViewModel
{
    [Utilis.RegisterService]
    public class TryConnect : Utilis.UI.ViewModel.Base
    {
        public TryConnect ( string server )
        {
            ServerName = server;

            Message = "Loading...";
            Utilis.Runner.RunAsync ( DoTryConnect );
        }

        private void DoTryConnect ( )
        {
            var navigationService = Utilis.ServiceLocator.Instance.GetInstance<Utilis.UI.Navigation.IService> ( );

            Message = "Trying to connect...";
            try
            {
                Model.Server server = new Model.Server ( ServerName );
                Message = "Loading sets...";
                server.Load ( );
                Message = "Loaded " + server.Sets.Length + " sets.";

                var config = Utilis.ServiceLocator.Instance.GetInstance<Model.Config> ( );
                config.ServerName = ServerName;
                config.Save ( );
                Message = "Saved server name.";

                navigationService.NavigateAndRemoveCurrentFromBackStack ( new Home ( server ) );
            }
            catch ( Exception e )
            {
                navigationService.NavigateAndRemoveCurrentFromBackStack ( new Connect ( ServerName, e.Message ) );
            }
        }

        private string m_serverName;
        public string ServerName
        {
            get { return m_serverName; }
            set { SetProperty ( ref m_serverName, value ); }
        }

        private string m_message;
        public string Message
        {
            get { return m_message; }
            set { SetProperty ( ref m_message, value ); }
        }
    }

    public class TryConnectDesign : TryConnect
    {
        public TryConnectDesign ( ) : base ( "magicnumber.blorq.com:15613" )
        {

        }
    }
}
