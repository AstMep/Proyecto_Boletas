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
using System.Data; // Incluido para asegurar la compatibilidad con System.Data si se usa.

namespace Proyecto_Boletas
{
    using PdfFont = iTextSharp.text.Font;
    using PdfImage = iTextSharp.text.Image;
    using PdfRectangle = iTextSharp.text.Rectangle;

    internal class GeneradorBoletaT
    {
        // ====================================================================
        // PROPIEDADES, FUENTES Y COLORES (TUS DEFINICIONES ORIGINALES)
        // ====================================================================
        private readonly PdfFont fontTitulo = new PdfFont(PdfFont.FontFamily.HELVETICA, 12, PdfFont.BOLD);
        private readonly PdfFont fontSubtitulo = new PdfFont(PdfFont.FontFamily.HELVETICA, 8, PdfFont.BOLD);
        private readonly PdfFont fontNormal = new PdfFont(PdfFont.FontFamily.HELVETICA, 7, PdfFont.NORMAL);
        private readonly PdfFont fontAlumnoNombreLargo = new PdfFont(PdfFont.FontFamily.HELVETICA, 6, PdfFont.NORMAL);
        private readonly PdfFont fontEncabezadoRotado = new PdfFont(PdfFont.FontFamily.HELVETICA, 7, PdfFont.BOLD, BaseColor.BLACK);

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
        private readonly BaseColor colorGrisClaro = new BaseColor(173, 216, 230); // Español
        private readonly BaseColor colorLenguaje = new BaseColor(255, 192, 203); // Rosa Claro
        private readonly BaseColor colorMatematicasF = new BaseColor(173, 216, 230); // Celeste
        private readonly BaseColor colorExploracion = new BaseColor(189, 252, 177); // Verde Claro
        private readonly BaseColor colorDesarrollo = new BaseColor(255, 239, 153); // Amarillo Pálido

        private string[] materiasBase;
        private string nombreMateriaCiencias;

        // Lista de Campos Formativos para el cálculo consolidado
        private readonly string[] camposConsolidados = new[] {
            "LENGUAJES",
            "SABERES Y PENSAMIENTO CIENTÍFICO",
            "ÉTICA, NATURALEZA Y SOCIEDADES",
            "DE LO HUMANO Y LO COMUNITARIO"
        };

        // ====================================================================
        // ESTRUCTURAS DE DATOS DE ALUMNOS Y RESULTADOS
        // ====================================================================

        public class Alumno
        {
            public int AlumnoID { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Genero { get; set; }
            public Dictionary<string, double> Notas { get; set; } = new Dictionary<string, double>();
            public double PromedioFinalTrimestral { get; set; } = 0.0;
        }

        public class PromediosGrupales
        {
            public Dictionary<string, double> Mensuales { get; set; } = new Dictionary<string, double>();
            public Dictionary<string, double> Consolidados { get; set; } = new Dictionary<string, double>();
            public double PromedioFinalTrimestral { get; set; } = 0.0;
        }

        // ====================================================================
        // FUNCIONES DE UTILIDAD (CONVERSIÓN Y MAPEO)
        // ====================================================================

        private string[] ObtenerMeses(string trimestre)
        {
            return trimestre.ToUpper().Trim() switch
            {
                // Incluimos NOV_DIC, SEP y OCT para el 1er Trimestre.
                "1ER TRIMESTRE" => new[] { "DIAGNOSTICO", "SEP", "OCT", "NOV_DIC" },
                "2DO TRIMESTRE" => new[] { "ENE", "FEB", "MAR" },
                "3ER TRIMESTRE" => new[] { "ABR", "MAY", "JUN" },
                _ => new[] { "SEP", "OCT", "NOV_DIC" }
            };
        }

