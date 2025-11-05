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
using System.Data;

namespace Proyecto_Boletas
{
    using PdfRectangle = iTextSharp.text.Rectangle;


    // ====================================================================
    // ESTRUCTURAS AUXILIARES
    // ====================================================================

    internal class AlumnoInfo
    {
        public int AlumnoID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; } // ApellidoPaterno ApellidoMaterno Nombre
        public string Grupo { get; set; }
        public string Maestro { get; set; }
        public int IdGrupo { get; set; } // Necesario para buscar materias
        public int NoLista { get; set; }
    }

    internal class DatosBoleta
    {
        // [8 materias, 10 meses]
        public decimal?[,] CalificacionesMensuales { get; set; } = new decimal?[8, 10];
        // [8 materias, 3 trimestres + 1 P. Final]
        public decimal?[,] CalificacionesPeriodales { get; set; } = new decimal?[8, 4];
        // [10 meses]
        public decimal?[] PromediosMensuales { get; set; } = new decimal?[10];
        public decimal? PFinalPromedio { get; set; }
        public decimal? PromedioEdFinal { get; set; }
        public int?[] Inasistencias { get; set; } = new int?[14]; // Asumo 10 meses + 4 totales/finales
    }

    // ====================================================================
    // CLASE GENERADORA DEL PDF
    // ====================================================================

    internal class GeneradorBoletaP
    {
        // 🎯 AJUSTE DE TAMAÑO DE FUENTES 
        private readonly iTextSharp.text.Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
        private readonly iTextSharp.text.Font fontClave = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
        private readonly iTextSharp.text.Font fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 7);
        private readonly iTextSharp.text.Font fontPromedio = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6);
        private readonly iTextSharp.text.Font fontNiveles = FontFactory.GetFont(FontFactory.HELVETICA, 6);

        // --- Colores (Tomados de GeneradorBoletaT) ---
        private readonly BaseColor colorBorde = BaseColor.BLACK;
        private readonly BaseColor colorBlanco = BaseColor.WHITE;
        private readonly BaseColor colorLila = new BaseColor(173, 216, 230); // LENGUAJES (Esp, Ing, Art)
        private readonly BaseColor colorMagenta = new BaseColor(255, 182, 193); // SABERES (Mate, Tec)
        private readonly BaseColor colorAmarilloClaro = new BaseColor(255, 245, 200); // ÉTICA (F. Cívica, Ciencias)
        private readonly BaseColor colorVerdeClaro  = new BaseColor(152, 251, 152); // Ed. Física // HUMANO (Ed. Física)
        private readonly BaseColor colorPromedio = new BaseColor(255, 223, 100);
        private readonly BaseColor colorGrisInasistencias = new BaseColor(240, 240, 240);

        // --- Mapeo Interno de Materias y Meses ---
        // CÓDIGO CORREGIDO para coincidir con la BD si usas NOV_DIC
        private readonly string[] mesesBD = { "DIAGNOSTICO", "SEP", "OCT", "NOV_DIC", "ENE", "FEB", "MAR", "ABR", "MAY", "JUN" };
        private readonly string[] materiasBD = { "ESPAÑOL", "INGLÉS", "ARTES", "MATEMÁTICAS", "TECNOLOGÍA", "CIENCIAS_CONDICIONAL", "FORM. CÍV Y ÉTICA", "ED. FISICA" };


        private string[] materiasBase;

        private MySqlConnection GetConnection()
        {
            Conexion conexion = new Conexion();
            return conexion.GetConnection();
        }

        private string ObtenerNombreMateriaCiencias(string nombreGrupo)
        {
            string nombreNormalizado = nombreGrupo.ToLower().Trim();

            if (nombreNormalizado.Contains("primero") || nombreNormalizado.Contains("segundo"))
            {
                return "CONOCIMIENTO DEL MEDIO";
            }
            else if (nombreNormalizado.Contains("tercero") ||
                     nombreNormalizado.Contains("cuarto") ||
                     nombreNormalizado.Contains("quinto") ||
                     nombreNormalizado.Contains("sexto"))
            {
                return "CIENCIAS NATURALES";
            }
            return "CONOCIMIENTO DEL MEDIO";
        }

        // Nuevo: Obtiene el color de la fila basado en la lógica de los Campos Formativos
        private BaseColor ObtenerColorMateriaFila(int indexMateria)
        {
            if (indexMateria >= 0 && indexMateria <= 2) return colorLila; // LENGUAJES
            if (indexMateria >= 3 && indexMateria <= 4) return colorMagenta; // SABERES (Mate, Tec)
            if (indexMateria >= 5 && indexMateria <= 6) return colorAmarilloClaro; // ÉTICA (Ciencias, F. Civica)
            if (indexMateria == 7) return colorVerdeClaro; // HUMANO (Ed. Física)
            return colorBlanco;
        }

        // ====================================================================
        // LÓGICA DE EXTRACCIÓN DE DATOS REALES
        // ====================================================================

        private AlumnoInfo ObtenerInfoAlumno(int idAlumno)
        {
            AlumnoInfo info = null;
            string query = @"
                SELECT A.Nombre, A.ApellidoPaterno, A.ApellidoMaterno, G.nombre_grupo, G.id_grupo, 
                       M.NombreMaestro, M.ApellidoPMaestro, M.ApellidoMMaestro,
                       (SELECT COUNT(*) FROM alumnos WHERE id_grupo = A.id_grupo AND AlumnoID <= A.AlumnoID) AS NoLista
                FROM alumnos A
                INNER JOIN grupo G ON A.id_grupo = G.id_grupo
                INNER JOIN maestro M ON G.id_maestro = M.id_maestro
                WHERE A.AlumnoID = @idAlumno";

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idAlumno", idAlumno);
                conn.Open();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        info = new AlumnoInfo
                        {
                            AlumnoID = idAlumno,
                            Nombre = dr["Nombre"].ToString(),
                            ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                            Grupo = dr["nombre_grupo"].ToString(),
                            IdGrupo = dr.GetInt32("id_grupo"),
                            Maestro = $"{dr["NombreMaestro"]} {dr["ApellidoPMaestro"]} {dr["ApellidoMMaestro"]}",
                            NoLista = dr.GetInt32("NoLista"),
                        };
                        info.NombreCompleto = $"{info.ApellidoPaterno} {info.ApellidoMaterno} {info.Nombre}".ToUpper();
                    }
                }
            }
            return info;
        }

        public DataTable ObtenerPromediosTrimestrales(int alumnoId)
        {
            DataTable dtPromedios = new DataTable();
            Conexion db = new Conexion();

            // Esta es la consulta combinada que calcula los 3 trimestres
            string query = @"
        SELECT
            M.Nombre AS Materia,
            ROUND(AVG(CASE WHEN C.Periodo IN ('SEP', 'OCT', 'NOV_DIC') THEN C.Calificacion END), 1) AS P_1TRIM,
            ROUND(AVG(CASE WHEN C.Periodo IN ('ENE', 'FEB', 'MAR') THEN C.Calificacion END), 1) AS P_2TRIM,
            ROUND(AVG(CASE WHEN C.Periodo IN ('ABR', 'MAY', 'JUN') THEN C.Calificacion END), 1) AS P_3TRIM
        FROM 
            calificaciones C
        JOIN 
            materias M ON C.MateriaID = M.MateriaID
        WHERE 
            C.AlumnoID = @alumnoId
        GROUP BY 
            M.Nombre, M.MateriaID
        ORDER BY 
            M.MateriaID;"; // Usar MateriaID para asegurar el orden

            try
            {
                using (MySqlConnection connection = db.GetConnection())
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@alumnoId", alumnoId);
                        connection.Open();

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dtPromedios);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener promedios trimestrales: " + ex.Message);
            }
            return dtPromedios;
        }

        private DatosBoleta ObtenerDatosBoleta(int idAlumno, string trimestre, int idGrupo)
        {
            DatosBoleta datos = new DatosBoleta();

            // 1. Mapeo de Materias Condicionales para la consulta
            string nombreCienciasBD = ObtenerNombreMateriaCiencias(idGrupo.ToString()).ToUpper();

            // Reemplazar la materia condicional en el array de mapeo interno
            string[] materiasBDActualizadas = (string[])materiasBD.Clone();
            materiasBDActualizadas[5] = nombreCienciasBD;

            // 2. Determinar Periodos a Consultar (Ajustado a NOV_DIC)
            string[] mesesTrimestre;
            string periodoFinal;

            if (trimestre.Contains("1er")) { mesesTrimestre = new[] { "SEP", "OCT", "NOV_DIC" }; periodoFinal = "1ER_TRIMESTRE_FINAL"; }
            else if (trimestre.Contains("2do")) { mesesTrimestre = new[] { "ENE", "FEB", "MAR" }; periodoFinal = "2DO_TRIMESTRE_FINAL"; }
            else { mesesTrimestre = new[] { "ABR", "MAY", "JUN" }; periodoFinal = "3ER_TRIMESTRE_FINAL"; }

            // 3. Extracción de Calificaciones MENSUALES (Directa desde la BD)
            string periodosInQuery = $"'{string.Join("','", mesesBD)}'";

            string queryMensual = $@"
        SELECT M.Nombre AS NombreMateria, C.Calificacion, C.Periodo 
        FROM calificaciones C
        INNER JOIN materias M ON C.MateriaID = M.MateriaID
        WHERE C.AlumnoID = @idAlumno
        AND C.Periodo IN ({periodosInQuery})"; // Consulta solo los 10 períodos de captura

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryMensual, conn);
                cmd.Parameters.AddWithValue("@idAlumno", idAlumno);
                conn.Open();

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string materia = dr["NombreMateria"].ToString().ToUpper().Trim();
                        string periodo = dr["Periodo"].ToString().ToUpper().Trim();
                        decimal calificacion = dr.GetDecimal("Calificacion");

                        int r = Array.IndexOf(materiasBDActualizadas, materia);
                        int cMensual = Array.IndexOf(mesesBD, periodo);

                        if (r != -1 && cMensual != -1) // Si la materia y el período se mapean
                        {
                            datos.CalificacionesMensuales[r, cMensual] = calificacion;
                        }
                    }
                }
                conn.Close(); // Cerrar la primera conexión

                // --- Extracción de PROMEDIOS TRIMESTRALES (Usando la función SQL) ---

                // Ejecutamos la función que calcula los promedios trimestrales por materia en SQL
                DataTable dtPromedios = ObtenerPromediosTrimestrales(idAlumno);

                // Llenar la matriz CalificacionesPeriodales con los resultados de la BD
                if (dtPromedios != null && dtPromedios.Rows.Count > 0)
                {
                    foreach (DataRow row in dtPromedios.Rows)
                    {
                        string materiaNombre = row["Materia"].ToString().ToUpper().Trim();
                        int r = Array.IndexOf(materiasBDActualizadas, materiaNombre);

                        if (r != -1)
                        {
                            // 1º TRIM (Columna 0)
                            if (row["P_1TRIM"] != DBNull.Value) datos.CalificacionesPeriodales[r, 0] = Convert.ToDecimal(row["P_1TRIM"]);
                            // 2º TRIM (Columna 1)
                            if (row["P_2TRIM"] != DBNull.Value) datos.CalificacionesPeriodales[r, 1] = Convert.ToDecimal(row["P_2TRIM"]);
                            // 3º TRIM (Columna 2)
                            if (row["P_3TRIM"] != DBNull.Value) datos.CalificacionesPeriodales[r, 2] = Convert.ToDecimal(row["P_3TRIM"]);
                        }
                    }
                }
            }


            // 4. CÁLCULO DE PROMEDIOS MENSUALES (Sigue igual)
            decimal sumaFinalTrim = 0;
            int countFinalTrim = 0;

            for (int c = 0; c < 10; c++)
            {
                decimal suma = 0;
                int count = 0;

                for (int r = 0; r < 8; r++) // 8 materias
                {
                    if (datos.CalificacionesMensuales[r, c].HasValue)
                    {
                        suma += datos.CalificacionesMensuales[r, c].Value;
                        count++;
                    }
                }
                if (count > 0) datos.PromediosMensuales[c] = Math.Round(suma / count, 1);

                // Sumar promedios mensuales para el cálculo del Promedio Ed. Final (c > 0 excluye Diagnóstico)
                if (c > 0 && datos.PromediosMensuales[c].HasValue)
                {
                    sumaFinalTrim += datos.PromediosMensuales[c].Value;
                    countFinalTrim++;
                }
            }

            //5.CÁLCULO DE PROMEDIOS GENERALES TRIMESTRALES(FILA AMARILLA)
