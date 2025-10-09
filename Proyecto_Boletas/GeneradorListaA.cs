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
            private const string ClaveEscolar = "29DPR0035Z";

            // Paleta de Colores
            private static readonly BaseColor MoradoIntenso = new BaseColor(155, 89, 182);
            private static readonly BaseColor RosaClaro = new BaseColor(245, 183, 177);
            private static readonly BaseColor RosaPalido = new BaseColor(250, 219, 216);
            private static readonly BaseColor NegroFino = new BaseColor(0, 0, 0);

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
                        iTextSharp.text.Font fontEncabezado = new iTextSharp.text.Font(bf, 8f, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font fontTexto = new iTextSharp.text.Font(bf, 9f, iTextSharp.text.Font.NORMAL);
                        iTextSharp.text.Font fontMini = new iTextSharp.text.Font(bf, 7f, iTextSharp.text.Font.BOLD);

                        // 2. Obtener datos del grupo y maestro
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

                        // 3. ENCABEZADO: Logo + Membrete
                        PdfPTable headerTable = new PdfPTable(2);
                        headerTable.WidthPercentage = 100;
                        headerTable.SetWidths(new float[] { 0.15f, 0.85f });

                        // Celda del Logo
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
                            headerTable.AddCell(new PdfPCell(new Phrase("LOGO FALTANTE", fontTexto)) { Border = 0 });
                        }

                        // Celda de Datos de la Escuela
                        Paragraph datosEscuela = new Paragraph();
                        datosEscuela.Alignment = Element.ALIGN_LEFT;
                        datosEscuela.Add(new Chunk("INSTITUTO MANUEL M. ACOSTA\n\n", fontEscuela));
                        datosEscuela.Add(new Chunk($"Clave Escolar: {ClaveEscolar}\n\n", fontSubtitulo));
                        datosEscuela.Add(new Chunk($"Grado: {nombreGrupo}", fontSubtitulo));

                        PdfPCell dataCell = new PdfPCell(datosEscuela);
                        dataCell.Border = 0;
                        dataCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        headerTable.AddCell(dataCell);

                        doc.Add(headerTable);
                        doc.Add(new Paragraph("\n"));

                        // 4. Título Principal y Maestro
                        Paragraph titulo = new Paragraph($"CONTROL DE ASISTENCIA MENSUAL", fontTitulo)
                        {
                            Alignment = Element.ALIGN_CENTER
                        };
                        doc.Add(titulo);
                        doc.Add(new Paragraph($"Ciclo Escolar {DateTime.Now.Year}-{DateTime.Now.Year + 1}", fontSubtitulo) { Alignment = Element.ALIGN_CENTER });
                        doc.Add(new Paragraph("\n"));

                        // Información del mes y profesor
                        Paragraph info = new Paragraph();
                        info.Add(new Chunk($"Mes: {mes} \u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0\u00a0", fontSubtitulo));
                        info.Add(new Chunk($"Profesor(a): {nombreMaestro}", fontSubtitulo));
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
                        int totalColumnasTabla = 1 + 1 + 1 + 1 + totalColumnasAsistencia; // N°, CURP, G, Alumno + días

                        // 6. Crear Tabla Dinámica
                        PdfPTable tabla = new PdfPTable(totalColumnasTabla);
                        tabla.WidthPercentage = 100;
                        tabla.DefaultCell.MinimumHeight = 15f;

                        // *** AJUSTE PARA BORDES FINOS Y SOLUCIÓN DE ERRORES ***
                        tabla.DefaultCell.BorderWidth = 0f; // Eliminamos bordes de celda por defecto

                        // Nota: Las líneas problemáticas (tabla.Border, BorderWidth, BorderColor) han sido ELIMINADAS
                        // ya que la configuración individual de cada celda ya provee los bordes finos deseados.

                        // Definir anchos relativos (N°, CURP, G, Alumno, Días)
                        float anchoTotalFijo = 0.03f + 0.15f + 0.03f + 0.25f;
                        float anchoDia = (1f - anchoTotalFijo) / totalColumnasAsistencia;

                        float[] widths = new float[totalColumnasTabla];
                        widths[0] = 0.03f;
                        widths[1] = 0.15f;
                        widths[2] = 0.03f;
                        widths[3] = 0.25f;
                        for (int i = 4; i < totalColumnasTabla; i++)
                        {
                            widths[i] = anchoDia;
                        }
                        tabla.SetWidths(widths);

                        // Headers: Fila 1 (N°, CURP, Género y Alumnos)
                        string[] headersFijos = { "N°", "CURP", "G", "Alumno (Apellido P, M, Nombre)" };
                        foreach (string header in headersFijos)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(header, fontEncabezado)) { BackgroundColor = MoradoIntenso };
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Rowspan = 2;
                            cell.BorderWidth = 0.5f;
                            cell.BorderColor = NegroFino;
                            tabla.AddCell(cell);
                        }

                        // Días de la semana (L, M, M, J, V) - Fila 1
                        foreach (DateTime dia in diasHabiles)
                        {
                            string diaSemana = new CultureInfo("es-ES").DateTimeFormat.GetAbbreviatedDayName(dia.DayOfWeek).ToUpper()[0].ToString();
                            PdfPCell cellDia = new PdfPCell(new Phrase(diaSemana, fontMini)) { BackgroundColor = RosaClaro };
                            cellDia.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellDia.BorderWidth = 0.5f;
                            cellDia.BorderColor = NegroFino;
                            tabla.AddCell(cellDia);
                        }

                        // Headers: Fila 2 (Fechas - Días del mes)
                        foreach (DateTime dia in diasHabiles)
                        {
                            PdfPCell cellFecha = new PdfPCell(new Phrase(dia.Day.ToString(), fontMini)) { BackgroundColor = RosaClaro };
                            cellFecha.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellFecha.BorderWidth = 0.5f;
                            cellFecha.BorderColor = NegroFino;
                            tabla.AddCell(cellFecha);
                        }

                        // 7. Llenar Filas de Alumnos (Orden Alfabético)
                        string queryAlumnos = "SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno, CURP, genero " +
                                              "FROM alumnos WHERE id_grupo = @idGrupo " +
                                              "ORDER BY ApellidoPaterno, ApellidoMaterno, Nombre";
                        MySqlCommand cmdAlumnos = new MySqlCommand(queryAlumnos, conn);
                        cmdAlumnos.Parameters.AddWithValue("@idGrupo", idGrupo);
                        MySqlDataReader readerAlumnos = cmdAlumnos.ExecuteReader();

                        int contador = 1;
                        while (readerAlumnos.Read())
                        {
                            // Lógica para obtener solo 'F' o 'M'
                            string generoDB = readerAlumnos["genero"].ToString();
                            string generoLetra = string.IsNullOrEmpty(generoDB) ? "" : generoDB.Substring(0, 1).ToUpper();

                            // Celda N°
                            PdfPCell cellContador = new PdfPCell(new Phrase(contador.ToString(), fontTexto)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = RosaPalido };
                            cellContador.BorderWidth = 0.5f; cellContador.BorderColor = NegroFino;
                            tabla.AddCell(cellContador);

                            // Celda CURP
                            PdfPCell cellCurp = new PdfPCell(new Phrase(readerAlumnos["CURP"].ToString(), fontTexto)) { BackgroundColor = RosaPalido };
                            cellCurp.BorderWidth = 0.5f; cellCurp.BorderColor = NegroFino;
                            tabla.AddCell(cellCurp);

                            // Celda GÉNERO
                            PdfPCell cellGenero = new PdfPCell(new Phrase(generoLetra, fontTexto)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = RosaPalido };
                            cellGenero.BorderWidth = 0.5f; cellGenero.BorderColor = NegroFino;
                            tabla.AddCell(cellGenero);

                            // Celda Alumno (Apellido P, M, Nombre)
                            string nombreCompleto = $"{readerAlumnos["ApellidoPaterno"]} {readerAlumnos["ApellidoMaterno"]} {readerAlumnos["Nombre"]}";
                            PdfPCell cellNombre = new PdfPCell(new Phrase(nombreCompleto, fontTexto)) { BackgroundColor = RosaPalido };
                            cellNombre.BorderWidth = 0.5f; cellNombre.BorderColor = NegroFino;
                            tabla.AddCell(cellNombre);

                            // Celdas de asistencia (Casillas Vacías)
                            for (int i = 0; i < totalColumnasAsistencia; i++)
                            {
                                PdfPCell celdaAsis = new PdfPCell(new Phrase("", fontTexto));
                                celdaAsis.HorizontalAlignment = Element.ALIGN_CENTER;
                                celdaAsis.MinimumHeight = 15f;
                                celdaAsis.BorderWidth = 0.5f;
                                celdaAsis.BorderColor = NegroFino;
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

                        MessageBox.Show("PDF de Control de Asistencia generado con el diseño final (bordes finos y ajustados).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el PDF: " + ex.Message + "\nVerifique la ruta del logo y la conexión a la base de datos.", "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
    }
}
