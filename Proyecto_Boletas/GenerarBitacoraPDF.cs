using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Boletas
{
    internal class GenerarBitacoraPDF
    {
        private Conexion conexion = new Conexion();
        private const string NombreEscuela = "INSTITUTO MANUEL M. ACOSTA"; // Nombre actualizado
        private const string ClaveEscolar = "29DPR0035Z";

        private static readonly iTextSharp.text.BaseColor MoradoIntenso = new iTextSharp.text.BaseColor(155, 89, 182);
        private static readonly iTextSharp.text.BaseColor RosaClaro = new iTextSharp.text.BaseColor(245, 183, 177);
        private static readonly iTextSharp.text.BaseColor RosaPalido = new iTextSharp.text.BaseColor(250, 219, 216); // Para filas alternadas si se desea
        private static readonly iTextSharp.text.BaseColor NegroFino = new iTextSharp.text.BaseColor(0, 0, 0);

        // NOTA: Los colores anteriores (ColorPrincipal, ColorClaro) ya no se usan.

        private readonly string query = @"SELECT
                                            b.BitacoraID,
                                            u.Nombre AS Usuario,
                                            u.Rol,
                                            b.Accion,
                                            b.FechaAccion
                                          FROM bitacora b
                                          INNER JOIN usuarios u ON b.UsuarioID = u.UsuarioID
                                          WHERE
                                            (@Rol = 'Todos' OR u.Rol = @Rol)
                                            AND (@Mes = 0 OR MONTH(b.FechaAccion) = @Mes)
                                          ORDER BY b.FechaAccion DESC;";

        public void GenerarPDF(string rolSeleccionado, int mesSeleccionado, string rutaLogo) // rutaLogo ya no se usará
        {
            iTextSharp.text.Document doc = null;
            DataTable dt = new DataTable();

            // --- 1. Obtención de datos (sin cambios) ---
            try
            {
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Rol", rolSeleccionado);
                    cmd.Parameters.AddWithValue("@Mes", mesSeleccionado);
                    conn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros con los filtros seleccionados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al consultar la base de datos: " + ex.Message, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- 2. Configuración de PDF y Fuentes ---
            try
            {
                doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 50f, 50f, 80f, 50f);
                string rutaPDF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Bitacora.pdf");
                PdfWriter.GetInstance(doc, new FileStream(rutaPDF, FileMode.Create));
                doc.Open();

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
                iTextSharp.text.Font fontEscuela = new iTextSharp.text.Font(bf, 16f, iTextSharp.text.Font.BOLD, MoradoIntenso); // Usando MoradoIntenso
                iTextSharp.text.Font fontTitulo = new iTextSharp.text.Font(bf, 14f, iTextSharp.text.Font.BOLD, NegroFino);
                iTextSharp.text.Font fontSubtitulo = new iTextSharp.text.Font(bf, 10f, iTextSharp.text.Font.BOLD, NegroFino);
                iTextSharp.text.Font fontEncabezado = new iTextSharp.text.Font(bf, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE); // Blanco sobre MoradoIntenso
                iTextSharp.text.Font fontTexto = new iTextSharp.text.Font(bf, 9f, iTextSharp.text.Font.NORMAL, NegroFino);

                // --- 3. ENCABEZADO: Logo + Membrete (ESTRUCTURA SOLICITADA) ---
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 0.15f, 0.85f });

                // ****************************************************
                // CAMBIO 2: Lógica del logo y ruta fija
                // ****************************************************
                string rutaLogoFija = @"C:\Users\Isis Astrid\source\repos\Proyecto_Boletas\Proyecto_Boletas\Resources\logo_escuela350.png";

                if (File.Exists(rutaLogoFija))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaLogoFija);
                    logo.ScaleToFit(70f, 70f);
                    PdfPCell logoCell = new PdfPCell(logo, false);
                    logoCell.Border = 0;
                    logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    headerTable.AddCell(logoCell);
                }
                else
                {
                    headerTable.AddCell(new PdfPCell(new Phrase("LOGO FALTANTE", fontTexto)) { Border = 0 });
                }

                // Celda de Datos de la Escuela
                Paragraph datosEscuela = new Paragraph();
                datosEscuela.Alignment = Element.ALIGN_LEFT;
                datosEscuela.Add(new iTextSharp.text.Chunk("INSTITUTO MANUEL M. ACOSTA\n\n", fontEscuela)); // Texto fijo
                datosEscuela.Add(new iTextSharp.text.Chunk($"Clave Escolar: {ClaveEscolar}\n\n", fontSubtitulo)); // Clave

                // NOTA: Se omite "Grado: {nombreGrupo}" porque esa información no existe en la bitácora.
                // Se mantiene la estructura para los filtros:
                datosEscuela.Add(new iTextSharp.text.Chunk($"Filtros Aplicados: Rol={rolSeleccionado}, Mes={mesSeleccionado}", fontSubtitulo));


                PdfPCell dataCell = new PdfPCell(datosEscuela);
                dataCell.Border = 0;
                dataCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                headerTable.AddCell(dataCell);

                doc.Add(headerTable);
                doc.Add(new Paragraph("\n"));

                // --- 4. Título Principal y Fecha ---
                Paragraph titulo = new Paragraph("BITÁCORA DE ACTIVIDADES", fontTitulo)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(titulo);
                doc.Add(new Paragraph($"Fecha de Impresión: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", fontSubtitulo) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("\n"));

                // --- 5. Creación de la Tabla de Bitácora ---
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.WidthPercentage = 100;
                table.DefaultCell.MinimumHeight = 20f;

                float[] widths = { 0.8f, 1.5f, 1f, 3.5f, 1.5f };
                table.SetWidths(widths);

                // Headers
                string[] nombresColumnas = { "ID", "Usuario", "Rol", "Acción Realizada", "Fecha y Hora" };
                foreach (string nombre in nombresColumnas)
                {
                    // CAMBIO 3: Usando MoradoIntenso para el encabezado
                    PdfPCell cell = new PdfPCell(new Phrase(nombre, fontEncabezado)) { BackgroundColor = MoradoIntenso };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.BorderWidth = 1f;
                    cell.BorderColor = NegroFino;
                    table.AddCell(cell);
                }

                // Llenar Filas
                foreach (DataRow r in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string valorCelda = r[i].ToString();

                        if (dt.Columns[i].ColumnName == "FechaAccion" && DateTime.TryParse(valorCelda, out DateTime fecha))
                        {
                            valorCelda = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                        }

                        // CAMBIO 4: Usando RosaPalido para las celdas
                        PdfPCell cell = new PdfPCell(new Phrase(valorCelda, fontTexto)) { BackgroundColor = RosaPalido };
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.BorderWidth = 0.5f; cell.BorderColor = NegroFino;
                        cell.Padding = 3;
                        table.AddCell(cell);
                    }
                }

                doc.Add(table);
                doc.Close();

                MessageBox.Show("Bitácora generada exitosamente en el escritorio: " + rutaPDF, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (doc != null && doc.IsOpen())
                {
                    doc.Close();
                }
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error de PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
