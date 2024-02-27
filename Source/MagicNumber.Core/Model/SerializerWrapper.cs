using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using SolTechnology.Avro;

namespace MagicNumber.Core.Model
{
    public interface ISerializer
    {
        T Deserialize<T> ( byte [] data );
        byte [] Serialize<T> ( T data );
    }

    public class AvroSerializer : ISerializer
    {
        public T Deserialize<T> ( byte [] data )
        {
            return AvroConvert.Deserialize<T> ( data );
        }

        public byte [] Serialize<T> ( T data )
        {
            return AvroConvert.Serialize ( data );
        }
    }

    //internal class ProtoSerializer : ISerializer
    //{
    //    public T Deserialize<T> ( byte [] data ) where T : IMessage, new()
    //    {
    //        var parser = new Google.Protobuf.MessageParser<T> ( ( ) => new T ( ) );
    //        return parser.ParseFrom ( data );
    //    }

    //    public byte [] Serialize<T> ( T data )
    //    {


    //    }
    //}

}