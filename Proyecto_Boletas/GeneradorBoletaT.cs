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
    using PdfFont = iTextSharp.text.Font;

    internal class GeneradorBoletaT
    {
        // Definición de las fuentes para usarlas en todo el documento
        private readonly PdfFont fontTitulo = new PdfFont(PdfFont.FontFamily.HELVETICA, 12, PdfFont.BOLD);
        private readonly PdfFont fontSubtitulo = new PdfFont(PdfFont.FontFamily.HELVETICA, 8, PdfFont.BOLD);
        private readonly PdfFont fontNormal = new PdfFont(PdfFont.FontFamily.HELVETICA, 8, PdfFont.NORMAL);
        private readonly PdfFont fontAlumno = new PdfFont(PdfFont.FontFamily.HELVETICA, 9, PdfFont.BOLD);
        private readonly BaseColor colorBorde = BaseColor.BLACK;


        public void CrearBoleta(int idAlumno, string trimestre)
        {
            try
            {
                using (MySqlConnection conn = new Conexion().GetConnection())
                {
                    conn.Open();

                    // --- 1. OBTENER DATOS DEL ALUMNO Y GRUPO ---
                    string query = @"
                SELECT a.Nombre, a.ApellidoPaterno, a.ApellidoMaterno, g.nombre_grupo, 
                       m.NombreMaestro, m.ApellidoPMaestro, m.ApellidoMMaestro, g.id_grupo
                FROM alumnos a
                INNER JOIN grupo g ON a.id_grupo = g.id_grupo
                INNER JOIN maestro m ON g.id_maestro = m.id_maestro
                WHERE a.AlumnoID = @idAlumno";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (!dr.Read())
                    {
                        MessageBox.Show("No se encontró información del alumno.");
                        return;
                    }

                    string nombreAlumno = $"{dr["ApellidoPaterno"]} {dr["ApellidoMaterno"]} {dr["Nombre"]}";
                    string grupo = dr["nombre_grupo"].ToString();
                    string maestro = $"{dr["NombreMaestro"]} {dr["ApellidoPMaestro"]} {dr["ApellidoMMaestro"]}";
                    dr.Close();

                    // --- 2. CREAR EL PDF ---
                    string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Boletas");
                    Directory.CreateDirectory(carpeta);
                    string archivoPDF = Path.Combine(carpeta, $"{nombreAlumno}_{trimestre}_BoletaInterna.pdf");

                    // Configuración del documento (márgenes y orientación)
                    Document doc = new Document(PageSize.LETTER.Rotate(), 60, 60, 70, 70);

                    PdfWriter.GetInstance(doc, new FileStream(archivoPDF, FileMode.Create));
                    doc.Open();

                    // --- TÍTULO Y DATOS GENERALES ---
                    doc.Add(CrearCabeceraSuperior());
                    doc.Add(new Paragraph("\n\n"));

                    PdfPTable tablaDatosAlumno = new PdfPTable(4);
                    tablaDatosAlumno.WidthPercentage = 100;
                    tablaDatosAlumno.SetWidths(new float[] { 0.2f, 0.4f, 0.2f, 0.2f });

                    tablaDatosAlumno.AddCell(CrearCelda("MAESTRA:", fontNormal, Element.ALIGN_LEFT, 1, 1, colorBorde));
                    tablaDatosAlumno.AddCell(CrearCelda(maestro, fontAlumno, Element.ALIGN_LEFT, 1, 3, colorBorde));
                    tablaDatosAlumno.AddCell(CrearCelda("GRADO Y GRUPO:", fontNormal, Element.ALIGN_LEFT, 1, 1, colorBorde));
                    tablaDatosAlumno.AddCell(CrearCelda(grupo, fontAlumno, Element.ALIGN_LEFT, 1, 1, colorBorde));
                    tablaDatosAlumno.AddCell(CrearCelda("TRIMESTRE:", fontNormal, Element.ALIGN_LEFT, 1, 1, colorBorde));
                    tablaDatosAlumno.AddCell(CrearCelda(trimestre, fontAlumno, Element.ALIGN_LEFT, 1, 1, colorBorde));

                    tablaDatosAlumno.SpacingAfter = 20f;
                    doc.Add(tablaDatosAlumno);

                    // --- TABLA PRINCIPAL DE CALIFICACIONES (AJUSTADA A 45 COLUMNAS) ---
                    // 🎯 TABLA PRINCIPAL DE CALIFICACIONES (AJUSTADA A 45 COLUMNAS)
                    PdfPTable tablaCalificaciones = new PdfPTable(45);
                    tablaCalificaciones.WidthPercentage = 100;
                    tablaCalificaciones.SpacingBefore = 15f;
                    tablaCalificaciones.SpacingAfter = 20f;

                    // --- AJUSTE DE ANCHOS A 45 COLUMNAS ---
                    float[] widths = new float[45];
                    widths[0] = 0.03f; // No.Lista
                    widths[1] = 0.04f; // Grado/Grupo
                    widths[2] = 0.14f; // Nombre Alumno (Más ancho)
                                       // 3 Bloques * 13 celdas (10 materias + 3 promedios de bloque)
                    for (int i = 3; i < 42; i++) { widths[i] = 0.017f; } // 39 celdas pequeñas (1.7% c/u)
                    widths[42] = 0.02f; // Promedio Final 1
                    widths[43] = 0.02f; // Promedio Final 2
                    widths[44] = 0.02f; // Promedio Final 3
                    tablaCalificaciones.SetWidths(widths);


                    // --- Fila 1: Cabeceras de Trimestre ---
                    // Celda inicial para las 3 filas
                    tablaCalificaciones.AddCell(CrearCelda("", fontSubtitulo, Element.ALIGN_CENTER, 3, 3, colorBorde));

                    // Tres trimestres (ColSpan 13)
                    tablaCalificaciones.AddCell(CrearCelda("1° TRIMESTRE", fontSubtitulo, Element.ALIGN_CENTER, 1, 13, colorBorde));
                    tablaCalificaciones.AddCell(CrearCelda("2° TRIMESTRE", fontSubtitulo, Element.ALIGN_CENTER, 1, 13, colorBorde));
                    tablaCalificaciones.AddCell(CrearCelda("3° TRIMESTRE", fontSubtitulo, Element.ALIGN_CENTER, 1, 13, colorBorde));

                    // Columna Final de Promedios (RowSpan 3)
                    tablaCalificaciones.AddCell(CrearCelda("DEL\nTRIMESTRE", fontSubtitulo, Element.ALIGN_CENTER, 3, 3, colorBorde));


                    // --- Fila 2: Cabeceras de Áreas (Campos Formativos) ---
                    // Las 3 primeras columnas están cubiertas por el RowSpan=3 de la Fila 1

                    for (int i = 0; i < 3; i++) // Repetir para 3 trimestres
                    {
                        // LENGUAJES (ColSpan 4)
                        tablaCalificaciones.AddCell(CrearCelda("LENGUAJES", fontSubtitulo, Element.ALIGN_CENTER, 1, 4, colorBorde));
                        // SABERES Y PENSAMIENTO CIENTÍFICO (ColSpan 4)
                        tablaCalificaciones.AddCell(CrearCelda("SABERES Y PENS.\nMAT.", fontSubtitulo, Element.ALIGN_CENTER, 1, 4, colorBorde));
                        // ÉTICA, NATURALEZA Y SOCIEDADES (ColSpan 3)
                        tablaCalificaciones.AddCell(CrearCelda("ÉTICA, NAT Y SOC.", fontSubtitulo, Element.ALIGN_CENTER, 1, 3, colorBorde));
                        // DE LO HUMANO Y LO COMUNITARIO (ColSpan 2)
                        tablaCalificaciones.AddCell(CrearCelda("HUMANO Y COM.", fontSubtitulo, Element.ALIGN_CENTER, 1, 2, colorBorde));
                    }


                    // --- Fila 3: Nombres de las Materias y Promedio de Bloque ---
                    // Las 3 primeras columnas están cubiertas por el RowSpan=3 de la Fila 1

                    for (int i = 0; i < 3; i++) // Repetir para 3 trimestres
                    {
                        // LENGUAJES (4 celdas)
                        tablaCalificaciones.AddCell(CrearCelda("ESPAÑOL", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("INGLÉS", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("ARTES", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("PROMEDIO", fontSubtitulo, Element.ALIGN_CENTER, BaseColor.LIGHT_GRAY)); // Promedio del Bloque

                        // SABERES Y PENSAMIENTO CIENTÍFICO (4 celdas)
                        tablaCalificaciones.AddCell(CrearCelda("MATEMÁTICAS", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("TECNOLOGÍA", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("CON. DEL MEDIO", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("PROMEDIO", fontSubtitulo, Element.ALIGN_CENTER, BaseColor.LIGHT_GRAY));

                        // ÉTICA, NATURALEZA Y SOCIEDADES (3 celdas)
                        tablaCalificaciones.AddCell(CrearCelda("F. CÍVICA Y ÉTICA", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("E. NAT. Y SOC.", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("PROMEDIO", fontSubtitulo, Element.ALIGN_CENTER, BaseColor.LIGHT_GRAY));

                        // DE LO HUMANO Y LO COMUNITARIO (2 celdas)
                        tablaCalificaciones.AddCell(CrearCelda("H. Y COM.", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda("PROMEDIO", fontSubtitulo, Element.ALIGN_CENTER, BaseColor.LIGHT_GRAY));
                    }
                    // Las 3 columnas finales están cubiertas por el RowSpan=3 de la Fila 1


                    // --- Fila de Datos del Alumno (Debe sumar 45 celdas) ---

                    tablaCalificaciones.AddCell(CrearCelda("1", fontNormal, Element.ALIGN_CENTER));
                    tablaCalificaciones.AddCell(CrearCelda(grupo, fontNormal, Element.ALIGN_CENTER));
                    tablaCalificaciones.AddCell(CrearCelda(nombreAlumno, fontAlumno, Element.ALIGN_LEFT));

                    // Celdas de Calificaciones (42 celdas vacías, 13 por bloque)
                    for (int i = 0; i < 3; i++) // 3 Bloques
                    {
                        // LENGUAJES (3 materias + 1 promedio)
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontAlumno, Element.ALIGN_CENTER, BaseColor.YELLOW)); // PROMEDIO

                        // SABERES Y PENSAMIENTO CIENTÍFICO (3 materias + 1 promedio)
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontAlumno, Element.ALIGN_CENTER, BaseColor.YELLOW)); // PROMEDIO

                        // ÉTICA, NATURALEZA Y SOCIEDADES (2 materias + 1 promedio)
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontAlumno, Element.ALIGN_CENTER, BaseColor.YELLOW)); // PROMEDIO

                        // DE LO HUMANO Y LO COMUNITARIO (1 materia + 1 promedio)
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        tablaCalificaciones.AddCell(CrearCelda(" ", fontAlumno, Element.ALIGN_CENTER, BaseColor.YELLOW)); // PROMEDIO
                    }
                    // 🎯 Celdas de Promedio Trimestral Final (3 celdas)
                    tablaCalificaciones.AddCell(CrearCelda(" ", fontAlumno, Element.ALIGN_CENTER, BaseColor.YELLOW));
                    tablaCalificaciones.AddCell(CrearCelda(" ", fontAlumno, Element.ALIGN_CENTER, BaseColor.YELLOW));
                    tablaCalificaciones.AddCell(CrearCelda(" ", fontAlumno, Element.ALIGN_CENTER, BaseColor.YELLOW));

                    doc.Add(tablaCalificaciones);
                    doc.Close();

                    MessageBox.Show("Boleta generada correctamente en: " + archivoPDF);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar boleta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        /// <summary>
        /// Crea una celda de tabla con colSpan y rowSpan.
        /// </summary>
        private PdfPCell CrearCelda(string texto, PdfFont fuente, int alineacion, int rowSpan, int colSpan, BaseColor bordeColor)
        {
            PdfPCell cell = CrearCelda(texto, fuente, alineacion);
            cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;
            cell.BorderColor = bordeColor;
            return cell;
        }

        /// <summary>
        /// Crea la cabecera superior del documento (Título y Escuela).
        /// </summary>
        private PdfPTable CrearCabeceraSuperior()
        {
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 0.3f, 0.4f, 0.3f });

            // Celda 1: LOGO
            table.AddCell(CrearCelda("LOGO DE LA ESCUELA", fontNormal, Element.ALIGN_CENTER));

            // Celda 2: TÍTULO PRINCIPAL
            var titulo = new Paragraph("ESCUELA PRIMARIA JUAN ESCUTIA\nBoleta Interna", fontTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            PdfPCell cellTitulo = new PdfPCell(titulo);
            cellTitulo.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTitulo.Border = PdfPCell.NO_BORDER;
            table.AddCell(cellTitulo);

            // Celda 3: CICLO ESCOLAR
            PdfPCell cellCiclo = CrearCelda("CICLO ESCOLAR\n2025-2026", fontSubtitulo, Element.ALIGN_RIGHT);
            table.AddCell(cellCiclo);

            return table;
        }

    }
}
