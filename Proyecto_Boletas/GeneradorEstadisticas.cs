using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using PdfFont = iTextSharp.text.Font;
using PdfRectangle = iTextSharp.text.Rectangle;
using PdfImage = iTextSharp.text.Image;

namespace Proyecto_Boletas
{
    internal class GeneradorEstadisticas
    {
        // ======================================================
        // 🔹 LOGOS Y DATOS ESCUELA
        // ======================================================
        private const string LOGO_SEP_PATH = "C:\\Users\\DELL\\source\\repos\\Proyecto_Boletas\\Proyecto_Boletas\\Resources\\transfromando.png";
        private const string LOGO_ESCUELA_PATH = "C:\\Users\\DELL\\source\\repos\\Proyecto_Boletas\\Proyecto_Boletas\\Resources\\secretariaedu.png";

        private const string CCT_ESCUELA = "29DPR0035Z";
        private const string NOMBRE_ESCUELA = "NSTITUTO MANUEL M. ACOSTA";
        private const string TURNO_ESCUELA = "MATUTINO";
        private const string DOMICILIO_ESCUELA = "CALLE VICENTE GUERRERO";
        private const string LOCALIDAD_ESCUELA = "ACAPULCO DE JUÁREZ";
        private const string MUNICIPIO_ESCUELA = "ACAPULCO DE JUÁREZ";
        private const string ZONA_CCT_ESCUELA = "29DPR0035Z";
        private const string CICLO_ESCOLAR = "2025-2026";
        private const string NOMBRE_DIRECTOR = "CAROLINA ASTUDILLO HERNANDEZ";

