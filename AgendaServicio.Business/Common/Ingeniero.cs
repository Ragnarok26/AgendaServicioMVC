using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaServicio.Business.Common
{
    public class Ingeniero
    {
        public static Entities.Tools.SqlCollectionResult GetIngenieros(string ConnectionString, long? RoleId = null)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            Entities.Tools.SqlCollectionResult ingenieroResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult sucursalResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = null;
            if (RoleId.HasValue)
            {
                parameters = new List<Entities.Tools.SqlParam>();
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@IdRol",
                        Type = "BigInt",
                        Value = RoleId.Value,
                    }
                );
            }
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                //ingenieroResult = ingeniero.GetAllData();
                ingenieroResult = ingeniero.GetAllDataByRole(parameters);
                if (!ingenieroResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).Count; x++)
                    {
                        sucursalResult = Business.Common.Sucursal.GetSucursalPorIngeniero(ConnectionString,
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Id
                        );
                        if (!sucursalResult.HasError)
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = ((List<Entities.Common.Sucursal>)sucursalResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = string.Empty };
                        }
                    }
                }
                return ingenieroResult;
            }
            finally
            {
                ingeniero = null;
                sucursalResult = null;
                ingenieroResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetIngenierosPorNombre(string ConnectionString, string nombre, long? RoleId = null)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            Entities.Tools.SqlCollectionResult ingenieroResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult sucursalResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            if (nombre != null)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@name",
                        Type = "NVarChar",
                        Value = nombre,
                    }
                );
            }
            if (RoleId.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@IdRol",
                        Type = "BigInt",
                        Value = RoleId.Value,
                    }
                );
            }
            else
            {
                parameters = null;
            }
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                ingenieroResult = ingeniero.GetAllDataByName(parameters);
                if (!ingenieroResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).Count; x++)
                    {
                        sucursalResult = Business.Common.Sucursal.GetSucursalPorIngeniero(ConnectionString,
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Id
                        );
                        if (!sucursalResult.HasError)
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = ((List<Entities.Common.Sucursal>)sucursalResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = string.Empty };
                        }
                    }
                }
                return ingenieroResult;
            }
            finally
            {
                ingeniero = null;
                parameters = null;
                sucursalResult = null;
                ingenieroResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetIngenieroPorId(string ConnectionString, int EngineerId, long? RoleId = null)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            Entities.Tools.SqlCollectionResult ingenieroResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult sucursalResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@EngineerID",
                    Type = "Int",
                    Value = EngineerId,
                }
            );
            if (RoleId.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@IdRol",
                        Type = "BigInt",
                        Value = RoleId.Value,
                    }
                );
            }
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                ingenieroResult = ingeniero.GetAllDataByParameters(parameters);
                if (!ingenieroResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).Count; x++)
                    {
                        sucursalResult = Business.Common.Sucursal.GetSucursalPorIngeniero(ConnectionString,
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Id
                        );
                        if (!sucursalResult.HasError)
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = ((List<Entities.Common.Sucursal>)sucursalResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = string.Empty };
                        }
                    }
                }
                return ingenieroResult;
            }
            finally
            {
                ingeniero = null;
                parameters = null;
                sucursalResult = null;
                ingenieroResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetIngenierosCallCenter(string ConnectionString, DateTime date, long? RoleId = null)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@date",
                    Type = "Date",
                    Value = date,
                }
            );
            if (RoleId.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@IdRol",
                        Type = "BigInt",
                        Value = RoleId.Value,
                    }
                );
            }
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                return ingeniero.GetAllDataCallCenter(parameters);
            }
            finally
            {
                ingeniero = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetDisponibilidadIngenieros(string ConnectionString, DateTime date, int? CallId, long? RoleId = null)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            Entities.Tools.SqlCollectionResult ingenieroResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult sucursalResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@date",
                    Type = "Date",
                    Value = date,
                }
            );
            if (CallId.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@callID",
                        Type = "int",
                        Value = CallId.Value,
                    }
                );
            }
            if (RoleId.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@IdRol",
                        Type = "BigInt",
                        Value = RoleId.Value,
                    }
                );
            }
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                ingenieroResult = ingeniero.GetAllDataAvailability(parameters);
                if (!ingenieroResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).Count; x++)
                    {
                        sucursalResult = Business.Common.Sucursal.GetSucursalPorIngeniero(ConnectionString,
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Id
                        );
                        if (!sucursalResult.HasError)
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = ((List<Entities.Common.Sucursal>)sucursalResult.Collection).FirstOrDefault();
                            if (((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal == null)
                            {
                                ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = "Zin Asignar Sucursal" };
                            }
                        }
                        else
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = string.Empty };
                        }
                    }
                }
                return ingenieroResult;
            }
            finally
            {
                ingeniero = null;
                parameters = null;
                sucursalResult = null;
                ingenieroResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetIngenierosNoDisponiblesPorRango(string ConnectionString, DateTime date, DateTime? start, DateTime? end, long? RoleId = null)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            Entities.Tools.SqlCollectionResult ingenieroResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult sucursalResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@date",
                    Type = "Date",
                    Value = date,
                }
            );
            if (start.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@start",
                        Type = "Date",
                        Value = start.Value,
                    }
                );
            }
            if (end.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@end",
                        Type = "Date",
                        Value = end.Value,
                    }
                );
            }
            if (RoleId.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@IdRol",
                        Type = "BigInt",
                        Value = RoleId.Value,
                    }
                );
            }
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                ingenieroResult = ingeniero.GetAllDataByDateRange(parameters);
                if (!ingenieroResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).Count; x++)
                    {
                        sucursalResult = Business.Common.Sucursal.GetSucursalPorIngeniero(ConnectionString,
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Id
                        );
                        if (!sucursalResult.HasError)
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = ((List<Entities.Common.Sucursal>)sucursalResult.Collection).FirstOrDefault();
                            if (((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal == null)
                            {
                                ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = "Zin Asignar Sucursal" };
                            }
                        }
                        else
                        {
                            ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = string.Empty };
                        }
                    }
                }
                return ingenieroResult;
            }
            finally
            {
                ingeniero = null;
                parameters = null;
                sucursalResult = null;
                ingenieroResult = null;
            }
        }

        public static Entities.Tools.SqlChangesResult AddIngenieroCallCenter(string ConnectionString, DateTime date, int EngineerId)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            Entities.Tools.SqlChangesResult ingenieroResult = new Entities.Tools.SqlChangesResult();
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                return ingeniero.InsertCC(
                    new Entities.Common.Ingeniero()
                    {
                        Id = EngineerId,
                        DateCallCenter = date
                    }
                );
            }
            finally
            {
                ingeniero = null;
            }
        }

        public static Entities.Tools.SqlChangesResult AddRangoIngenierosCallCenter(string ConnectionString, DateTime date, int[] EngineersId)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            List<Entities.Common.Ingeniero> ingenieros = new List<Entities.Common.Ingeniero>();
            Entities.Tools.SqlChangesResult ingenieroResult = new Entities.Tools.SqlChangesResult();
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                if (EngineersId != null)
                {
                    for (int x = 0; x < EngineersId.Length; x++)
                    {
                        ingenieros.Add(
                            new Entities.Common.Ingeniero()
                            {
                                Id = EngineersId[x],
                                DateCallCenter = date
                            }
                        );
                    }
                }
                return ingeniero.InsertCC(ingenieros);
            }
            finally
            {
                ingeniero = null;
            }
        }

        public static Entities.Tools.SqlChangesResult RemoveIngenieroCallCenter(string ConnectionString, DateTime date, int EngineerId)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            Entities.Tools.SqlChangesResult ingenieroResult = new Entities.Tools.SqlChangesResult();
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                return ingeniero.DeleteCC(
                    new Entities.Common.Ingeniero()
                    {
                        Id = EngineerId,
                        DateCallCenter = date
                    }
                );
            }
            finally
            {
                ingeniero = null;
            }
        }

        public static Entities.Tools.SqlChangesResult UpdateIngenieroSucursal(string ConnectionString, string IdIngeniero, string IdSucursal)
        {
            DataAccess.Common.Ingeniero ingeniero = null;
            Entities.Tools.SqlChangesResult ingenieroResult = new Entities.Tools.SqlChangesResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            if (!string.IsNullOrEmpty(IdIngeniero))
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@Id",
                        Type = "NVarChar",
                        Value = IdIngeniero,
                    }
                );
            }
            if (!string.IsNullOrEmpty(IdSucursal))
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@IdSucursal",
                        Type = "NVarChar",
                        Value = IdSucursal,
                    }
                );
            }
            try
            {
                ingeniero = new DataAccess.Common.Ingeniero(ConnectionString);
                return ingeniero.UpdateIngSuc(parameters);
            }
            finally
            {
                ingeniero = null;
            }
        }
    }
}
