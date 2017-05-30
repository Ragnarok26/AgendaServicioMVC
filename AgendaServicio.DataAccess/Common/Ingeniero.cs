using System;
using System.Collections.Generic;

namespace AgendaServicio.DataAccess.Common
{
    public class Ingeniero : Tools.DAO<Entities.Common.Ingeniero>
    {
        public Ingeniero(string ConnectionString)
            : base(ConnectionString)
        {
        }

        public override Entities.Tools.SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP("GetEngineers", null, typeof(Entities.Common.Ingeniero));
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

        public Entities.Tools.SqlCollectionResult GetAllDataByRole(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetEngineers", parameters, typeof(Entities.Common.Ingeniero));
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
                return connection.GetDataFromSP("GetEngineerById", parameters, typeof(Entities.Common.Ingeniero));
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
                return connection.GetDataFromSP("GetEngineersByName", parameters, typeof(Entities.Common.Ingeniero));
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

        public Entities.Tools.SqlCollectionResult GetAllDataCallCenter(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetEngineersCallCenter", parameters, typeof(Entities.Common.Ingeniero));
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

        public Entities.Tools.SqlCollectionResult GetAllDataAvailability(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetEngineersAvailability", parameters, typeof(Entities.Common.Ingeniero));
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

        public Entities.Tools.SqlCollectionResult GetAllDataByDateRange(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP("GetEngineersVacationsByDateRange", parameters, typeof(Entities.Common.Ingeniero));
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

        public override Entities.Tools.SqlChangesResult Insert(Entities.Common.Ingeniero dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Update(Entities.Common.Ingeniero dataObject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public override Entities.Tools.SqlChangesResult Delete(Entities.Common.Ingeniero dataobject)
        {
            return new Entities.Tools.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        public Entities.Tools.SqlChangesResult InsertCC(Entities.Common.Ingeniero dataobject)
        {
            try
            {
                return connection.ApplyChanges("INSERT INTO EngineerCCenter(IdIngeniero, Date) VALUES(" + dataobject.Id.ToString() + ", '" + dataobject.DateCallCenter.ToString("yyyy-MM-dd") + "')");
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

        public Entities.Tools.SqlChangesResult InsertCC(List<Entities.Common.Ingeniero> dataobject)
        {
            string query = "INSERT INTO EngineerCCenter(IdIngeniero, Date) VALUES";
            try
            {
                if (dataobject != null)
                {
                    if (dataobject.Count > 0)
                    {
                        for (int x = 0; x < dataobject.Count; x++)
                        {
                            query += "(" + dataobject[x].Id.ToString() + ", '" + dataobject[x].DateCallCenter.ToString("yyyy-MM-dd") + "')";
                            if (x < dataobject.Count - 1)
                            {
                                query += ",";
                            }
                        }
                        return connection.ApplyChanges(query);
                    }
                }
                return new Entities.Tools.SqlChangesResult()
                {
                    RowsAffected = null,
                    HasError = true,
                    Message = "Error: No existen elementos a agregar."
                };
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
                query = null;
            }
        }

        public Entities.Tools.SqlChangesResult DeleteCC(Entities.Common.Ingeniero dataobject)
        {
            try
            {
                return connection.ApplyChanges("DELETE FROM EngineerCCenter WHERE IdIngeniero = " + dataobject.Id.ToString() + " AND Date = '" + dataobject.DateCallCenter.ToString("yyyy-MM-dd") + "'");
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

        public Entities.Tools.SqlChangesResult UpdateIngSuc(List<Entities.Tools.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP("UpdateEngineer", parameters);
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
