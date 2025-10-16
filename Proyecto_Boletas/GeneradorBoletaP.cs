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
    // Alias para evitar ambigüedad
    using PdfRectangle = iTextSharp.text.Rectangle;

    // Clase auxiliar para guardar temporalmente los datos del alumno y ordenar.
    internal class AlumnoInfo
    {
        public int AlumnoID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public string Grupo { get; set; }
        public string Maestro { get; set; }
    }

    internal class GeneradorBoletaP
    {
        // --- Fuentes (Usando iTextSharp.text.Font para evitar ambigüedad) ---
        private readonly iTextSharp.text.Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
        private readonly iTextSharp.text.Font fontClave = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
        private readonly iTextSharp.text.Font fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 8);
        private readonly iTextSharp.text.Font fontPromedio = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7);
        private readonly iTextSharp.text.Font fontNiveles = FontFactory.GetFont(FontFactory.HELVETICA, 7);

        // --- Colores ---
        private readonly BaseColor colorBorde = BaseColor.BLACK;
        private readonly BaseColor colorLila = new BaseColor(235, 230, 245);
        private readonly BaseColor colorAmarillo = new BaseColor(255, 255, 180);
        private readonly BaseColor colorRosa = new BaseColor(255, 200, 200);

        private readonly string[] camposAcademicos = {
            "ESPAÑOL", "INGLÉS", "ARTES", "MATEMÁTICAS", "TECNOLOGÍA",
            "CIENCIAS NATURALES", "FORMACIÓN CÍVICA Y ÉTICA", "ED. FÍSICA"
        };

        // Asumiendo que esta clase existe y proporciona la conexión.
        public MySqlConnection GetConnection()
        {
            // Implementación simulada, DEBES usar tu clase real
            // Ejemplo: return new MySqlConnection("server=localhost;database=midb;uid=user;pwd=pass;");
            throw new NotImplementedException("Debes usar la clase Conexion existente en tu proyecto.");
        }

        // ----------------------------------------------------------------------
        // --- MÉTODO PRINCIPAL (Calcula lista y obtiene datos) ---
        // ----------------------------------------------------------------------

        public void CrearBoletaPersonal(int idAlumno, string trimestre)
        {
            string nombreAlumno = "", nombreGrupo = "", nombreMaestro = "";
            string cicloEscolar = "2024-2025";
            string noLista = "S/N";

            AlumnoInfo alumnoObjetivo = null;
            int? idGrupo = null;

            try
            {
                // PASO 1: Obtener ID del grupo y datos básicos del alumno objetivo
                using (MySqlConnection conn = GetConnection()) // Usando la conexión
                {
                    conn.Open();
                    string queryGrupo = @"
                        SELECT a.id_grupo, a.Nombre, a.ApellidoPaterno, a.ApellidoMaterno, 
                               g.nombre_grupo, m.NombreMaestro, m.ApellidoPMaestro, m.ApellidoMMaestro
                        FROM alumnos a
                        INNER JOIN grupo g ON a.id_grupo = g.id_grupo
                        INNER JOIN maestro m ON g.id_maestro = m.id_maestro
                        WHERE a.AlumnoID = @idAlumno";

                    using (MySqlCommand cmd = new MySqlCommand(queryGrupo, conn))
                    {
                        cmd.Parameters.AddWithValue("@idAlumno", idAlumno);
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                idGrupo = dr.GetInt32("id_grupo");

                                alumnoObjetivo = new AlumnoInfo
                                {
                                    AlumnoID = idAlumno,
                                    Nombre = dr.GetString("Nombre"),
                                    ApellidoPaterno = dr.GetString("ApellidoPaterno"),
                                    ApellidoMaterno = dr.GetString("ApellidoMaterno"),
                                    NombreCompleto = $"{dr["ApellidoPaterno"]} {dr["ApellidoMaterno"]} {dr["Nombre"]}",
                                    Grupo = dr.GetString("nombre_grupo"),
                                    Maestro = $"{dr["NombreMaestro"]} {dr["ApellidoPMaestro"]} {dr["ApellidoMMaestro"]}"
                                };
                            }
                        }
                    }
                }

                if (alumnoObjetivo == null)
                {
                    MessageBox.Show("No se encontró información para el alumno seleccionado.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // PASO 2: Obtener a TODOS los alumnos del mismo grupo para el cálculo de lista
                using (MySqlConnection conn = GetConnection()) // Usando la conexión
                {
                    conn.Open();
                    List<AlumnoInfo> listaCompletaAlumnos = new List<AlumnoInfo>();
                    string queryTodosAlumnos = @"
                        SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno
                        FROM alumnos
                        WHERE id_grupo = @idGrupo";

                    using (MySqlCommand cmd = new MySqlCommand(queryTodosAlumnos, conn))
                    {
                        cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listaCompletaAlumnos.Add(new AlumnoInfo
                                {
                                    AlumnoID = dr.GetInt32("AlumnoID"),
                                    Nombre = dr.GetString("Nombre"),
                                    ApellidoPaterno = dr.GetString("ApellidoPaterno"),
                                    ApellidoMaterno = dr.GetString("ApellidoMaterno"),
                                });
                            }
                        }
                    }

                    // PASO 3: Ordenar y calcular No. Lista
                    var listaOrdenada = listaCompletaAlumnos
                                            .OrderBy(a => a.ApellidoPaterno)
                                            .ThenBy(a => a.ApellidoMaterno)
                                            .ThenBy(a => a.Nombre)
                                            .ToList();

                    int indiceAlumno = listaOrdenada.FindIndex(a => a.AlumnoID == idAlumno);

                    if (indiceAlumno >= 0)
                        noLista = (indiceAlumno + 1).ToString();
                }

                // Asignar datos finales
                nombreAlumno = alumnoObjetivo.NombreCompleto;
                nombreGrupo = alumnoObjetivo.Grupo;
                nombreMaestro = alumnoObjetivo.Maestro;


                // --- Configuración y Generación del PDF ---
                Document doc = new Document(PageSize.LETTER, 30, 30, 30, 30);
                string rutaSalida = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Boleta_Personal_{idAlumno}_{trimestre}.pdf");
                PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // --- Agregar secciones al PDF ---
                doc.Add(CrearEncabezadoSuperior(nombreGrupo, nombreMaestro, cicloEscolar, noLista));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph($"ALUMNO (A): {nombreAlumno}", fontClave));
                doc.Add(new Paragraph("\n"));
                doc.Add(CrearTablaPrincipalCalificaciones());
                doc.Add(new Paragraph("\n"));
                doc.Add(CrearTablaInasistenciasNiveles());
                doc.Add(new Paragraph("\n"));
                doc.Add(CrearSeccionFirmas());

                doc.Close();

                MessageBox.Show($"Boleta Personal generada correctamente en:\n{rutaSalida}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(rutaSalida);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la boleta: {ex.Message}", "ERROR CRÍTICO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------------
        // --- MÉTODOS AUXILIARES: Creación de Celdas ---
        // ----------------------------------------------------------------------

        /// <summary>Crea una celda de texto básica con estilo de borde.</summary>
        private PdfPCell CrearCelda(string texto, iTextSharp.text.Font fuente, int alineacion, int bordeEstilo)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                HorizontalAlignment = alineacion,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 3f,
                Border = bordeEstilo,
                BorderColor = colorBorde
            };
            return cell;
        }

        /// <summary>Crea una celda de texto con colspan y rowspan, con o sin fondo.</summary>
        private PdfPCell CrearCelda(string texto, iTextSharp.text.Font fuente, int alineacion, int colspan, int rowspan, BaseColor fondo)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                HorizontalAlignment = alineacion,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = colspan,
                Rowspan = rowspan,
                Padding = 3f,
                BorderColor = colorBorde,
                BackgroundColor = fondo
            };
            return cell;
        }

        /// <summary>Crea una celda de texto con fondo y alineación simple.</summary>
        private PdfPCell CrearCelda(string texto, iTextSharp.text.Font fuente, int alineacion, BaseColor fondo = null)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                HorizontalAlignment = alineacion,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 3f,
                BorderColor = colorBorde
            };
            if (fondo != null)
            {
                cell.BackgroundColor = fondo;
            }
            return cell;
        }

        /// <summary>Crea una celda con texto vertical.</summary>
        private PdfPCell CrearCeldaVertical(string texto, iTextSharp.text.Font fuente, BaseColor fondo, int rowspan, int colspan, BaseColor bordeColor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                Rotation = 90, // Rotación de 90 grados
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Rowspan = rowspan,
                Colspan = colspan,
                BackgroundColor = fondo,
                BorderColor = bordeColor,
                Padding = 2f
            };
            return cell;
        }

        // ----------------------------------------------------------------------------------
        // --- MÉTODOS DE ESTRUCTURA (Secciones del PDF) ---
        // ----------------------------------------------------------------------------------

        private PdfPTable CrearEncabezadoSuperior(string grupo, string maestro, string ciclo, string lista)
        {
            PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 0.2f, 0.4f, 0.2f, 0.2f });

            // Logo Placeholder
            table.AddCell(CrearCelda("LOGO", fontTexto, Element.ALIGN_CENTER, PdfRectangle.NO_BORDER));

            // Título central
            PdfPCell titleCell = new PdfPCell { Border = PdfRectangle.NO_BORDER };
            titleCell.AddElement(new Paragraph("INSTITUTO MANUEL M. ACOSTA", fontTitulo) { Alignment = Element.ALIGN_CENTER });
            titleCell.AddElement(new Paragraph("BOLETA INTERNA TRIMESTRAL", fontTexto) { Alignment = Element.ALIGN_CENTER });
            titleCell.AddElement(new Paragraph($"Grupo: {grupo} - Maestro: {maestro}", fontTexto) { Alignment = Element.ALIGN_CENTER });
            table.AddCell(titleCell);

            // Claves
            table.AddCell(CrearCelda("SECCIÓN: 3º PRIMARIA\nGRADO Y GRUPO:\nNO. DE LISTA:\nCICLO ESCOLAR:", fontClave, Element.ALIGN_RIGHT, PdfRectangle.NO_BORDER));
            table.AddCell(CrearCelda($"CLAVE:\n{grupo}\n{lista}\n{ciclo}", fontClave, Element.ALIGN_LEFT, PdfRectangle.NO_BORDER));

            return table;
        }

        private PdfPTable CrearTablaPrincipalCalificaciones()
        {
            PdfPTable tablaBase = new PdfPTable(2) { WidthPercentage = 100 };
            tablaBase.SetWidths(new float[] { 0.25f, 0.75f });

            // Campos Formativos (Columna Izquierda de la tabla principal)
            PdfPTable subCampos = new PdfPTable(2) { WidthPercentage = 100 };
            subCampos.SetWidths(new float[] { 0.35f, 0.65f });
            subCampos.AddCell(CrearCelda("CAMPOS DE FORMACIÓN ACADÉMICA", fontClave, Element.ALIGN_CENTER, 2, 1, colorBorde));

            // Lenguajes
            subCampos.AddCell(CrearCeldaVertical("LENGUAJES", fontClave, colorLila, 3, 1, colorBorde));
            subCampos.AddCell(CrearCelda(camposAcademicos[0], fontTexto, Element.ALIGN_LEFT));
            subCampos.AddCell(CrearCelda(camposAcademicos[1], fontTexto, Element.ALIGN_LEFT));
            subCampos.AddCell(CrearCelda(camposAcademicos[2], fontTexto, Element.ALIGN_LEFT));

            // Saberes
            subCampos.AddCell(CrearCeldaVertical("SABERES\nY PENS.\nCIENTÍFICO", fontClave, colorRosa, 3, 1, colorBorde));
            subCampos.AddCell(CrearCelda(camposAcademicos[3], fontTexto, Element.ALIGN_LEFT));
            subCampos.AddCell(CrearCelda(camposAcademicos[4], fontTexto, Element.ALIGN_LEFT));
            subCampos.AddCell(CrearCelda("CIENCIAS NATURALES", fontTexto, Element.ALIGN_LEFT, colorAmarillo));

            // Ética y Humano
            subCampos.AddCell(CrearCeldaVertical("DE LO\nHUMANO\nY LO\nCOMUN.", fontClave, colorLila, 2, 1, colorBorde));
            subCampos.AddCell(CrearCelda("FORMACIÓN CÍVICA Y ÉTICA", fontTexto, Element.ALIGN_LEFT, colorAmarillo));
            subCampos.AddCell(CrearCelda(camposAcademicos[7], fontTexto, Element.ALIGN_LEFT));

            // Promedio Mensual
            subCampos.AddCell(CrearCelda("PROM. MENSUAL", fontClave, Element.ALIGN_CENTER, 2, 1, colorAmarillo));

            tablaBase.AddCell(new PdfPCell(subCampos) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            // Calificaciones (Columna Derecha de la tabla principal)
            tablaBase.AddCell(new PdfPCell(CrearTablaCalificacionesMensuales()) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            return tablaBase;
        }

        private PdfPTable CrearTablaCalificacionesMensuales()
        {
            PdfPTable table = new PdfPTable(16) { WidthPercentage = 100 };
            // 10 meses + 5 periodos + 1 final
            float[] widths = { 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.08f, 0.08f, 0.08f, 0.08f, 0.08f, 0.08f };
            table.SetWidths(widths);

            // Fila 1: Títulos principales CORREGIDOS
            table.AddCell(CrearCelda("CALIFICACIONES MENSUALES", fontClave, Element.ALIGN_CENTER, 10, 1, colorBorde));
            table.AddCell(CrearCelda("PERÍODOS", fontClave, Element.ALIGN_CENTER, 5, 1, colorBorde));
            table.AddCell(CrearCelda("P. FINAL", fontClave, Element.ALIGN_CENTER, 1, 2, colorBorde)); // Rowspan 2

            // Fila 2: Subtítulos (Meses y Trimestres)
            string[] meses = { "DIAG.", "SEPT.", "OCT.", "NOV.", "ENE.", "FEBR.", "MARZO", "ABRIL", "MAYO", "JUNIO" };
            foreach (string mes in meses) table.AddCell(CrearCelda(mes, fontPromedio, Element.ALIGN_CENTER));

            string[] trimestres = { "TRIM. 1", "TRIM. 2", "TRIM. 3", "CALIF.", "P. FINAL" };
            foreach (string t in trimestres) table.AddCell(CrearCelda(t, fontPromedio, Element.ALIGN_CENTER));

            // Filas de datos (9 materias + 1 promedio) x 16 columnas
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 16; c++)
                {
                    BaseColor fondo = BaseColor.WHITE;
                    if (r == 9 || c >= 10)
                        fondo = colorAmarillo;

                    table.AddCell(CrearCelda(" ", fontTexto, Element.ALIGN_CENTER, fondo));
                }
            }

            return table;
        }



        private PdfPTable CrearTablaInasistenciasNiveles()
        {
            PdfPTable tablaFinal = new PdfPTable(1) { WidthPercentage = 100 };
            tablaFinal.DefaultCell.Border = PdfRectangle.NO_BORDER;

            // --- 1. Tabla de Inasistencias ---
            PdfPTable inasistencias = new PdfPTable(14) { WidthPercentage = 100 };
            // 1 Título + 10 Meses (D, S, O, N, E, F, M, A, M, J) + 3 CALIF. + P.F
            float[] widths = { 0.16f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.08f, 0.08f, 0.08f, 0.08f };
            inasistencias.SetWidths(widths);

            // Fila 1: Título de inasistencias y Meses/Periodos
            inasistencias.AddCell(CrearCelda("INASISTENCIAS", fontClave, Element.ALIGN_LEFT));

            string[] mesesYPeriodos = { "D", "S", "O", "N", "E", "F", "M", "A", "M", "J", "CALIF.", "CALIF.", "CALIF.", "P.F" };
            foreach (string item in mesesYPeriodos)
            {
                // Usamos colorAmarillo para los campos de CALIF. y P.F.
                BaseColor colorFondo = item.Contains("CALIF.") || item == "P.F" ? colorAmarillo : BaseColor.WHITE;
                inasistencias.AddCell(CrearCelda(item, fontPromedio, Element.ALIGN_CENTER, colorFondo));
            }

            // Fila 2: Celdas vacías para datos
            for (int i = 0; i < 14; i++)
            {
                inasistencias.AddCell(CrearCelda(" ", fontTexto, Element.ALIGN_CENTER));
            }

            tablaFinal.AddCell(new PdfPCell(inasistencias) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            // --- 2. Niveles de Desempeño ---
            tablaFinal.AddCell(new Paragraph("\n"));
            PdfPTable niveles = new PdfPTable(1) { WidthPercentage = 100 };
            niveles.AddCell(CrearCelda("NIVELES DE DESEMPEÑO", fontClave, Element.ALIGN_CENTER, colorBorde));

            float[] nivelWidths = { 0.2f, 0.8f };

            // Nivel I a IV con sus descripciones
            (string titulo, string descripcion, BaseColor color)[] nivelesData = new (string, string, BaseColor)[]
            {
                ("NIVEL I - EQUIVALE A 5", "El estudiante tiene carencias fundamentales en valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", BaseColor.WHITE),
                ("NIVEL II - EQUIVALE A 6 Y 7", "El estudiante tiene dificultades para demostrar valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorAmarillo),
                ("NIVEL III - EQUIVALE A 8 Y 9", "El estudiante ha demostrado los valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", BaseColor.WHITE),
                ("NIVEL IV - EQUIVALE A 10", "El estudiante ha demostrado los valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorLila)
            };

            foreach (var nivel in nivelesData)
            {
                PdfPTable subTable = new PdfPTable(2) { WidthPercentage = 100 };
                subTable.SetWidths(nivelWidths);
                subTable.AddCell(CrearCelda(nivel.titulo, fontNiveles, Element.ALIGN_LEFT, nivel.color));
                subTable.AddCell(CrearCelda(nivel.descripcion, fontNiveles, Element.ALIGN_LEFT, nivel.color));
                niveles.AddCell(new PdfPCell(subTable) { Padding = 0, Border = PdfRectangle.BOX, BorderColor = colorBorde });
            }

            tablaFinal.AddCell(new PdfPCell(niveles) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            return tablaFinal;
        }

        private PdfPTable CrearSeccionFirmas()
        {
            PdfPTable tablaBase = new PdfPTable(2) { WidthPercentage = 100 };
            tablaBase.SetWidths(new float[] { 0.5f, 0.5f });
            tablaBase.DefaultCell.Border = PdfRectangle.NO_BORDER;

            float[] firmaWidths = { 0.4f, 0.6f };

            Action<PdfPTable> addFirmaHeaders = (table) =>
            {
                table.AddCell(CrearCelda("MES", fontClave, Element.ALIGN_CENTER));
                table.AddCell(CrearCelda("FIRMA DEL PADRE O TUTOR", fontClave, Element.ALIGN_CENTER));
            };

            // --- Columna Izquierda (Agosto a Enero) ---
            PdfPTable colIzquierda = new PdfPTable(2) { WidthPercentage = 100 };
            colIzquierda.SetWidths(firmaWidths);
            addFirmaHeaders(colIzquierda);

            string[] mesesIzq = { "AGOSTO DIAGNÓSTICO", "SEPTIEMBRE", "OCTUBRE", "NOV./ DIC.", "ENERO" };
            foreach (string mes in mesesIzq)
            {
                colIzquierda.AddCell(CrearCelda(mes, fontTexto, Element.ALIGN_LEFT, PdfRectangle.NO_BORDER));
                PdfPCell firmaCell = new PdfPCell(new Phrase(" ", fontTexto))
                {
                    Border = PdfRectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    PaddingTop = 15f
                };
                colIzquierda.AddCell(firmaCell);
            }
            tablaBase.AddCell(new PdfPCell(colIzquierda) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            // --- Columna Derecha (Febrero a Junio) ---
            PdfPTable colDerecha = new PdfPTable(2) { WidthPercentage = 100 };
            colDerecha.SetWidths(firmaWidths);
            addFirmaHeaders(colDerecha);

            string[] mesesDer = { "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO" };
            foreach (string mes in mesesDer)
            {
                colDerecha.AddCell(CrearCelda(mes, fontTexto, Element.ALIGN_LEFT, PdfRectangle.NO_BORDER));
                PdfPCell firmaCell = new PdfPCell(new Phrase(" ", fontTexto))
                {
                    Border = PdfRectangle.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    PaddingTop = 15f
                };
                colDerecha.AddCell(firmaCell);
            }
            tablaBase.AddCell(new PdfPCell(colDerecha) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            return tablaBase;
        }

    }
}