using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_Boletas
{
    using PdfFont = iTextSharp.text.Font;
    using PdfImage = iTextSharp.text.Image;
    using PdfRectangle = iTextSharp.text.Rectangle;

    internal class GeneradorBoletaT
    {
        // Definiciones de fuentes y colores
        private readonly PdfFont fontTitulo = new PdfFont(PdfFont.FontFamily.HELVETICA, 12, PdfFont.BOLD);
        private readonly PdfFont fontSubtitulo = new PdfFont(PdfFont.FontFamily.HELVETICA, 8, PdfFont.BOLD);
        private readonly PdfFont fontNormal = new PdfFont(PdfFont.FontFamily.HELVETICA, 8, PdfFont.NORMAL);
        private readonly PdfFont fontAlumnoNombreLargo = new PdfFont(PdfFont.FontFamily.HELVETICA, 6, PdfFont.NORMAL);
        // Fuente para encabezados rotados, un poco más grande para mejor legibilidad
        private readonly PdfFont fontEncabezadoRotado = new PdfFont(PdfFont.FontFamily.HELVETICA, 8, PdfFont.BOLD, BaseColor.WHITE);

        private readonly BaseColor colorBorde = BaseColor.BLACK;
        private readonly BaseColor colorEncabezadoFijo = new BaseColor(64, 64, 64);
        private readonly BaseColor colorPromedio = new BaseColor(255, 223, 100);
        private readonly BaseColor colorGrisClaro = new BaseColor(230, 230, 230);
        private readonly BaseColor colorEncabezadoMateria = new BaseColor(2, 60, 102);

        // Nombres de las materias base (usadas en la Fila 3 y bucles)
        private readonly string[] materiasBase = { "Español", "Inglés", "Artes", "Matemáticas", "Tecnología", "Con. del Medio", "F. Cívica y Ética", "Ed. Física", "Promedio" };

        // 🎯 RUTA DEL LOGO: Ahora definida aquí para un solo punto de control.
        private const string RUTA_LOGO = "logo_escuela350.png"; // Asume que está en la carpeta de ejecución (bin/Debug o Release)

        // Lógica para obtener los nombres de los meses por trimestre
        private string[] ObtenerMeses(string trimestre)
        {
            return trimestre switch
            {
                "1er Trimestre" => new[] { "Septiembre", "Octubre", "Noviembre" },
                "2do Trimestre" => new[] { "Enero", "Febrero", "Marzo" },
                "3er Trimestre" => new[] { "Abril", "Mayo", "Junio" },
                _ => new[] { "Mes 1", "Mes 2", "Mes 3" }
            };
        }

        public void CrearBoletaGrupal(int idGrupo, string trimestre)
        {
            string nombreGrupo = "";
            string nombreMaestro = "";
            List<Alumno> alumnos = new List<Alumno>();
            string rutaSalida = "";

            try
            {
                // ===== 1. SELECCIONAR RUTA DE GUARDADO (SaveFileDialog) =====
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar Boleta Grupal";
                saveFileDialog.FileName = $"Boleta_Grupo_{idGrupo}_{trimestre}.pdf";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                rutaSalida = saveFileDialog.FileName;

                // ===== 2. EXTRAER DATOS DEL GRUPO Y MAESTRO (Bloque 1) =====
                using (MySqlConnection conn = new Conexion().GetConnection())
                {
                    conn.Open();
                    string queryGrupo = "SELECT g.nombre_grupo, g.id_maestro, " +
                                         "m.NombreMaestro, m.ApellidoPMaestro, m.ApellidoMMaestro " +
                                         "FROM grupo g " +
                                         "INNER JOIN maestro m ON g.id_maestro = m.id_maestro " +
                                         "WHERE g.id_grupo = @idGrupo";

                    using (MySqlCommand cmd = new MySqlCommand(queryGrupo, conn))
                    {
                        cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                nombreGrupo = dr["nombre_grupo"].ToString();
                                nombreMaestro = $"{dr["NombreMaestro"]} {dr["ApellidoPMaestro"]} {dr["ApellidoMMaestro"]}";
                            }
                            dr.Close();
                        }
                    }
                }

                // ===== 3. EXTRAER ALUMNOS Y ORDENAR ALFABÉTICAMENTE =====
                using (MySqlConnection conn = new Conexion().GetConnection())
                {
                    conn.Open();
                    string queryAlumnos = "SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno, Genero FROM alumnos WHERE id_grupo = @idGrupo";

                    using (MySqlCommand cmd = new MySqlCommand(queryAlumnos, conn))
                    {
                        cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                alumnos.Add(new Alumno
                                {
                                    ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                                    ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Genero = dr["genero"].ToString(),
                                });
                            }
                        }
                    }
                }

                alumnos = alumnos.OrderBy(a => a.ApellidoPaterno)
                                 .ThenBy(a => a.ApellidoMaterno)
                                 .ThenBy(a => a.Nombre)
                                 .ToList();

                // ===== 4. CREAR PDF (INICIO) =====
                string[] meses = ObtenerMeses(trimestre); // Obtiene los 3 meses

                Document doc = new Document(PageSize.A4.Rotate(), 15, 15, 40, 20);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // --- CABECERA DE DATOS GENERALES ---
                PdfPTable encabezado = new PdfPTable(2) { WidthPercentage = 100 };
                encabezado.SetWidths(new float[] { 20, 80 });

                // 🎯 LÓGICA DEL LOGO MEJORADA
                if (File.Exists(RUTA_LOGO))
                {
                    try
                    {
                        PdfImage logo = PdfImage.GetInstance(RUTA_LOGO);
                        logo.ScaleToFit(70f, 70f);
                        PdfPCell logoCell = new PdfPCell(logo, false);
                        logoCell.Border = PdfRectangle.NO_BORDER;
                        logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        encabezado.AddCell(logoCell);
                    }
                    catch (Exception ex)
                    {
                        // Si hay un error al cargar la imagen (ej. corrupción), mostrará "ERROR LOGO"
                        encabezado.AddCell(CrearCelda("ERROR LOGO\n" + ex.Message.Substring(0, Math.Min(ex.Message.Length, 50)), fontNormal, Element.ALIGN_CENTER));
                    }
                }
                else
                {
                    encabezado.AddCell(CrearCelda("LOGO FALTANTE", fontNormal, Element.ALIGN_CENTER));
                }

                PdfPCell celdaTexto = new PdfPCell();
                celdaTexto.AddElement(new Paragraph("INSTITUTO MANUEL M. ACOSTA", fontTitulo) { Alignment = Element.ALIGN_CENTER }); // 🎯 Nombre de la escuela cambiado
                celdaTexto.AddElement(new Paragraph("BOLETA INTERNA TRIMESTRAL", fontSubtitulo) { Alignment = Element.ALIGN_CENTER });
                celdaTexto.AddElement(new Paragraph($"Grupo: {nombreGrupo}      Maestro: {nombreMaestro}      Trimestre: {trimestre}", fontNormal) { Alignment = Element.ALIGN_CENTER });
                celdaTexto.Border = PdfRectangle.NO_BORDER;
                encabezado.AddCell(celdaTexto);

                doc.Add(encabezado);
                doc.Add(new Paragraph(" "));

                // --- TABLA PRINCIPAL DE CALIFICACIONES (34 COLUMNAS) ---

                int totalColumnas = 3 + (meses.Length * materiasBase.Length) + 1; // 3 + (3 * 9) + 1 = 31

                PdfPTable tablaCalificaciones = new PdfPTable(31) { WidthPercentage = 100, HeaderRows = 2 }; // HeaderRows ayuda a repetir el encabezado en nuevas páginas

                // Definición de anchos (sin cambios)
                float[] widths = new float[31];
                widths[0] = 0.02f; widths[1] = 0.02f; widths[2] = 0.15f;
                float smallWidth = (1.00f - 0.19f) / 28f;
                for (int i = 3; i < 31; i++) { widths[i] = smallWidth; }
                tablaCalificaciones.SetWidths(widths);

                // Fila 1: Cabeceras Principales (Horizontal y Rotadas)
                // 🎯 No. LISTA y SEXO - Celdas rotadas que cubren 2 filas
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("NO. LISTA", fontEncabezadoRotado, 2, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("SEXO", fontEncabezadoRotado, 2, colorEncabezadoFijo));

                // NOMBRE DEL ALUMNO - Celda horizontal que cubre 2 filas
                tablaCalificaciones.AddCell(CreateHeaderCell("NOMBRE DEL ALUMNO", fontSubtitulo, 2, 1, colorEncabezadoFijo));

                foreach (string mes in meses)
                {
                    tablaCalificaciones.AddCell(CrearCelda(mes, fontSubtitulo, Element.ALIGN_CENTER, 1, materiasBase.Length, colorEncabezadoMateria)); // ColSpan 9
                }
                // 🎯 PROM. TRIMESTRAL - Celda rotada que cubre 2 filas
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("PROMEDIO\nTRIMESTRAL", fontEncabezadoRotado, 2, colorPromedio));

                // Fila 2: Nombres de Materias (Esta fila se salta 3 celdas iniciales y la final debido al Rowspan 2)

                for (int m = 0; m < meses.Length; m++)
                {
                    foreach (string materia in materiasBase)
                    {
                        BaseColor fondo = materia == "Promedio" ? colorPromedio : colorGrisClaro;
                        tablaCalificaciones.AddCell(CreateRotatedCell(materia, fontNormal, fondo));
                    }
                }

                // --- RELLENO DE DATOS DE CADA ALUMNO (ORDENADO) ---
                int listaContador = 1;
                foreach (var alumno in alumnos)
                {
                    tablaCalificaciones.AddCell(CrearCelda((listaContador++).ToString(), fontNormal, Element.ALIGN_CENTER));
                    tablaCalificaciones.AddCell(CrearCelda(alumno.Genero.Substring(0, 1), fontNormal, Element.ALIGN_CENTER));

                    string nombreCompleto = $"{alumno.ApellidoPaterno} {alumno.ApellidoMaterno} {alumno.Nombre}";
                    tablaCalificaciones.AddCell(new PdfPCell(new Phrase(nombreCompleto, fontAlumnoNombreLargo))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Padding = 2f
                    });

                    for (int i = 0; i < 28; i++)
                    {
                        if (i % 9 == 8 || i == 27)
                        {
                            tablaCalificaciones.AddCell(CrearCelda("0.0", fontAlumnoNombreLargo, Element.ALIGN_CENTER, colorPromedio));
                        }
                        else
                        {
                            tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        }
                    }
                }

                // Fila de Promedio Final del Grupo 
                tablaCalificaciones.AddCell(CrearCelda("PROMEDIO GRUPAL", fontSubtitulo, Element.ALIGN_CENTER, 1, 3, colorBorde));

                for (int i = 0; i < 28; i++) // 28 celdas de promedios de grupo
                {
                    if (i % 9 == 8 || i == 27)
                    {
                        tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER, colorPromedio));
                    }
                    else
                    {
                        tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER));
                    }
                }

                doc.Add(tablaCalificaciones);
                doc.Close();
                writer.Close();

                MessageBox.Show($"Boleta grupal generada correctamente en:\n{rutaSalida}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar boleta grupal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --- MÉTODOS AUXILIARES ---

        // 🎯 Aumento MinimumHeight y ajuste de font para los encabezados rotados
        private PdfPCell CreateRotatedHeaderCell(string text, PdfFont font, int rowspan, BaseColor background)
        {
            return new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = background,
                Rotation = 90,
                Rowspan = rowspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                MinimumHeight = 70f, // Aumentado para dar más espacio
                Padding = 2f
            };
        }

        private PdfPCell CreateRotatedCell(string text, PdfFont font, BaseColor background)
        {
            return new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = background,
                Rotation = 90,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                MinimumHeight = 50f // Asegura que las letras quepan
            };
        }

        private PdfPCell CreateHeaderCell(string text, PdfFont font, int rowspan, int colspan, BaseColor background)
        {
            return new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = background,
                Rowspan = rowspan,
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                MinimumHeight = 70f // Aumentado para que coincida con las celdas rotadas
            };
        }

        public void CrearBoleta(int idAlumno, string trimestre)
        {
        }

        private PdfPCell CrearCelda(string texto, PdfFont fuente, int alineacion, BaseColor fondo = null)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente));
            cell.HorizontalAlignment = alineacion;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 4f;
            cell.BorderColor = colorBorde;
            if (fondo != null) cell.BackgroundColor = fondo;
            return cell;
        }

        private PdfPCell CrearCelda(string texto, PdfFont fuente, int alineacion, int rowSpan, int colSpan, BaseColor bordeColor)
        {
            PdfPCell cell = CrearCelda(texto, fuente, alineacion);
            cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;
            cell.BorderColor = bordeColor;
            return cell;
        }
        /*
        private PdfPTable CrearCabeceraSuperior()
        {
            // Define la ruta de tu logo aquí
            string rutaLogo = @"C:\Users\Isis Astrid\Source\Repos\Proyecto_Boletas\Proyecto_Boletas\Resources\logo_escuela350.png";
            string nombreEscuela = "INSTITUTO MANUEL M. ACOSTA";

            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 0.3f, 0.4f, 0.3f });

            // --- CELDA 1: LOGO ---
            if (File.Exists(rutaLogo))
            {
                PdfImage logo = PdfImage.GetInstance(rutaLogo); // Usa el alias PdfImage
                logo.ScaleToFit(70f, 70f);

                // Crea la celda y centra la imagen
                PdfPCell logoCell = new PdfPCell(logo, false);
                logoCell.Border = PdfRectangle.NO_BORDER;
                logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(logoCell);
            }
            else
            {
                // Si el logo no existe, deja la celda en blanco o con texto de advertencia
                table.AddCell(CrearCelda("LOGO FALTANTE", fontNormal, Element.ALIGN_CENTER));
            }

            // --- CELDA 2: TÍTULO PRINCIPAL (Nombre de la escuela cambiado) ---
            PdfPCell cellTexto = new PdfPCell();
            cellTexto.Border = PdfRectangle.NO_BORDER;

            var titulo = new Paragraph(nombreEscuela, fontTitulo) { Alignment = Element.ALIGN_CENTER };
            cellTexto.AddElement(titulo);

            var subtitulo = new Paragraph("Boleta Interna", fontSubtitulo) { Alignment = Element.ALIGN_CENTER };
            cellTexto.AddElement(subtitulo);

            table.AddCell(cellTexto);

            // --- CELDA 3: CICLO ESCOLAR ---
            PdfPCell cellCiclo = CrearCelda("CICLO ESCOLAR\n2025-2026", fontSubtitulo, Element.ALIGN_RIGHT);
            table.AddCell(cellCiclo);

            return table;
        }
        */

        public class Alumno
        {
            public int AlumnoID { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Genero { get; set; }
        }
    }
}