using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumber.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static T [] Append<T> ( this T [] list, T item )
        {
            if ( list == null || list.Length == 0 )
                return new [] { item };

            T [] final = new T [ list.Length + 1 ];

            Array.Copy ( list, final, list.Length );

            final [ list.Length ] = item;

            return final;
        }
    }
}
