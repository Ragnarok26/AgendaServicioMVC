using AgendaServicio.Entities.SAP;
using AgendaServicio.Entities.Tools;
using System;
using System.Reflection;

namespace AgendaServicio.DataAccess.SAP
{
    public class SapManagement
    {
        public string saveOnSap(SapActivity sapActividad)
        {
            long logId = 0;
            string empresa = "HaasCnc";

            logId = LogTools.RegisterLog(0, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "", DateTime.Now);

            SAPbobsCOM.Company company = null;
            try
            {
                lock (SapConnection.Instance)
                {
                    if (SapConnection.Instance.company.Connected)
                    {
                        LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "conexion existente", DateTime.Now);
                    }
                    else
                    {
                        if (SapConnection.Instance.Conectar())
                        {
                            LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "conexion exitosa", DateTime.Now);
                        }
                        else
                        {
                            string errorcmp = SapConnection.Instance.company.GetLastErrorDescription();
                            LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, errorcmp, DateTime.Now);
                            return errorcmp;
                        }
                    }

                    company = SapConnection.Instance.company;

                    #region datos de la actividad
                    SAPbobsCOM.CompanyService CompServ = company.GetCompanyService();
                    SAPbobsCOM.ActivitiesService ActivityServ = (SAPbobsCOM.ActivitiesService)CompServ.GetBusinessService(SAPbobsCOM.ServiceTypes.ActivitiesService);
                    SAPbobsCOM.ActivityParams Params = (SAPbobsCOM.ActivityParams)ActivityServ.GetDataInterface(SAPbobsCOM.ActivitiesServiceDataInterfaces.asActivityParams);
                    Params.ActivityCode = sapActividad.ActivityId;
                    SAPbobsCOM.Activity Act = null;
                    //int result;
                    try
                    {
                        Act = ActivityServ.GetActivity(Params);
                        Act.StartDate = sapActividad.FechaInicio;
                        Act.StartTime = sapActividad.FechaInicio;
                        Act.EndDuedate = sapActividad.FechaFin;
                        Act.EndTime = sapActividad.FechaFin;
                        Act.DurationType = SAPbobsCOM.BoDurations.du_Minuts;
                        TimeSpan span = sapActividad.FechaFin - sapActividad.FechaInicio;
                        Act.Duration = span.TotalMinutes;
                        Act.Details = sapActividad.Detalle;
                        if (sapActividad.IdIngeniero.HasValue)
                        {
                            //Act.HandledBy = 0;
                            Act.HandledByEmployee = sapActividad.IdIngeniero.Value;
                        }
                        ActivityServ.UpdateActivity(Act);
                    }
                    catch (Exception ex)
                    {
                        LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, DateTime.Now);
                        return "Error al actualizar la Actividad: " + ex.Message;
                    }
                    finally
                    {
                        if (Act != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(Act);
                            Act = null;
                        }
                        if (ActivityServ != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(ActivityServ);
                            ActivityServ = null;
                        }
                        if (Params != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(Params);
                            Params = null;
                        }
                    }
                    /*SAPbobsCOM.CompanyService CompServ = company.GetCompanyService();
                    SAPbobsCOM.ServiceCalls call = (SAPbobsCOM.ServiceCalls)company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oServiceCalls);
                    SAPbobsCOM.ActivitiesService ActivityServ = (SAPbobsCOM.ActivitiesService)CompServ.GetBusinessService(SAPbobsCOM.ServiceTypes.ActivitiesService);
                    SAPbobsCOM.ActivityParams Params = (SAPbobsCOM.ActivityParams)ActivityServ.GetDataInterface(SAPbobsCOM.ActivitiesServiceDataInterfaces.asActivityParams);
                    Params.ActivityCode = sapActividad.ActivityId;
                    SAPbobsCOM.Activity Act = null;
                    int result = 0;
                    int contAct = 0;
                    int totalAct = 0;
                    int aux = -1;
                    try
                    {
                        if (call.GetByKey(sapActividad.CallId))
                        {
                            Act = ActivityServ.GetActivity(Params);
                            aux = Act.HandledBy;
                            call.Activities.ActivityCode = sapActividad.ActivityId;
                            totalAct = call.Activities.Count;
                            //call.Activities.Delete();
                            //result = call.Update();
                            if (result != 0)
                            {
                                LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, company.GetLastErrorDescription(), DateTime.Now);
                                return "Error al actualizar la llamada: " + company.GetLastErrorDescription();
                            }
                            else
                            {
                                //Act = ActivityServ.GetActivity(Params);
                                Act.StartDate = sapActividad.FechaInicio;
                                Act.StartTime = sapActividad.FechaInicio;
                                Act.EndDuedate = sapActividad.FechaFin;
                                Act.EndTime = sapActividad.FechaFin;
                                Act.DurationType = SAPbobsCOM.BoDurations.du_Minuts;
                                TimeSpan span = sapActividad.FechaFin - sapActividad.FechaInicio;
                                Act.Duration = span.TotalMinutes;
                                Act.Details = sapActividad.Detalle;
                                if (sapActividad.IdIngeniero.HasValue)
                                {
                                    Act.HandledByEmployee = sapActividad.IdIngeniero.Value;
                                }
                                ActivityServ.UpdateActivity(Act);
                                call.Activities.ActivityCode = sapActividad.ActivityId;
                                call.Activities.Add();
                                result = call.Update();
                                if (result != 0)
                                {
                                    LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, company.GetLastErrorDescription() + " (Activity Id: " + sapActividad.ActivityId + ")", DateTime.Now);
                                    return "Error al actualizar la Actividad: " + company.GetLastErrorDescription();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, DateTime.Now);
                        return "Error al actualizar la Actividad: " + ex.Message;
                    }
                    finally
                    {
                        if (call != null)
                        {
                            contAct = call.Activities.Count;
                            if (contAct < totalAct)
                            {
                                call.Activities.ActivityCode = sapActividad.ActivityId;
                                call.Activities.Add();
                                result = call.Update();
                                if (result != 0)
                                {
                                    LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, company.GetLastErrorDescription() + " (Activity Id: " + sapActividad.ActivityId + ")", DateTime.Now);
                                }
                            }
                        }
                        if (Act != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(Act);
                            Act = null;
                        }
                        if (ActivityServ != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(ActivityServ);
                            ActivityServ = null;
                        }
                        if (Params != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(Params);
                            Params = null;
                        }
                        if (call != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(call);
                            call = null;
                        }
                    }*/
                    #endregion
                }
                LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "actividad Actualizada", DateTime.Now);
                return "0";
            }
            catch (Exception e)
            {
                LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, e.Message, DateTime.Now);
                return e.Message;
            }
            finally
            {
                /*if (company != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(company);
                    company = null;
                }*/
            }
        }

        public string saveOnSap(SapServiceCall sapLlamada)
        {
            long logId = 0;
            string empresa = "HaasCnc";

            logId = LogTools.RegisterLog(0, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "", DateTime.Now);

            SAPbobsCOM.Company company = null;
            SAPbobsCOM.ServiceCalls call = null;
            try
            {
                lock (SapConnection.Instance)
                {
                    if (SapConnection.Instance.company.Connected)
                    {
                        LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "conexion existente", DateTime.Now);
                    }
                    else
                    {
                        if (SapConnection.Instance.Conectar())
                        {
                            LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "conexion exitosa", DateTime.Now);
                        }
                        else
                        {
                            string errorcmp = SapConnection.Instance.company.GetLastErrorDescription();
                            LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, errorcmp, DateTime.Now);
                            return errorcmp;
                        }
                    }

                    company = SapConnection.Instance.company;

                    #region datos de la llamada
                    call = (SAPbobsCOM.ServiceCalls)company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oServiceCalls);
                    if (call.GetByKey(sapLlamada.CallId))
                    {
                        if (sapLlamada.IdTipoServicio.HasValue)
                        {
                            call.CallType = sapLlamada.IdTipoServicio.Value;
                        }
                        if (sapLlamada.IdEstatusServicio.HasValue)
                        {
                            if (sapLlamada.IdEstatusServicio.Value == (int)Entities.Common.EstadoServicio.ACTIVIDADES_TERMINADAS)
                            {
                                call.Status = 3;
                            }
                        }
                        call.Description = sapLlamada.Descripcion;
                        call.UserFields.Fields.Item("U_SucursalServicio").Value = sapLlamada.Sucursal;
                        int result = call.Update();
                        if (result != 0)
                        {
                            LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, company.GetLastErrorDescription(), DateTime.Now);
                            return "Error al actualizar la llamada: " + company.GetLastErrorDescription();
                        }
                    }
                    #endregion
                }
                LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "Llamada Actualizada", DateTime.Now);
                return "0";
            }
            catch (Exception e)
            {
                LogTools.RegisterLog(logId, "", empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, e.Message, DateTime.Now);
                return e.Message;
            }
            finally
            {
                if (call != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(call);
                    call = null;
                }
            }
        }
    }
}
