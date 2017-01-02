using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber.Core.ViewModel
{
    public class Home : Utilis.UI.ViewModel.Base
    {
        public Home ( Model.IServer server )
        {
            Server = server;
            server.Load ( );
        }

        public Model.IServer Server { get; }

        private Utilis.UI.Navigation.NavigationCommand<AddSet> m_Add;
        public Utilis.UI.Navigation.NavigationCommand<AddSet> Add
        {
            get { return m_Add; }
            set { SetProperty ( ref m_Add, value ); }
        }
    }

    public class HomeDesign : Home
    {
        public HomeDesign ( ) : base ( CreateFakeServer ( ) )
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
                    new Model.Set() {BlockSize = 512, Id = Guid.NewGuid(), Name = "Dogliis"},
                    new Model.Set() {BlockSize = 100, Id = Guid.NewGuid(), Name = "CMPO"},
                    new Model.Set() {BlockSize = 100, Id = Guid.NewGuid(), Name = "MagicNumber"},
                }
            };
            return s;
        }
    }
}