        private string ConvertirMesParaBD(string mesCompleto)
        {
            string limpio = mesCompleto.ToUpper().Trim();
            if (limpio.Contains("AGOSTO") || limpio.Contains("DIAGNOSTICO")) return "DIAGNOSTICO";
            // Asume que la función ObtenerMeses ya devuelve NOV_DIC si es necesario.
            if (limpio.Contains("NOVIEMBRE")) return "NOV_DIC";
            return limpio.Substring(0, 3);
        }

        private string ObtenerNombreMateriaCiencias(string nombreGrupo)
        {
            string nombreNormalizado = nombreGrupo.ToLower().Trim();

            if (nombreNormalizado.Contains("primero") || nombreNormalizado.Contains("segundo"))
            { return "CONOCIMIENTO DEL MEDIO"; }
            else if (nombreNormalizado.Contains("tercero") || nombreNormalizado.Contains("cuarto") || nombreNormalizado.Contains("quinto") || nombreNormalizado.Contains("sexto"))
            { return "CIENCIAS NATURALES"; }
            return "CONOCIMIENTO DEL MEDIO";
        }
        private BaseColor ObtenerColorMateria(string materia)
        {
            return materia.ToUpper().Trim() switch
            {
                "ESPAÑOL" => colorLenguaje,
                "INGLÉS" => colorLenguaje,
                "ARTES" => colorLenguaje,
                "MATEMÁTICAS" => colorMatematicasF,
                "TECNOLOGÍA" => colorMatematicasF,
                "CIENCIAS NATURALES" => colorMatematicasF,
                "CONOCIMIENTO DEL MEDIO" => colorMatematicasF,
                "FORM. CÍV Y ÉTICA" => colorExploracion,
                "ED. FISICA" => colorDesarrollo,
                "PROMEDIO" => colorAmarillo,
                _ => BaseColor.WHITE
            };
        }

        private string ObtenerCampoFormativo(string nombreMateria)
        {
            string materia = nombreMateria.ToUpper().Trim();
            return materia switch
            {
                "ESPAÑOL" or "INGLÉS" or "ARTES" => "LENGUAJES",
                "MATEMÁTICAS" or "TECNOLOGÍA" or "CIENCIAS NATURALES" or "CONOCIMIENTO DEL MEDIO" => "SABERES Y PENSAMIENTO CIENTÍFICO",
                "FORM. CÍV Y ÉTICA" => "ÉTICA, NATURALEZA Y SOCIEDADES",
                "ED. FISICA" => "DE LO HUMANO Y LO COMUNITARIO",
                _ => "SIN CAMPO"
            };
        }

        // ====================================================================
        // FUNCIONES DE EXTRACCIÓN Y CÁLCULO
        // ====================================================================

