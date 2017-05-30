using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AgendaServicioMVC.Models
{
    public class PlantillaCorreoLlamada
    {
        public static string GetHtml(AgendaServicio.Entities.Common.LlamadaServicio llamada, string Path)
        {
            string html = string.Empty;
            using (StreamReader sr = new StreamReader(Path))
            {
                html = sr.ReadToEnd();
            }
            if (llamada != null)
            {
                html = html.Replace("{Ingeniero}", llamada.Actividades.FirstOrDefault().Ingeniero.Name);
                html = html.Replace("{Modelo}", llamada.ItemCode);
                html = html.Replace("{NoSerie}", llamada.SN);
                html = html.Replace("{Descripcion}", llamada.Subject);
                html = html.Replace("{DocNum}", llamada.CallDocNum.ToString());
                html = html.Replace("{Cliente}", llamada.CustomerName.ToString());
                html = html.Replace("{DirCliente}", llamada.CustomerAddress.ToString());
                if (llamada.Actividades != null)
                {
                    if (llamada.Actividades.Count > 0)
                    {
                        if (llamada.Actividades.First().ActivityBeginTime.HasValue)
                        {
                            html = html.Replace("{FechaInicio}", llamada.Actividades.First().ActivityBeginTime.Value.ToString("dd/MM/yyyy"));
                        }
                    }
                }
            }
            return html;
        }

        public static List<string> GetImages(string Path)
        {
            try
            {
                return Directory.GetFiles(Path).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}