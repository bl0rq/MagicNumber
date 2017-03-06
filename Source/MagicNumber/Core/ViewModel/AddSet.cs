using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber.Core.ViewModel
{
    public class AddSet : Utilis.UI.ViewModel.Base
    {
        private readonly Model.IServer m_server;
        private readonly Model.ILocalServer m_localServer;

        public AddSet ( Model.IServer server, Model.ILocalServer localServer )
        {
            Utilis.Contract.AssertNotNull ( ( ) => server, server );
            m_server = server;
            m_localServer = localServer;
            DoAddSet = new Utilis.UI.DelegateCommand ( AddToServer, ( ) => IsValidName );
        }

        private void AddToServer ( )
        {
            try
            {
                m_server.Add ( new Model.Set ( ) { BlockSize = BlockSize, Name = SetName, Id = Guid.NewGuid ( ) }, InitialBlock );
                Navigate.To ( new Home ( m_server, m_localServer ) );
            }
            catch ( Exception e )
            {
                Message = "Unable to add set: " + e.Message;
            }
        }

        private bool CheckName ( )
        {
            if ( string.IsNullOrWhiteSpace ( m_setName ) )
                return false;
            else
            {
                //TODO: better validation?  Parse as 'Server(:port)?' and ping?  Try to AddSet to Reddis?
                return true;
            }
        }


        private Utilis.UI.DelegateCommand m_doAddSet;
        public Utilis.UI.DelegateCommand DoAddSet
        {
            get { return m_doAddSet; }
            set { SetProperty ( ref m_doAddSet, value ); }
        }

        private string m_setName;
        public string SetName
        {
            get { return m_setName; }
            set
            {
                if ( SetProperty ( ref m_setName, value ) )
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
                    DoAddSet.FireCanExecuteChanged ( );
            }
        }

        private string m_message;
        public string Message
        {
            get { return m_message; }
            set { SetProperty ( ref m_message, value ); }
        }

        private int m_blockSize = 500;
        public int BlockSize
        {
            get { return m_blockSize; }
            set
            {
                if ( SetProperty ( ref m_blockSize, value ) )
                    OnPropertyChanged ( ( ) => InitialNumber );
            }
        }

        private int m_initialBlock;
        public int InitialBlock
        {
            get { return m_initialBlock; }
            set
            {
                if ( SetProperty ( ref m_initialBlock, value ) )
                    OnPropertyChanged ( ( ) => InitialNumber );
            }
        }

        public int InitialNumber => BlockSize * InitialBlock + 1;

        public int [] BlockSizes { get; } = new [] { 100, 128, 200, 256, 500, 512, 1000, 1024 };
    }

    public class AddSetDesign : AddSet
    {
        public AddSetDesign ( ) : base ( new Model.FakeServer ( ) { Name = "Some Server Name" }, new Model.FakeLocalServer ( ) )
        {
            InitialBlock = 15;
        }
    }
}
