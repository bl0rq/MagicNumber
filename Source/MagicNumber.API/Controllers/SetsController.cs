using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicNumber.API.Controllers
{
    [ApiController]
    public class SetsController : Controller
    {
        private MagicNumber.Core.Model.Server _server;

        public SetsController(MagicNumber.Core.Model.Server server)
        {
            _server = server;    
        }

        [HttpGet]
        [Route ( "api/v1/GetSets" )]
        [Produces ( "application/json" )]
        public Core.Model.Set[] GetSets ( )
        {
            return _server.Sets;
        }

        [HttpPost]
        [Route ( "api/v1/AddSet" )]
        [Produces ( "application/json" )]
        public Core.Model.Set [] AddSet ( Core.Model.Set s, int initialBlock )
        {
            _server.Add(s, initialBlock);
            return _server.Sets;
        }

        [HttpPost]
        [Route ( "api/v1/RemoveSet" )]
        [Produces ( "application/json" )]
        public Core.Model.Set [] RemoveSet ( Core.Model.Set s )
        {
            _server.Remove ( s );
            return _server.Sets;
        }

        [HttpGet]
        [Route ( "api/v1/GetNextBlock" )]
        [Produces ( "application/json" )]
        public int GetNextBlock ( Guid setId )
        {
            return _server.GetNextBlock ( _server.Sets.Single(o => o.IdAsGuid.Value == setId) );
        }
    }
}
