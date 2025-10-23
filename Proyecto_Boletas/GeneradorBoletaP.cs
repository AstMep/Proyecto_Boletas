using System.Drawing;
using System.Drawing.Imaging; // Necesario para el MemoryStream
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

    // ESTRUCTURA DE DATOS (Necesaria para futura integración con MySQL)
    internal class DatosBoleta
    {
        public decimal?[,] CalificacionesMensuales { get; set; } = new decimal?[8, 10];
        public decimal?[,] CalificacionesPeriodales { get; set; } = new decimal?[8, 4];
        public decimal?[] PromediosMensuales { get; set; } = new decimal?[10];
        public decimal? PFinalPromedio { get; set; }
        public decimal? PromedioEdFinal { get; set; }
        public int?[] Inasistencias { get; set; } = new int?[14];
    }

    // -----------------------------------------------------------
    // CLASE GENERADORA DEL PDF
    // -----------------------------------------------------------

    internal class GeneradorBoletaP
    {
        // 🎯 AJUSTE DE TAMAÑO DE FUENTES PARA COMPACTAR LA TABLA
        private readonly iTextSharp.text.Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
        private readonly iTextSharp.text.Font fontClave = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
        private readonly iTextSharp.text.Font fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 7); // Reducida a 7
        private readonly iTextSharp.text.Font fontPromedio = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6); // Reducida a 6
        private readonly iTextSharp.text.Font fontNiveles = FontFactory.GetFont(FontFactory.HELVETICA, 6); // Reducida a 6

        // --- Colores ---
        private readonly BaseColor colorBorde = BaseColor.BLACK;
        private readonly BaseColor colorLila = new BaseColor(220, 220, 240);
        private readonly BaseColor colorMagenta = new BaseColor(255, 220, 255);
        private readonly BaseColor colorAmarilloClaro = new BaseColor(255, 245, 200);
        private readonly BaseColor colorVerdeClaro = new BaseColor(230, 255, 200);
        private readonly BaseColor colorPromedio = new BaseColor(255, 223, 100);
        private readonly BaseColor colorGrisInasistencias = new BaseColor(240, 240, 240);

        // Nombres base de las materias (se inicializa en CrearBoletaPersonal)
        private string[] materiasBase;

        // ❌ RUTA DEL LOGO ELIMINADA para usar recursos en memoria
        // private const string RUTA_LOGO = "C:\\Users\\eugen\\Source\\Repos\\Proyecto_Boletas\\Proyecto_Boletas\\Resources\\logo_escuela350.png";

        private MySqlConnection GetConnection()
        {
            Conexion conexion = new Conexion();
            return conexion.GetConnection();
        }

        // LÓGICA CONDICIONAL DE CIENCIAS/CONOCIMIENTO (Se mantiene)
        private string ObtenerNombreMateriaCiencias(string nombreGrupo)
        {
            string nombreNormalizado = nombreGrupo.ToLower().Trim();

            if (nombreNormalizado.Contains("primero") || nombreNormalizado.Contains("segundo"))
            {
                return "Conoc. del Medio";
            }
            else if (nombreNormalizado.Contains("tercero") ||
                     nombreNormalizado.Contains("cuarto") ||
                     nombreNormalizado.Contains("quinto") ||
                     nombreNormalizado.Contains("sexto"))
            {
                return "C. Naturales";
            }
            return "Conoc. del Medio";
        }

        // FUNCIÓN DE DATOS VACÍA (Dummy para evitar datos "WTF")
        private DatosBoleta ObtenerDatosBoletaDummy()
        {
            return new DatosBoleta();
        }


        public void CrearBoletaPersonal(int idAlumno, string trimestre)
        {
            string nombreAlumno = "", nombreGrupo = "", nombreMaestro = "";
            string cicloEscolar = "2024-2025";
            string noLista = "S/N";
            string rutaSalida = string.Empty; // Inicializar rutaSalida

            AlumnoInfo alumnoObjetivo = null;
            int? idGrupo = null;

            try
            {
                // PASO 1 y 2: Obtener datos y lista (Lógica restaurada)
                using (MySqlConnection conn = GetConnection())
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

                    if (alumnoObjetivo == null)
                    {
                        MessageBox.Show("No se encontró información para el alumno seleccionado.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

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

                // INICIALIZAR ARREGLO DE MATERIAS CON LÓGICA CONDICIONAL
                string nombreMateriaCiencias = ObtenerNombreMateriaCiencias(nombreGrupo);
                materiasBase = new[] {
                    "ESPAÑOL", "INGLÉS", "ARTES",
                    "MATEMÁTICAS", "TECNOLOGÍA",
                    nombreMateriaCiencias, // Materia condicional
                    "FORM. CÍV Y ÉTICA",
                    "ED. FISICA"
                };

                // OBTENER DATOS VACÍOS
                DatosBoleta datosDelAlumno = ObtenerDatosBoletaDummy();

                // USO DE SaveFileDialog
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    // Nombre sugerido por defecto
                    saveFileDialog.FileName = $"Boleta_Personal_{idAlumno}_{trimestre}.pdf";
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        // Si el usuario cancela, simplemente terminamos la función
                        return;
                    }

                    rutaSalida = saveFileDialog.FileName;
                }

                // --- Configuración y Generación del PDF (Tamaño Carta Vertical) ---
                Document doc = new Document(PageSize.LETTER, 30, 30, 30, 30);
                PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // --- Agregar secciones al PDF ---
                // 🎯 LLAMADA A LA FUNCIÓN MODIFICADA
                doc.Add(CrearEncabezadoSuperior(nombreGrupo, nombreMaestro, cicloEscolar, noLista));

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph($"ALUMNO (A): {nombreAlumno}", fontClave));
                doc.Add(new Paragraph("\n"));


                doc.Add(CrearTablaPrincipalCalificaciones(datosDelAlumno));

                doc.Add(new Paragraph("\n"));

                doc.Add(CrearTablaInasistenciasNiveles(datosDelAlumno));

                doc.Add(new Paragraph("\n"));
                doc.Add(CrearSeccionFirmas());

                doc.Close();


                MessageBox.Show($"Boleta Personal generada correctamente en:\n{rutaSalida}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el PDF correctamente en Windows
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = rutaSalida,
                        UseShellExecute = true
                    });
                }
                catch (Exception exOpen)
                {
                    MessageBox.Show($"El PDF se generó correctamente, pero no se pudo abrir automáticamente.\n\nPuedes encontrarlo en:\n{rutaSalida}",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la boleta: {ex.Message}", "ERROR CRÍTICO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ----------------------------------------------------------------------
        // --- FUNCIÓN MODIFICADA PARA CARGAR LOGO SIN RUTA ---
        // ----------------------------------------------------------------------
        private PdfPTable CrearEncabezadoSuperior(string grupo, string maestro, string ciclo, string lista)
        {
            PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 0.2f, 0.4f, 0.2f, 0.2f });

            // Logo Placeholder
            PdfPCell logoCell;
            iTextSharp.text.Image logo = null;

            try
            {
                // 1. Carga la imagen desde los recursos internos del proyecto (NO NECESITA RUTA)
                System.Drawing.Image imageFromResources = Proyecto_Boletas.Properties.Resources.logo_escuela350;

                using (MemoryStream ms = new MemoryStream())
                {
                    // 2. Transfiere la imagen al stream de memoria
                    imageFromResources.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                    // 3. iTextSharp toma los datos de la imagen directamente desde el array de bytes en memoria
                    logo = iTextSharp.text.Image.GetInstance(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                // Manejo de error si la imagen no se carga del recurso.
                Console.WriteLine("Error al cargar el logo desde recursos internos: " + ex.Message);
            }


            if (logo != null)
            {
                logo.ScaleToFit(70f, 70f);
                logoCell = new PdfPCell(logo, false) { Border = PdfRectangle.NO_BORDER };
            }
            else
            {
                // Si la carga falló, inserta el texto de "LOGO FALTANTE"
                logoCell = CrearCelda("LOGO FALTANTE\n(Verificar Recursos)", fontTexto, Element.ALIGN_CENTER, PdfRectangle.NO_BORDER);
            }

            logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
            logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(logoCell);
            // ----------------------------------------------------------------------
            // --- FIN DE MODIFICACIÓN ---
            // ----------------------------------------------------------------------

            // Título central
            PdfPCell titleCell = new PdfPCell { Border = PdfRectangle.NO_BORDER };
            titleCell.AddElement(new Paragraph("INSTITUTO MANUEL M. ACOSTA", fontTitulo) { Alignment = Element.ALIGN_CENTER });
            titleCell.AddElement(new Paragraph("BOLETA INTERNA TRIMESTRAL", fontTexto) { Alignment = Element.ALIGN_CENTER });
            titleCell.AddElement(new Paragraph($"Grupo: {grupo} - Maestro: {maestro}", fontTexto) { Alignment = Element.ALIGN_CENTER });
            table.AddCell(titleCell);

            // Claves
            table.AddCell(CrearCelda("SECCIÓN: PRIMARIA\nGRADO Y GRUPO:\nNO. DE LISTA:\nCICLO ESCOLAR:", fontClave, Element.ALIGN_RIGHT, PdfRectangle.NO_BORDER));
            table.AddCell(CrearCelda($"CLAVE:\n{grupo}\n{lista}\n{ciclo}", fontClave, Element.ALIGN_LEFT, PdfRectangle.NO_BORDER));

            return table;
        }

        // ----------------------------------------------------------------------
        // --- MÉTODOS AUXILIARES Y DE TABLA (Sin Cambios Relevantes) ---
        // ----------------------------------------------------------------------

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

        // 🎯 1. TABLA PRINCIPAL UNIFICADA (16 columnas) - Acepta DatosBoleta
        private PdfPTable CrearTablaPrincipalCalificaciones(DatosBoleta datos)
        {
            // Total de 16 columnas: 2 (Campos) + 10 (Meses) + 4 (Periodos)
            PdfPTable tablaBase = new PdfPTable(16) { WidthPercentage = 100 };

            // Anchos de columna que suman 1.0f:
            float[] widths = { 0.10f, 0.10f,  // A, B (Campos Formativos)
                                 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, // C-L (Meses)
                                 0.06f, 0.06f, 0.06f, // M, N, O (Trimestres)
                                 0.10f // P (P. Final)
                                 };
            tablaBase.SetWidths(widths);

            // --- FILA 1: TÍTULOS DE ENCABEZADO SUPERIOR (Rows 1 & 2 en el Excel) ---

            // Columna 1-2 (A1:B2 fusionado) - CAMPOS DE FORMACIÓN
            PdfPCell tituloCampos = CrearCelda("CAMPOS DE FORMACIÓN ACADÉMICA", fontClave, Element.ALIGN_CENTER, 2, 2, BaseColor.LIGHT_GRAY);
            tituloCampos.MinimumHeight = 35f;
            tablaBase.AddCell(tituloCampos);

            // Columna 3-12 (C1:L1 fusionado) - CALIFICACIONES MENSUALES
            PdfPCell tituloMensuales = CrearCelda("CALIFICACIONES MENSUALES", fontClave, Element.ALIGN_CENTER, 10, 1, BaseColor.LIGHT_GRAY);
            tituloMensuales.MinimumHeight = 15f;
            tablaBase.AddCell(tituloMensuales);

            // Columna 13-16 (M1:P1 fusionado) - PERÍODOS
            PdfPCell periodosCell = CrearCelda("PERÍODOS", fontClave, Element.ALIGN_CENTER, 4, 1, BaseColor.LIGHT_GRAY);
            tablaBase.AddCell(periodosCell);

            // --- FILA 2: SUBTÍTULOS DE CALIFICACIONES (Rows 3 en el Excel) ---

            // Subtítulos Meses (C2:L2) - Horizontales
            string[] mesesAbrev = { "DIAG.", "SEPT", "OCT", "NOV/DI", "ENERO", "FEB", "MAR", "ABRIL", "MAY", "JUN" };
            foreach (string mes in mesesAbrev)
            {
                PdfPCell cell = CrearCelda(mes, fontPromedio, Element.ALIGN_CENTER);
                cell.MinimumHeight = 20f;
                tablaBase.AddCell(cell);
            }

            // Subtítulos Periodales (M2:O2) - Horizontales y concisos
            string[] trimestres = { "1º TRIM", "2º TRIM", "3º TRIM" };
            foreach (string t in trimestres)
            {
                PdfPCell cell = CrearCelda(t, fontPromedio, Element.ALIGN_CENTER);
                cell.MinimumHeight = 20f;
                tablaBase.AddCell(cell);
            }

            // Subtítulo Columna P2: "P. FINAL" y "CALIF" (Vertical)
            PdfPCell finalCell = new PdfPCell()
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_TOP,
                MinimumHeight = 20f,
                PaddingTop = 2f,
                BorderColor = colorBorde
            };
            finalCell.AddElement(new Paragraph("P. FINAL", fontPromedio) { Alignment = Element.ALIGN_CENTER });
            finalCell.AddElement(new Paragraph("CALIF", fontPromedio) { Alignment = Element.ALIGN_CENTER });
            tablaBase.AddCell(finalCell);

            // --- FILAS 3-11: DATOS DE MATERIAS Y PROMEDIOS ---

            BaseColor[] coloresMaterias = { colorLila, colorLila, colorLila, colorMagenta, colorMagenta, colorAmarilloClaro, colorAmarilloClaro, colorVerdeClaro };

            // Itera sobre 9 filas (8 materias + 1 promedio)
            for (int r = 0; r < 9; r++)
            {
                BaseColor colorFondoFila = (r == 8) ? colorPromedio : coloresMaterias[r];

                if (r < 8) // Filas de Materias
                {
                    // Columna 1 (A): Campo Formativo Vertical
                    if (r == 0) tablaBase.AddCell(CrearCeldaVertical("LENGUAJES", fontClave, colorLila, 3, 1, colorBorde));
                    else if (r == 3) tablaBase.AddCell(CrearCeldaVertical("SABERES\n Y \nPENS. \nCIENT.", fontClave, colorMagenta, 2, 1, colorBorde));
                    else if (r == 5) tablaBase.AddCell(CrearCeldaVertical("ÉTICA,\n NAT.\n Y SOC.", fontClave, colorAmarilloClaro, 2, 1, colorBorde));
                    else if (r == 7) tablaBase.AddCell(CrearCeldaVertical("DE LO \nHUM. A\n LO \nCOMUN.", fontClave, colorVerdeClaro, 1, 1, colorBorde));

                    // Columna 2 (B): Nombre de la Materia (Ajustado a una línea, excepto FORM. CÍV Y ÉTICA)
                    string nombreMateria = materiasBase[r].ToUpper();
                    PdfPCell cellMateria;

                    if (r == 6) // FORM. CÍV Y ÉTICA (Fuerza dos líneas para que no se salga del ancho)
                    {
                        cellMateria = CrearCelda(nombreMateria.Replace(" Y ", "\nY\n"), fontTexto, Element.ALIGN_LEFT, colorFondoFila);
                    }
                    else
                    {
                        cellMateria = CrearCelda(nombreMateria, fontTexto, Element.ALIGN_LEFT, colorFondoFila);
                    }
                    cellMateria.MinimumHeight = 20f;
                    tablaBase.AddCell(cellMateria);


                    // Columnas 3-16 (Calificaciones) - 14 columnas de datos
                    for (int c = 0; c < 14; c++)
                    {
                        PdfPCell dataCell = CrearCelda("", fontTexto, Element.ALIGN_CENTER, colorFondoFila);
                        dataCell.MinimumHeight = 20f;
                        tablaBase.AddCell(dataCell);
                    }
                }
                else // Fila de Promedio (r=8)
                {
                    // Columna 1-2 (A-B): Fusión para "PROM. MENSUAL"
                    PdfPCell promMensualCell = CrearCelda("PROM. MENSUAL", fontClave, Element.ALIGN_CENTER, 2, 1, colorPromedio);
                    promMensualCell.MinimumHeight = 20f;
                    tablaBase.AddCell(promMensualCell);

                    // Columnas 3-16 (Promedios) - 14 columnas
                    for (int c = 0; c < 14; c++)
                    {
                        PdfPCell dataCell = CrearCelda("", fontTexto, Element.ALIGN_CENTER, colorFondoFila);
                        dataCell.MinimumHeight = 20f;
                        tablaBase.AddCell(dataCell);
                    }
                }
            }

            return tablaBase;
        }
        // 🎯 2. TABLA INASISTENCIAS Y NIVELES (DISPOSICIÓN APILADA Y ALINEADA)
        private PdfPTable CrearTablaInasistenciasNiveles(DatosBoleta datos)
        {
            // La tabla final contendrá las dos subtablas (Inasistencias y Niveles) una encima de la otra.
            // Usamos una tabla contenedora de 1 columna para asegurar que cada subtabla se agregue en su propio bloque
            PdfPTable tablaContenedoraPrincipal = new PdfPTable(1) { WidthPercentage = 100 };
            tablaContenedoraPrincipal.DefaultCell.Border = PdfRectangle.NO_BORDER;


            // --- A. SUB-TABLA DE INASISTENCIAS (100% Ancho) ---

            // Usamos 16 columnas internas para mantener la alineación de contenido con la tabla principal
            PdfPTable inasistencias = new PdfPTable(16) { WidthPercentage = 100 };
            // Mismos anchos relativos que la tabla de calificaciones
            float[] inasWidths = { 0.10f, 0.10f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.06f, 0.06f, 0.06f, 0.10f };
            inasistencias.SetWidths(inasWidths);

            // Fila 1 (Etiquetas)

            // Columna 1-2 (A-B): Título INASISTENCIAS (Rowspan 2)
            PdfPCell tituloInasCell = CrearCelda("INASISTENCIAS", fontClave, Element.ALIGN_CENTER, 2, 2, colorGrisInasistencias);
            tituloInasCell.MinimumHeight = 40f;
            tituloInasCell.BorderColor = colorBorde;
            inasistencias.AddCell(tituloInasCell);

            // Columna 3-12 (C-L): Meses (10 celdas individuales)
            string[] mesesAbrev = { "D", "S", "O", "N", "E", "F", "M", "A", "M", "J" };
            foreach (string mes in mesesAbrev)
            {
                PdfPCell cell = CrearCelda(mes, fontPromedio, Element.ALIGN_CENTER, 1, 1, colorGrisInasistencias);
                cell.MinimumHeight = 20f;
                cell.BorderColor = colorBorde;
                inasistencias.AddCell(cell);
            }

            // Columna 13-15 (M-O): CALIF (Fusión 3 columnas, centrado en Trimestres)
            PdfPCell califTrimCell = CrearCelda("CALIF", fontPromedio, Element.ALIGN_CENTER, 3, 1, colorGrisInasistencias);
            califTrimCell.MinimumHeight = 20f;
            califTrimCell.BorderColor = colorBorde;
            inasistencias.AddCell(califTrimCell);

            // Columna 16 (P): P. FINAL (Se mantiene como 1 columna)
            PdfPCell pFinalCell = CrearCelda("P. FINAL", fontPromedio, Element.ALIGN_CENTER, 1, 1, colorGrisInasistencias);
            pFinalCell.MinimumHeight = 20f;
            pFinalCell.BorderColor = colorBorde;
            inasistencias.AddCell(pFinalCell);

            // Fila 2 (Datos de Inasistencias)

            // Columna 3-16 (14 celdas de datos)
            for (int i = 0; i < 14; i++)
            {
                PdfPCell dataCell = CrearCelda("", fontTexto, Element.ALIGN_CENTER, BaseColor.WHITE);
                dataCell.MinimumHeight = 20f;
                dataCell.BorderColor = colorBorde;
                inasistencias.AddCell(dataCell);
            }

            // Agregar Inasistencias al contenedor principal (100% de ancho)
            tablaContenedoraPrincipal.AddCell(new PdfPCell(inasistencias) { Padding = 0, Border = PdfRectangle.BOX, BorderColor = colorBorde });


            // --- B. SUB-TABLA DE NIVELES DE DESEMPEÑO (100% Ancho) ---

            // Usamos una tabla de 1 columna para el título y otra interna para la definición.
            PdfPTable nivelesContenedor = new PdfPTable(1) { WidthPercentage = 100 };
            nivelesContenedor.DefaultCell.Border = PdfRectangle.NO_BORDER;

            // Título principal
            nivelesContenedor.AddCell(CrearCelda("NIVELES DE DESEMPEÑO", fontClave, Element.ALIGN_CENTER, BaseColor.BLACK));

            // Tabla interna para la definición de niveles (que es la que necesita la división de columnas)
            float[] nivelWidths = { 0.25f, 0.75f }; // Ajustado para que el título de nivel se vea mejor

            (string titulo, string descripcion, BaseColor color)[] nivelesData = new (string, string, BaseColor)[]
            {
        ("NIVEL I - EQUIVALE A 5", "El estudiante tiene carencias fundamentales en valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", BaseColor.WHITE),
        ("NIVEL II - EQUIVALE A 6 Y 7", "El estudiante tiene dificultades para demostrar valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorPromedio),
        ("NIVEL III - EQUIVALE A 8 Y 9", "El estudiante ha demostrado los valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", BaseColor.WHITE),
        ("NIVEL IV - EQUIVALE A 10", "El estudiante ha demostrado los valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorLila)
            };

            foreach (var nivel in nivelesData)
            {
                // Esta sub-tabla ocupa el 100% del ancho del contenedor y maneja las 2 columnas de definición.
                PdfPTable subTable = new PdfPTable(2) { WidthPercentage = 100 };
                subTable.SetWidths(nivelWidths);
                subTable.AddCell(CrearCelda(nivel.titulo, fontNiveles, Element.ALIGN_LEFT, nivel.color));
                subTable.AddCell(CrearCelda(nivel.descripcion, fontNiveles, Element.ALIGN_LEFT, nivel.color));

                // Agregar la definición al contenedor de niveles
                nivelesContenedor.AddCell(new PdfPCell(subTable) { Padding = 0, Border = PdfRectangle.BOX, BorderColor = colorBorde });
            }

            // Agregar Niveles al contenedor principal (100% de ancho)
            tablaContenedoraPrincipal.AddCell(new PdfPCell(nivelesContenedor) { Padding = 0, Border = PdfRectangle.NO_BORDER });

            // Devolvemos el contenedor principal que tiene INASISTENCIAS arriba y NIVELES abajo
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