        private Dictionary<int, Alumno> ObtenerCalificacionesTrimestrales(List<Alumno> alumnos, string[] periodosBD, string periodoFinalBD)
        {
            var resultados = alumnos.ToDictionary(a => a.AlumnoID, a => a);
            Conexion db = new Conexion();

            string alumnoIds = string.Join(",", alumnos.Select(a => a.AlumnoID));
            List<string> periodos = periodosBD.ToList();
            periodos.Add(periodoFinalBD);
            string periodosIn = string.Join(",", periodos.Select(p => $"'{p}'"));

            string query = $@"
                SELECT c.AlumnoID, m.Nombre AS NombreMateria, c.Calificacion, c.Periodo 
                FROM calificaciones c 
                INNER JOIN materias m ON c.MateriaID = m.MateriaID 
                WHERE c.AlumnoID IN ({alumnoIds}) AND c.Periodo IN ({periodosIn})";

            try
            {
                using (MySqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int alumnoId = dr.GetInt32("AlumnoID");
                            string materia = dr.GetString("NombreMateria").ToUpper().Trim();
                            string periodo = dr.GetString("Periodo").ToUpper().Trim();
                            double calificacion = dr.GetDouble("Calificacion");

                            if (resultados.TryGetValue(alumnoId, out Alumno alumno))
                            {
                                if (periodo == periodoFinalBD)
                                {
                                    alumno.PromedioFinalTrimestral = calificacion;
                                }
                                else
                                {
                                    // Clave de almacenamiento: MATERIA_PERIODO (Ej: ESPAÑOL_SEP)
                                    string clave = $"{materia}_{periodo}";
                                    alumno.Notas[clave] = calificacion;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener calificaciones trimestrales: " + ex.Message);
            }
            return resultados;
        }

        private double CalcularPromedioTrimestralCampo(Dictionary<string, double> calificaciones, string[] materiasBase, string[] periodosBD, string campoObjetivo)
        {
            double sumaTotalCalificaciones = 0;
            int numMateriasEnCampo = 0;

            foreach (string nombreMateria in materiasBase)
            {
                if (nombreMateria.ToUpper() == "PROMEDIO") continue;
                if (ObtenerCampoFormativo(nombreMateria.ToUpper().Trim()) == campoObjetivo)
                {
                    numMateriasEnCampo++;
                }
            }

            int divisorFijo = numMateriasEnCampo * periodosBD.Length;

            if (divisorFijo == 0) return 0.0;

            foreach (string nombreMateria in materiasBase)
            {
                if (nombreMateria.ToUpper() == "PROMEDIO") continue;

                if (ObtenerCampoFormativo(nombreMateria.ToUpper().Trim()) == campoObjetivo)
                {
                    foreach (string periodo in periodosBD)
                    {
                        string clave = $"{nombreMateria.ToUpper().Trim()}_{periodo.ToUpper().Trim()}";
                        double calif = 0.0;
                        if (calificaciones.TryGetValue(clave, out double valorEncontrado))
                        {
                            calif = valorEncontrado;
                        }

                        sumaTotalCalificaciones += calif;
                    }
                }
            }

            return Math.Round(sumaTotalCalificaciones / divisorFijo, 1);
        }

        private PromediosGrupales CalcularPromediosGrupales(Dictionary<int, Alumno> resultadosAlumnos, string[] materiasBase, string[] periodosBD)
        {
            var promedios = new PromediosGrupales();
            int totalAlumnos = resultadosAlumnos.Count;
            if (totalAlumnos == 0) return promedios;

            var sumasMensuales = new Dictionary<string, double>();
            var sumasConsolidadas = new Dictionary<string, double>();
            double sumaFinalTrimestral = 0;

            foreach (var alumno in resultadosAlumnos.Values)
            {
                // 1. Suma de Calificaciones Mensuales (Materia por Mes)
                foreach (var kvp in alumno.Notas)
                {
                    if (!sumasMensuales.ContainsKey(kvp.Key)) sumasMensuales[kvp.Key] = 0;
                    sumasMensuales[kvp.Key] += kvp.Value;
                }

                // 2. Cálculo de Promedios Consolidados por Campo Formativo (por alumno)
                foreach (var campo in camposConsolidados)
                {
                    double promCampo = CalcularPromedioTrimestralCampo(alumno.Notas, materiasBase, periodosBD, campo);

                    if (!sumasConsolidadas.ContainsKey(campo)) sumasConsolidadas[campo] = 0;
                    sumasConsolidadas[campo] += promCampo;
                }

                // 3. Suma del Promedio Final Trimestral (el que está guardado en la BD)
                sumaFinalTrimestral += alumno.PromedioFinalTrimestral;
            }

            // 4. CALCULAR PROMEDIO FINAL DIVIDIENDO POR EL TOTAL DE ALUMNOS
            foreach (var kvp in sumasMensuales)
            { promedios.Mensuales[kvp.Key] = Math.Round(kvp.Value / totalAlumnos, 1); }

            foreach (var kvp in sumasConsolidadas)
            { promedios.Consolidados[kvp.Key] = Math.Round(kvp.Value / totalAlumnos, 1); }

            promedios.PromedioFinalTrimestral = Math.Round(sumaFinalTrimestral / totalAlumnos, 1);

            return promedios;
        }

        private void RegistrarGeneracionBoletaGrupal(int idGrupo, string trimestre, string rutaArchivo)
        {
            Conexion db = new Conexion();
            string periodoBD = $"{trimestre.Replace(" ", "_").ToUpper()}";

            string query = "INSERT INTO boletas (AlumnoID, CalificaionID, Periodo, RutaArchivo, FechaGeneracion) " +
                           "VALUES (@idGrupo, NULL, @periodo, @ruta, NOW())";

            try
            {
                using (MySqlConnection conn = db.GetConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                    cmd.Parameters.AddWithValue("@periodo", periodoBD);
                    cmd.Parameters.AddWithValue("@ruta", rutaArchivo);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar boleta en BD: {ex.Message}");
            }
        }

        // ====================================================================
        // FUNCIÓN PRINCIPAL: CREAR BOLETA GRUPAL (COMPLETA)
        // ====================================================================

        public void CrearBoletaGrupal(int idGrupo, string trimestre)
        {
            string nombreGrupo = "";
            string nombreMaestro = "";
            List<Alumno> alumnos = new List<Alumno>();
            string rutaSalida = "";

            // 1. DEFINICIÓN DE PARÁMETROS TRIMESTRALES
            string[] meses = ObtenerMeses(trimestre);
            string[] periodosBD = meses.Select(m => ConvertirMesParaBD(m)).ToArray();
            string periodoFinalBD = $"{trimestre.Replace(" ", "_").ToUpper()}_FINAL";

            // Variables de configuración de PDF
            int numMeses = periodosBD.Length;
            int numMaterias = 0;
            int totalColumnas = 0;

            Document doc = null;
            PdfWriter writer = null;

            try
            {
                // 2. SELECCIONAR RUTA DE GUARDADO
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar Boleta Grupal";
                saveFileDialog.FileName = $"Boleta_Grupo_{idGrupo}_{trimestre}.pdf";

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                rutaSalida = saveFileDialog.FileName;

                // 3. EXTRAER DATOS DEL GRUPO, MAESTRO, ALUMNOS
                using (MySqlConnection conn = new Conexion().GetConnection())
                {
                    conn.Open();

                    // Obtener Grupo y Maestro
                    string queryGrupo = "SELECT g.nombre_grupo, g.id_maestro, m.NombreMaestro, m.ApellidoPMaestro, m.ApellidoMMaestro FROM grupo g INNER JOIN maestro m ON g.id_maestro = m.id_maestro WHERE g.id_grupo = @idGrupo";
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

                    // Obtener Alumnos
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
                                    AlumnoID = dr.GetInt32("AlumnoID"),
                                    ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                                    ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Genero = dr["genero"].ToString(),
                                });
                            }
                        }
                    }
                }

                alumnos = alumnos.OrderBy(a => a.ApellidoPaterno).ThenBy(a => a.ApellidoMaterno).ThenBy(a => a.Nombre).ToList();

                nombreMateriaCiencias = ObtenerNombreMateriaCiencias(nombreGrupo);

                // Lista de 9 elementos (8 materias + 1 PROMEDIO)
                materiasBase = new[] {
                    "ESPAÑOL", "INGLÉS", "ARTES", "MATEMÁTICAS", "TECNOLOGÍA",
                    nombreMateriaCiencias,
                    "FORM. CÍV Y ÉTICA", "ED. FISICA", "PROMEDIO"
                };
                numMaterias = materiasBase.Length;

                // Total Columnas: 3 (Lista, Sexo, Nombre) + (NumMeses * 9 Materias) + 5 (Columnas Trimestrales)
                // 3 + (4 * 9) + 5 = 44 COLUMNAS TOTALES si el 1er Trimestre tiene 4 períodos.
                totalColumnas = 3 + (numMeses * numMaterias) + 5;

                // 4. EXTRAER CALIFICACIONES TRIMESTRALES DE TODOS LOS ALUMNOS
                var resultadosAlumnos = ObtenerCalificacionesTrimestrales(alumnos, periodosBD, periodoFinalBD);

                // 5. CÁLCULO DE PROMEDIOS GRUPALES
                var promediosGrupo = CalcularPromediosGrupales(resultadosAlumnos, materiasBase, periodosBD);

                // 6. CONFIGURACIÓN DEL PDF
                doc = new Document(PageSize.A4.Rotate(), 15, 15, 20, 20);
                writer = PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // ====================================================================
                // CABECERA 
                // ====================================================================
                PdfPTable encabezado = new PdfPTable(2) { WidthPercentage = 100 };
                encabezado.SetWidths(new float[] { 20, 80 });

                // ... (Código de Encabezado) ...
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
                catch { /* Manejo de error de logo */ }

                if (logo != null)
                {
                    logo.ScaleToFit(70f, 70f);
                    PdfPCell logoCell = new PdfPCell(logo, false);
                    logoCell.Border = PdfRectangle.NO_BORDER;
                    logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    logoCell.PaddingBottom = 5f;
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

                // ====================================================================
                // TABLA PRINCIPAL - ESTRUCTURA Y ENCABEZADOS
                // ====================================================================

                float[] widths = new float[totalColumnas];
                widths[0] = 0.02f; widths[1] = 0.02f; widths[2] = 0.15f;
                float dataWidth = (1.00f - 0.19f) / (totalColumnas - 3);
                for (int i = 3; i < totalColumnas; i++) { widths[i] = dataWidth; }

                PdfPTable tablaCalificaciones = new PdfPTable(totalColumnas) { WidthPercentage = 100, HeaderRows = 2 };
                tablaCalificaciones.SetWidths(widths);

                // --- FILA 1: TÍTULOS DE PERÍODOS Y CAMPOS ---
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("NO.\nLISTA", fontEncabezadoRotado, 2, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("SEXO", fontEncabezadoRotado, 2, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CreateHeaderCell("NOMBRE DEL ALUMNO", fontSubtitulo, 2, 1, colorEncabezadoFijo));

                // Encabezados de MESES (LENGUAJES, SABERES, etc.)
                foreach (string mes in meses)
                {
                    string mesTexto = mes.ToUpper().Replace("DIAGNOSTICO", "DIAG.").Replace("NOV_DIC", "NOV/DIC");
                    tablaCalificaciones.AddCell(CrearCelda(mesTexto, fontSubtitulo, Element.ALIGN_CENTER, numMaterias, 1, colorEncabezadoFijo));
                }

                // Encabezado TRIMESTRE (Consolidados)
                string textoTrimestre = $"TRIMESTRE ({trimestre.ToUpper()}) 2025";
                // Colspan = 5 (4 Campos + 1 Promedio Final)
                tablaCalificaciones.AddCell(CrearCelda(textoTrimestre, fontSubtitulo, Element.ALIGN_CENTER, 5, 1, colorEncabezadoFijo));

                // --- FILA 2: NOMBRES DE MATERIAS Y CAMPOS (ROTADOS) ---

                // Este bucle crea la sub-fila de materias para cada mes
                for (int m = 0; m < meses.Length; m++)
                {
                    foreach (string materia in materiasBase)
                    {
                        string textoCelda = materia;
                        if (materia == nombreMateriaCiencias) textoCelda = "C. NAT./CON. MEDIO";
                        else if (materia == "FORM. CÍV Y ÉTICA") textoCelda = "F. CÍV Y ÉTICA";
                        else if (materia == "MATEMÁTICAS") textoCelda = "MAT.";
                        else if (materia == "TECNOLOGÍA") textoCelda = "TECNOLOGÍA";
                        else if (materia == "ED. FISICA") textoCelda = "ED. FÍSICA";
                        else if (materia == "PROMEDIO") textoCelda = "PROMEDIO";

                        BaseColor fondo = ObtenerColorMateria(materia);
                        tablaCalificaciones.AddCell(CreateRotatedCell(textoCelda, fontNormal, fondo));
                    }
                }

                // Sub-fila TRIMESTRE (Nombres de Campos Consolidados)
                foreach (var campo in camposConsolidados)
                {
                    string textoCelda = campo.ToUpper().Replace(" Y PENSAMIENTO CIENTÍFICO", " Y PENS. CIENT.").Replace("Y LO COMUNITARIO", " Y COM.");
                    tablaCalificaciones.AddCell(CreateRotatedCell(textoCelda, fontNormal, BaseColor.LIGHT_GRAY)); // Fondo gris para campos
                }
                // Columna de Promedio Final Trimestral
                tablaCalificaciones.AddCell(CreateRotatedCell("PROMEDIO\nTRIMESTRAL", fontEncabezadoRotado, colorAmarillo));


                // ====================================================================
                // LLENADO DE DATOS POR ALUMNO
                // ====================================================================

                int listaContador = 1;
                foreach (var alumno in alumnos)
                {
                    // Columnas fijas
                    tablaCalificaciones.AddCell(CrearCelda(listaContador++.ToString(), fontNormal, Element.ALIGN_CENTER));
                    tablaCalificaciones.AddCell(CrearCelda(alumno.Genero.Substring(0, 1), fontNormal, Element.ALIGN_CENTER));
                    string nombreCompleto = $"{alumno.ApellidoPaterno} {alumno.ApellidoMaterno} {alumno.Nombre}";
                    tablaCalificaciones.AddCell(new PdfPCell(new Phrase(nombreCompleto, fontAlumnoNombreLargo))
                    { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2f });

                    // 1. Datos de los Meses (Calificaciones Mensuales y Promedio Mensual)
                    for (int m = 0; m < meses.Length; m++)
                    {
                        string periodo = periodosBD[m];

                        // Aseguramos que solo iteramos sobre las materias base (8 materias + PROMEDIO)
                        for (int i = 0; i < numMaterias; i++)
                        {
                            string materia = materiasBase[i].ToUpper().Trim();

                            if (materia == "PROMEDIO")
                            {
                                // CALCULA Y MUESTRA EL PROMEDIO MENSUAL POR ALUMNO
                                double sumMes = 0;
                                int countMes = 0;
                                // Iterar solo sobre las 8 materias reales (no el promedio)
                                for (int j = 0; j < numMaterias - 1; j++)
                                {
                                    string matActual = materiasBase[j].ToUpper().Trim();
                                    string clave = $"{matActual}_{periodo}";
                                    if (alumno.Notas.ContainsKey(clave))
                                    {
                                        sumMes += alumno.Notas[clave];
                                        countMes++;
                                    }
                                }

                                double promMes = (countMes > 0) ? Math.Round(sumMes / countMes, 1) : 0.0;

                                tablaCalificaciones.AddCell(CrearCelda(promMes.ToString("F1"), fontAlumnoNombreLargo, Element.ALIGN_CENTER, colorAmarillo));
                            }
                            else
                            {
                                // Calificación individual
                                string clave = $"{materia}_{periodo}";
                                double calif = alumno.Notas.ContainsKey(clave) ? alumno.Notas[clave] : 0.0;
                                string textoCalif = calif > 0 ? calif.ToString("F0") : "-";
                                tablaCalificaciones.AddCell(CrearCelda(textoCalif, fontNormal, Element.ALIGN_CENTER));
                            }
                        }
                    }

                    // 2. Datos del TRIMESTRE (PROMEDIOS CONSOLIDADOS POR CAMPO)
                    double promGeneralTrimestre = 0;
                    int contadorCampos = 0;

                    foreach (var campo in camposConsolidados)
                    {
                        double promCampo = CalcularPromedioTrimestralCampo(alumno.Notas, materiasBase, periodosBD, campo);

                        // Colspan de la celda de Campo Formativo
                        int colSpan = (campo == "LENGUAJES" || campo == "SABERES Y PENSAMIENTO CIENTÍFICO") ? 3 : 1;

                        tablaCalificaciones.AddCell(CrearCelda(promCampo.ToString("F1"), fontNormal, Element.ALIGN_CENTER, colSpan, 1));

                        promGeneralTrimestre += promCampo;
                        contadorCampos++;
                    }

                    // 3. Promedio Final Trimestral del ALUMNO (Valor guardado en BD)
                    string promFinalDB = alumno.PromedioFinalTrimestral > 0 ? alumno.PromedioFinalTrimestral.ToString("F1") : "0.0";
                    tablaCalificaciones.AddCell(CrearCelda(promFinalDB, fontAlumnoNombreLargo, Element.ALIGN_CENTER, colorAmarillo));
                }

                // ====================================================================
                // FILA PROMEDIO GRUPAL (ULTIMA FILA)
                // ====================================================================

                // Fijas (NO. LISTA, SEXO, NOMBRE DEL ALUMNO)
                tablaCalificaciones.AddCell(CrearCelda("##", fontSubtitulo, Element.ALIGN_CENTER, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CrearCelda("##", fontSubtitulo, Element.ALIGN_CENTER, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CrearCelda("PROMEDIO", fontSubtitulo, Element.ALIGN_CENTER, colorEncabezadoFijo));

                // 1. Promedio Grupal de los Meses (Materias y Promedio Mensual)
                for (int m = 0; m < meses.Length; m++)
                {
                    string periodo = periodosBD[m];
                    for (int i = 0; i < numMaterias; i++)
                    {
                        string materia = materiasBase[i].ToUpper().Trim();
                        string clave = $"{materia}_{periodo}";

                        double prom = promediosGrupo.Mensuales.ContainsKey(clave) ? promediosGrupo.Mensuales[clave] : 0.0;
                        BaseColor fondo = (materia == "PROMEDIO") ? colorAmarillo : BaseColor.WHITE;

                        tablaCalificaciones.AddCell(CrearCelda(prom.ToString("F1"), fontSubtitulo, Element.ALIGN_CENTER, fondo));
                    }
                }

                // 2. Promedio Grupal del TRIMESTRE (5 celdas CONSOLIDADAS)
                foreach (var campo in camposConsolidados)
                {
                    double promCampo = promediosGrupo.Consolidados.ContainsKey(campo) ? promediosGrupo.Consolidados[campo] : 0.0;

                    int colSpan = (campo == "LENGUAJES" || campo == "SABERES Y PENSAMIENTO CIENTÍFICO") ? 3 : 1;

                    tablaCalificaciones.AddCell(CrearCelda(promCampo.ToString("F1"), fontSubtitulo, Element.ALIGN_CENTER, colSpan, 1));
                }

                // 3. Promedio Trimestral Final GRUPAL 
                string promFinalTrimestralGrupal = promediosGrupo.PromedioFinalTrimestral.ToString("F1");
                tablaCalificaciones.AddCell(CrearCelda(promFinalTrimestralGrupal, fontSubtitulo, Element.ALIGN_CENTER, colorAmarillo));

                // 7. Cierre y Registro
                doc.Add(tablaCalificaciones);
                doc.Close();
                writer.Close();

                RegistrarGeneracionBoletaGrupal(idGrupo, trimestre, rutaSalida);

                MessageBox.Show($"Boleta grupal generada correctamente en:\n{rutaSalida}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (doc != null && doc.IsOpen()) doc.Close();
                if (writer != null) writer.Close();
                MessageBox.Show($"Error al generar boleta grupal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CrearBoleta(int idAlumno, string trimestre)
        {
            // Este método queda vacío ya que no se implementó la lógica de boleta individual
        }

        // ... (Tus métodos auxiliares CreateRotatedHeaderCell, CrearCelda, etc. deben ir aquí)

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
            PdfPCell cell = CrearCelda(texto, fuente, alineacion);
            cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;
            return cell;
        }
    }
}