using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicNumber.Core.Extensions;

namespace MagicNumber.Core.Model
{
    public interface IServer
    {
        string Name { get; }
        Set [] Sets { get; }
        void Load ( );
        void Add ( Set s, int initialBlock );
        void Remove ( Set s );
        int GetNextBlock ( Set s );
    }

    public class Server : Utilis.ObjectModel.BaseNotifyPropertyChanged, IServer
    {
        private const string SetKey = "Sets";
        private readonly StackExchange.Redis.ConnectionMultiplexer m_redis;
        private readonly Microsoft.Hadoop.Avro.IAvroSerializer<Set> m_setSerializer;

        public string Name { get; }

        private Set [] m_sets;
        public Set [] Sets
        {
            get { return m_sets; }
            private set { SetProperty ( ref m_sets, value ); }
        }

        public Server ( string name, string password )
        {
            Utilis.Contract.AssertNotEmpty ( ( ) => name, name );
            Name = name;
            m_redis = string.IsNullOrEmpty ( password )
                ? StackExchange.Redis.ConnectionMultiplexer.Connect ( name )
                : StackExchange.Redis.ConnectionMultiplexer.Connect ( name + ",Password=" + password );

            m_setSerializer = Microsoft.Hadoop.Avro.AvroSerializer.Create<Set> ( );
        }

        public void Load ( )
        {
            StackExchange.Redis.IDatabase db = m_redis.GetDatabase ( );
            var sets = db.ListRange ( SetKey );

            Sets =
                sets.Select ( o =>
                {
                    using ( var ms = new System.IO.MemoryStream ( o ) )
                    {
                        var set = m_setSerializer.Deserialize ( ms );

                        var redisValue = db.StringGet ( GetCurrentBlockKey ( set ) );
                        if ( redisValue.HasValue )
                        {
                            int intValue;
                            if ( redisValue.TryParse ( out intValue ) )
                                set.CurrentBlock = intValue;
                        }

                        //NOTE: little hack to remove keys (while a proper delete can be written)
                        //if (set.Name == "Test" || set.Name == "SmallBlock" || set.Name == "InitValueTest")
                        //{
                        //    db.ListRemove ( SetKey, o );
                        //    if (redisValue.HasValue)
                        //    {
                        //        db.KeyDelete ( GetCurrentBlockKey ( set ) );
                        //    }
                        //}
                        return set;
                    }
                } ).ToArray ( );
        }

        public void Add ( Set s, int initialBlock )
        {
            StackExchange.Redis.IDatabase db = m_redis.GetDatabase ( );
            db.ListRightPush ( SetKey, SerializeSet ( s ) );
            if ( initialBlock > 0 )
            {
                db.StringSet ( GetCurrentBlockKey ( s ), (long)( initialBlock - 1 ) );// -1 here makes getting the NEXT one get the "initial" value as specified.
            }
            Sets = Sets.Append ( s );
        }

        private byte [] SerializeSet ( Set s )
        {
            byte [] data;
            using ( var ms = new System.IO.MemoryStream ( ) )
            {
                m_setSerializer.Serialize ( ms, s );
                data = ms.ToArray ( );
            }
            return data;
        }

        public void Remove ( Set s )
        {
            StackExchange.Redis.IDatabase db = m_redis.GetDatabase ( );
            db.ListRemove ( SetKey, SerializeSet ( s ) );
        }

        public int GetNextBlock ( Set s )
        {
            StackExchange.Redis.IDatabase db = m_redis.GetDatabase ( );
            var blockAslong = db.StringIncrement ( GetCurrentBlockKey ( s ), 1L );
            return (int)blockAslong;
        }

        private StackExchange.Redis.RedisKey GetCurrentBlockKey ( Set set )
        {
            return "Set/" + set.Id.ToString ( "N" ) + "/CurrentBlock";
        }
    }

    public class FakeServer : IServer
    {
        public string Name { get; set; }
        public Set [] Sets { get; set; }

        public void Load ( )
        {

        }

        public void Add ( Set s, int initialBlock )
        {
            Sets = Sets.Append ( s );
        }

        public void Remove ( Set s )
        {
            Sets = Sets.Where ( o => o.Id != s.Id ).ToArray ( );
        }

        public int GetNextBlock ( Set s )
        {
            return s.CurrentBlock + 1;
        }
    }
}
