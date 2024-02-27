using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilis.Extensions;

namespace MagicNumber.Core.ViewModel
{
    public class Set : Utilis.UI.ViewModel.Base
    {
        private readonly Model.IServer m_server;
        private readonly Model.ILocalServer m_localServer;

        public Set ( Model.IServer server, Model.ILocalServer localServer, Action<Set> select, Model.Set model, Model.MySet mySet = null )
        {
            Utilis.Contract.AssertNotNull ( ( ) => server, server );
            Utilis.Contract.AssertNotNull ( ( ) => localServer, localServer );
            Utilis.Contract.AssertNotNull ( ( ) => select, select );
            Utilis.Contract.AssertNotNull ( ( ) => model, model );

            m_server = server;
            m_localServer = localServer;

            Model = model;
            MySet = mySet;

            Select = new Utilis.UI.DelegateCommand ( ( ) => select ( this ) );

            Command = new Utilis.UI.DelegateCommand ( DoCommand );
        }

        private void DoCommand ( )
        {
            if ( MySet == null )
            {
                Core.Model.MySet myset = new Core.Model.MySet { Id = m_model.Id };
                myset.Block = m_server.GetNextBlock ( m_model );
                myset.Index = 0;
                myset.LastUpdated = DateTimeOffset.UtcNow.Ticks;
                m_localServer.AddSet ( myset );
                MySet = myset;
            }
            else
            {
                if ( MySet.Index == m_model.BlockSize - 1 )
                {
                    //TODO: ensure online
                    MySet.Block = m_server.GetNextBlock ( m_model );
                    MySet.Index = 0;
                }
                else
                    MySet.Index++;

                MySet.LastUpdated = DateTimeOffset.UtcNow.Ticks;

                m_localServer.UpdateSet ( MySet );
            }

            OnPropertyChanged ( ( ) => CurrentValue );
            OnPropertyChanged ( ( ) => Timestamp );
            OnPropertyChanged ( ( ) => Display );

            System.Windows.Clipboard.SetText ( CurrentValue.ToString ( ) );
        }

        public long CurrentValue => Model.BlockSize * MySet?.Block + MySet?.Index ?? 0;
        public string Timestamp => MySet == null ? "" : new DateTimeOffset(MySet.LastUpdated, TimeSpan.Zero).ToTimestampString ( );
        public string Display => MySet == null ? "--" : CurrentValue.ToString ( );

        private Model.Set m_model;
        public Model.Set Model
        {
            get { return m_model; }
            set { SetProperty ( ref m_model, value ); }
        }

        private bool m_isSelected;
        public bool IsSelected
        {
            get { return m_isSelected; }
            set { SetProperty ( ref m_isSelected, value ); }
        }

        private System.Windows.Input.ICommand m_select;
        public System.Windows.Input.ICommand Select
        {
            get { return m_select; }
            set { SetProperty ( ref m_select, value ); }
        }

        private Model.MySet m_mySet;
        public Model.MySet MySet
        {
            get { return m_mySet; }
            set { SetProperty ( ref m_mySet, value ); }
        }

        public System.Windows.Input.ICommand Command { get; }
    }
}