        // ======================================================
        // 🔹 FUENTES ESTILO MEMBRETADO (Arial Narrow)
        // ======================================================
        private readonly PdfFont fontTitulo = FontFactory.GetFont("Arial Narrow", 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        private readonly PdfFont fontNormal = FontFactory.GetFont("Arial Narrow", 8f, BaseColor.BLACK);
        private readonly PdfFont fontNegrita = FontFactory.GetFont("Arial Narrow", 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        private readonly PdfFont fontFecha = FontFactory.GetFont("Arial Narrow", 7f, BaseColor.BLACK);
        private readonly PdfFont fontTabla = FontFactory.GetFont("Arial Narrow", 8f, BaseColor.BLACK);
        private readonly PdfFont fontTablaHeader = FontFactory.GetFont("Arial Narrow", 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        private readonly BaseColor COLOR_SUBTOTAL = new BaseColor(230, 230, 230);
        private readonly BaseColor COLOR_TOTAL = new BaseColor(200, 200, 200);

        // ======================================================
        // 🔹 MÉTODO PRINCIPAL
        // ======================================================
        public void CrearPDFEstadisticaTotal()
        {
            string rutaFinal = string.Empty;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "ESTADISTICA_BASICA_TOTAL.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                rutaFinal = sfd.FileName;
            }

            Document doc = new Document(PageSize.LETTER, 36, 36, 40, 36);
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(rutaFinal, FileMode.Create));
                doc.Open();

                string fechaImpresion = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                // ------------------------------------------------------
                // ENCABEZADO
                // ------------------------------------------------------
                PdfPTable encabezado = new PdfPTable(3) { WidthPercentage = 100 };
                encabezado.SetWidths(new float[] { 2f, 5f, 2f });

                encabezado.AddCell(CreateImageCell(LOGO_SEP_PATH, 55, Element.ALIGN_LEFT));
                encabezado.AddCell(CreateTextCell("ESTADÍSTICA BÁSICA POR CENTRO DE TRABAJO", fontTitulo, Element.ALIGN_CENTER, PdfRectangle.NO_BORDER));
                encabezado.AddCell(CreateImageCell(LOGO_ESCUELA_PATH, 55, Element.ALIGN_RIGHT));
                doc.Add(encabezado);

                Paragraph fecha = new Paragraph($"Fecha de Impresión : {fechaImpresion}", fontFecha);
                fecha.Alignment = Element.ALIGN_RIGHT;
                doc.Add(fecha);
                doc.Add(new Paragraph("\n", fontNormal));

                // ------------------------------------------------------
                // DATOS ESCUELA
                // ------------------------------------------------------
                PdfPTable datos = new PdfPTable(4) { WidthPercentage = 100 };
                datos.DefaultCell.Border = PdfRectangle.NO_BORDER;
                datos.SetWidths(new float[] { 1.5f, 4f, 1.5f, 2.5f });

                AgregarCeldaDatos(datos, "C.C.T. :", CCT_ESCUELA);
                AgregarCeldaDatos(datos, "Turno :", TURNO_ESCUELA);
                AgregarCeldaDatos(datos, "Nombre :", NOMBRE_ESCUELA);
                AgregarCeldaDatos(datos, "Zona CCT :", ZONA_CCT_ESCUELA);
                AgregarCeldaDatos(datos, "Domicilio :", DOMICILIO_ESCUELA);
                AgregarCeldaDatos(datos, "Ciclo Escolar :", CICLO_ESCOLAR);
                AgregarCeldaDatos(datos, "Localidad :", LOCALIDAD_ESCUELA);
                AgregarCeldaDatos(datos, "Municipio :", MUNICIPIO_ESCUELA);

                doc.Add(datos);
                doc.Add(new Paragraph(" ", fontNormal));

                // ------------------------------------------------------
                // TABLA DE ESTADÍSTICA
                // ------------------------------------------------------
                var datosEstadistica = ObtenerEstadisticasDesdeBD();
                var promedios = ObtenerPromediosPorGrado();

                PdfPTable tabla = new PdfPTable(6) { WidthPercentage = 100 };
                tabla.SpacingBefore = 10f;  // Más espacio superior
                tabla.SpacingAfter = 10f;   // Más espacio inferior
                tabla.SetWidths(new float[] { 2f, 2f, 2f, 2f, 2f, 2.5f });

                tabla.AddCell(CreateHeaderCell("GRADO"));
                tabla.AddCell(CreateHeaderCell("GRUPO"));
                tabla.AddCell(CreateHeaderCell("HOMBRES"));
                tabla.AddCell(CreateHeaderCell("MUJERES"));
                tabla.AddCell(CreateHeaderCell("TOTAL"));
                tabla.AddCell(CreateHeaderCell("PROMEDIO"));

                int totalH = 0, totalM = 0;
                double totalProm = 0;
                int gradosConProm = 0;

                foreach (var e in datosEstadistica)
                {
                    string grado = e.Grado;
                    string grupo = "A";
                    double prom = promedios.ContainsKey(grado) ? promedios[grado] : 0;

                    tabla.AddCell(CreateCell(grado));
                    tabla.AddCell(CreateCell(grupo));
                    tabla.AddCell(CreateCell(e.Hombres.ToString()));
                    tabla.AddCell(CreateCell(e.Mujeres.ToString()));
                    tabla.AddCell(CreateCell(e.Total.ToString()));
                    tabla.AddCell(CreateCell(prom > 0 ? prom.ToString("0.00") : "-"));

                    totalH += e.Hombres;
                    totalM += e.Mujeres;

                    if (prom > 0)
                    {
                        totalProm += prom;
                        gradosConProm++;
                    }

                    // espacio entre grados (salto visual)
                    PdfPCell separador = new PdfPCell(new Phrase(" ")) { Colspan = 6, Border = PdfRectangle.NO_BORDER, FixedHeight = 6f };
                    tabla.AddCell(separador);
                }

                double promGral = gradosConProm > 0 ? totalProm / gradosConProm : 0;

                // Totales generales
                tabla.AddCell(CreateSubtotalCell("TOTAL GENERAL", 2, COLOR_TOTAL));
                tabla.AddCell(CreateSubtotalCell(totalH.ToString(), 1, COLOR_TOTAL));
                tabla.AddCell(CreateSubtotalCell(totalM.ToString(), 1, COLOR_TOTAL));
                tabla.AddCell(CreateSubtotalCell((totalH + totalM).ToString(), 1, COLOR_TOTAL));
                tabla.AddCell(CreateSubtotalCell(promGral.ToString("0.00"), 1, COLOR_TOTAL));

                doc.Add(tabla);
                doc.Add(new Paragraph("\n", fontNormal));

                string mayor = totalH > totalM ? "NIÑOS" : totalM > totalH ? "NIÑAS" : "IGUAL CANTIDAD";
                Paragraph resumen = new Paragraph($"Mayor cantidad: {mayor}", fontNegrita);
                resumen.Alignment = Element.ALIGN_LEFT;
                doc.Add(resumen);
                doc.Add(new Paragraph("\n\n", fontNormal));

                // ------------------------------------------------------
                // FIRMAS
                // ------------------------------------------------------
                PdfPTable firmas = new PdfPTable(2) { WidthPercentage = 100 };
                firmas.SetWidths(new float[] { 1f, 1f });
                firmas.AddCell(CreateFirmaLinea());
                firmas.AddCell(CreateFirmaLinea());
                firmas.AddCell(CreateFirmaCell("SUPERVISOR (A)"));
                firmas.AddCell(CreateFirmaCell("DIRECTOR (A)"));
                firmas.AddCell(CreateEmptyCell());
                firmas.AddCell(CreateFirmaCell(NOMBRE_DIRECTOR));
                doc.Add(firmas);

                doc.Close();
                Process.Start(new ProcessStartInfo(rutaFinal) { UseShellExecute = true });
                MessageBox.Show("PDF generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (doc.IsOpen()) doc.Close();
                MessageBox.Show("Error al generar PDF: " + ex.Message);
            }
        }


        private List<EstadisticaGrupo> ObtenerEstadisticasDesdeBD()
        {
            List<EstadisticaGrupo> lista = new();
            string query = @"
                SELECT 
                    SUBSTRING_INDEX(g.nombre_grupo, '-', 1) AS Grado,
                    SUM(CASE WHEN a.genero = 'M' THEN 1 ELSE 0 END) AS Hombres,
                    SUM(CASE WHEN a.genero = 'F' THEN 1 ELSE 0 END) AS Mujeres
                FROM grupo g
                INNER JOIN alumnos a ON a.id_grupo = g.id_grupo
                GROUP BY Grado
                ORDER BY Grado;";

            using (MySqlConnection conn = new Conexion().GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new EstadisticaGrupo
                        {
                            Grado = dr["Grado"].ToString(),
                            Hombres = Convert.ToInt32(dr["Hombres"]),
                            Mujeres = Convert.ToInt32(dr["Mujeres"]),
                            Total = Convert.ToInt32(dr["Hombres"]) + Convert.ToInt32(dr["Mujeres"])
                        });
                    }
                }
            }
            return lista;
        }

        private Dictionary<string, double> ObtenerPromediosPorGrado()
        {
            Dictionary<string, double> promedios = new();
            string query = @"
                SELECT 
                    SUBSTRING_INDEX(g.nombre_grupo, '-', 1) AS Grado,
                    ROUND(AVG(c.Calificacion), 2) AS Promedio
                FROM calificaciones c
                INNER JOIN alumnos a ON a.AlumnoID = c.AlumnoID
                INNER JOIN grupo g ON a.id_grupo = g.id_grupo
                GROUP BY Grado;";

            using (MySqlConnection conn = new Conexion().GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        promedios[dr["Grado"].ToString()] = Convert.ToDouble(dr["Promedio"]);
                    }
                }
            }
            return promedios;
        }

        private class EstadisticaGrupo
        {
            public string Grado { get; set; }
            public int Hombres { get; set; }
            public int Mujeres { get; set; }
            public int Total { get; set; }
        }

        // ======================================================
        // 🔹 MÉTODOS DE FORMATO PDF
        // ======================================================
        private void AgregarCeldaDatos(PdfPTable t, string label, string valor)
        {
            t.AddCell(new PdfPCell(new Phrase(label, fontNegrita)) { Border = 0, Padding = 2f });
            t.AddCell(new PdfPCell(new Phrase(valor, fontNormal)) { Border = 0, Padding = 2f });
        }

        private PdfPCell CreateHeaderCell(string text)
        {
            return new PdfPCell(new Phrase(text, fontTablaHeader))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = new BaseColor(230, 230, 230),
                Padding = 6f
            };
        }

        private PdfPCell CreateCell(string text)
        {
            return new PdfPCell(new Phrase(text, fontTabla))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 5f
            };
        }

        private PdfPCell CreateSubtotalCell(string text, int colspan = 1, BaseColor color = null)
        {
            return new PdfPCell(new Phrase(text, fontNegrita))
            {
                BackgroundColor = color ?? COLOR_SUBTOTAL,
                Colspan = colspan,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 6f
            };
        }

        private PdfPCell CreateImageCell(string path, float scale, int align)
        {
            try
            {
                PdfImage img = PdfImage.GetInstance(path);
                img.ScalePercent(scale);
                return new PdfPCell(img) { Border = 0, HorizontalAlignment = align };
            }
            catch
            {
                return new PdfPCell(new Phrase("[LOGO]", fontNormal)) { Border = 0, HorizontalAlignment = align };
            }
        }

        private PdfPCell CreateTextCell(string text, PdfFont font, int align, int border)
        {
            return new PdfPCell(new Phrase(text, font)) { Border = border, HorizontalAlignment = align };
        }

        private PdfPCell CreateEmptyCell() => new PdfPCell(new Phrase("")) { Border = 0 };

        private PdfPCell CreateFirmaLinea()
        {
            return new PdfPCell(new Phrase("_________________________________________", fontNormal))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingTop = 20f
            };
        }

        private PdfPCell CreateFirmaCell(string text)
        {
            return new PdfPCell(new Phrase(text, fontNegrita))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingTop = 5f
            };
        }
    }
}
