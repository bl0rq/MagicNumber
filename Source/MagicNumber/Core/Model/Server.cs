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
        void Add ( Set s );
        void Remove ( Set s );
    }

    public class Server : Utilis.ObjectModel.BaseNotifyPropertyChanged, IServer
    {
        private const string SetKey = "Sets";
        private readonly StackExchange.Redis.ConnectionMultiplexer m_redis;
        private readonly Microsoft.Hadoop.Avro.IAvroSerializer<Set> m_setSerializer;

        public string Name { get; }

        private Set[] m_sets;
        public Set[] Sets
        {
            get { return m_sets; }
            private set { SetProperty ( ref m_sets, value ); }
        }

        public Server ( string name )
        {
            Utilis.Contract.AssertNotEmpty ( ( ) => name, name );
            Name = name;
            m_redis = StackExchange.Redis.ConnectionMultiplexer.Connect ( name );

            m_setSerializer = Microsoft.Hadoop.Avro.AvroSerializer.Create<Set> ( );
        }

        public void Load ( )
        {
            StackExchange.Redis.IDatabase db = m_redis.GetDatabase ( );
            var sets = db.ListRange ( SetKey );
            Sets =
                sets.Select ( o =>
                {
                    using ( var ms = new System.IO.MemoryStream ( ) )
                    {
                        return m_setSerializer.Deserialize ( ms );
                    }
                } ).ToArray ( );
        }

        public void Add ( Set s )
        {
            StackExchange.Redis.IDatabase db = m_redis.GetDatabase ( );
            db.ListRightPush ( SetKey, SerializeSet ( s ) );

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
    }

    public class FakeServer : IServer
    {
        public string Name { get; set; }
        public Set [] Sets { get; set; }

        public void Load ( )
        {

        }

        public void Add ( Set s )
        {
            Sets = Sets.Append ( s );
        }

        public void Remove ( Set s )
        {
            Sets = Sets.Where ( o => o.Id != s.Id ).ToArray ( );
        }
    }
}
