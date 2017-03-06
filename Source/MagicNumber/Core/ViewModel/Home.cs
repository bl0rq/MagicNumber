using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilis.Extensions;

namespace MagicNumber.Core.ViewModel
{
    public class Home : Utilis.UI.ViewModel.Base
    {
        public Home ( Model.IServer server, Model.ILocalServer localServer )
        {
            Server = server;
            server.Load ( );

            Add = new Utilis.UI.Navigation.NavigationCommand<AddSet> ( ( ) => new AddSet ( server, localServer ) );

            var mySets = localServer.GetMySets ( ).ToDictionary ( o => o.Id );
            Sets = new Utilis.ObjectModel.ObservableCollection<Set> ( server.Sets.Select ( o => new Set ( server, localServer, SelectSet, o, mySets.SafeGet ( o.Id ) ) ) );
        }

        private void SelectSet ( Set s )
        {
            if ( SelectedSet != null )
                SelectedSet.IsSelected = false;
            s.IsSelected = true;
            SelectedSet = s;
        }

        public Model.IServer Server { get; }

        private Utilis.ObjectModel.ObservableCollection<Set> m_sets;
        public Utilis.ObjectModel.ObservableCollection<Set> Sets
        {
            get { return m_sets; }
            set { SetProperty ( ref m_sets, value ); }
        }

        private Set m_selectedSet;
        public Set SelectedSet
        {
            get { return m_selectedSet; }
            private set { SetProperty ( ref m_selectedSet, value ); }
        }

        public Utilis.UI.Navigation.NavigationCommand<AddSet> Add { get; }
    }

    public class HomeDesign : Home
    {
        private static readonly Guid ms_dogliisFakeGuid = Guid.NewGuid ( );

        public HomeDesign ( ) : base ( CreateFakeServer ( ), CreateLocalServer ( ) )
        {

        }

        private static Model.IServer CreateFakeServer ( )
        {
            var s = new Model.FakeServer ( )
            {
                Name = "magicNumber.blorq.com:15613",
                Sets = new Model.Set []
                {
                    new Model.Set() {BlockSize = 1000, Id = Guid.NewGuid(), Name = "CATLIIS"},
                    new Model.Set() {BlockSize = 512, Id = ms_dogliisFakeGuid, Name = "Dogliis"},
                    new Model.Set() {BlockSize = 100, Id = Guid.NewGuid(), Name = "CMPO"},
                    new Model.Set() {BlockSize = 100, Id = Guid.NewGuid(), Name = "MagicNumber"},
                }
            };
            return s;
        }

        private static Model.ILocalServer CreateLocalServer ( )
        {
            var localServer = new Model.FakeLocalServer ( );

            localServer.AddSet ( new Model.MySet ( ) { Id = ms_dogliisFakeGuid, Block = 3, Index = 10 } );
            return localServer;
        }
    }
}
