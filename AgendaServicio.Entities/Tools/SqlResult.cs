using System;

namespace AgendaServicio.Entities.Tools
{
    public abstract class SqlResult
    {
        public bool HasError { get; set; }
        public string Message { get; set; }

        public SqlResult()
        {
            HasError = false;
            Message = string.Empty;
        }
    }
}
