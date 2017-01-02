using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber.Core.ViewModel
{
    [Utilis.RegisterService]
    public class Connect : Utilis.UI.ViewModel.Base
    {
        public Connect ( )
        {
            DoConnect = new Utilis.UI.Navigation.NavigationCommand<TryConnect> ( ( ) => new TryConnect ( ServerName ), ( ) => IsValidName );
        }
        public Connect ( string serverName, string message ) : this ( )
        {
            Message = message;
            ServerName = serverName;
        }

        private bool CheckName ( )
        {
            if ( string.IsNullOrWhiteSpace ( m_serverName ) )
                return false;
            else
            {
                //TODO: better validation?  Parse as 'Server(:port)?' and ping?  Try to connect to Reddis?
                return true;
            }
        }


        private Utilis.UI.Navigation.NavigationCommand<TryConnect> m_doConnect;
        public Utilis.UI.Navigation.NavigationCommand<TryConnect> DoConnect
        {
            get { return m_doConnect; }
            set { SetProperty ( ref m_doConnect, value ); }
        }

        private string m_serverName;
        public string ServerName
        {
            get { return m_serverName; }
            set
            {
                if ( SetProperty ( ref m_serverName, value ) )
                {
                    IsValidName = CheckName ( );
                }
            }
        }

        private bool m_isValidName;
        public bool IsValidName
        {
            get { return m_isValidName; }
            set
            {
                if ( SetProperty ( ref m_isValidName, value ) )
                    DoConnect.FireCanExecuteChanged ( );
            }
        }

        private string m_message;
        public string Message
        {
            get { return m_message; }
            set { SetProperty ( ref m_message, value ); }
        }
    }

    public class ConnectDesign : Connect
    {

    }
}
