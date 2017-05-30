using System;

namespace AgendaServicio.Entities.Tools
{
    public class SqlParam
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Direction { get; set; }
        public object Value { get; set; }
    }
}
