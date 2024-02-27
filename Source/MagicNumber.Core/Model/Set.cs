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
    public partial class Set
    {
        //[DataMember]
        //[Key]
        //public Guid Id { get; set; }

        //[DataMember]
        //[MaxLength (512)]
        //public string Name { get; set; }

        //[DataMember]
        //public int BlockSize { get; set; }

        //NO DataMember, as this gets loaded separately!
        [IgnoreDataMember]
        public int CurrentBlock { get; set; }

        [IgnoreDataMember]
        public Lazy<Guid> IdAsGuid { get; private set; }

        partial void OnConstruction ( )
        {
            IdAsGuid = new Lazy<Guid> ( ( ) => Guid.Parse ( Id ) );
        }

        public void EnsureLazy ( )
        {
            if ( IdAsGuid == null )
                IdAsGuid = new Lazy<Guid> ( ( ) => Guid.Parse ( Id ) );
        }
    }

    //[DataContract]
    public partial class MySet
    {
        public Lazy<Guid> IdAsGuid { get; private set; }

        partial void OnConstruction ( )
        {
            IdAsGuid = new Lazy<Guid> ( ( ) => Guid.Parse ( Id ) );
        }
    }
    //    [DataMember]
    //    [Key]
    //    public Guid Id { get; set; }

    //    [DataMember]
    //    public int Block { get; set; }

    //    [DataMember]
    //    public uint Index { get; set; }

    //    [DataMember]
    //    public DateTimeOffset LastUpdated { get; set; }
    //}
}
