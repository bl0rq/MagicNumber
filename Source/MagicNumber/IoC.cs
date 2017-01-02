using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Utilis.Extensions;

namespace MagicNumber
{
    public static class IoC
    {
        public static void Start ( Core.Model.Config config )
        {
            ContainerBuilder builder = new ContainerBuilder ( );

            builder.RegisterByAttribute ( typeof ( IoC ).Assembly );

            //builder.Register ( context => new Lei.Config.ConfigDeviceAssemblyGuidance ( ) )
            //    .AsImplementedInterfaces ( )
            //    .AsSelf ( )
            //    .SingleInstance ( );

            builder.Register ( context => config )
                .AsSelf ( )
                .AsImplementedInterfaces ( )
                .SingleInstance ( );

            Utilis.ServiceLocator.Instance = new Utilis.ServiceLocator ( builder.Build ( ) );
        }
    }
}
