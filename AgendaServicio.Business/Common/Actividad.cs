using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaServicio.Business.Common
{
    public class Actividad
    {
        public static Entities.Tools.SqlCollectionResult GetActividadesPorLlamada(string ConnectionString, List<Entities.Tools.SqlParam> parameters)
        {
            DataAccess.Common.Actividad actividad = null;
            Entities.Tools.SqlCollectionResult actividadResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult ingenieroResult = new Entities.Tools.SqlCollectionResult();
            try
            {
                actividad = new DataAccess.Common.Actividad(ConnectionString);
                actividadResult = actividad.GetAllDataByParameters(parameters);
                if (!actividadResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.Actividad>)actividadResult.Collection).Count; x++)
                    {
                        if (((List<Entities.Common.Actividad>)actividadResult.Collection).ElementAt(x).EngineerId.HasValue)
                        {
                            ingenieroResult = Business.Common.Ingeniero.GetIngenieroPorId(ConnectionString,
                                ((List<Entities.Common.Actividad>)actividadResult.Collection).ElementAt(x).EngineerId.Value
                            );
                            if (!actividadResult.HasError)
                            {
                                ((List<Entities.Common.Actividad>)actividadResult.Collection).ElementAt(x).Ingeniero = ((List<Entities.Common.Ingeniero>)ingenieroResult.Collection).FirstOrDefault();
                                if (((List<Entities.Common.Actividad>)actividadResult.Collection).ElementAt(x).Ingeniero == null)
                                {
                                    ((List<Entities.Common.Actividad>)actividadResult.Collection).ElementAt(x).Ingeniero = new Entities.Common.Ingeniero() { Id = 0, Name = string.Empty, CallCenter = false, Vacaciones = string.Empty, VacacionesLetra = string.Empty, Servicio = false };
                                }
                            }
                            else
                            {
                                ((List<Entities.Common.Actividad>)actividadResult.Collection).ElementAt(x).Ingeniero = new Entities.Common.Ingeniero() { Id = 0, Name = string.Empty, CallCenter = false, Vacaciones = string.Empty, VacacionesLetra = string.Empty, Servicio = false };
                            }
                        }
                    }
                }
                return actividadResult;
            }
            finally
            {
                actividad = null;
                parameters = null;
                actividadResult = null;
                ingenieroResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetActividadesAgendadasPorLlamada(string ConnectionString, List<Entities.Tools.SqlParam> parameters)
        {
            DataAccess.Common.Actividad actividad = null;
            try
            {
                actividad = new DataAccess.Common.Actividad(ConnectionString);
                return actividad.GetAllDataScheduledByParameters(parameters);
            }
            finally
            {
                actividad = null;
                parameters = null;
            }
        }
    }
}
