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
    [DataContract]
    public class Set
    {
        [DataMember]
        [Key]
        public Guid Id { get; set; }

        [DataMember]
        [MaxLength(512)]
        public string Name { get; set; }

        [DataMember]
        public int BlockSize { get; set; }

        //NO DataMember, as this gets loaded separately!
        public int CurrentBlock { get; set; }
    }

    public class MySet
    {
        [DataMember]
        [Key]
        public Guid Id { get; set; }

        [DataMember]
        public int Block { get; set; }

        [DataMember]
        public uint Index { get; set; }

        [DataMember]
        public DateTimeOffset LastUpdated { get; set; }
    }
}
