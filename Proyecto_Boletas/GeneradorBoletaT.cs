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
            return trimestre switch
            {
                "1er Trimestre" => new[] { "Septiembre", "Octubre", "Noviembre" },
                "2do Trimestre" => new[] { "Enero", "Febrero", "Marzo" },
                "3er Trimestre" => new[] { "Abril", "Mayo", "Junio" },
                _ => new[] { "Mes 1", "Mes 2", "Mes 3" }
            };
        }

        private string ConvertirMesParaBD(string mesCompleto)
        {
            // La BD usa SEP, OCT, NOV, etc., que es el código que el sistema de captura debe usar.
            string limpio = mesCompleto.ToUpper().Trim();
            if (limpio.Contains("AGOSTO")) return "DIAGNOSTICO"; // Por si se llega a usar
            return limpio.Substring(0, 3);
        }

        private string ObtenerNombreMateriaCiencias(string nombreGrupo)
        {
            string nombreNormalizado = nombreGrupo.ToLower().Trim();

            if (nombreNormalizado.Contains("primero") || nombreNormalizado.Contains("segundo"))
            { return "CONOCIMIENTO DEL MEDIO"; } // Nombre completo para los cálculos
            else if (nombreNormalizado.Contains("tercero") || nombreNormalizado.Contains("cuarto") || nombreNormalizado.Contains("quinto") || nombreNormalizado.Contains("sexto"))
            { return "CIENCIAS NATURALES"; } // Nombre completo para los cálculos
            return "CONOCIMIENTO DEL MEDIO";
        }
        private BaseColor ObtenerColorMateria(string materia)
        {
            return materia.ToUpper().Trim() switch
            {
                "ESPAÑOL" => colorGrisClaro,
                "INGLÉS" => colorCeleste,
                "ARTES" => colorMorado,
                "MATEMÁTICAS" => colorRosa,
                "TECNOLOGÍA" => colorNaranja,
                // Usar los nombres completos aquí también para el mapeo de color
                "CIENCIAS NATURALES" => colorAzulClaro,
                "CONOCIMIENTO DEL MEDIO" => colorAzulClaro,
                "FORM. CÍV Y ÉTICA" => colorVerde,
                "ED. FISICA" => colorVerdeOscuro,
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
            double sumaPromediosMensuales = 0;
            int contadorMateriasEncontradas = 0;

            foreach (string nombreMateria in materiasBase)
            {
                if (nombreMateria.ToUpper() == "PROMEDIO") continue;

                if (ObtenerCampoFormativo(nombreMateria.ToUpper().Trim()) == campoObjetivo)
                {
                    double sumaMensual = 0;
                    int contadorMeses = 0;

                    foreach (string periodo in periodosBD)
                    {
                        string clave = $"{nombreMateria.ToUpper().Trim()}_{periodo.ToUpper().Trim()}";
                        if (calificaciones.TryGetValue(clave, out double calif))
                        {
                            sumaMensual += calif;
                            contadorMeses++;
                        }
                    }

                    if (contadorMeses > 0)
                    {
                        sumaPromediosMensuales += sumaMensual / contadorMeses;
                        contadorMateriasEncontradas++;
                    }
                }
            }

            if (contadorMateriasEncontradas == 0) return 0.0;
            return Math.Round(sumaPromediosMensuales / contadorMateriasEncontradas, 1);
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

            // Asume que AlumnoID en la tabla boletas puede usarse para guardar el GroupID para registros grupales
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
            int numMeses = meses.Length;
            int numGrupos = meses.Length + 1;
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

                // Aquí nombreMateriaCiencias se usará internamente con el nombre completo
                nombreMateriaCiencias = ObtenerNombreMateriaCiencias(nombreGrupo);

                materiasBase = new[] {
            "ESPAÑOL", "INGLÉS", "ARTES", "MATEMÁTICAS", "TECNOLOGÍA",
            // Usa el nombre completo aquí para que las funciones de cálculo y extracción funcionen correctamente
            nombreMateriaCiencias,
            "FORM. CÍV Y ÉTICA", "ED. FISICA", "PROMEDIO"
        };
                numMaterias = materiasBase.Length;
                totalColumnas = 3 + ((numMeses + 1) * numMaterias) + 1;

                // 4. EXTRAER CALIFICACIONES TRIMESTRALES DE TODOS LOS ALUMNOS
                var resultadosAlumnos = ObtenerCalificacionesTrimestrales(alumnos, periodosBD, periodoFinalBD);

                // 5. CÁLCULO DE PROMEDIOS GRUPALES
                var promediosGrupo = CalcularPromediosGrupales(resultadosAlumnos, materiasBase, periodosBD);

                // 6. CONFIGURACIÓN DEL PDF
                // Ajustamos los márgenes para que haya menos espacio arriba si es necesario
                doc = new Document(PageSize.A4.Rotate(), 15, 15, 20, 20); // Margen superior de 20 (antes 40)
                writer = PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // ====================================================================
                // CABECERA 
                // ====================================================================
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
                catch { /* Manejo de error de logo */ }

                if (logo != null)
                {
                    logo.ScaleToFit(70f, 70f);
                    PdfPCell logoCell = new PdfPCell(logo, false);
                    logoCell.Border = PdfRectangle.NO_BORDER;
                    logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    logoCell.PaddingBottom = 5f; // Ajuste para subir un poco el logo
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
                // Eliminamos el Paragraph(" ") para reducir el espacio y subir la tabla
                // doc.Add(new Paragraph(" ")); 
                // ====================================================================

                // TABLA PRINCIPAL 
                PdfPTable tablaCalificaciones = new PdfPTable(totalColumnas) { WidthPercentage = 100, HeaderRows = 2 };

                float[] widths = new float[totalColumnas];
                widths[0] = 0.02f; widths[1] = 0.02f; widths[2] = 0.15f;
                float smallWidth = (1.00f - 0.19f) / 37f;
                for (int i = 3; i < totalColumnas; i++) { widths[i] = smallWidth; }
                tablaCalificaciones.SetWidths(widths);

                // ====================================================================
                // FILA 1 & 2: Encabezados (Meses, Materias y Trimestre)
                // ====================================================================
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("NO.\nLISTA", fontEncabezadoRotado, 2, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("SEXO", fontEncabezadoRotado, 2, colorEncabezadoFijo));
                tablaCalificaciones.AddCell(CreateHeaderCell("NOMBRE DEL ALUMNO", fontSubtitulo, 2, 1, colorEncabezadoFijo));

                foreach (string mes in meses)
                { tablaCalificaciones.AddCell(CrearCelda(mes.ToUpper(), fontSubtitulo, Element.ALIGN_CENTER, 1, numMaterias, colorEncabezadoFijo)); }

                string textoTrimestre = $"TRIMESTRE ({trimestre.ToUpper()}) 2025";
                tablaCalificaciones.AddCell(CrearCelda(textoTrimestre, fontSubtitulo, Element.ALIGN_CENTER, 1, numMaterias, colorEncabezadoFijo));

                tablaCalificaciones.AddCell(CreateRotatedHeaderCell("PROMEDIO\nTRIMESTRAL", fontEncabezadoRotado, 2, colorAmarillo));

                // FILA 2: Nombres de materias con colores (con abreviación para la impresión)
                for (int m = 0; m < meses.Length; m++)
                {
                    foreach (string materia in materiasBase)
                    {
                        string textoCelda = materia;

                        // APLICAR ABREVIATURA PARA LA IMPRESIÓN ROTADA
                        if (materia == "CONOCIMIENTO DEL MEDIO")
                        {
                            textoCelda = "CON. DEL MEDIO";
                        }
                        else if (materia == "CIENCIAS NATURALES")
                        {
                            textoCelda = "C. NATURALES";
                        }

                        BaseColor fondo = ObtenerColorMateria(materia);
                        tablaCalificaciones.AddCell(CreateRotatedCell(textoCelda, fontNormal, fondo));
                    }
                }
                // Materias del TRIMESTRE (igual, con abreviación)
                foreach (string materia in materiasBase)
                {
                    string textoCelda = materia;

                    if (materia == "CONOCIMIENTO DEL MEDIO")
                    {
                        textoCelda = "CON. DEL MEDIO";
                    }
                    else if (materia == "CIENCIAS NATURALES")
                    {
                        textoCelda = "C. NATURALES";
                    }

                    BaseColor fondo = ObtenerColorMateria(materia);
                    tablaCalificaciones.AddCell(CreateRotatedCell(textoCelda, fontNormal, fondo));
                }

                // ====================================================================
                // LLENADO DE DATOS POR ALUMNO
                // ====================================================================

                int listaContador = 1;
                double promGeneralTrimestre = 0;
                int contadorCampos = 0;
                string promFinalDB = "";

                foreach (var alumno in alumnos)
                {
                    promGeneralTrimestre = 0;
                    contadorCampos = 0;

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
                        for (int i = 0; i < numMaterias; i++)
                        {
                            string materia = materiasBase[i].ToUpper().Trim();

                            if (materia == "PROMEDIO")
                            {
                                double sumMes = alumno.Notas.Where(kv => kv.Key.EndsWith($"_{periodo.ToUpper()}") && ObtenerCampoFormativo(kv.Key.Split('_')[0]) != "SIN CAMPO").Sum(kv => kv.Value);
                                int countMes = alumno.Notas.Count(kv => kv.Key.EndsWith($"_{periodo.ToUpper()}") && ObtenerCampoFormativo(kv.Key.Split('_')[0]) != "SIN CAMPO");
                                double promMes = (countMes > 0) ? Math.Round(sumMes / countMes, 1) : 0.0;

                                tablaCalificaciones.AddCell(CrearCelda(promMes.ToString("F1"), fontAlumnoNombreLargo, Element.ALIGN_CENTER, colorAmarillo));
                            }
                            else
                            {
                                string clave = $"{materia}_{periodo}";
                                double calif = alumno.Notas.ContainsKey(clave) ? alumno.Notas[clave] : 0.0;
                                string textoCalif = calif > 0 ? calif.ToString("F0") : "-";
                                tablaCalificaciones.AddCell(CrearCelda(textoCalif, fontNormal, Element.ALIGN_CENTER));
                            }
                        }
                    }

                    // 2. Datos del TRIMESTRE (PROMEDIOS CONSOLIDADOS)
                    foreach (var campo in camposConsolidados)
                    {
                        double promCampo = CalcularPromedioTrimestralCampo(alumno.Notas, materiasBase, periodosBD, campo);
                        int colSpan = (campo == "LENGUAJES" || campo == "SABERES Y PENSAMIENTO CIENTÍFICO") ? 3 : 1;

                        tablaCalificaciones.AddCell(CrearCelda(promCampo.ToString("F1"), fontNormal, Element.ALIGN_CENTER, 1, colSpan));

                        promGeneralTrimestre += promCampo;
                        contadorCampos++;
                    }

                    double promFinalCampos = (contadorCampos > 0) ? Math.Round(promGeneralTrimestre / contadorCampos, 1) : 0.0;
                    tablaCalificaciones.AddCell(CrearCelda(promFinalCampos.ToString("F1"), fontNormal, Element.ALIGN_CENTER, colorAmarillo));

                    // 3. Promedio Trimestral Final (Guardado como 'TRIMESTRE_X_FINAL' en la BD)
                    promFinalDB = alumno.PromedioFinalTrimestral > 0 ? alumno.PromedioFinalTrimestral.ToString("F1") : "0.0";
                    tablaCalificaciones.AddCell(CrearCelda(promFinalDB, fontAlumnoNombreLargo, Element.ALIGN_CENTER, colorAmarillo));
                }

                // ====================================================================
                // FILA PROMEDIO GRUPAL 
                // ====================================================================
                tablaCalificaciones.AddCell(CrearCelda("TRIMESTRAL", fontSubtitulo, Element.ALIGN_CENTER, 1, 3, colorEncabezadoFijo));

                // 1. Promedio Grupal de los Meses
                for (int m = 0; m < meses.Length; m++)
                {
                    string periodo = periodosBD[m];
                    for (int i = 0; i < numMaterias; i++)
                    {
                        string materia = materiasBase[i].ToUpper().Trim();
                        string clave = $"{materia}_{periodo}";

                        if (materia == "PROMEDIO")
                        {
                            double promMes = promediosGrupo.Mensuales.ContainsKey(clave) ? promediosGrupo.Mensuales[clave] : 0.0;
                            tablaCalificaciones.AddCell(CrearCelda(promMes.ToString("F1"), fontSubtitulo, Element.ALIGN_CENTER, colorAmarillo));
                        }
                        else
                        {
                            double promMateria = promediosGrupo.Mensuales.ContainsKey(clave) ? promediosGrupo.Mensuales[clave] : 0.0;
                            tablaCalificaciones.AddCell(CrearCelda(promMateria.ToString("F1"), fontSubtitulo, Element.ALIGN_CENTER));
                        }
                    }
                }

                // 2. Promedio Grupal del TRIMESTRE (5 celdas CONSOLIDADAS)
                double promGeneralTrimestreGrupo = 0;
                contadorCampos = 0;

                foreach (var campo in camposConsolidados)
                {
                    double promCampo = promediosGrupo.Consolidados.ContainsKey(campo) ? promediosGrupo.Consolidados[campo] : 0.0;
                    int colSpan = (campo == "LENGUAJES" || campo == "SABERES Y PENSAMIENTO CIENTÍFICO") ? 3 : 1;

                    tablaCalificaciones.AddCell(CrearCelda(promCampo.ToString("F1"), fontSubtitulo, Element.ALIGN_CENTER, 1, colSpan));

                    promGeneralTrimestreGrupo += promCampo;
                    contadorCampos++;
                }

                double promFinalCamposGrupo = (contadorCampos > 0) ? Math.Round(promGeneralTrimestreGrupo / contadorCampos, 1) : 0.0;
                tablaCalificaciones.AddCell(CrearCelda(promFinalCamposGrupo.ToString("F1"), fontSubtitulo, Element.ALIGN_CENTER, colorAmarillo));

                // 3. Promedio Trimestral Final GRUPAL 
                promFinalDB = promediosGrupo.PromedioFinalTrimestral.ToString("F1");
                tablaCalificaciones.AddCell(CrearCelda(promFinalDB, fontSubtitulo, Element.ALIGN_CENTER, colorAmarillo));

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

        // ====================================================================
        // MÉTODOS AUXILIARES (TUS DEFINICIONES ORIGINALES)
        // ====================================================================

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
            // Este método queda vacío ya que no se implementó la lógica de boleta individual
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