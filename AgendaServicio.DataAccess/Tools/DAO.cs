using System;
using System.Collections.Generic;

namespace AgendaServicio.DataAccess.Tools
{
    public abstract class DAO<T>
    {
        protected Tools.Connection connection;

        public DAO(string ConnectionString)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                connection = new Tools.Connection(ConnectionString);
            }
            else
            {
                connection = null;
            }
        }

        public abstract Entities.Tools.SqlCollectionResult GetAllData();

        public abstract Entities.Tools.SqlCollectionResult GetAllDataByParameters(List<Entities.Tools.SqlParam> parameters = null);

        public abstract Entities.Tools.SqlChangesResult Insert(T dataObject);

        public abstract Entities.Tools.SqlChangesResult Update(T dataObject);

        public abstract Entities.Tools.SqlChangesResult Delete(T dataobject);
    }
}
