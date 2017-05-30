using System;
using System.Collections.Generic;

namespace AgendaServicio.DataAccess.Common
{
    public class Actividad : Tools.DAO<Entities.Common.Actividad>
    {
        public Actividad(string ConnectionString)
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
            try
            {
                return connection.GetDataFromSP("GetActivitiesByServiceCall", parameters, typeof(Entities.Common.Actividad));
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

        public Entities.Tools.SqlCollectionResult GetAllDataScheduledByParameters(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetActivitiesByDateRange", parameters, typeof(Entities.Common.Actividad));
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

        public override Entities.Tools.SqlChangesResult Insert(Entities.Common.Actividad dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Update(Entities.Common.Actividad dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Delete(Entities.Common.Actividad dataobject)
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
