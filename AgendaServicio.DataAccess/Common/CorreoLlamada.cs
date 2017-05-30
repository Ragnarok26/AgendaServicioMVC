using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaServicio.DataAccess.Common
{
    public class CorreoLlamada : Tools.DAO<Entities.Common.CorreoLlamada>
    {
        public CorreoLlamada(string ConnectionString)
            : base(ConnectionString)
        {
        }

        public override Entities.Tools.SqlCollectionResult GetAllData()
        {
            return new Entities.Tools.SqlCollectionResult()
            {
                Collection = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlCollectionResult GetAllDataByParameters(List<Entities.Tools.SqlParam> parameters = null)
        {
            return new Entities.Tools.SqlCollectionResult()
            {
                Collection = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Insert(Entities.Common.CorreoLlamada dataObject)
        {
            try
            {
                return connection.ApplyChanges("INSERT INTO CorreoEnviado(IdLlamada, FechaEnvío, Usuario) VALUES(" + dataObject.IdLlamada.ToString() + ", GETDATE(), '" + dataObject.Usuario + "')");
            }
            catch (Exception ex)
            {
                return new Entities.Tools.SqlChangesResult()
                {
                    RowsAffected = null,
                    HasError = true,
                    Message = "Error: No se estableció la conexión (" + ex.Message + ")."
                };
            }
            finally
            {
                connection = null;
            }
        }

        public override Entities.Tools.SqlChangesResult Update(Entities.Common.CorreoLlamada dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Delete(Entities.Common.CorreoLlamada dataobject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }
    }
}
