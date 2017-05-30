using System;
using System.Collections.Generic;

namespace AgendaServicio.DataAccess.Common
{
    public class Compania : Tools.DAO<Entities.Common.Compania>
    {
        public Compania(string ConnectionString)
            : base(ConnectionString)
        {
        }

        public override Entities.Tools.SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP("GetCompanies", null, typeof(Entities.Common.Compania));
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
            try
            {
                return connection.GetDataFromSP("GetCompanyById", parameters, typeof(Entities.Common.Compania));
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

        public Entities.Tools.SqlCollectionResult GetAllDataByName(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetCompaniesByName", parameters, typeof(Entities.Common.Compania));
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

        public override Entities.Tools.SqlChangesResult Insert(Entities.Common.Compania dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Update(Entities.Common.Compania dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Delete(Entities.Common.Compania dataobject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public Entities.Tools.SqlChangesResult UpdateCompanySP(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP("UpdateCompany", parameters);
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

        public Entities.Tools.SqlChangesResult DeleteCompanySP(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP("DeleteCompany", parameters);
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
