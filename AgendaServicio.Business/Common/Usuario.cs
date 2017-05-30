using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaServicio.Business.Common
{
    public class Usuario
    {
        public static Entities.Tools.SqlCollectionResult GetUsuarioPorId(string ConnectionString, Entities.Common.Usuario user)
        {
            DataAccess.Common.Usuario usuario = null;
            Entities.Tools.SqlCollectionResult usuarioResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult companiaResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult rolResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = user.Id,
                }
            );
            try
            {
                usuario = new DataAccess.Common.Usuario(ConnectionString);
                usuarioResult = usuario.GetAllDataById(parameters);
                if (!usuarioResult.HasError)
                {
                    if (((List<Entities.Common.Usuario>)usuarioResult.Collection).Count == 1)
                    {
                        rolResult = Business.Common.Rol.GetRolPorUsuario(ConnectionString,
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Id
                        );
                        if (!rolResult.HasError)
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Roles = ((List<Entities.Common.Rol>)rolResult.Collection);
                        }
                        else
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Roles = new List<Entities.Common.Rol>();
                        }
                        companiaResult = Business.Common.Compania.GetCompaniaPorId(ConnectionString,
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).IdCompany
                        );
                        if (!companiaResult.HasError)
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Compania = ((List<Entities.Common.Compania>)companiaResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Compania = new Entities.Common.Compania() { Id = 0, Name = string.Empty };
                        }
                    }
                    else
                    {
                        usuarioResult.Collection = null;
                    }
                }
                return usuarioResult;
            }
            finally
            {
                usuario = null;
                rolResult = null;
                parameters = null;
                usuarioResult = null;
                companiaResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetUsuariosPorNombre(string ConnectionString, Entities.Common.Usuario user)
        {
            DataAccess.Common.Usuario usuario = null;
            Entities.Tools.SqlCollectionResult usuarioResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult companiaResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult rolResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@name",
                    Type = "NVarChar",
                    Value = user.Name,
                }
            );
            try
            {
                usuario = new DataAccess.Common.Usuario(ConnectionString);
                usuarioResult = usuario.GetAllDataByName(parameters);
                if (!usuarioResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Common.Usuario>)usuarioResult.Collection).Count; x++)
                    {
                        rolResult = Business.Common.Rol.GetRolPorUsuario(ConnectionString,
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(x).Id
                        );
                        if (!rolResult.HasError)
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(x).Roles = ((List<Entities.Common.Rol>)rolResult.Collection);
                        }
                        else
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(x).Roles = new List<Entities.Common.Rol>();
                        }
                        companiaResult = Business.Common.Compania.GetCompaniaPorId(ConnectionString,
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(x).IdCompany
                        );
                        if (!companiaResult.HasError)
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(x).Compania = ((List<Entities.Common.Compania>)companiaResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(x).Compania = new Entities.Common.Compania() { Id = 0, Name = string.Empty };
                        }
                    }
                }
                return usuarioResult;
            }
            finally
            {
                usuario = null;
                rolResult = null;
                parameters = null;
                usuarioResult = null;
                companiaResult = null;
            }
        }

        public static Entities.Tools.SqlCollectionResult GetUsuarioPorCredencial(string ConnectionString, Entities.Common.Usuario user)
        {
            DataAccess.Common.Usuario usuario = null;
            Entities.Tools.SqlCollectionResult usuarioResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult companiaResult = new Entities.Tools.SqlCollectionResult();
            Entities.Tools.SqlCollectionResult rolResult = new Entities.Tools.SqlCollectionResult();
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@username",
                    Type = "NVarChar",
                    Value = user.UserName,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@password",
                    Type = "NVarChar",
                    Value = user.Password,
                }
            );
            try
            {
                usuario = new DataAccess.Common.Usuario(ConnectionString);
                usuarioResult = usuario.GetAllDataByParameters(parameters);
                if (!usuarioResult.HasError)
                {
                    if (((List<Entities.Common.Usuario>)usuarioResult.Collection).Count == 1)
                    {
                        rolResult = Business.Common.Rol.GetRolPorUsuario(ConnectionString,
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Id
                        );
                        if (!rolResult.HasError)
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Roles = ((List<Entities.Common.Rol>)rolResult.Collection);
                        }
                        else
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Roles = new List<Entities.Common.Rol>();
                        }
                        companiaResult = Business.Common.Compania.GetCompaniaPorId(ConnectionString,
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).IdCompany
                        );
                        if (!companiaResult.HasError)
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Compania = ((List<Entities.Common.Compania>)companiaResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Common.Usuario>)usuarioResult.Collection).ElementAt(0).Compania = new Entities.Common.Compania() { Id = 0, Name = string.Empty };
                        }
                    }
                    else
                    {
                        usuarioResult.Collection = null;
                    }
                }
                return usuarioResult;
            }
            finally
            {
                usuario = null;
                rolResult = null;
                parameters = null;
                usuarioResult = null;
                companiaResult = null;
            }
        }

        public static Entities.Tools.SqlChangesResult UpdateUsuario(string ConnectionString, Entities.Common.Usuario user)
        {
            DataAccess.Common.Usuario usuario = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = user.Id,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@name",
                    Type = "NVarChar",
                    Value = user.Name,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@firstName",
                    Type = "NVarChar",
                    Value = user.FirstName,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@lastName",
                    Type = "NVarChar",
                    Value = user.LastName,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@userName",
                    Type = "NVarChar",
                    Value = user.UserName,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@password",
                    Type = "NVarChar",
                    Value = user.Password,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@idCompany",
                    Type = "Int",
                    Value = user.IdCompany,
                }
            );
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@idRol",
                    Type = "BigInt",
                    Value = user.IdRol,
                }
            );
            try
            {
                usuario = new DataAccess.Common.Usuario(ConnectionString);
                return usuario.UpdateUserSP(parameters);
            }
            finally
            {
                usuario = null;
                parameters = null;
            }
        }

        public static Entities.Tools.SqlChangesResult RemoveUsuario(string ConnectionString, Entities.Common.Usuario user)
        {
            DataAccess.Common.Usuario usuario = null;
            List<Entities.Tools.SqlParam> parameters = new List<Entities.Tools.SqlParam>();
            parameters.Add(
                new Entities.Tools.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = user.Id,
                }
            );
            try
            {
                usuario = new DataAccess.Common.Usuario(ConnectionString);
                return usuario.DeleteUserSP(parameters);
            }
            finally
            {
                usuario = null;
                parameters = null;
            }
        }
    }
}
