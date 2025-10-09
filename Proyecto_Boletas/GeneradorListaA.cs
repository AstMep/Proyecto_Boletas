using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Boletas
{
    internal class GeneradorListaA
    {

        Conexion conexion = new Conexion();

        // ** CLAVE ESCOLAR FIJA (EJEMPLO) **
        private const string ClaveEscolar = "29DPR0035Z";

        public void GenerarListasMensuales(string rutaArchivo, int idGrupo, string mes)
        {
            try
            {
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // 1. Configuración del PDF y Writer
                    Document doc = new Document(PageSize.LETTER.Rotate(), 20f, 20f, 20f, 20f);
                    PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                    doc.Open();

                    // ** SOLUCIÓN PARA ACENTOS Y Ñ (Fuente UTF-8)**
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

                    // Tipos de fuente
                    iTextSharp.text.Font fontEscuela = new iTextSharp.text.Font(bf, 16f, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                    iTextSharp.text.Font fontTitulo = new iTextSharp.text.Font(bf, 14f, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font fontSubtitulo = new iTextSharp.text.Font(bf, 10f);
                    iTextSharp.text.Font fontEncabezado = new iTextSharp.text.Font(bf, 8f, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font fontTexto = new iTextSharp.text.Font(bf, 9f);
                    iTextSharp.text.Font fontMini = new iTextSharp.text.Font(bf, 7f, iTextSharp.text.Font.BOLD);

                    // 2. Obtener datos del grupo y maestro (Lógica de BD)
                    string queryGrupo = "SELECT g.nombre_grupo, m.NombreMaestro, m.ApellidoPMaestro, m.ApellidoMMaestro, m.Correo_maestro " +
                                        "FROM grupo g JOIN maestro m ON g.id_maestro = m.id_maestro WHERE g.id_grupo = @idGrupo";
                    MySqlCommand cmdGrupo = new MySqlCommand(queryGrupo, conn);
                    cmdGrupo.Parameters.AddWithValue("@idGrupo", idGrupo);
                    MySqlDataReader readerGrupo = cmdGrupo.ExecuteReader();

                    string nombreGrupo = "";
                    string nombreMaestro = "";
                    string correoMaestro = "";
                    if (readerGrupo.Read())
                    {
                        nombreGrupo = readerGrupo["nombre_grupo"].ToString();
                        nombreMaestro = $"{readerGrupo["NombreMaestro"]} {readerGrupo["ApellidoPMaestro"]} {readerGrupo["ApellidoMMaestro"]}";
                        correoMaestro = readerGrupo["Correo_maestro"].ToString();
                    }
                    readerGrupo.Close();

                    // 3. ENCABEZADO: Logo + Datos de la Escuela

                    // Tabla para el logo y los datos de la escuela
                    PdfPTable headerTable = new PdfPTable(2);
                    headerTable.WidthPercentage = 100;
                    headerTable.SetWidths(new float[] { 0.15f, 0.85f }); // 15% para el logo, 85% para el texto

                    // 🖼 Celda del Logo (Izquierda)
                    string rutaLogo = @"C:\Users\Isis Astrid\source\repos\Proyecto_Boletas\Proyecto_Boletas\Resources\logo_escuela350.png";
                    if (File.Exists(rutaLogo))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaLogo);
                        logo.ScaleToFit(70f, 70f);
                        PdfPCell logoCell = new PdfPCell(logo, false);
                        logoCell.Border = 0;
                        logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        headerTable.AddCell(logoCell);
                    }
                    else
                    {
                        // Celda de texto si el logo no existe
                        headerTable.AddCell(new PdfPCell(new Phrase("LOGO NO ENCONTRADO", fontTexto)) { Border = 0 });
                    }

                    // 📝 Celda de Datos de la Escuela (Derecha)
                    Paragraph datosEscuela = new Paragraph();
                    datosEscuela.Alignment = Element.ALIGN_LEFT;
                    datosEscuela.Add(new Chunk("INSTITUTO MANUEL M. ACOSTA\n", fontEscuela));
                    datosEscuela.Add(new Chunk($"Clave Escolar: {ClaveEscolar}\n", fontSubtitulo));
                    datosEscuela.Add(new Chunk($"Grado y Grupo: {nombreGrupo}", fontSubtitulo));

                    PdfPCell dataCell = new PdfPCell(datosEscuela);
                    dataCell.Border = 0;
                    dataCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    headerTable.AddCell(dataCell);

                    doc.Add(headerTable);
                    doc.Add(new Paragraph("\n"));

                    // 4. Título Principal (Bajo los datos de la escuela)
                    Paragraph titulo = new Paragraph($"Control de Asistencia Mensual\nCiclo Escolar {DateTime.Now.Year}-{DateTime.Now.Year + 1}", fontTitulo)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(titulo);
                    doc.Add(new Paragraph("\n"));

                    // Información del mes y profesor
                    Paragraph info = new Paragraph();
                    info.Add(new Chunk($"Mes: {mes}", fontSubtitulo));
                    info.Add(new Chunk($"\t\t Profesor(a): {nombreMaestro}", fontSubtitulo));
                    info.Alignment = Element.ALIGN_LEFT;
                    doc.Add(info);
                    doc.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));

                    // 5. Generación Dinámica de Días

                    int anioActual = DateTime.Now.Year;
                    int numMes = DateTime.ParseExact(mes, "MMMM", new CultureInfo("es-ES")).Month;
                    DateTime primerDiaMes = new DateTime(anioActual, numMes, 1);

                    List<DateTime> diasHabiles = new List<DateTime>();
                    int diasEnMes = DateTime.DaysInMonth(anioActual, numMes);

                    for (int i = 0; i < diasEnMes; i++)
                    {
                        DateTime dia = primerDiaMes.AddDays(i);
                        if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday)
                        {
                            diasHabiles.Add(dia);
                        }
                    }

                    int totalColumnasAsistencia = diasHabiles.Count;
                    int totalColumnasTabla = 2 + totalColumnasAsistencia;

                    // 6. Crear Tabla Dinámica
                    PdfPTable tabla = new PdfPTable(totalColumnasTabla);
                    tabla.WidthPercentage = 100;
                    tabla.DefaultCell.MinimumHeight = 15f;

                    // Definir anchos relativos
                    float anchoAlumnos = 0.25f;
                    float anchoNo = 0.04f;
                    float anchoDia = (1f - anchoAlumnos - anchoNo) / totalColumnasAsistencia;

                    float[] widths = new float[totalColumnasTabla];
                    widths[0] = anchoNo;
                    widths[1] = anchoAlumnos;
                    for (int i = 2; i < totalColumnasTabla; i++)
                    {
                        widths[i] = anchoDia;
                    }
                    tabla.SetWidths(widths);

                    // Headers: Fila 1 (N°, Alumnos y Días de la Semana)
                    PdfPCell cellNo = new PdfPCell(new Phrase("N°", fontEncabezado)) { BackgroundColor = new BaseColor(255, 192, 0) };
                    cellNo.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellNo.Rowspan = 2;
                    tabla.AddCell(cellNo);

                    PdfPCell cellAlumnos = new PdfPCell(new Phrase("Alumnos", fontEncabezado)) { BackgroundColor = new BaseColor(255, 192, 0) };
                    cellAlumnos.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellAlumnos.Rowspan = 2;
                    tabla.AddCell(cellAlumnos);

                    // Días de la semana (L, M, M, J, V)
                    foreach (DateTime dia in diasHabiles)
                    {
                        string diaSemana = new CultureInfo("es-ES").DateTimeFormat.GetAbbreviatedDayName(dia.DayOfWeek).ToUpper()[0].ToString();
                        PdfPCell cellDia = new PdfPCell(new Phrase(diaSemana, fontMini)) { BackgroundColor = new BaseColor(255, 230, 153) };
                        cellDia.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla.AddCell(cellDia);
                    }

                    // Headers: Fila 2 (Fechas - Días del mes)
                    foreach (DateTime dia in diasHabiles)
                    {
                        PdfPCell cellFecha = new PdfPCell(new Phrase(dia.Day.ToString(), fontMini)) { BackgroundColor = new BaseColor(255, 230, 153) };
                        cellFecha.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla.AddCell(cellFecha);
                    }

                    // 7. Llenar Filas de Alumnos
                    // **NOTA:** La consulta SQL ya ordena alfabéticamente por Apellido Paterno, Materno y Nombre.
                    string queryAlumnos = "SELECT Nombre, ApellidoPaterno, ApellidoMaterno " +
                                          "FROM alumnos WHERE id_grupo = @idGrupo " +
                                          "ORDER BY ApellidoPaterno, ApellidoMaterno, Nombre";
                    MySqlCommand cmdAlumnos = new MySqlCommand(queryAlumnos, conn);
                    cmdAlumnos.Parameters.AddWithValue("@idGrupo", idGrupo);
                    MySqlDataReader readerAlumnos = cmdAlumnos.ExecuteReader();

                    int contador = 1;
                    while (readerAlumnos.Read())
                    {
                        // Celda N°
                        PdfPCell cellContador = new PdfPCell(new Phrase(contador.ToString(), fontTexto)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(255, 230, 153) };
                        tabla.AddCell(cellContador);

                        // Celda Alumno
                        string nombreCompleto = $"{readerAlumnos["ApellidoPaterno"]} {readerAlumnos["ApellidoMaterno"]} {readerAlumnos["Nombre"]}";
                        PdfPCell cellNombre = new PdfPCell(new Phrase(nombreCompleto, fontTexto)) { BackgroundColor = new BaseColor(255, 230, 153) };
                        tabla.AddCell(cellNombre);

                        // Celdas de asistencia
                        for (int i = 0; i < totalColumnasAsistencia; i++)
                        {
                            PdfPCell celdaAsis = new PdfPCell(new Phrase("", fontTexto));
                            celdaAsis.HorizontalAlignment = Element.ALIGN_CENTER;
                            celdaAsis.MinimumHeight = 15f;
                            tabla.AddCell(celdaAsis);
                        }

                        contador++;
                    }
                    readerAlumnos.Close();
                    doc.Add(tabla);

                    // 8. Datos del Maestro (Abajo de la tabla)
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph($"Profesor(a) Titular: {nombreMaestro}", fontSubtitulo));
                    doc.Add(new Paragraph($"Correo de Contacto: {correoMaestro}", fontTexto));

                    // Espacio para Notas de Faltas, Permisos, Enfermos, etc.
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("F= Faltas. \u00a0\u00a0\u00a0\u00a0\u00a0 P= Permisos. \u00a0\u00a0\u00a0\u00a0\u00a0 E= Enfermos. \u00a0\u00a0\u00a0\u00a0\u00a0 A= Asistencia.", fontEncabezado));

                    doc.Close();
                    conn.Close();

                    MessageBox.Show("PDF de Control de Asistencia generado con el nuevo diseño y datos escolares.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message + "\nVerifique la ruta del logo y la conexión a la base de datos.", "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
