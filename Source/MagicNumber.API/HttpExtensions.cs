using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Utilis.Web.Extensions
{
    public static class HttpExtensions
    {
        public static string? GetIp ( this HttpRequest request )
        {
            string? ip =
              request.Headers [ "HTTP_X_FORWARDED_FOR" ];
            if ( string.IsNullOrEmpty ( ip ) )
            {
                ip = request.Headers [ "REMOTE_ADDR" ];
            }
            return ip;
        }
    }
}
