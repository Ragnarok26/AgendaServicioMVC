using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaServicio.Business.Common
{
    public class LlamadaServicio
    {
        public static Entities.Tools.SqlCollectionResult GetLlamadasServicio(string ConnectionString, int status, DateTime? date, int? callId, long? RoleId = null)
        {
            DataAccess.Common.LlamadaServicio llamada = null;
            Entities.Tools.SqlCollectionResult llamadaResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult actividadResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult sucursalResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@status",
                    Type = "Int",
                    Value = status,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@date",
                    Type = "Date",
                    Value = date,
                }
            );
            if (callId.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@callID",
                        Type = "Int",
                        Value = callId.Value,
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
                llamada = new DataAccess.Common.LlamadaServicio(ConnectionString);
                llamadaResult = llamada.GetAllDataByParameters(parameters);
                if (!llamadaResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).Count; x++)
                    {
                        ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).DescriptionEstatus = EstadoServicio.GetEstadoServicio().Where(v => v.Value == ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Estatus).Select(v => v.Key).FirstOrDefault();
                        if (parameters.Any(v => v.Name.Equals("@callID")))
                        {
                            parameters.Find(v => v.Name.Equals("@callID")).Value = ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).CallId;
                        }
                        else
                        {
                            parameters.Add(
                                new Entities.Tools.SqlParam()
                                {
                                    Name = "@callID",
                                    Type = "Int",
                                    Value = ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).CallId,
                                }
                            );
                        }
                        actividadResult = Business.Common.Actividad.GetActividadesPorLlamada(ConnectionString, parameters);
                        sucursalResult = Business.Common.Sucursal.GetSucursalPorLlamada(ConnectionString,
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).CallId
                        );
                        if (!actividadResult.HasError)
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Actividades = ((List<Entities.Common.Actividad>)actividadResult.Collection).OrderByDescending(v => v.TiempoEspera).ToList();
                        }
                        else
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Actividades = new List<Entities.Common.Actividad>();
                        }
                        if (!sucursalResult.HasError)
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Sucursal = ((List<Entities.Common.Sucursal>)sucursalResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = string.Empty };
                        }
                    }
                }
                return llamadaResult;
            }
            finally
            {
                llamada = null;
                parameters = null;
                llamadaResult = null;
                sucursalResult = null;
                actividadResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetLlamadasServicioPorFecha(string ConnectionString, DateTime? date, long? RoleId = null)
        {
            DataAccess.Common.LlamadaServicio llamada = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            if (date.HasValue)
            {
                parameters.Add(
                    new Entities.Tools.SqlParam()
                    {
                        Name = "@date",
                        Type = "Date",
                        Value = date,
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
                llamada = new DataAccess.Common.LlamadaServicio(ConnectionString);
                return llamada.GetAllDataByDate(parameters);
            }
            finally
            {
                llamada = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetLlamadasServicioAgendadas(string ConnectionString, DateTime date, DateTime? start, DateTime? end, long? RoleId = null)
        {
            DataAccess.Common.LlamadaServicio llamada = null;
            Entities.Tools.SqlCollectionResult llamadaResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult actividadResult = new Entities.Tools.SqlCollectionResult();
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
                llamada = new DataAccess.Common.LlamadaServicio(ConnectionString);
                llamadaResult = llamada.GetAllDataScheduledByParameters(parameters);
                if (!llamadaResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).Count; x++)
                    {
                        if (parameters.Any(v => v.Name.Equals("@callID")))
                        {
                            parameters.Find(v => v.Name.Equals("@callID")).Value = ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).CallId;
                        }
                        else
                        {
                            parameters.Add(
                                new Entities.Tools.SqlParam()
                                {
                                    Name = "@callID",
                                    Type = "Int",
                                    Value = ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).CallId,
                                }
                            );
                        }
                        actividadResult = Business.Common.Actividad.GetActividadesAgendadasPorLlamada(ConnectionString, parameters);
                        if (!actividadResult.HasError)
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Actividades = ((List<Entities.Common.Actividad>)actividadResult.Collection).ToList();
                        }
                        else
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Actividades = new List<Entities.Common.Actividad>();
                        }
                    }
                }
                return llamadaResult;
            }
            finally
            {
                llamada = null;
                parameters = null;
                llamadaResult = null;
                actividadResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetLlamadasServicioReprogramadas(string ConnectionString, long? RoleId = null)
        {
            DataAccess.Common.LlamadaServicio llamada = null;
            Entities.Tools.SqlCollectionResult llamadaResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult actividadResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult sucursalResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
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
                llamada = new DataAccess.Common.LlamadaServicio(ConnectionString);
                llamadaResult = llamada.GetAllRescheduleDataByParameters(parameters);
                if (!llamadaResult.HasError)
                {
                    parameters.Add(
                        new Entities.Tools.SqlParam()
                        {
                            Name = "@status",
                            Type = "Int",
                            Value = 3,
                        }
                    );
                    parameters.Add(
                        new Entities.Tools.SqlParam()
                        {
                            Name = "@date",
                            Type = "Date",
                            Value = DateTime.Today,
                        }
                    );
                    parameters.Add(
                        new Entities.Tools.SqlParam()
                        {
                            Name = "@callID",
                            Type = "Int",
                            Value = null,
                        }
                    );
                    for (int x = 0; x < ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).Count; x++)
                    {
                        //parameters.ElementAt(2).Value = ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).CallId;
                        parameters.Find(v => v.Name.Equals("@callID")).Value = ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).CallId;
                        actividadResult = Business.Common.Actividad.GetActividadesPorLlamada(ConnectionString, parameters);
                        sucursalResult = Business.Common.Sucursal.GetSucursalPorLlamada(ConnectionString,
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).CallId
                        );
                        if (!actividadResult.HasError)
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Actividades = ((List<Entities.Common.Actividad>)actividadResult.Collection).OrderByDescending(v => v.TiempoEspera).ToList();
                        }
                        else
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Actividades = new List<Entities.Common.Actividad>();
                        }
                        if (!sucursalResult.HasError)
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Sucursal = ((List<Entities.Common.Sucursal>)sucursalResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Common.LlamadaServicio>)llamadaResult.Collection).ElementAt(x).Sucursal = new Entities.Common.Sucursal() { Id = string.Empty, Name = string.Empty };
                        }
                    }
                }
                return llamadaResult;
            }
            finally
            {
                llamada = null;
                parameters = null;
                llamadaResult = null;
                sucursalResult = null;
                actividadResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetTiposLlamadasServicio(string ConnectionString)
        {
            DataAccess.Common.LlamadaServicio llamada = null;
            try
            {
                llamada = new DataAccess.Common.LlamadaServicio(ConnectionString);
                return llamada.GetAllDataCallTypes();
            }
            finally
            {
                llamada = null;
            }
        }

        public static Entities.Tools.SqlChangesResult AddLlamadaEstatus(string ConnectionString, int CallId, int IdEstatusServicio)
        {
            DataAccess.Common.LlamadaServicio llamada = null;
            Entities.Tools.SqlChangesResult llamadaResult = new Entities.Tools.SqlChangesResult();
            try
            {
                llamada = new DataAccess.Common.LlamadaServicio(ConnectionString);
                return llamada.InsertEstatus(
                    new Entities.Common.LlamadaServicio()
                    {
                        CallId = CallId,
                        Estatus = IdEstatusServicio
                    }
                );
            }
            finally
            {
                llamada = null;
            }
        }
    }
}
