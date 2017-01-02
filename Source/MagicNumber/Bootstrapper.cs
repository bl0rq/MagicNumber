using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber
{
    public class Bootstrapper : Utilis.UI.Controller.IBootStrapper
    {
        public void Dispose ( )
        {

        }

        public async Task StartAsync ( )
        {
            Utilis.Messaging.Bus.Instance.Send ( new Core.Messages.LoadingProgress ( "Beginning Bootstrap..." ) );

            Utilis.Runner.Dispatcher = new Utilis.Win.DispatcherWrapper ( System.Windows.Application.Current.Dispatcher );

            Utilis.Messaging.Bus.Instance.Send ( new Core.Messages.LoadingProgress ( "Loading config..." ) );
            var config = new Core.Model.Config ( "." );
            config.Load ( );

            IoC.Start ( config );

            Utilis.Messaging.Bus.Instance.Send ( new Core.Messages.LoadingProgress ( "Building Main Window..." ) );
            Windows.Main mainWindow = null;
            await Utilis.Runner.RunOnDispatcherThreadAsync ( ( ) =>
            {
                mainWindow = new Windows.Main ( );
                mainWindow.Show ( );
            } );

            var service = StartNavigation ( mainWindow );

            Utilis.Messaging.Bus.Instance.Send ( new Core.Messages.LoadingProgress ( "Showing initial Page..." ) );

            if ( string.IsNullOrEmpty ( config.ServerName ) )
            {
                var vm = Utilis.ServiceLocator.Instance.GetInstance<Core.ViewModel.Connect> ( );
                vm.ServerName = "magicNumber.blorq.com:15613";
                service.Navigate ( vm );
            }
            else
            {
                var vm = new Core.ViewModel.TryConnect ( config.ServerName );
                service.Navigate ( vm );
            }

        }

        private Utilis.UI.Navigation.IService StartNavigation ( Windows.FrameContainerWindow mainWindow )
        {
            Utilis.UI.Navigation.Win.Service service =
                new Utilis.UI.Navigation.Win.Service (
                    mainWindow.GetFrame ( ),
                    new Utilis.UI.ViewMapper ( new Utilis.UI.ViewFinder ( ), GetType ( ).Assembly ) );

            Utilis.ServiceLocator.Instance.RegisterInstance<Utilis.UI.Navigation.IService> ( service );

            return service;
        }
    }
}
