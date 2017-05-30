using System;
using System.Collections.Generic;

namespace AgendaServicio.DataAccess.Common
{
    public class Mantenimiento : Tools.DAO<Entities.Common.Mantenimiento>
    {
        public Mantenimiento(string ConnectionString)
            : base(ConnectionString)
        {
        }

        public override Entities.Tools.SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP("GetPreventiveMaintenanceWithNewMachineReport", null, typeof(Entities.Common.Mantenimiento));
            }
            catch (Exception ex)
            {
                return new Entities.Tools.SqlCollectionResult()
                {
                    Collection = null,
                    HasError = true,
                    Message = "Error: No se estableció la conexión (" + ex.Message + ")."
                };
            }
            finally
            {
                connection = null;
            }
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

        public override Entities.Tools.SqlChangesResult Insert(Entities.Common.Mantenimiento dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Update(Entities.Common.Mantenimiento dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Delete(Entities.Common.Mantenimiento dataobject)
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
