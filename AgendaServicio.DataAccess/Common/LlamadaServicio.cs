using System;
using System.Collections.Generic;

namespace AgendaServicio.DataAccess.Common
{
    public class LlamadaServicio : Tools.DAO<Entities.Common.LlamadaServicio>
    {
        public LlamadaServicio(string ConnectionString)
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
                return connection.GetDataFromSP("GetServiceCallsByStatus", parameters, typeof(Entities.Common.LlamadaServicio));
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

        public Entities.Tools.SqlCollectionResult GetAllDataByDate(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetTotalServiceCallsByDate", parameters, typeof(Entities.Common.LlamadaServicio));
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
                return connection.GetDataFromSP("GetServiceCallsByDateRange", parameters, typeof(Entities.Common.LlamadaServicio));
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

        public Entities.Tools.SqlCollectionResult GetAllRescheduleDataByParameters(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetRescheduleServiceCalls", parameters, typeof(Entities.Common.LlamadaServicio));
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

        public Entities.Tools.SqlCollectionResult GetAllDataCallTypes()
        {
            try
            {
                return connection.GetDataFromSP("GetCallTypes", null, typeof(Entities.Common.LlamadaServicio));
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

        public override Entities.Tools.SqlChangesResult Insert(Entities.Common.LlamadaServicio dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Update(Entities.Common.LlamadaServicio dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Delete(Entities.Common.LlamadaServicio dataobject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public Entities.Tools.SqlChangesResult InsertEstatus(Entities.Common.LlamadaServicio dataobject)
        {
            try
            {
                return connection.ApplyChanges("INSERT INTO LlamadasEstatus(idLlamada, idEstatus) VALUES(" + dataobject.CallId.ToString() + ", " + dataobject.Estatus.ToString() + ")");
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
    }
}
