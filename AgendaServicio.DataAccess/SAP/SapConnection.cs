using System;
using System.Configuration;

namespace AgendaServicio.DataAccess.SAP
{
    public sealed class SapConnection
    {
        private static volatile SapConnection instance;
        private static object syncRoot = new Object();
        public static string Pais = ConfigurationManager.AppSettings["HaasCnc.App.Country"];

        public SAPbobsCOM.Company company { get; private set; }
        public DateTime lastConnection { get; private set; }

        private SapConnection()
        {
            Conectar();
        }

        public bool Conectar()
        {
            bool connected = false;
            company = new SAPbobsCOM.Company();
            SAPbobsCOM.BoDataServerTypes DbType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
            if (!Enum.TryParse(ConfigurationManager.AppSettings[Pais + ".HaasCnc.DbServerType"], out DbType))
            {
                DbType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
            }
            company.DbServerType = DbType;
            company.CompanyDB = ConfigurationManager.AppSettings[Pais + ".HaasCnc.CompanyDB"];
            company.Server = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Server"];
            company.DbUserName = ConfigurationManager.AppSettings[Pais + ".HaasCnc.DbUserName"];
            company.DbPassword = ConfigurationManager.AppSettings[Pais + ".HaasCnc.DbPassword"];
            company.UserName = ConfigurationManager.AppSettings[Pais + ".HaasCnc.UserName"];
            company.Password = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Password"];
            company.LicenseServer = ConfigurationManager.AppSettings[Pais + ".HaasCnc.LicenseServer"];
            if (company.Connect() != 0)
            {
                string mensaje = company.GetLastErrorDescription();
                connected = false;
            }
            else
            {
                connected = true;
                this.lastConnection = DateTime.Now;
            }
            return connected;
        }

        public static SapConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SapConnection();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
