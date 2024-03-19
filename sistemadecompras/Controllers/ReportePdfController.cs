using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using sistemadecompras.Models;

namespace Autentificacion.Controllers
{
    [Authorize]
    public class ReportePdfController : Controller
    {
        public ActionResult GenerarReporte()
        {
            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            writer.PageEvent = new HeaderFooter(); // Utiliza el mismo evento personalizado

            doc.Open();

            // Encabezado con "ValJos" a la izquierda y la fecha a la derecha
            PdfPTable valJosDateTable = new PdfPTable(2);
            valJosDateTable.WidthPercentage = 100;
            valJosDateTable.DefaultCell.Border = PdfPCell.NO_BORDER;

            // Celda para "ValJos"
            Phrase valJosPhrase = new Phrase("ValJos", new Font(Font.FontFamily.HELVETICA, 26));
            PdfPCell valJosCell = new PdfPCell(valJosPhrase);
            valJosCell.Border = PdfPCell.NO_BORDER;
            valJosCell.HorizontalAlignment = Element.ALIGN_LEFT;
            valJosDateTable.AddCell(valJosCell);

            // Celda para la fecha
            DateTime currentDate = DateTime.Now;
            Phrase datePhrase = new Phrase("Fecha del reporte: " + currentDate.ToShortDateString(), new Font(Font.FontFamily.HELVETICA, 12));
            PdfPCell dateCell = new PdfPCell(datePhrase);
            dateCell.Border = PdfPCell.NO_BORDER;
            dateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            valJosDateTable.AddCell(dateCell);

            // Agregar la fila con las dos celdas al documento
            doc.Add(valJosDateTable);

            // Agrega un espacio adicional
            doc.Add(new Paragraph(" "));

            // Agrega el letrero "Model: Seguridad"
            Phrase modelPhrase = new Phrase("Reportes de usuarios", new Font(Font.FontFamily.HELVETICA, 20));
            PdfPCell modelCell = new PdfPCell(modelPhrase);
            modelCell.Border = PdfPCell.NO_BORDER;
            modelCell.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPTable modelTable = new PdfPTable(1);
            modelTable.WidthPercentage = 100;
            modelTable.AddCell(modelCell);
            doc.Add(modelTable);

            // Agrega otro espacio adicional
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));

            // Agrega el nombre del usuario si está autenticado
            if (User.Identity.IsAuthenticated)
            {
                string userName = User.Identity.Name;
                Phrase userNamePhrase = new Phrase("Nombre del Usuario: " + userName, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
                PdfPCell userNameCell = new PdfPCell(userNamePhrase);
                userNameCell.Border = PdfPCell.NO_BORDER;
                userNameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                PdfPTable userNameTable = new PdfPTable(1);
                userNameTable.WidthPercentage = 100;
                userNameTable.AddCell(userNameCell);
                doc.Add(userNameTable);

                // Agrega un espacio adicional después del nombre de usuario
                doc.Add(new Paragraph(" "));
            }

            PdfPTable dataTable = new PdfPTable(4);
            dataTable.WidthPercentage = 100;
            dataTable.SpacingBefore = 40f;  // Ajusta el espacio vertical aquí
            dataTable.DefaultCell.Border = PdfPCell.NO_BORDER;
            dataTable.SetWidths(new float[] { 1.5f, 3f, 3f, 3f });
            dataTable.HorizontalAlignment = Element.ALIGN_CENTER;

            // Configuración de fuente y color para los encabezados
            Font headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE);

            // Encabezados de la tabla con fondo negro y texto blanco solo para la primera fila
            PdfPCell idHeaderCell = new PdfPCell(new Phrase("ID", headerFont));
            idHeaderCell.BackgroundColor = BaseColor.BLACK;

            PdfPCell userNameHeaderCell = new PdfPCell(new Phrase("Nombre de Usuario", headerFont));
            userNameHeaderCell.BackgroundColor = BaseColor.BLACK;

            PdfPCell passwordHeaderCell = new PdfPCell(new Phrase("Contraseña", headerFont));
            passwordHeaderCell.BackgroundColor = BaseColor.BLACK;

            PdfPCell rolHeaderCell = new PdfPCell(new Phrase("Rol", headerFont));
            rolHeaderCell.BackgroundColor = BaseColor.BLACK;

            dataTable.AddCell(idHeaderCell);
            dataTable.AddCell(userNameHeaderCell);
            dataTable.AddCell(passwordHeaderCell);
            dataTable.AddCell(rolHeaderCell);

            // Datos de la base de datos
            string connectionString = "Data Source=JOSSANTONY;Initial Catalog=SistemasComprasDB;Persist Security Info=True;User ID=SA;Password=787646;MultipleActiveResultSets=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT U.id, U.UserName, U.Password, R.strValor AS Rol FROM Usuarios U " +
                    "INNER JOIN Roles R ON U.idRol = R.id;"; // Personaliza tu consulta SQL
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader["id"].ToString();
                    string userName = reader["UserName"].ToString();
                    string password = EncryptPassword(reader["Password"].ToString());
                    string rol = reader["Rol"].ToString();

                    dataTable.AddCell(new PdfPCell(new Phrase(id, new Font(Font.FontFamily.HELVETICA, 12))));
                    dataTable.AddCell(new PdfPCell(new Phrase(userName, new Font(Font.FontFamily.HELVETICA, 12))));
                    dataTable.AddCell(new PdfPCell(new Phrase(password, new Font(Font.FontFamily.HELVETICA, 12))));
                    dataTable.AddCell(new PdfPCell(new Phrase(rol, new Font(Font.FontFamily.HELVETICA, 12))));
                }

                reader.Close();
            }

            doc.Add(dataTable);
            doc.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            // Devolver el archivo PDF como respuesta para su descarga
            return File(bytes, "application/pdf", "Reporte.pdf");
        }

        private string EncryptPassword(string password)
        {
            // Lógica de encriptación de contraseña
            // Convierte la cadena de contraseña en bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Crea un objeto de la clase SHA256 para calcular el hash de la contraseña
            using (SHA256 sha256 = SHA256.Create())
            {
                // Calcula el hash de la contraseña
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Convierte el hash en una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public class HeaderFooter : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                // Línea horizontal para delimitar el pie de página
                PdfContentByte contentByte = writer.DirectContent;
                contentByte.MoveTo(doc.LeftMargin, doc.BottomMargin);
                contentByte.LineTo(doc.PageSize.Width - doc.RightMargin, doc.BottomMargin);
                contentByte.Stroke();

                // Número de página centrado debajo de la línea
                PdfPTable pageNumberTable = new PdfPTable(1);
                pageNumberTable.DefaultCell.Border = 0;
                pageNumberTable.WidthPercentage = 100;
                pageNumberTable.TotalWidth = doc.PageSize.Width - doc.RightMargin;
                PdfPCell pageNumberCell = new PdfPCell(new Phrase(writer.PageNumber.ToString()));
                pageNumberCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pageNumberCell.Border = 0;
                pageNumberTable.AddCell(pageNumberCell);
                pageNumberTable.WriteSelectedRows(0, -1, doc.LeftMargin, doc.BottomMargin + 10, contentByte);
            }
        }
    }

}
