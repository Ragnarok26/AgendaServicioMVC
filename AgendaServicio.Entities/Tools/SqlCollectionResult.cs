using System;
using System.Collections;

namespace AgendaServicio.Entities.Tools
{
    public class SqlCollectionResult : SqlResult
    {
        public IEnumerable Collection { get; set; }

        public SqlCollectionResult()
            : base()
        {
            Collection = null;
        }
    }
}
