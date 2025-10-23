using System.Drawing;
using System.Drawing.Imaging; // Necesario para el MemoryStream
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Proyecto_Boletas
{
    internal class GeneradorListaProf
    {

        private Conexion conexion = new Conexion();
        private const string ClaveEscolar = "29DPR0035Z";

        // Paleta de Colores
        private static readonly BaseColor MoradoIntenso = new BaseColor(155, 89, 182);
        private static readonly BaseColor RosaClaro = new BaseColor(245, 183, 177);
        private static readonly BaseColor NegroFino = new BaseColor(0, 0, 0);

        public void GenerarLista(string rutaArchivo)
        {
            try
            {
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // 1. Configuración del PDF y Writer (Usamos orientación vertical para esta lista)
                    Document doc = new Document(PageSize.LETTER, 40f, 40f, 40f, 40f);
                    PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                    doc.Open();

                    // Carga de fuente (Solución para acentos y Ñ)
                    string pathArial = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    iTextSharp.text.pdf.BaseFont bf;

                    if (File.Exists(pathArial))
                    {
                        bf = iTextSharp.text.pdf.BaseFont.CreateFont(pathArial, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);
                    }
                    else
                    {
                        bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                    }

                    // Definición de fuentes
                    iTextSharp.text.Font fontEscuela = new iTextSharp.text.Font(bf, 16f, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                    iTextSharp.text.Font fontTitulo = new iTextSharp.text.Font(bf, 14f, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font fontSubtitulo = new iTextSharp.text.Font(bf, 10f, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font fontEncabezado = new iTextSharp.text.Font(bf, 10f, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font fontTexto = new iTextSharp.text.Font(bf, 10f, iTextSharp.text.Font.NORMAL);

                    // 2. ENCABEZADO: Logo + Membrete
                    PdfPTable headerTable = new PdfPTable(2);
                    headerTable.WidthPercentage = 100;
                    headerTable.SetWidths(new float[] { 0.15f, 0.85f });

                    // INICIO DE REEMPLAZO (Carga desde memoria)
                    iTextSharp.text.Image logo = null;

                    try
                    {
                        // 1. Carga la imagen desde los recursos internos del proyecto (NO NECESITA RUTA)
                        System.Drawing.Image imageFromResources = Proyecto_Boletas.Properties.Resources.logo_escuela350;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            // 2. Transfiere la imagen al stream de memoria
                            imageFromResources.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                            // 3. iTextSharp toma los datos de la imagen directamente desde el array de bytes en memoria
                            logo = iTextSharp.text.Image.GetInstance(ms.ToArray());
                        }
                    }
                    catch (Exception ex)
                    {
                        // Si la imagen no se encuentra en los recursos (p. ej., olvido al configurar)
                        Console.WriteLine("Error al cargar el logo desde recursos internos: " + ex.Message);
                    }

                    // Bloque que añade el logo a la tabla (ya no necesita verificar File.Exists)
                    if (logo != null)
                    {
                        logo.ScaleToFit(70f, 70f);
                        PdfPCell logoCell = new PdfPCell(logo, false);
                        logoCell.Border = 0;
                        logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        headerTable.AddCell(logoCell);
                    }
                    else
                    {
                        // Si la carga falló, inserta el texto de "LOGO FALTANTE"
                        headerTable.AddCell(new PdfPCell(new Phrase("LOGO FALTANTE", fontTexto)) { Border = 0 });
                    }
                    // FIN DE REEMPLAZO

                    // Celda de Datos de la Escuela
                    Paragraph datosEscuela = new Paragraph();
                    datosEscuela.Alignment = Element.ALIGN_LEFT;
                    datosEscuela.Add(new Chunk("INSTITUTO MANUEL M. ACOSTA\n\n", fontEscuela));
                    datosEscuela.Add(new Chunk($"Clave Escolar: {ClaveEscolar}\n", fontSubtitulo));

                    PdfPCell dataCell = new PdfPCell(datosEscuela);
                    dataCell.Border = 0;
                    dataCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    headerTable.AddCell(dataCell);

                    doc.Add(headerTable);
                    doc.Add(new Paragraph("\n"));

                    // 4. Título Principal
                    Paragraph titulo = new Paragraph("LISTA DE PROFESORES Y ASIGNACIONES", fontTitulo)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(titulo);
                    doc.Add(new Paragraph($"Fecha de Generación: {DateTime.Now.ToString("dd/MM/yyyy")}", fontSubtitulo) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph("\n"));

                    // 5. Consulta de Maestros y Grupos
                    string queryMaestros = @"
                        SELECT 
                            m.id_maestro,
                            CONCAT(m.NombreMaestro, ' ', m.ApellidoPMaestro, ' ', m.ApellidoMMaestro) AS NombreCompleto,
                            m.Correo_maestro,
                            g.nombre_grupo
                        FROM maestro m
                        LEFT JOIN grupo g ON g.id_maestro = m.id_maestro
                        ORDER BY g.nombre_grupo, m.ApellidoPMaestro";

                    MySqlCommand cmd = new MySqlCommand(queryMaestros, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // 6. Creación de la Tabla de Maestros
                    // Columnas: N°, Nombre Completo, Correo, Grupo Asignado
                    PdfPTable tabla = new PdfPTable(4);
                    tabla.WidthPercentage = 100;
                    tabla.DefaultCell.MinimumHeight = 20f;

                    // Definición de anchos de columna
                    float[] widths = new float[] { 0.05f, 0.40f, 0.35f, 0.20f };
                    tabla.SetWidths(widths);

                    // Headers
                    string[] headers = { "N°", "Nombre Completo", "Correo Electrónico", "Grado Asignado" };
                    foreach (string header in headers)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, fontEncabezado)) { BackgroundColor = MoradoIntenso };
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.BorderWidth = 1f; // Borde más marcado en el encabezado
                        cell.BorderColor = NegroFino;
                        tabla.AddCell(cell);
                    }

                    // Llenar Filas
                    int contador = 1;
                    while (reader.Read())
                    {
                        string nombreCompleto = reader["NombreCompleto"].ToString();
                        string correo = reader["Correo_maestro"].ToString();
                        string grupo = reader["nombre_grupo"] == DBNull.Value ? "Sin Asignar" : reader["nombre_grupo"].ToString();

                        // Celda N°
                        PdfPCell cellNo = new PdfPCell(new Phrase(contador.ToString(), fontTexto)) { BackgroundColor = RosaClaro };
                        cellNo.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellNo.BorderWidth = 0.5f; cellNo.BorderColor = NegroFino;
                        tabla.AddCell(cellNo);

                        // Celda Nombre
                        PdfPCell cellNombre = new PdfPCell(new Phrase(nombreCompleto, fontTexto)) { BackgroundColor = RosaClaro };
                        cellNombre.BorderWidth = 0.5f; cellNombre.BorderColor = NegroFino;
                        tabla.AddCell(cellNombre);

                        // Celda Correo
                        PdfPCell cellCorreo = new PdfPCell(new Phrase(correo, fontTexto)) { BackgroundColor = RosaClaro };
                        cellCorreo.BorderWidth = 0.5f; cellCorreo.BorderColor = NegroFino;
                        tabla.AddCell(cellCorreo);

                        // Celda Grupo
                        PdfPCell cellGrupo = new PdfPCell(new Phrase(grupo, fontTexto)) { BackgroundColor = RosaClaro };
                        cellGrupo.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellGrupo.BorderWidth = 0.5f; cellGrupo.BorderColor = NegroFino;
                        tabla.AddCell(cellGrupo);

                        contador++;
                    }
                    reader.Close();
                    doc.Add(tabla);

                    doc.Close();
                    conn.Close();

                    MessageBox.Show("Lista de profesores generada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF de profesores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