// Creamos un array para almacenar el promedio general de la boleta por cada trimestre.
decimal?[] promediosGeneralesTrimestrales = new decimal?[4]; // 3 trimestres + P. Final

            for (int cTrimestre = 0; cTrimestre < 3; cTrimestre++) // Itera sobre 1º TRIM (0), 2º TRIM (1), 3º TRIM (2)
            {
                decimal sumaPromediosTrimestrales = 0;
                int materiasContadas = 0;

                // Sumar el promedio trimestral de cada materia (r=0 a r=7) para el trimestre actual
                for (int r = 0; r < 8; r++) // 8 materias
                {
                    // Se asume que datos.CalificacionesPeriodales ya se llenó con los promedios por materia (P_1TRIM, P_2TRIM, P_3TRIM)
                    if (datos.CalificacionesPeriodales[r, cTrimestre].HasValue)
                    {
                        sumaPromediosTrimestrales += datos.CalificacionesPeriodales[r, cTrimestre].Value;
                        materiasContadas++;
                    }
                }

                if (materiasContadas > 0)
                {
                    // Guardar el Promedio General del Trimestre (el valor que va en la fila amarilla)
                    promediosGeneralesTrimestrales[cTrimestre] = Math.Round(sumaPromediosTrimestrales / materiasContadas, 1);
                }
            }

            // 6. CÁLCULO DEL PROMEDIO FINAL CALIF (P. FINAL CALIF)
            // Promedio de los 3 promedios generales trimestrales
            decimal sumaFinalCalif = 0;
            int countFinalCalif = 0;

            for (int i = 0; i < 3; i++) // Sumar los 3 promedios generales
            {
                if (promediosGeneralesTrimestrales[i].HasValue)
                {
                    sumaFinalCalif += promediosGeneralesTrimestrales[i].Value;
                    countFinalCalif++;
                }
            }

            if (countFinalCalif > 0)
            {
                datos.PFinalPromedio = Math.Round(sumaFinalCalif / countFinalCalif, 1);
                promediosGeneralesTrimestrales[3] = datos.PFinalPromedio; // Guardar también en la posición 3 (P. Final)
            }



            return datos; 
            }

        // ====================================================================
        // CÓDIGO DEL GENERADOR DE PDF
        // ====================================================================

        public void CrearBoletaPersonal(int idAlumno, string trimestre)
        {
            string rutaSalida = string.Empty;

            try
            {
                // 1. OBTENER INFO Y DATOS
                AlumnoInfo infoAlumno = ObtenerInfoAlumno(idAlumno);
                if (infoAlumno == null)
                {
                    MessageBox.Show("No se encontró información para el alumno seleccionado.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Extraer datos de calificaciones (llena las matrices)
                DatosBoleta datosDelAlumno = ObtenerDatosBoleta(idAlumno, trimestre, infoAlumno.IdGrupo);

                // Re-inicializar materiasBase con el nombre condicional correcto y en mayúsculas
                string nombreMateriaCiencias = ObtenerNombreMateriaCiencias(infoAlumno.Grupo).ToUpper();
                materiasBase = new[] {
                    "ESPAÑOL", "INGLÉS", "ARTES",
                    "MATEMÁTICAS", "TECNOLOGÍA",
                    nombreMateriaCiencias,
                    "FORM. CÍV Y ÉTICA",
                    "ED. FISICA"
                };

                // 2. USO DE SaveFileDialog
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = $"Boleta_Personal_{infoAlumno.NombreCompleto.Replace(" ", "_")}_{trimestre}.pdf";
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";

                    if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                    rutaSalida = saveFileDialog.FileName;
                }

                // 3. Configuración y Generación del PDF
                Document doc = new Document(PageSize.LETTER, 30, 30, 30, 30);
                PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // 4. Agregar secciones al PDF
                doc.Add(CrearEncabezadoSuperior(infoAlumno.Grupo, infoAlumno.Maestro, "2024-2025", infoAlumno.NoLista.ToString()));

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph($"ALUMNO (A): {infoAlumno.NombreCompleto}", fontClave));
                doc.Add(new Paragraph("\n"));


                doc.Add(CrearTablaPrincipalCalificaciones(datosDelAlumno));

                doc.Add(new Paragraph("\n"));

                doc.Add(CrearTablaInasistenciasNiveles(datosDelAlumno));

                doc.Add(new Paragraph("\n"));
                doc.Add(CrearSeccionFirmas());

                doc.Close();

                // 5. Abrir el PDF
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = rutaSalida, UseShellExecute = true });
                }
                catch { /* Manejo de error de apertura */ }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la boleta: {ex.Message}", "ERROR CRÍTICO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ====================================================================
        // MÉTODOS AUXILIARES: TABLAS Y CELDAS
        // ====================================================================
        private PdfPTable CrearEncabezadoSuperior(string grupo, string maestro, string ciclo, string lista)
        {
            PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 0.2f, 0.4f, 0.2f, 0.2f });

            PdfPCell logoCell;
            iTextSharp.text.Image logo = null;

            try
            {
                System.Drawing.Image imageFromResources = Proyecto_Boletas.Properties.Resources.logo_escuela350;

                using (MemoryStream ms = new MemoryStream())
                {
                    imageFromResources.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    logo = iTextSharp.text.Image.GetInstance(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el logo desde recursos internos: " + ex.Message);
            }


            if (logo != null)
            {
                logo.ScaleToFit(70f, 70f);
                logoCell = new PdfPCell(logo, false) { Border = PdfRectangle.NO_BORDER };
            }
            else
            {
                logoCell = CrearCelda("LOGO FALTANTE\n(Verificar Recursos)", fontTexto, Element.ALIGN_CENTER, PdfRectangle.NO_BORDER);
            }

            logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
            logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(logoCell);

            PdfPCell titleCell = new PdfPCell { Border = PdfRectangle.NO_BORDER };
            titleCell.AddElement(new Paragraph("INSTITUTO MANUEL M. ACOSTA", fontTitulo) { Alignment = Element.ALIGN_CENTER });
            titleCell.AddElement(new Paragraph("BOLETA INTERNA TRIMESTRAL", fontTexto) { Alignment = Element.ALIGN_CENTER });
            titleCell.AddElement(new Paragraph($"Grupo: {grupo} - Maestro: {maestro}", fontTexto) { Alignment = Element.ALIGN_CENTER });
            table.AddCell(titleCell);

            table.AddCell(CrearCelda("SECCIÓN: PRIMARIA\nGRADO Y GRUPO:\nNO. DE LISTA:\nCICLO ESCOLAR:", fontClave, Element.ALIGN_RIGHT, PdfRectangle.NO_BORDER));
            table.AddCell(CrearCelda($"CLAVE:\n{grupo}\n{lista}\n{ciclo}", fontClave, Element.ALIGN_LEFT, PdfRectangle.NO_BORDER));

            return table;
        }

        private PdfPTable CrearTablaPrincipalCalificaciones(DatosBoleta datos)
        {
            PdfPTable tablaBase = new PdfPTable(17) { WidthPercentage = 100 };

            float[] widths = { 0.11f, 0.12f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.015f, 0.06f, 0.06f, 0.06f, 0.09f };
            tablaBase.SetWidths(widths);

            // --- FILA 1: TÍTULOS DE ENCABEZADO SUPERIOR ---

            // Campo: CAMPOS DE FORMACIÓN (Rowspan 2)
            PdfPCell tituloCampos = CrearCelda("CAMPOS DE FORMACIÓN ACADÉMICA", fontClave, Element.ALIGN_CENTER, 2, 2, colorGrisInasistencias);
            tituloCampos.MinimumHeight = 35f;
            tablaBase.AddCell(tituloCampos);

            // Calificaciones Mensuales (Colspan 10)
            PdfPCell tituloMensuales = CrearCelda("CALIFICACIONES MENSUALES", fontClave, Element.ALIGN_CENTER, 10, 1, colorGrisInasistencias);
            tituloMensuales.MinimumHeight = 16f;
            tablaBase.AddCell(tituloMensuales);

            // ESPACIO VACÍO (Rowspan 2) - SIN BORDES
            PdfPCell espacioSeparador = new PdfPCell(new Phrase("", fontTexto))
            {
                Rowspan = 2,
                Border = PdfRectangle.NO_BORDER,
                BackgroundColor = BaseColor.WHITE,
                MinimumHeight = 35f
            };
            tablaBase.AddCell(espacioSeparador);

            // Períodos (Colspan 4)
            PdfPCell periodosCell = CrearCelda("PERÍODOS", fontClave, Element.ALIGN_CENTER, 4, 1, colorGrisInasistencias);
            tablaBase.AddCell(periodosCell);

            // --- FILA 2: SUBTÍTULOS DE CALIFICACIONES ---
            string[] mesesAbrev = { "DIA G.", "SEP T", "OC T", "NOV/DI", "ENE RO", "FEB", "MAR", "ABR IL", "MAY", "JUN" };
            foreach (string mes in mesesAbrev)
            {
                PdfPCell cell = CrearCelda(mes, fontPromedio, Element.ALIGN_CENTER, colorGrisInasistencias);
                cell.MinimumHeight = 22f;
                tablaBase.AddCell(cell);
            }

            string[] trimestres = { "1º TRIM", "2º TRIM", "3º TRIM" };
            foreach (string t in trimestres)
            {
                PdfPCell cell = CrearCelda(t, fontPromedio, Element.ALIGN_CENTER, colorGrisInasistencias);
                cell.MinimumHeight = 22f;
                tablaBase.AddCell(cell);
            }

            // P. FINAL y CALIF
            PdfPCell finalCell = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_TOP,
                MinimumHeight = 22f,
                PaddingTop = 3f,
                BackgroundColor = colorGrisInasistencias,
                BorderColor = colorBorde
            };
            finalCell.AddElement(new Paragraph("P. FINAL", fontPromedio) { Alignment = Element.ALIGN_CENTER });
            finalCell.AddElement(new Paragraph("CALIF", fontPromedio) { Alignment = Element.ALIGN_CENTER });
            tablaBase.AddCell(finalCell);

            // --- FILAS 3-11: DATOS DE MATERIAS Y PROMEDIOS ---
            for (int r = 0; r < 9; r++)
            {
                BaseColor colorFondoDatos = colorBlanco;

                if (r < 8) // Filas de Materias
                {
                    BaseColor colorTituloCampo = ObtenerColorMateriaFila(r);

                    // Columna 1 (A): Campo Formativo Vertical con ROWSPAN (Tu lógica original)
                    if (r == 0) tablaBase.AddCell(CrearCeldaVertical("LENGUAJES", fontClave, colorTituloCampo, 3, 1, colorBorde));
                    else if (r == 3) tablaBase.AddCell(CrearCeldaVertical("SABERES\n Y \nPENS. \nCIENT.", fontClave, colorTituloCampo, 2, 1, colorBorde));
                    else if (r == 5) tablaBase.AddCell(CrearCeldaVertical("ÉTICA,\n NAT.\n Y SOC.", fontClave, colorTituloCampo, 2, 1, colorBorde));
                    else if (r == 7) tablaBase.AddCell(CrearCeldaVertical("DE LO \nHUM. A\n LO \nCOMUN.", fontClave, colorTituloCampo, 1, 1, colorBorde));

                    // Columna 2 (B): Nombre de la Materia
                    string nombreMateria = materiasBase[r].ToUpper();
                    PdfPCell cellMateria;

                    if (r == 6) // F. Cívica
                    {
                        cellMateria = CrearCelda(nombreMateria.Replace(" Y ", "\nY\n"), fontTexto, Element.ALIGN_LEFT, colorTituloCampo);
                    }
                    else
                    {
                        cellMateria = CrearCelda(nombreMateria, fontTexto, Element.ALIGN_LEFT, colorTituloCampo);
                    }
                    cellMateria.MinimumHeight = 26f;
                    tablaBase.AddCell(cellMateria);

                    // Columnas 3-12: Calificaciones Mensuales (10 columnas)
                    for (int c = 0; c < 10; c++)
                    {
                        string valor = datos.CalificacionesMensuales[r, c] != null ? datos.CalificacionesMensuales[r, c].Value.ToString("0.0") : "";
                        BaseColor fontColor = BaseColor.BLACK;

                        if (datos.CalificacionesMensuales[r, c].HasValue && datos.CalificacionesMensuales[r, c].Value < 6.0m)
                        {
                            fontColor = BaseColor.RED;
                        }

                        PdfPCell dataCell = CrearCelda(valor, FontFactory.GetFont(FontFactory.HELVETICA, 7, fontColor), Element.ALIGN_CENTER, colorFondoDatos);
                        dataCell.MinimumHeight = 26f;
                        tablaBase.AddCell(dataCell);
                    }

                    // ESPACIO SEPARADOR (sin borde)
                    PdfPCell espacioFila = new PdfPCell(new Phrase("", fontTexto))
                    {
                        Border = PdfRectangle.NO_BORDER,
                        BackgroundColor = BaseColor.WHITE,
                        MinimumHeight = 26f
                    };
                    tablaBase.AddCell(espacioFila);

                    // Columnas 14-17: Calificaciones Periodales (4 columnas: 3 trimestres + P. Final)
                    for (int c = 0; c < 4; c++)
                    {
                        string valor = "";
                        BaseColor fontColor = BaseColor.BLACK;

                        if (c < 3) // Trimestres
                        {
                            valor = datos.CalificacionesPeriodales[r, c] != null ? datos.CalificacionesPeriodales[r, c].Value.ToString("0.0") : "";
                            if (datos.CalificacionesPeriodales[r, c].HasValue && datos.CalificacionesPeriodales[r, c].Value < 6.0m)
                            {
                                fontColor = BaseColor.RED;
                            }
                        }
                        // La columna c=3 (P. Final) se deja vacía para las materias individuales

                        PdfPCell dataCell = CrearCelda(valor, FontFactory.GetFont(FontFactory.HELVETICA, 7, fontColor), Element.ALIGN_CENTER, colorFondoDatos);
                        dataCell.MinimumHeight = 26f;
                        tablaBase.AddCell(dataCell);
                    }
                }
                else // Fila de Promedio (r=8)
                {
                    BaseColor colorFondoPromedio = colorPromedio;

                    // --- CÁLCULO DE PROMEDIOS GENERALES TRIMESTRALES (NECESARIO PARA LLENAR LA FILA) ---
                    // Usamos esta matriz local para guardar los 3 promedios generales
                    decimal?[] promediosGeneralesTrimestrales = new decimal?[3];

                    for (int c = 0; c < 3; c++) // Itera sobre 1º TRIM (0), 2º TRIM (1), 3º TRIM (2)
                    {
                        decimal sumaTrim = 0;
                        int countTrim = 0;

                        // Sumamos los promedios de las 8 materias (r=0 a r=7) para el trimestre actual (c)
                        for (int rTrim = 0; rTrim < 8; rTrim++)
                        {
                            // Usamos los promedios por materia que ya se llenaron desde la BD
                            if (datos.CalificacionesPeriodales[rTrim, c].HasValue)
                            {
                                sumaTrim += datos.CalificacionesPeriodales[rTrim, c].Value;
                                countTrim++;
                            }
                        }
                        // Guardamos el promedio general de la boleta para ese trimestre
                        if (countTrim > 0) promediosGeneralesTrimestrales[c] = Math.Round(sumaTrim / countTrim, 1);
                    }

                    // Columna 1-2 (A-B): Fusión para "PROM. MENSUAL"
                    PdfPCell promMensualCell = CrearCelda("PROM. MENSUAL", fontClave, Element.ALIGN_CENTER, 2, 1, colorFondoPromedio);
                    promMensualCell.MinimumHeight = 26f;
                    tablaBase.AddCell(promMensualCell);

                    // Columnas 3-12: Promedios Mensuales (10 columnas)
                    for (int c = 0; c < 10; c++)
                    {
                        string valor = datos.PromediosMensuales[c] != null ? datos.PromediosMensuales[c].Value.ToString("0.0") : "";

                        PdfPCell dataCell = CrearCelda(valor, fontClave, Element.ALIGN_CENTER, colorFondoPromedio);
                        dataCell.MinimumHeight = 26f;
                        tablaBase.AddCell(dataCell);
                    }

                    // ESPACIO SEPARADOR (sin borde)
                    PdfPCell espacioPromedio = new PdfPCell(new Phrase("", fontTexto))
                    {
                        Border = PdfRectangle.NO_BORDER,
                        BackgroundColor = BaseColor.WHITE,
                        MinimumHeight = 26f
                    };
                    tablaBase.AddCell(espacioPromedio);

                    // Columnas 14-16: PROMEDIOS TRIMESTRALES GENERALES (LA FILA RODEADA)
                    for (int i = 0; i < 3; i++)
                    {
                        // 💡 CAMBIO CLAVE: Usamos el promedio general calculado en 'promediosGeneralesTrimestrales[i]'
                        string valor = promediosGeneralesTrimestrales[i] != null ? promediosGeneralesTrimestrales[i].Value.ToString("0.0") : "";
                        PdfPCell dataCell = CrearCelda(valor, fontClave, Element.ALIGN_CENTER, colorFondoPromedio);
                        dataCell.MinimumHeight = 26f;
                        tablaBase.AddCell(dataCell);
                    }

                    // Columna 17: Promedio Final CALIF (P. Final)
                    // Este valor se calcula y asigna en ObtenerDatosBoleta antes de llamar a esta función.
                    string valorFinal = datos.PFinalPromedio != null ? datos.PFinalPromedio.Value.ToString("0.0") : "";
                    PdfPCell finalPromedioCell = CrearCelda(valorFinal, fontClave, Element.ALIGN_CENTER, colorFondoPromedio);
                    finalPromedioCell.MinimumHeight = 26f;
                    tablaBase.AddCell(finalPromedioCell);
                }
            }

            return tablaBase;
        }

        private PdfPTable CrearTablaInasistenciasNiveles(DatosBoleta datos)
        {
            PdfPTable tablaContenedoraPrincipal = new PdfPTable(1) { WidthPercentage = 100 };
            tablaContenedoraPrincipal.DefaultCell.Border = PdfRectangle.NO_BORDER;


            // --- A. SUB-TABLA DE INASISTENCIAS ---
            PdfPTable inasistencias = new PdfPTable(16) { WidthPercentage = 100 };
            float[] inasWidths = { 0.11f, 0.12f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.06f, 0.06f, 0.06f, 0.09f };
            inasistencias.SetWidths(inasWidths);

            // Título INASISTENCIAS (Rowspan 2)
            PdfPCell tituloInasCell = CrearCelda("INASISTENCIAS", fontClave, Element.ALIGN_CENTER, 2, 2, colorGrisInasistencias);
            tituloInasCell.MinimumHeight = 45f;
            tituloInasCell.BorderColor = colorBorde;
            inasistencias.AddCell(tituloInasCell);

            // Meses (10 celdas)
            string[] mesesAbrev = { "D", "S", "O", "N", "E", "F", "M", "A", "M", "J" };
            foreach (string mes in mesesAbrev)
            {
                PdfPCell cell = CrearCelda(mes, fontPromedio, Element.ALIGN_CENTER, 1, 1, colorGrisInasistencias);
                cell.MinimumHeight = 20f;
                cell.BorderColor = colorBorde;
                inasistencias.AddCell(cell);
            }

            // CALIF (Colspan 3)
            PdfPCell califTrimCell = CrearCelda("CALIF", fontPromedio, Element.ALIGN_CENTER, 1, 1, colorGrisInasistencias);
            califTrimCell.MinimumHeight = 20f;
            califTrimCell.BorderColor = colorBorde;
            inasistencias.AddCell(califTrimCell);

            PdfPCell califTrimCell1 = CrearCelda("CALIF", fontPromedio, Element.ALIGN_CENTER, 1, 1, colorGrisInasistencias);
            califTrimCell.MinimumHeight = 20f;
            califTrimCell.BorderColor = colorBorde;
            inasistencias.AddCell(califTrimCell);

            PdfPCell califTrimCell2 = CrearCelda("CALIF", fontPromedio, Element.ALIGN_CENTER, 1, 1, colorGrisInasistencias);
            califTrimCell.MinimumHeight = 20f;
            califTrimCell.BorderColor = colorBorde;
            inasistencias.AddCell(califTrimCell);

            // P. FINAL
            PdfPCell pFinalCell = CrearCelda("P. F", fontPromedio, Element.ALIGN_CENTER, 1, 1, colorGrisInasistencias);
            pFinalCell.MinimumHeight = 20f;
            pFinalCell.BorderColor = colorBorde;
            inasistencias.AddCell(pFinalCell);

            // Datos de Inasistencias (14 celdas)
            // Ya que el encabezado INASISTENCIAS usa Rowspan=2 y Colspan=2,
            // debemos insertar dos celdas vacías en la fila de datos para ocupar ese espacio.

            // 1. Celdas vacías para el espacio del encabezado (Columna 1 y 2)
            for (int i = 0; i < 2; i++)
            {
                // Usar BaseColor.WHITE para evitar barras negras
                PdfPCell emptyCell = CrearCelda("", fontTexto, Element.ALIGN_CENTER, BaseColor.WHITE);
                emptyCell.MinimumHeight = 25f; // Mantener la altura
                emptyCell.BorderColor = colorBorde;
                inasistencias.AddCell(emptyCell);
            }

            // 2. Datos de inasistencias (las 14 celdas restantes)
            for (int i = 0; i < 14; i++)
            {
                // Ajustamos los índices para acceder a los 14 valores (0 a 13)
                string valor = datos.Inasistencias[i] != null ? datos.Inasistencias[i].Value.ToString() : "";

                PdfPCell dataCell = CrearCelda(valor, fontTexto, Element.ALIGN_CENTER, BaseColor.WHITE);
                dataCell.MinimumHeight = 25f;
                dataCell.BorderColor = colorBorde;
                inasistencias.AddCell(dataCell);
            }

            // El total de celdas añadidas es 2 (espacio) + 14 (datos) = 16, lo que coincide con el ancho de la tabla.

            tablaContenedoraPrincipal.AddCell(new PdfPCell(inasistencias) { Padding = 0, Border = PdfRectangle.BOX, BorderColor = colorBorde });
            // --- B. SUB-TABLA DE NIVELES DE DESEMPEÑO ---
            PdfPTable nivelesContenedor = new PdfPTable(1) { WidthPercentage = 100 };
            nivelesContenedor.DefaultCell.Border = PdfRectangle.NO_BORDER;

            nivelesContenedor.AddCell(CrearCelda("NIVELES DE DESEMPEÑO", fontClave, Element.ALIGN_CENTER, BaseColor.BLACK));

            float[] nivelWidths = { 0.25f, 0.75f };

            (string titulo, string descripcion, BaseColor color)[] nivelesData = new (string, string, BaseColor)[]
            {
                ("NIVEL I - EQUIVALE A 5", "El estudiante tiene carencias fundamentales en valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorBlanco),
                ("NIVEL II - EQUIVALE A 6 Y 7", "El estudiante tiene dificultades para demostrar valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorAmarilloClaro),
                ("NIVEL III - EQUIVALE A 8 Y 9", "El estudiante ha demostrado los valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorBlanco),
                ("NIVEL IV - EQUIVALE A 10", "El estudiante ha demostrado los valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorLila)
            };

            foreach (var nivel in nivelesData)
            {
                PdfPTable subTable = new PdfPTable(2) { WidthPercentage = 100 };
                subTable.SetWidths(nivelWidths);
                subTable.AddCell(CrearCelda(nivel.titulo, fontNiveles, Element.ALIGN_LEFT, nivel.color));
                subTable.AddCell(CrearCelda(nivel.descripcion, fontNiveles, Element.ALIGN_LEFT, nivel.color));

                nivelesContenedor.AddCell(new PdfPCell(subTable) { Padding = 0, Border = PdfRectangle.BOX, BorderColor = colorBorde });
            }

            tablaContenedoraPrincipal.AddCell(new PdfPCell(nivelesContenedor) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            return tablaContenedoraPrincipal;
        }

        private PdfPTable CrearSeccionFirmas()
        {
            PdfPTable tablaBase = new PdfPTable(2) { WidthPercentage = 100 };
            tablaBase.SetWidths(new float[] { 0.5f, 0.5f });
            tablaBase.DefaultCell.Border = PdfRectangle.NO_BORDER;

            float[] firmaWidths = { 0.4f, 0.6f };

            Action<PdfPTable> addFirmaHeaders = (table) =>
            {
                table.AddCell(CrearCelda("MES", fontClave, Element.ALIGN_CENTER, colorGrisInasistencias));
                table.AddCell(CrearCelda("FIRMA DEL PADRE O TUTOR", fontClave, Element.ALIGN_CENTER, colorGrisInasistencias));
            };

            // Columna Izquierda (Agosto a Enero)
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
                    PaddingTop = 20f
                };
                colIzquierda.AddCell(firmaCell);
            }
            tablaBase.AddCell(new PdfPCell(colIzquierda) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            // Columna Derecha (Febrero a Junio)
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
                    PaddingTop = 20f
                };
                colDerecha.AddCell(firmaCell);
            }
            tablaBase.AddCell(new PdfPCell(colDerecha) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            return tablaBase;
        }

        // Métodos CrearCelda...
        private PdfPCell CrearCelda(string texto, iTextSharp.text.Font fuente, int alineacion, int bordeEstilo)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                HorizontalAlignment = alineacion,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 4f,
                Border = bordeEstilo,
                BorderColor = colorBorde
            };
            return cell;
        }

        private PdfPCell CrearCelda(string texto, iTextSharp.text.Font fuente, int alineacion, int colspan, int rowspan, BaseColor fondo)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                HorizontalAlignment = alineacion,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Colspan = colspan,
                Rowspan = rowspan,
                Padding = 4f,
                BorderColor = colorBorde,
                BackgroundColor = fondo
            };
            return cell;
        }

        private PdfPCell CrearCelda(string texto, iTextSharp.text.Font fuente, int alineacion, BaseColor fondo = null)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                HorizontalAlignment = alineacion,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 4f,
                BorderColor = colorBorde
            };
            if (fondo != null)
            {
                cell.BackgroundColor = fondo;
            }
            return cell;
        }

        private PdfPCell CrearCeldaVertical(string texto, iTextSharp.text.Font fuente, BaseColor fondo, int rowspan, int colspan, BaseColor bordeColor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                Rotation = 90,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Rowspan = rowspan,
                Colspan = colspan,
                BackgroundColor = fondo,
                BorderColor = bordeColor,
                Padding = 3f
            };
            return cell;
        }
    }
}