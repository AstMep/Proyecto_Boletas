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

    // ESTRUCTURA DE DATOS
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
        // 🎯 AJUSTE DE TAMAÑO DE FUENTES 
        private readonly iTextSharp.text.Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
        private readonly iTextSharp.text.Font fontClave = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
        private readonly iTextSharp.text.Font fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 8); // Aumentado a 8 para más espacio
        private readonly iTextSharp.text.Font fontPromedio = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7); // Aumentado a 7
        private readonly iTextSharp.text.Font fontNiveles = FontFactory.GetFont(FontFactory.HELVETICA, 6);

        // --- Colores ---
        private readonly BaseColor colorBorde = BaseColor.BLACK;
        private readonly BaseColor colorBlanco = BaseColor.WHITE; // Nuevo: Usaremos blanco en lugar de lila/magenta
        private readonly BaseColor colorLila = new BaseColor(220, 220, 240); // Se mantiene para títulos de campos
        private readonly BaseColor colorMagenta = new BaseColor(255, 220, 255); // Se mantiene para títulos de campos
        private readonly BaseColor colorAmarilloClaro = new BaseColor(255, 245, 200); // Se mantiene para títulos de campos
        private readonly BaseColor colorVerdeClaro = new BaseColor(230, 255, 200); // Se mantiene para títulos de campos
        private readonly BaseColor colorPromedio = new BaseColor(255, 223, 100);
        private readonly BaseColor colorGrisInasistencias = new BaseColor(240, 240, 240);

        private string[] materiasBase;

        private MySqlConnection GetConnection()
        {
            // Nota: Se asume la existencia de la clase Conexion
            Conexion conexion = new Conexion();
            return conexion.GetConnection();
        }

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

        private DatosBoleta ObtenerDatosBoletaDummy()
        {
            // Simular datos de ejemplo para ver el llenado (solo para prueba)
            DatosBoleta datos = new DatosBoleta();
            datos.CalificacionesMensuales[0, 1] = 9.5m; // Español - Sept
            datos.CalificacionesMensuales[3, 3] = 7.0m; // Matemáticas - Nov/Dic
            datos.CalificacionesMensuales[7, 9] = 10.0m; // Ed. Física - Jun
            datos.Inasistencias[1] = 2; // Sept
            datos.PFinalPromedio = 8.8m;
            return datos;
        }


        public void CrearBoletaPersonal(int idAlumno, string trimestre)
        {
            string nombreAlumno = "", nombreGrupo = "", nombreMaestro = "";
            string cicloEscolar = "2024-2025";
            string noLista = "S/N";
            string rutaSalida = string.Empty;

            AlumnoInfo alumnoObjetivo = null;
            int? idGrupo = null;

            try
            {
                // NOTA: Se comenta la lógica de DB para el ejemplo y se usan datos dummy
                alumnoObjetivo = new AlumnoInfo
                {
                    AlumnoID = idAlumno,
                    NombreCompleto = "PÉREZ LÓPEZ JUAN",
                    Grupo = "PRIMERO A",
                    Maestro = "MÓNICA HERNÁNDEZ",
                };
                nombreAlumno = alumnoObjetivo.NombreCompleto;
                nombreGrupo = alumnoObjetivo.Grupo;
                nombreMaestro = alumnoObjetivo.Maestro;
                noLista = "1";
                // FIN: LÓGICA DUMMY

                if (alumnoObjetivo == null)
                {
                    MessageBox.Show("No se encontró información para el alumno seleccionado.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // INICIALIZAR ARREGLO DE MATERIAS
                string nombreMateriaCiencias = ObtenerNombreMateriaCiencias(nombreGrupo);
                materiasBase = new[] {
                    "ESPAÑOL", "INGLÉS", "ARTES",
                    "MATEMÁTICAS", "TECNOLOGÍA",
                    nombreMateriaCiencias,
                    "FORM. CÍV Y ÉTICA",
                    "ED. FISICA"
                };

                // OBTENER DATOS (Dummy o reales si se conecta la DB)
                DatosBoleta datosDelAlumno = ObtenerDatosBoletaDummy();

                // USO DE SaveFileDialog
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = $"Boleta_Personal_{idAlumno}_{trimestre}.pdf";
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    rutaSalida = saveFileDialog.FileName;
                }

                // --- Configuración y Generación del PDF (Tamaño Carta Vertical) ---
                Document doc = new Document(PageSize.LETTER, 30, 30, 30, 30);
                PdfWriter.GetInstance(doc, new FileStream(rutaSalida, FileMode.Create));
                doc.Open();

                // --- Agregar secciones al PDF ---
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


        // MÉTODOS AUXILIARES: SE MANTIENEN SIN CAMBIOS FUNCIONALES
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

        private PdfPCell CrearCelda(string texto, iTextSharp.text.Font fuente, int alineacion, int bordeEstilo)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fuente))
            {
                HorizontalAlignment = alineacion,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 4f, // AUMENTADO PARA MÁS ESPACIO
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
                Padding = 4f, // AUMENTADO PARA MÁS ESPACIO
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
                Padding = 4f, // AUMENTADO PARA MÁS ESPACIO
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
                Padding = 3f // Mantenemos el padding bajo para texto vertical
            };
            return cell;
        }

        // 🎯 1. FUNCIÓN CORREGIDA DE LA TABLA PRINCIPAL DE CALIFICACIONES (Con espacios y sin colores en calificaciones)
        private PdfPTable CrearTablaPrincipalCalificaciones(DatosBoleta datos)
        {
            // Tabla con 17 columnas (añadimos 1 columna para el espacio)
            PdfPTable tablaBase = new PdfPTable(17) { WidthPercentage = 100 };

            float[] widths = { 0.11f, 0.12f, // A, B (Campos)
                        0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, 0.04f, // C-L (10 Meses)
                        0.015f, // ESPACIO SEPARADOR (MUY PEQUEÑO)
                        0.06f, 0.06f, 0.06f, // M, N, O (3 Trimestres)
                        0.09f // P (P. Final)
                        };
            tablaBase.SetWidths(widths);

            // --- FILA 1: TÍTULOS DE ENCABEZADO SUPERIOR ---

            // Campo: CAMPOS DE FORMACIÓN (Rowspan 2)
            PdfPCell tituloCampos = CrearCelda("CAMPOS DE FORMACIÓN ACADÉMICA", fontClave, Element.ALIGN_CENTER, 2, 2, colorGrisInasistencias);
            tituloCampos.MinimumHeight = 35f; // Reducido para hacer tabla más compacta
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
            string[] mesesAbrev = { "DIAG.", "SEPT", "OCT", "NOV/DI", "ENERO", "FEB", "MAR", "ABRIL", "MAY", "JUN" };
            foreach (string mes in mesesAbrev)
            {
                PdfPCell cell = CrearCelda(mes, fontPromedio, Element.ALIGN_CENTER, colorGrisInasistencias);
                cell.MinimumHeight = 22f; // Reducido
                tablaBase.AddCell(cell);
            }

            // (El espacio separador ya tiene Rowspan=2, no se agrega nada aquí)

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

            BaseColor[] coloresTitulosCampos = { colorLila, colorLila, colorLila, colorMagenta, colorMagenta, colorAmarilloClaro, colorAmarilloClaro, colorVerdeClaro };

            for (int r = 0; r < 9; r++)
            {
                BaseColor colorFondoDatos = colorBlanco;

                if (r < 8) // Filas de Materias
                {
                    BaseColor colorTituloCampo = coloresTitulosCampos[r];

                    // Columna 1 (A): Campo Formativo Vertical con ROWSPAN 
                    if (r == 0) tablaBase.AddCell(CrearCeldaVertical("LENGUAJES", fontClave, colorTituloCampo, 3, 1, colorBorde));
                    else if (r == 3) tablaBase.AddCell(CrearCeldaVertical("SABERES\n Y \nPENS. \nCIENT.", fontClave, colorTituloCampo, 2, 1, colorBorde));
                    else if (r == 5) tablaBase.AddCell(CrearCeldaVertical("ÉTICA,\n NAT.\n Y SOC.", fontClave, colorTituloCampo, 2, 1, colorBorde));
                    else if (r == 7) tablaBase.AddCell(CrearCeldaVertical("DE LO \nHUM. A\n LO \nCOMUN.", fontClave, colorTituloCampo, 1, 1, colorBorde));

                    // Columna 2 (B): Nombre de la Materia
                    string nombreMateria = materiasBase[r].ToUpper();
                    PdfPCell cellMateria;

                    if (r == 6)
                    {
                        cellMateria = CrearCelda(nombreMateria.Replace(" Y ", "\nY\n"), fontTexto, Element.ALIGN_LEFT, colorTituloCampo);
                    }
                    else
                    {
                        cellMateria = CrearCelda(nombreMateria, fontTexto, Element.ALIGN_LEFT, colorTituloCampo);
                    }
                    cellMateria.MinimumHeight = 26f; // Reducido para compactar
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

                        PdfPCell dataCell = CrearCelda(valor,
                                                       FontFactory.GetFont(FontFactory.HELVETICA, 7, fontColor), // Fuente más pequeña
                                                       Element.ALIGN_CENTER,
                                                       colorFondoDatos);
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

                        PdfPCell dataCell = CrearCelda(valor,
                                                       FontFactory.GetFont(FontFactory.HELVETICA, 7, fontColor),
                                                       Element.ALIGN_CENTER,
                                                       colorFondoDatos);
                        dataCell.MinimumHeight = 26f;
                        tablaBase.AddCell(dataCell);
                    }
                }
                else // Fila de Promedio (r=8)
                {
                    BaseColor colorFondoPromedio = colorPromedio;

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

                    // Columnas 14-16: Vacías (no hay promedios trimestrales)
                    for (int i = 0; i < 3; i++)
                    {
                        PdfPCell emptyCell = CrearCelda("", fontClave, Element.ALIGN_CENTER, colorFondoPromedio);
                        emptyCell.MinimumHeight = 26f;
                        tablaBase.AddCell(emptyCell);
                    }

                    // Columna 17: Promedio Final
                    string valorFinal = datos.PFinalPromedio != null ? datos.PFinalPromedio.Value.ToString("0.0") : "";
                    PdfPCell finalPromedioCell = CrearCelda(valorFinal, fontClave, Element.ALIGN_CENTER, colorFondoPromedio);
                    finalPromedioCell.MinimumHeight = 26f;
                    tablaBase.AddCell(finalPromedioCell);
                }
            }

            return tablaBase;
        }

        // OTROS MÉTODOS AUXILIARES: SE MANTIENEN SIN CAMBIOS FUNCIONALES
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
            tituloInasCell.MinimumHeight = 45f; // AUMENTADO
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
            PdfPCell califTrimCell = CrearCelda("CALIF", fontPromedio, Element.ALIGN_CENTER, 3, 1, colorGrisInasistencias);
            califTrimCell.MinimumHeight = 20f;
            califTrimCell.BorderColor = colorBorde;
            inasistencias.AddCell(califTrimCell);

            // P. FINAL
            PdfPCell pFinalCell = CrearCelda("P. FINAL", fontPromedio, Element.ALIGN_CENTER, 1, 1, colorGrisInasistencias);
            pFinalCell.MinimumHeight = 20f;
            pFinalCell.BorderColor = colorBorde;
            inasistencias.AddCell(pFinalCell);

            // Datos de Inasistencias (14 celdas)
            for (int i = 0; i < 14; i++)
            {
                string valor = datos.Inasistencias[i] != null ? datos.Inasistencias[i].Value.ToString() : "";

                PdfPCell dataCell = CrearCelda(valor, fontTexto, Element.ALIGN_CENTER, BaseColor.WHITE);
                dataCell.MinimumHeight = 25f; // AUMENTADO
                dataCell.BorderColor = colorBorde;
                inasistencias.AddCell(dataCell);
            }

            tablaContenedoraPrincipal.AddCell(new PdfPCell(inasistencias) { Padding = 0, Border = PdfRectangle.BOX, BorderColor = colorBorde });


            // --- B. SUB-TABLA DE NIVELES DE DESEMPEÑO ---
            PdfPTable nivelesContenedor = new PdfPTable(1) { WidthPercentage = 100 };
            nivelesContenedor.DefaultCell.Border = PdfRectangle.NO_BORDER;

            nivelesContenedor.AddCell(CrearCelda("NIVELES DE DESEMPEÑO", fontClave, Element.ALIGN_CENTER, BaseColor.BLACK));

            float[] nivelWidths = { 0.25f, 0.75f };

            (string titulo, string descripcion, BaseColor color)[] nivelesData = new (string, string, BaseColor)[]
            {
                ("NIVEL I - EQUIVALE A 5", "El estudiante tiene carencias fundamentales en valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorBlanco),
                ("NIVEL II - EQUIVALE A 6 Y 7", "El estudiante tiene dificultades para demostrar valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorAmarilloClaro), // Uso de amarillo claro
                ("NIVEL III - EQUIVALE A 8 Y 9", "El estudiante ha demostrado los valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorBlanco),
                ("NIVEL IV - EQUIVALE A 10", "El estudiante ha demostrado los valores y principios para desarrollar una convivencia sana y pacífica, dentro y fuera del aula.", colorLila) // Uso de lila
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
                    PaddingTop = 20f // AUMENTADO PARA MÁS ESPACIO
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
                    PaddingTop = 20f // AUMENTADO PARA MÁS ESPACIO
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