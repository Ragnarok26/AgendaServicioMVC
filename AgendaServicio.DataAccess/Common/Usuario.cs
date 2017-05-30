using System;
using System.Collections.Generic;

namespace AgendaServicio.DataAccess.Common
{
    public class Usuario : Tools.DAO<Entities.Common.Usuario>
    {
        public Usuario(string ConnectionString)
            : base(ConnectionString)
        {
        }

        public override Entities.Tools.SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP("GetUsers", null, typeof(Entities.Common.Usuario));
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
                return connection.GetDataFromSP("GetUserByCredential", parameters, typeof(Entities.Common.Usuario));
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
                return connection.GetDataFromSP("GetUsersByName", parameters, typeof(Entities.Common.Usuario));
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

        public Entities.Tools.SqlCollectionResult GetAllDataById(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetUserById", parameters, typeof(Entities.Common.Usuario));
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

        public override Entities.Tools.SqlChangesResult Insert(Entities.Common.Usuario dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Update(Entities.Common.Usuario dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Delete(Entities.Common.Usuario dataobject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public Entities.Tools.SqlChangesResult UpdateUserSP(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                //return connection.ApplyChangesFromSP("UpdateUser", parameters);
                return connection.ApplyChangesWithValueFromSP("UpdateUser", parameters);
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

        public Entities.Tools.SqlChangesResult DeleteUserSP(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP("DeleteUser", parameters);
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
