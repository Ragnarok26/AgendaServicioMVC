using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaServicio.Business.Common
{
    public class Rol
    {
        public static Entities.Tools.SqlCollectionResult GetRoles(string ConnectionString)
        {
            DataAccess.Common.Rol rol = null;
            try
            {
                rol = new DataAccess.Common.Rol(ConnectionString);
                return rol.GetAllData();
            }
            finally
            {
                rol = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetRolPorId(string ConnectionString, long RolId)
        {
            DataAccess.Common.Rol rol = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@RoleId",
                    Type = "BigInt",
                    Value = RolId,
                }
            );
            try
            {
                rol = new DataAccess.Common.Rol(ConnectionString);
                return rol.GetAllDataByParameters(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetRolPorUsuario(string ConnectionString, long UserId)
        {
            DataAccess.Common.Rol rol = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@UserId",
                    Type = "BigInt",
                    Value = UserId,
                }
            );
            try
            {
                rol = new DataAccess.Common.Rol(ConnectionString);
                return rol.GetAllDataByUserParameters(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetRolesPorNombre(string ConnectionString, string name)
        {
            DataAccess.Common.Rol rol = null;
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
                rol = new DataAccess.Common.Rol(ConnectionString);
                return rol.GetAllDataByName(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlChangesResult UpdateRol(string ConnectionString, long Id, string name)
        {
            DataAccess.Common.Rol rol = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
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
                rol = new DataAccess.Common.Rol(ConnectionString);
                return rol.UpdateRolSP(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlChangesResult RemoveRol(string ConnectionString, long Id)
        {
            DataAccess.Common.Rol rol = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = Id,
                }
            );
            try
            {
                rol = new DataAccess.Common.Rol(ConnectionString);
                return rol.DeleteRolSP(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }
    }
}
