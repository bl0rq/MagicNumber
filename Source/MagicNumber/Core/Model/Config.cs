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
            System.IO.File.WriteAllText ( m_persistanceFileName, ServerName );
        }

        public void Load ( )
        {
            if ( System.IO.File.Exists ( m_persistanceFileName ) )
                ServerName = System.IO.File.ReadAllText ( m_persistanceFileName );
        }

        private string m_serverName;
        public string ServerName
        {
            get { return m_serverName; }
            set { SetProperty ( ref m_serverName, value ); }
        }
    }
}
