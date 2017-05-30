using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AgendaServicio.Business.Common
{
    public class EstadoServicio
    {
        public static Dictionary<string, int> GetEstadoServicio()
        {
            Dictionary<string, int> EstadoServicio = new Dictionary<string, int>();
            Array itemValues = Enum.GetValues(typeof(Entities.Common.EstadoServicio));
            foreach (int val in itemValues)
            {
                Entities.Common.EstadoServicio choice;
                if (Enum.TryParse(val.ToString(), out choice))
                {
                    int value = (int)choice;
                    string description = GetEnumDescription((Entities.Common.EstadoServicio)value);
                    EstadoServicio.Add(description, value);
                }
            }
            return EstadoServicio;
        }

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = 
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }
    }
}
