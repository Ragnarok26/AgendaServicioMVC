using System;

namespace AgendaServicio.Business.SAP
{
    public class SapManagement
    {
        public static string GuardarActividad(Entities.SAP.SapActivity actividad)
        {
            DataAccess.SAP.SapManagement activity = new DataAccess.SAP.SapManagement();
            return activity.saveOnSap(actividad);
        }

        public static string GuardarLlamadaServicio(Entities.SAP.SapServiceCall llamadaServicio)
        {
            DataAccess.SAP.SapManagement serviceCall = new DataAccess.SAP.SapManagement();
            return serviceCall.saveOnSap(llamadaServicio);
        }
    }
}
