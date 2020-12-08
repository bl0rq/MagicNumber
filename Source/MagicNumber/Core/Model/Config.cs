using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber.Core.Model
{
    public class Config : Utilis.ObjectModel.BaseNotifyPropertyChanged
    {
        private readonly string m_persistanceFileName;

        public Config ( string persistanceLocation )
        {
            Utilis.Contract.AssertNotEmpty ( ( ) => persistanceLocation, persistanceLocation );
            m_persistanceFileName = System.IO.Path.Combine ( persistanceLocation, ".config" );
        }

        public void Save ( )
        {
            System.IO.File.WriteAllLines ( m_persistanceFileName, new [ ] { ServerName, Password } );
        }

        public void Load ( )
        {
            if ( System.IO.File.Exists ( m_persistanceFileName ) )
            {
                var lines = System.IO.File.ReadAllLines ( m_persistanceFileName );
                ServerName = lines.First ( );
                if ( lines.Length > 1 )
                    Password = lines [ 1 ];
            }
        }

        private string m_serverName;
        public string ServerName
        {
            get { return m_serverName; }
            set { SetProperty ( ref m_serverName, value ); }
        }

        private string m_password;
        public string Password
        {
            get { return m_password; }
            set { SetProperty ( ref m_password, value ); }
        }
    }
}
