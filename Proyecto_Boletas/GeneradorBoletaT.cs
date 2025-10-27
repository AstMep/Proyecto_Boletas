using System.Drawing;
using System.Drawing.Imaging;
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
        // Definiciones de fuentes
        private readonly PdfFont fontTitulo = new PdfFont(PdfFont.FontFamily.HELVETICA, 12, PdfFont.BOLD);
        private readonly PdfFont fontSubtitulo = new PdfFont(PdfFont.FontFamily.HELVETICA, 8, PdfFont.BOLD);
        private readonly PdfFont fontNormal = new PdfFont(PdfFont.FontFamily.HELVETICA, 7, PdfFont.NORMAL);
        private readonly PdfFont fontAlumnoNombreLargo = new PdfFont(PdfFont.FontFamily.HELVETICA, 6, PdfFont.NORMAL);
        private readonly PdfFont fontEncabezadoRotado = new PdfFont(PdfFont.FontFamily.HELVETICA, 7, PdfFont.BOLD, BaseColor.BLACK);

        // Colores del diseño (según tu imagen)
        private readonly BaseColor colorBorde = BaseColor.BLACK;
        private readonly BaseColor colorEncabezadoFijo = BaseColor.WHITE;
        private readonly BaseColor colorAmarillo = new BaseColor(255, 255, 153); // Promedio
        private readonly BaseColor colorCeleste = new BaseColor(173, 216, 230); // Inglés
        private readonly BaseColor colorRosa = new BaseColor(255, 182, 193); // Matemáticas
        private readonly BaseColor colorVerde = new BaseColor(255, 200, 124); // F. Cívica
        private readonly BaseColor colorNaranja = new BaseColor(255, 182, 193); // Tecnología
        private readonly BaseColor colorMorado = new BaseColor(173, 216, 230); // Artes
        private readonly BaseColor colorAzulClaro = new BaseColor(255, 182, 193); // C. Naturales/Con. del Medio
        private readonly BaseColor colorVerdeOscuro = new BaseColor(152, 251, 152); // Ed. Física
        private readonly BaseColor colorGrisClaro= new BaseColor(173, 216, 230); // Español
        private readonly BaseColor colorLenguaje = new BaseColor(255, 192, 203); // Rosa Claro
        private readonly BaseColor colorMatematicasF = new BaseColor(173, 216, 230); // Celeste
        private readonly BaseColor colorExploracion = new BaseColor(189, 252, 177); // Verde Claro
        private readonly BaseColor colorDesarrollo = new BaseColor(255, 239, 153); // Amarillo Pálido

        private string[] materiasBase;
        private string nombreMateriaCiencias;

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

        private string ObtenerNombreMateriaCiencias(string nombreGrupo)
        {
            string nombreNormalizado = nombreGrupo.ToLower().Trim();

            if (nombreNormalizado.Contains("primero") || nombreNormalizado.Contains("segundo"))
            {
                return "Con. del Medio";
            }
            else if (nombreNormalizado.Contains("tercero") ||
                      nombreNormalizado.Contains("cuarto") ||
                      nombreNormalizado.Contains("quinto") ||
                      nombreNormalizado.Contains("sexto"))
            {
                return "C. Naturales";
            }

            return "Con. del Medio";
        }

        private BaseColor ObtenerColorMateria(string materia)
        {
            return materia switch
            {
                "Español" => colorGrisClaro,
                "Inglés" => colorCeleste,
                "Artes" => colorMorado,
                "Matemáticas" => colorRosa,
                "Tecnología" => colorNaranja,
                "C. Naturales" => colorAzulClaro,
                "Con. del Medio" => colorAzulClaro,
                "F. Cívica y Ética" => colorVerde,
                "Ed. Física" => colorVerdeOscuro,
                "Promedio" => colorAmarillo,
                _ => BaseColor.WHITE
            };
        }

        private void AddPromedioCampoFormativoRow(PdfPTable table, string campo, string[] materiasDelCampo, string[] meses)
        {
            // Este método se mantiene sin cambios, aunque no es usado en el cuerpo de CrearBoletaGrupal,
            // y su lógica de llenado es inconsistente con la nueva estructura de 40 columnas.
            // Se deja como estaba en la base original, ya que no se pidió su modificación explícita.
            BaseColor colorFondo = BaseColor.WHITE;
            switch (campo)
            {
                case "Lenguaje y Comunicación": colorFondo = colorLenguaje; break;
                case "Pensamiento Matemático": colorFondo = colorMatematicasF; break;
                case "Exploración y Comprensión": colorFondo = colorExploracion; break;
                case "Desarrollo Físico": colorFondo = colorDesarrollo; break;
            }

            table.AddCell(CrearCelda(campo.ToUpper(), fontSubtitulo, Element.ALIGN_CENTER, 1, 3, colorFondo));

            for (int m = 0; m < meses.Length; m++)
            {
                int contadorMaterias = 0;
                // Itera sobre todas las materias para el mes actual
                foreach (string materiaBase in materiasBase)
                {
                    if (materiasDelCampo.Contains(materiaBase))
                    {
                        // Celda de promedio de campo formativo (donde debería ir el promedio)
                        table.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER, colorFondo));
                        contadorMaterias++;
                    }
                    else
                    {
                        // Celda de relleno (para materias que NO pertenecen a este campo)
                        PdfPCell emptyCell = CrearCelda("", fontNormal, Element.ALIGN_CENTER, BaseColor.WHITE);
                        emptyCell.BorderColor = new BaseColor(200, 200, 200); // Borde más suave para relleno
                        emptyCell.BorderWidth = 0.2f;
                        table.AddCell(emptyCell);
                    }
                }
            }

            // Celda final del promedio trimestral del Campo Formativo
            table.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER, colorFondo));
        }

        public void CrearBoletaGrupal(int idGrupo, string trimestre)
        {
            string nombreGrupo = "";
            string nombreMaestro = "";
            List<Alumno> alumnos = new List<Alumno>();
            string rutaSalida = "";

            try
            {
                // 1. SELECCIONAR RUTA DE GUARDADO
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar Boleta Grupal";
                saveFileDialog.FileName = $"Boleta_Grupo_{idGrupo}_{trimestre}.pdf";

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                rutaSalida = saveFileDialog.FileName;

                // 2. EXTRAER DATOS DEL GRUPO Y MAESTRO (Manteniendo la lógica de BD)
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

                // Determinar el nombre de la materia de ciencias
                nombreMateriaCiencias = ObtenerNombreMateriaCiencias(nombreGrupo);

                // Inicializar materias (9 materias base)
                materiasBase = new[] {
                    "Español", "Inglés", "Artes", "Matemáticas", "Tecnología",
                    nombreMateriaCiencias, "F. Cívica y Ética", "Ed. Física", "Promedio"
                };

                // 3. EXTRAER ALUMNOS Y ORDENAR (Manteniendo la lógica de BD)
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


                // 4. CREAR PDF
                string[] meses = ObtenerMeses(trimestre);
                int numGrupos = meses.Length + 1; // 3 Meses + 1 Trimestre
                int numMaterias = materiasBase.Length; // 9 materias

                // Total de columnas: 3 (fijas) + 4 (grupos) * 9 (materias) + 1 (promedio final) = 40
                int totalColumnas = 3 + (numGrupos * numMaterias) + 1;

                Document doc = new Document(PageSize.A4.Rotate(), 15, 15, 40, 20);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // CABECERA (Manteniendo la lógica de logo)
                PdfPTable encabezado = new PdfPTable(2) { WidthPercentage = 100 };
                encabezado.SetWidths(new float[] { 20, 80 });

                PdfImage logo = null;
                try
                {
                    System.Drawing.Image imageFromResources = Proyecto_Boletas.Properties.Resources.logo_escuela350;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imageFromResources.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        logo = PdfImage.GetInstance(ms.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cargar el logo: " + ex.Message);
                }

                if (logo != null)
                {
                    logo.ScaleToFit(70f, 70f);
                    PdfPCell logoCell = new PdfPCell(logo, false);
                    logoCell.Border = PdfRectangle.NO_BORDER;
                    logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    encabezado.AddCell(logoCell);
                }
                else
                {
                    encabezado.AddCell(CrearCelda("LOGO FALTANTE", fontNormal, Element.ALIGN_CENTER));
                }

                PdfPCell celdaTexto = new PdfPCell();
                celdaTexto.AddElement(new Paragraph("INSTITUTO MANUEL M. ACOSTA", fontTitulo) { Alignment = Element.ALIGN_CENTER });
                celdaTexto.AddElement(new Paragraph("BOLETA INTERNA TRIMESTRAL", fontSubtitulo) { Alignment = Element.ALIGN_CENTER });
                celdaTexto.AddElement(new Paragraph($"Grupo: {nombreGrupo}      Maestro: {nombreMaestro}      Trimestre: {trimestre}", fontNormal) { Alignment = Element.ALIGN_CENTER });
                celdaTexto.Border = PdfRectangle.NO_BORDER;
                encabezado.AddCell(celdaTexto);

                doc.Add(encabezado);
                doc.Add(new Paragraph(" "));

                // TABLA PRINCIPAL (40 COLUMNAS)
                PdfPTable tablaCalificaciones = new PdfPTable(totalColumnas) { WidthPercentage = 100, HeaderRows = 2 };

                float[] widths = new float[totalColumnas];
                widths[0] = 0.02f; widths[1] = 0.02f; widths[2] = 0.15f;
                float smallWidth = (1.00f - 0.19f) / 37f; // 37 celdas de datos/promedio
                for (int i = 3; i < totalColumnas; i++) { widths[i] = smallWidth; }
                tablaCalificaciones.SetWidths(widths);

                // ====================================================================
                // FILA 1: Encabezados principales
                // ====================================================================
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("NO.\nLISTA", fontEncabezadoRotado, 2, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("SEXO", fontEncabezadoRotado, 2, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CreateHeaderCell("NOMBRE DEL ALUMNO", fontSubtitulo, 2, 1, colorEncabezadoFijo));

                // 1. Encabezados de los Meses (colspan = 9)
                BaseColor colorMeses = BaseColor.WHITE;
                foreach (string mes in meses)
                {
                    tablaCalificaciones.AddCell(CrearCelda(mes.ToUpper(), fontSubtitulo, Element.ALIGN_CENTER, 1, numMaterias, colorMeses));
                }

                // 2. Encabezado "TRIMESTRE" GRANDE (colspan = 9)
                BaseColor colorEncabezadoTrimestre = new BaseColor(50, 150, 250);

                // CELDA ÚNICA para el bloque TRIMESTRE, abarcando las 9 columnas (40 - 3 - 27 - 1 = 9)
                string textoTrimestre = $"TRIMESTRE ({trimestre.ToUpper()}) 2025";
                tablaCalificaciones.AddCell(CrearCelda(textoTrimestre, fontSubtitulo, Element.ALIGN_CENTER, 1, numMaterias, colorEncabezadoFijo));

                // Celda de Promedio Trimestral (Final)
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("PROMEDIO\nTRIMESTRAL", fontEncabezadoRotado, 2, colorAmarillo));

                // ====================================================================
                // FILA 2: Nombres de materias con colores (TODAS INDIVIDUALES, 9 por grupo)
                // ====================================================================
                // Materias de los Meses (3 * 9 = 27 celdas)
                for (int m = 0; m < meses.Length; m++)
                {
                    foreach (string materia in materiasBase)
                    {
                        BaseColor fondo = ObtenerColorMateria(materia);
                        tablaCalificaciones.AddCell(CreateRotatedCell(materia, fontNormal, fondo));
                    }
                }

                // Materias del TRIMESTRE (9 celdas rotadas individuales, IGUAL QUE LOS MESES)
                foreach (string materia in materiasBase)
                {
                    BaseColor fondo = ObtenerColorMateria(materia);
                    tablaCalificaciones.AddCell(CreateRotatedCell(materia, fontNormal, fondo));
                }

                int listaContador = 1;

                foreach (var alumno in alumnos)
                {
                    // Columnas fijas
                    tablaCalificaciones.AddCell(CrearCelda(listaContador++.ToString(), fontNormal, Element.ALIGN_CENTER));
                    tablaCalificaciones.AddCell(CrearCelda(alumno.Genero.Substring(0, 1), fontNormal, Element.ALIGN_CENTER));
                    string nombreCompleto = $"{alumno.ApellidoPaterno} {alumno.ApellidoMaterno} {alumno.Nombre}";
                    tablaCalificaciones.AddCell(new PdfPCell(new Phrase(nombreCompleto, fontAlumnoNombreLargo))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Padding = 2f
                    });

                    // 1. Datos de los Meses (3 * 9 = 27 celdas individuales)
                    for (int i = 0; i < 27; i++)
                    {
                        if (i % numMaterias == (numMaterias - 1)) // La 9ª celda de cada mes es el Promedio
                        {
                            tablaCalificaciones.AddCell(CrearCelda("0.0", fontAlumnoNombreLargo, Element.ALIGN_CENTER, colorAmarillo));
                        }
                        else
                        {
                            tablaCalificaciones.AddCell(CrearCelda(" ", fontNormal, Element.ALIGN_CENTER));
                        }
                    }

                    // 2. Datos del TRIMESTRE (5 celdas CONSOLIDADAS que ocupan 9 columnas)

                    // Celda Consolidada 1: Comunicación (colSpan = 3) -> Cubre Español, Inglés, Artes
                    tablaCalificaciones.AddCell(CrearCelda("##.#", fontNormal, Element.ALIGN_CENTER, 1, 3));

                    // Celda Consolidada 2: Ciencias/P. Mate (colSpan = 3) -> Cubre Matemáticas, Tecnología, Ciencias
                    tablaCalificaciones.AddCell(CrearCelda("##.#", fontNormal, Element.ALIGN_CENTER, 1, 3));

                    // Celda 3: F. Cívica (colSpan = 1) -> Cubre F. Cívica
                    tablaCalificaciones.AddCell(CrearCelda("##.#", fontNormal, Element.ALIGN_CENTER));

                    // Celda 4: Ed. Física (colSpan = 1) -> Cubre Ed. Física
                    tablaCalificaciones.AddCell(CrearCelda("##.#", fontNormal, Element.ALIGN_CENTER));

                    // Celda 5: Promedio (colSpan = 1) -> Cubre Promedio de Trimestre
                    tablaCalificaciones.AddCell(CrearCelda("##.#", fontNormal, Element.ALIGN_CENTER, colorAmarillo));

                    // 3. Promedio Trimestral Final (colSpan = 1)
                    tablaCalificaciones.AddCell(CrearCelda("0.0", fontAlumnoNombreLargo, Element.ALIGN_CENTER, colorAmarillo));
                }


                // ====================================================================
                // FILA PROMEDIO GRUPAL (Misma unificación de 5 celdas)
                // ====================================================================
                tablaCalificaciones.AddCell(CrearCelda("TRIMESTRAL", fontSubtitulo, Element.ALIGN_CENTER, 1, 3, colorEncabezadoFijo));

                // 1. Promedio Grupal de los Meses (3 * 9 = 27 celdas individuales)
                for (int i = 0; i < 27; i++)
                {
                    if (i % numMaterias == (numMaterias - 1))
                    {
                        tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER, colorAmarillo));
                    }
                    else
                    {
                        tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER));
                    }
                }

                // 2. Promedio Grupal del TRIMESTRE (5 celdas CONSOLIDADAS)

                // Celda Consolidada 1: Comunicación (colSpan = 3)
                tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER, 1, 3));

                // Celda Consolidada 2: Ciencias/P. Mate (colSpan = 3)
                tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER, 1, 3));

                // Celda 3: F. Cívica (colSpan = 1)
                tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER));

                // Celda 4: Ed. Física (colSpan = 1)
                tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER));

                // Celda 5: Promedio (colSpan = 1)
                tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER, colorAmarillo));

                // 3. Promedio Trimestral Final (colSpan = 1)
                tablaCalificaciones.AddCell(CrearCelda("##.#", fontSubtitulo, Element.ALIGN_CENTER, colorAmarillo));

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

        // MÉTODOS AUXILIARES
        private PdfPCell CreateRotatedHeaderCell(string text, PdfFont font, int rowspan, BaseColor background)
        {
            return new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = background,
                Rotation = 90,
                Rowspan = rowspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                MinimumHeight = 70f,
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
                MinimumHeight = 50f
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
                MinimumHeight = 70f
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

        private PdfPCell CrearCelda(string texto, PdfFont fuente, int alineacion, int rowSpan, int colSpan, BaseColor fondoColor)
        {
            PdfPCell cell = CrearCelda(texto, fuente, alineacion);
            cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;
            cell.BackgroundColor = fondoColor;
            cell.BorderColor = colorBorde;
            return cell;
        }

        private PdfPCell CrearCelda(string texto, PdfFont fuente, int alineacion, int rowSpan, int colSpan)
        {
            // Sobrecarga utilizada para las celdas de datos consolidadas sin color de fondo
            PdfPCell cell = CrearCelda(texto, fuente, alineacion);
            cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;
            return cell;
        }

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