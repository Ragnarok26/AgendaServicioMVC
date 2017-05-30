using System;
using System.Collections.Generic;

namespace AgendaServicio.Business.Common
{
    public class Sucursal
    {
        public static Entities.Tools.SqlCollectionResult GetSucursales(string ConnectionString)
        {
            DataAccess.Common.Sucursal sucursal = null;
            try
            {
                sucursal = new DataAccess.Common.Sucursal(ConnectionString);
                return sucursal.GetAllData();
            }
            finally
            {
                sucursal = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetSucursalPorLlamada(string ConnectionString, int CallId)
        {
            DataAccess.Common.Sucursal sucursal = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@callID",
                    Type = "Int",
                    Value = CallId,
                }
            );
            try
            {
                sucursal = new DataAccess.Common.Sucursal(ConnectionString);
                return sucursal.GetAllDataByParameters(parameters);
            }
            finally
            {
                sucursal = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetSucursalPorIngeniero(string ConnectionString, int EngineerId)
        {
            DataAccess.Common.Sucursal sucursal = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@EngineerID",
                    Type = "Int",
                    Value = EngineerId,
                }
            );
            try
            {
                sucursal = new DataAccess.Common.Sucursal(ConnectionString);
                return sucursal.GetAllDataByEngineer(parameters);
            }
            finally
            {
                sucursal = null;
                parameters = null;
            }
        }
    }
}
