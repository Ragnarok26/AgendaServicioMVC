using System;

namespace AgendaServicio.Entities.Tools
{
    public class SqlChangesResult : SqlResult
    {
        public int? RowsAffected { get; set; }

        public SqlChangesResult()
            : base()
        {
            RowsAffected = null;
        }
    }
}
