using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicNumber.Core.Extensions;

namespace MagicNumber.Core.Model
{
    public interface ILocalServer
    {
        MySet [] GetMySets ( );
        void AddSet ( MySet set );
        void UpdateSet ( MySet set );
    }

    [Utilis.RegisterSingletonService]
    public class LocalServer : ILocalServer
    {
        private const string LocalDataFile = @"MySets.db";

        public MySet [] GetMySets ( )
        {
            using ( var db = new LiteDB.LiteDatabase ( LocalDataFile ) )
            {
                var mySets = db.GetCollection<MySet> ( nameof ( MySet ) ).FindAll ( ).ToArray ( );
                return mySets;
            }
        }

        public void AddSet ( MySet set )
        {
            using ( var db = new LiteDB.LiteDatabase ( LocalDataFile ) )
            {
                db.GetCollection<MySet> ( nameof ( MySet ) ).Insert ( set );
            }
        }

        public void UpdateSet ( MySet set )
        {
            using ( var db = new LiteDB.LiteDatabase ( LocalDataFile ) )
            {
                db.GetCollection<MySet> ( nameof ( MySet ) ).Update ( set );
            }
        }
    }

    public class FakeLocalServer : ILocalServer
    {
        private MySet [] m_mySets = new MySet [] { };

        public MySet [] GetMySets ( )
        {
            return m_mySets;
        }

        public void AddSet ( MySet set )
        {
            if ( set.Id == Guid.Empty )
                throw new ArgumentException ( "Invalid SetId (empty)", nameof ( set ) );
            if ( m_mySets.Contains ( set ) || m_mySets.Any ( o => o.Id == set.Id ) )
                throw new ArgumentException ( "Set already added.", nameof ( set ) );

            m_mySets = m_mySets.Append ( set );
        }

        public void UpdateSet ( MySet set )
        {
            if ( set.Id == Guid.Empty )
                throw new ArgumentException ( "Invalid SetId (empty)", nameof ( set ) );
            if ( !m_mySets.Contains ( set ) || m_mySets.All ( o => o.Id != set.Id ) )
                throw new ArgumentException ( "Set not added.", nameof ( set ) );
        }
    }
}
