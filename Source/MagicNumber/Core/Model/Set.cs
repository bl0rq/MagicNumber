using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using MagicNumber.Core.Extensions;
using Utilis.Extensions;

namespace MagicNumber.Core.Model
{
    public class Server
    {
        private const string SetKey = "Sets";
        private readonly StackExchange.Redis.ConnectionMultiplexer m_redis;
        private readonly Microsoft.Hadoop.Avro.IAvroSerializer<Set> m_setSerializer;

        public string Name { get; }
        public Set [] Sets { get; private set; }

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

    [DataContract]
    public class Set
    {
        [DataMember]
        [Key]
        public Guid Id { get; set; }

        [DataMember]
        [MaxLength ( 512 )]
        public string Name { get; set; }
        [DataMember]
        public int BlockSize { get; set; }
    }
}
