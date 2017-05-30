using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaServicio.Business.Common
{
    public class Compania
    {
        public static Entities.Tools.SqlCollectionResult GetCompaniaPorId(string ConnectionString, int CompanyId)
        {
            DataAccess.Common.Compania compania = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@CompanyId",
                    Type = "Int",
                    Value = CompanyId,
                }
            );
            try
            {
                compania = new DataAccess.Common.Compania(ConnectionString);
                return compania.GetAllDataByParameters(parameters);
            }
            finally
            {
                compania = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetCompaniasPorNombre(string ConnectionString, string name)
        {
            DataAccess.Common.Compania compania = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@name",
                    Type = "NVarChar",
                    Value = name,
                }
            );
            try
            {
                compania = new DataAccess.Common.Compania(ConnectionString);
                return compania.GetAllDataByName(parameters);
            }
            finally
            {
                compania = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetCompanias(string ConnectionString)
        {
            DataAccess.Common.Compania compania = null;
            try
            {
                compania = new DataAccess.Common.Compania(ConnectionString);
                return compania.GetAllData();
            }
            finally
            {
                compania = null;
            }
        }

        public static Entities.Tools.SqlChangesResult UpdateCompania(string ConnectionString, int Id, string name)
        {
            DataAccess.Common.Compania compania = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@Id",
                    Type = "Int",
                    Value = Id,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@name",
                    Type = "NVarChar",
                    Value = name,
                }
            );
            try
            {
                compania = new DataAccess.Common.Compania(ConnectionString);
                return compania.UpdateCompanySP(parameters);
            }
            finally
            {
                compania = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlChangesResult RemoveCompania(string ConnectionString, int Id)
        {
            DataAccess.Common.Compania compania = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@Id",
                    Type = "Int",
                    Value = Id,
                }
            );
            try
            {
                compania = new DataAccess.Common.Compania(ConnectionString);
                return compania.DeleteCompanySP(parameters);
            }
            finally
            {
                compania = null;
                parameters = null;
            }
        }
    }
}
