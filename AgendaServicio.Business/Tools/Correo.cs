using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace AgendaServicio.Business.Tools
{
    public class Correo
    {
        public static bool Enviar(string ConnectionString, string usuarioSys, int IdLlamada, string asunto, string para, string copia, string copiaOculta, string mensaje, string smtp, string usuario, string pass, string de, Dictionary<string, byte[]> imagenes)
        {
            //Entities.Tools.LogTools.RegisterLog(logId, "", "", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "sending mail...", DateTime.Now);

            try
            {
                //validamos datos
                if (string.IsNullOrEmpty(para))
                {
                    throw new Exception("Email de destinatario no puede estar vacío.");
                }

                //creamos el objeto mail
                SmtpClient client = new SmtpClient(smtp);
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(usuario, pass);
                MailMessage message = new MailMessage();

                //llenamos el remitente y el destinatario
                message.From = new MailAddress(de);

                foreach (string mailDir in para.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(mailDir.Trim());
                }
                if (!string.IsNullOrEmpty(copia))
                {
                    foreach (string mailDir in copia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.CC.Add(mailDir.Trim());
                    }
                }
                if (!string.IsNullOrEmpty(copiaOculta))
                {
                    foreach (string mailDir in copiaOculta.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.Bcc.Add(mailDir.Trim());
                    }
                }

                AlternateView av = AlternateView.CreateAlternateViewFromString(mensaje, null, System.Net.Mime.MediaTypeNames.Text.Html);
                
                if (imagenes.Count > 0)
                {
                    foreach (string key in imagenes.Keys)
                    {
                        MemoryStream ms = new MemoryStream(imagenes[key]);
                        LinkedResource headerImage = null;
                        if (key.EndsWith("jpg") || key.EndsWith("png"))
                        {
                            headerImage = new LinkedResource(ms, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                            headerImage.ContentType = new ContentType("image/jpg");
                            headerImage.ContentId = key.Replace(".jpg", "").Replace(".png", "");
                            av.LinkedResources.Add(headerImage);
                        }
                        else if (key.EndsWith("gif"))
                        {
                            headerImage = new LinkedResource(ms, System.Net.Mime.MediaTypeNames.Image.Gif);
                            headerImage.ContentType = new ContentType("image/gif");
                            headerImage.ContentId = key.Replace(".gif", "");
                            av.LinkedResources.Add(headerImage);
                        }
                    }
                }
                //configuramos el mensaje
                message.AlternateViews.Add(av);
                //message.Body = mensaje;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Subject = asunto;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                //enviamos el mail
                client.TargetName = "STARTTLS/smtp.office365.com";
                client.Send(message);
                AgendaServicio.DataAccess.Common.CorreoLlamada c = new DataAccess.Common.CorreoLlamada(ConnectionString);
                c.Insert(
                    new Entities.Common.CorreoLlamada()
                    {
                        IdLlamada = IdLlamada,
                        Usuario = usuarioSys
                    }
                );
                return true;
            }
            catch (Exception ex)
            {
                //DAL.LogTools.RegisterLog(logId, "", "", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex.Message, DateTime.Now);
            }
            return false;
            //return null;
        }
    }
}
