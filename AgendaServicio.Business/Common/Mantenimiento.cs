using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AgendaServicio.Business.Common
{
    public class Mantenimiento
    {
        public static Entities.Tools.SqlCollectionResult GetMantenimientos(string ConnectionString)
        {
            DataAccess.Common.Mantenimiento mantenimiento = null;
            try
            {
                mantenimiento = new DataAccess.Common.Mantenimiento(ConnectionString);
                return mantenimiento.GetAllData();
            }
            finally
            {
                mantenimiento = null;
            }
        }

        public static Stream Exportar(List<AgendaServicio.Entities.Common.Mantenimiento> items)
        {
            ExcelPackage pkg = null;
            ExcelWorksheet sheet = null;
            MemoryStream ms = null;
            System.Reflection.MemberInfo[] member = null;
            try
            {
                pkg = new OfficeOpenXml.ExcelPackage();
                sheet = pkg.Workbook.Worksheets.Add("Hoja 1");
                member = typeof(AgendaServicio.Entities.Common.Mantenimiento).
                         GetProperties().
                         Where(v => v.Name != "Id"/* && v.Name != "TimbreUuid"
                         && v.Name != "Id" && v.Name != "OrdenCompraSap" && v.Name != "Status"*/).
                         Select(v => (System.Reflection.MemberInfo)v).
                         ToArray();
                sheet.Cells["A1"].LoadFromCollection(items, true, OfficeOpenXml.Table.TableStyles.None, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, member);
                ms = new MemoryStream();
                pkg.SaveAs(ms);
                ms.Position = 0;
                return ms;
            }
            catch
            {
                return ms;
            }
            finally
            {
                pkg = null;
                sheet = null;
                member = null;
            }
        }
    }
}
