using Microsoft.VisualBasic.Devices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;       
using System.Net.Mail;  
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Boletas
{
    public partial class CreacionPDF_Direc : Form
    {
        private string rolUsuario;
        private string rutaPdfGrupalGenerado = "";
        private string rutaPdfPersonalGenerado = "";
        private int idAlumnoPdfPersonal = 0;
        private int idGrupoPdfPersonal = 0;
        private string trimestrePdfPersonal = "";
        private string nombreAlumnoPdfPersonal = "";

        public CreacionPDF_Direc(string rol = "Director")
        {
            InitializeComponent();
            rolUsuario = rol;
        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }



        private void LlenarComboGrupos()
        {
            cmbGrupo.Items.Clear();

            using (MySqlConnection conn = new Conexion().GetConnection())
            {
                conn.Open();
                string query = "SELECT id_grupo, nombre_grupo FROM grupo";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem()
                    {
                        Text = reader["nombre_grupo"].ToString(),
                        Value = reader["id_grupo"]
                    };
                    cmbGrupo.Items.Add(item);
                }

                reader.Close();
            }
        }


        private void LlenarComboMeses()
        {
            cmbMes.Items.Clear();

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                       "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            cmbMes.Items.AddRange(meses);
        }


        private void ModEdicDatos_Direc_Load(object sender, EventArgs e)
        {
            LlenarComboGrupos();
            LlenarComboMeses();
            OcultarBotonesPorRol();
            CargarGrupos();
            cmbTrimestreGrup.Items.AddRange(new string[] { "1er Trimestre", "2do Trimestre", "3er Trimestre" });
            LlenarComboTrimestres();
        }

        private void CargarGrupos()
        {
            try
            {
                using (MySqlConnection conn = new Conexion().GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id_grupo, nombre_grupo FROM grupo";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        // 🎯 Limpiamos y llenamos AMBOS ComboBoxes de grupo
                        cmbGrup.Items.Clear();      // ComboBox de Boleta Grupal
                        cbGrupoPer.Items.Clear();   // ComboBox de Boleta Personal

                        while (dr.Read())
                        {
                            ComboboxItem item = new ComboboxItem
                            {
                                Text = dr["nombre_grupo"].ToString(),
                                Value = dr["id_grupo"]
                            };

                            cmbGrup.Items.Add(item);
                            cbGrupoPer.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar grupos: " + ex.Message);
            }
        }

        private void OcultarBotonesPorRol()
        {
            if (rolUsuario == "Secretaria")
            {
                // Ocultar botones solo para secretaria
                btnAdmSecre.Visible = false;
                btnBitacora.Visible = false;
                btn_admaestros.Visible = false;
            }
            else if (rolUsuario == "Director")
            {
                // Mostrar todos los botones para director
                btnAdmSecre.Visible = true;
                btnBitacora.Visible = true;
                btn_admaestros.Visible = true;
            }
        }


        private void btnGenerarListas_Click_1(object sender, EventArgs e)
        {
            if (cmbGrupo.SelectedItem == null || cmbMes.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un grupo y un mes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idGrupo = (int)((ComboboxItem)cmbGrupo.SelectedItem).Value; // Obtenemos el id real
            string mesSeleccionado = cmbMes.SelectedItem.ToString();


            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Archivo PDF|*.pdf";
            saveDialog.Title = "Guardar listas mensuales";
            saveDialog.FileName = $"Lista_{mesSeleccionado}.pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                GeneradorListaA generador = new GeneradorListaA();
                generador.GenerarListasMensuales(saveDialog.FileName, idGrupo, mesSeleccionado);
            }
        }

        private void panelApp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGenerarLisProf_Click_1(object sender, EventArgs e)
        {

            try
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = "Lista_Profesores_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                saveFileDialog.Title = "Guardar Lista de Profesores";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = saveFileDialog.FileName;

                    // 2. Crear instancia del generador y ejecutar la generación del PDF
                    GeneradorListaProf generador = new GeneradorListaProf();
                    generador.GenerarLista(rutaArchivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar la generación de la lista de profesores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            Form1 nuevoFormulario = new Form1();
            nuevoFormulario.Show();
            this.Close();
        }

        private void btn_inscripcion_Click(object sender, EventArgs e)
        {
            Mod_inscripcion inscripcion = new Mod_inscripcion(rolUsuario);
            inscripcion.Show();
            this.Hide();
        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {
            Mod_capCal cap_calificacion = new Mod_capCal(rolUsuario);
            cap_calificacion.Show();
            this.Hide();
        }

        private void btnAdmSecre_Click(object sender, EventArgs e)
        {
            adm_Secretaria administrar_secre = new adm_Secretaria();
            administrar_secre.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora bitacora = new Bitacora();
            bitacora.Show();
            this.Hide();
        }

        private void btn_admaestros_Click(object sender, EventArgs e)
        {
            adm_maestros administrar_maestros = new adm_maestros();
            administrar_maestros.Show();
            this.Hide();
        }

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {

        }



        private void cmbGrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGrup.SelectedItem != null)
            {
                int idGrupo = (int)((ComboboxItem)cmbGrup.SelectedItem).Value;

                try
                {
                    using (MySqlConnection conn = new Conexion().GetConnection())
                    {
                        conn.Open();
                        string query = "SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno FROM alumnos WHERE id_grupo = @idGrupo";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idGrupo", idGrupo);

                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar alumnos: " + ex.Message);
                }
            }
        }




        private void cmbTrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerarBoletas_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbGrup.SelectedItem == null || cmbTrimestreGrup.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un grupo y trimestre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idGrupo = (int)((ComboboxItem)cmbGrup.SelectedItem).Value;
                string trimestre = cmbTrimestreGrup.SelectedItem.ToString();
                string nombreGrupo = ((ComboboxItem)cmbGrup.SelectedItem).Text;

                // --- INICIO DE CAMBIOS ---

                // 1. Preguntar al usuario dónde guardar
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Archivo PDF|*.pdf";
                saveDialog.Title = "Guardar Boleta Grupal";
                saveDialog.FileName = $"Boleta_Grupal_{nombreGrupo.Replace(" ", "_")}_{trimestre.Replace(" ", "_")}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // 2. Guardar la ruta en nuestra variable de "memoria"
                    this.rutaPdfGrupalGenerado = saveDialog.FileName;

                    // 3. Pasar la ruta al generador
                    // !! IMPORTANTE: Tu clase 'GeneradorBoletaT' debe ser modificada
                    // para que su método 'CrearBoletaGrupal' acepte la ruta como parámetro.
                    GeneradorBoletaT generador = new GeneradorBoletaT();
                    generador.CrearBoletaGrupal(idGrupo, trimestre, this.rutaPdfGrupalGenerado); // <-- ¡Cambio clave!

                    MessageBox.Show($"Boleta generada y guardada en:\n{this.rutaPdfGrupalGenerado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Si el usuario cancela, limpiamos la ruta
                    this.rutaPdfGrupalGenerado = "";
                }
                // --- FIN DE CAMBIOS ---
            }
            catch (Exception ex)
            {
                this.rutaPdfGrupalGenerado = ""; // Limpiar la ruta si hay error
                MessageBox.Show("Error al generar la boleta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cbGrupoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAlumno.DataSource = null;
            cbAlumno.Items.Clear();

            if (cbGrupoPer.SelectedItem != null)
            {
                try
                {
                    // Extrae correctamente el id del grupo seleccionado
                    int idGrupo = Convert.ToInt32(((ComboboxItem)cbGrupoPer.SelectedItem).Value);

                    List<ComboboxItem> alumnosList = new List<ComboboxItem>();

                    using (MySqlConnection conn = new Conexion().GetConnection())
                    {
                        conn.Open();
                        string query = @"
                SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno
                FROM alumnos
                WHERE id_grupo = @idGrupo
                ORDER BY ApellidoPaterno, Nombre";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idGrupo", idGrupo);

                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    string nombreCompleto = $"{dr["ApellidoPaterno"]} {dr["ApellidoMaterno"]} {dr["Nombre"]}";

                                    alumnosList.Add(new ComboboxItem
                                    {
                                        Text = nombreCompleto,
                                        Value = Convert.ToInt32(dr["AlumnoID"])
                                    });
                                }
                            }
                        }
                    }

                    if (alumnosList.Count > 0)
                    {
                        cbAlumno.DisplayMember = "Text";
                        cbAlumno.ValueMember = "Value";
                        cbAlumno.DataSource = alumnosList;
                        cbAlumno.SelectedIndex = 0; // selecciona automáticamente el primero
                    }
                    else
                    {
                        cbAlumno.DataSource = null;
                        cbAlumno.Items.Clear();
                        cbAlumno.Text = "Sin alumnos en este grupo";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar alumnos: " + ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un grupo válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LlenarComboTrimestres()
        {
            // Aseguramos que solo llenamos el ComboBox de la Boleta Personal
            cbTrimestrePer.Items.Clear();
            cbTrimestrePer.Items.AddRange(new string[] { "1er Trimestre", "2do Trimestre", "3er Trimestre" });
        }

        private void cbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbTrimestrePer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ENLACE (LECTURA): Verifica si un documento específico ya fue enviado.
        /// </summary>
        /// <returns>True si ya existe un registro, False si no existe.</returns>
        private bool VerificarEnvioPrevio(string tipoDoc, string idRef, string trimestre, string email)
        {
            // Usamos la misma cadena de conexión de tu clase Conexion
            string connectionString = new Conexion().GetConnection().ConnectionString;

            // Contamos cuántos registros coinciden con estos parámetros
            string queryCheck = @"
        SELECT COUNT(*) FROM envios_tracking 
        WHERE tipo_documento = @tipoDoc 
        AND id_referencia = @idRef 
        AND trimestre = @trimestre 
        AND destinatario_contacto = @contacto
        AND estado = 'Enviado'"; // Solo contamos los que SÍ se enviaron

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmdCheck = new MySqlCommand(queryCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@tipoDoc", tipoDoc);
                        cmdCheck.Parameters.AddWithValue("@idRef", idRef);
                        cmdCheck.Parameters.AddWithValue("@trimestre", trimestre);
                        cmdCheck.Parameters.AddWithValue("@contacto", email);

                        conn.Open();

                        // ExecuteScalar obtiene el primer valor (el resultado del COUNT)
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        // Si el contador es mayor que 0, significa que ya se envió
                        return (count > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // Si hay un error al verificar, es más seguro NO enviar.
                MessageBox.Show("Error al verificar envíos previos: " + ex.Message, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Devolvemos 'true' para bloquear el envío por seguridad
            }
        }

        private void button1_Click(object sender, EventArgs e) // Este es tu btnGenerarBP
        {
            if (cbGrupoPer.SelectedItem == null || cbAlumno.SelectedItem == null || cbTrimestrePer.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona Grupo, Alumno y Trimestre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // --- INICIO DE CAMBIOS ---
                // 1. Obtener todos los datos
                int idAlumno = Convert.ToInt32(cbAlumno.SelectedValue);
                string trimestre = cbTrimestrePer.SelectedItem.ToString();
                string nombreAlumno = ((ComboboxItem)cbAlumno.SelectedItem).Text;
                int idGrupo = Convert.ToInt32(((ComboboxItem)cbGrupoPer.SelectedItem).Value);

                // 2. Preguntar dónde guardar
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Archivo PDF|*.pdf";
                saveDialog.Title = "Guardar Boleta Personal";
                saveDialog.FileName = $"Boleta_Personal_{nombreAlumno.Replace(" ", "_")}_{trimestre.Replace(" ", "_")}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // 3. Guardar la información en las variables de "memoria"
                    this.rutaPdfPersonalGenerado = saveDialog.FileName;
                    this.idAlumnoPdfPersonal = idAlumno;
                    this.idGrupoPdfPersonal = idGrupo;
                    this.trimestrePdfPersonal = trimestre;
                    this.nombreAlumnoPdfPersonal = nombreAlumno;

                    // 4. Llamar al generador (¡DEBE SER MODIFICADO!)
                    // ⚠️ DEBES MODIFICAR 'GeneradorBoletaP' para que acepte la ruta
                    GeneradorBoletaP generador = new GeneradorBoletaP();
                    generador.CrearBoletaPersonal(idAlumno, trimestre, this.rutaPdfPersonalGenerado); // <-- Asumimos 3 parámetros

                    MessageBox.Show($"Boleta personal generada y guardada en:\n{this.rutaPdfPersonalGenerado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Si el usuario cancela, limpiamos las variables
                    this.rutaPdfPersonalGenerado = "";
                    this.idAlumnoPdfPersonal = 0;
                    this.idGrupoPdfPersonal = 0;
                }
                // --- FIN DE CAMBIOS ---
            }
            catch (Exception ex)
            {
                // Limpiar variables si hay error
                this.rutaPdfPersonalGenerado = "";
                this.idAlumnoPdfPersonal = 0;
                this.idGrupoPdfPersonal = 0;
                MessageBox.Show("Error al generar la boleta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelito4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEdicionDatos_Click(object sender, EventArgs e)
        {
            Mod_Modificacion Modificacion = new Mod_Modificacion(rolUsuario);
            Modificacion.Show();
            this.Hide();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            Mod_Estadisticas estadisticas = new Mod_Estadisticas(rolUsuario);
            estadisticas.Show();
            this.Hide();
        }

        // ---- INICIO: LÓGICA DE ENVÍO DE CORREO ----

        private void btnEnviarcorreo_Click(object sender, EventArgs e)
        {
            // --- (Verificaciones 1, 2 y 3: que el PDF exista y los combos estén seleccionados) ---
            if (string.IsNullOrWhiteSpace(this.rutaPdfGrupalGenerado))
            {
                MessageBox.Show("Primero debes generar la boleta grupal (Botón 'Generar Boletas').", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!File.Exists(this.rutaPdfGrupalGenerado))
            {
                MessageBox.Show($"El archivo PDF no se encuentra en la ruta esperada:\n{this.rutaPdfGrupalGenerado}\n\nPor favor, genera la boleta de nuevo.", "Archivo no Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.rutaPdfGrupalGenerado = "";
                return;
            }
            if (cmbGrup.SelectedItem == null || cmbTrimestreGrup.SelectedItem == null)
            {
                MessageBox.Show("Selecciona el grupo y el trimestre para enviar la boleta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // --- 4. Obtener datos del formulario ---
                int idGrupo = (int)((ComboboxItem)cmbGrup.SelectedItem).Value;
                string trimestre = cmbTrimestreGrup.SelectedItem.ToString();
                string nombreGrupo = ((ComboboxItem)cmbGrup.SelectedItem).Text;
                string idReferencia = $"Grupo_{idGrupo}"; // ID para el tracking

                // --- 5. Obtener el correo del profesor desde la BD ---
                string emailProfesor = ObtenerEmailProfesor(idGrupo);

                if (string.IsNullOrWhiteSpace(emailProfesor))
                {
                    MessageBox.Show($"No se pudo encontrar un correo electrónico para el profesor del grupo '{nombreGrupo}'. Revisa la base de datos.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ====================================================================
                // ¡NUEVO! PASO 5.5: VERIFICACIÓN DE ENVÍO PREVIO
                // ====================================================================
                bool yaEnviado = VerificarEnvioPrevio("Boleta Grupal", idReferencia, trimestre, emailProfesor);

                if (yaEnviado)
                {
                    // CAMBIO: En lugar de un "no" rotundo, preguntamos al usuario.
                    // Esto cumple con "evitar" envíos dobles, pero permite un reenvío
                    // intencional si el profesor lo borró, por ejemplo.
                    DialogResult respuesta = MessageBox.Show(
                        $"ADVERTENCIA: El sistema muestra que esta boleta ({trimestre}) ya fue enviada a {emailProfesor}.\n\n" +
                        "¿Estás seguro de que deseas enviarla de nuevo?",
                        "Posible Envío Duplicado",
                        MessageBoxButtons.YesNo, // <-- Botones de Sí y No
                        MessageBoxIcon.Question
                    );

                    // Si el usuario presiona "No", detenemos todo.
                    if (respuesta == DialogResult.No)
                    {
                        MessageBox.Show("Envío cancelado por el usuario.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // <-- Detiene la ejecución del método
                    }
                    // Si presiona "Sí", el código simplemente continúa.
                }

                // --- 6. Preparar y enviar el correo ---
                string asunto = $"Boletas Grupales - {nombreGrupo} - {trimestre}";
                string cuerpo = $"Estimado profesor,\n\nSe adjuntan las boletas grupales del {trimestre} para el grupo {nombreGrupo}.\n\nSaludos cordiales,\nDirección.";

                this.Cursor = Cursors.WaitCursor;
                EnviarCorreoConAdjunto(emailProfesor, asunto, cuerpo, this.rutaPdfGrupalGenerado);
                this.Cursor = Cursors.Default;

                

                // --- 8. REGISTRAR EN BITÁCORA GENERAL ---
                BitacoraManager.RegistrarAccion($"Envío de Boleta Grupal: Grupo {idGrupo} ({trimestre}) a {emailProfesor}");

                MessageBox.Show($"Correo enviado exitosamente a: {emailProfesor}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // ... (el resto de tu código de catch no cambia) ...
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error al enviar el correo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                try
                {
                    int idGrupo = (int)((ComboboxItem)cmbGrup.SelectedItem).Value;
                    string trimestre = cmbTrimestreGrup.SelectedItem.ToString();
                    string idReferencia = $"Grupo_{idGrupo}";

                    RegistrarEnvio("Boleta Grupal", idReferencia, idGrupo, trimestre, "Error", "Fallido", "Profesor");
                }
                catch { }
            }
        }

        /// <summary>
        /// ENLACE (LECTURA): Obtiene el email del maestro de la BD.
        /// </summary>
        private string ObtenerEmailProfesor(int idGrupo)
        {
            string email = null;
            string query = @"
        SELECT m.Correo_maestro 
        FROM maestro m
        JOIN grupo g ON m.id_maestro = g.id_maestro
        WHERE g.id_grupo = @idGrupo";

            // Usamos tu clase de Conexión
            using (MySqlConnection conn = new Conexion().GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                    conn.Open();
                    object result = cmd.ExecuteScalar(); // Obtenemos solo el primer valor

                    if (result != null && result != DBNull.Value)
                    {
                        email = result.ToString();
                    }
                }
            }
            return email;
        }

        /// <summary>
        /// EJECUCIÓN: Envía el correo usando SmtpClient.
        /// </summary>
        private void EnviarCorreoConAdjunto(string destinatario, string asunto, string cuerpo, string rutaAdjunto)
        {

            string emailEmisor = "fbautistagonzaga@gmail.com";
            string passwordEmisor = "iuaascclbikaroza"; // NO USES tu contraseña normal
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;


            using (SmtpClient cliente = new SmtpClient(smtpHost, smtpPort))
            {
                cliente.EnableSsl = true; // Gmail/Outlook requieren SSL
                cliente.UseDefaultCredentials = false;
                cliente.Credentials = new NetworkCredential(emailEmisor, passwordEmisor);
                cliente.Timeout = 10000; // 10 segundos

                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress(emailEmisor, "Dirección Escolar");
                    mensaje.To.Add(destinatario);
                    mensaje.Subject = asunto;
                    mensaje.Body = cuerpo;
                    mensaje.IsBodyHtml = false;

                    // Adjuntar el archivo PDF
                    Attachment adjunto = new Attachment(rutaAdjunto);
                    mensaje.Attachments.Add(adjunto);

                    // Enviar
                    cliente.Send(mensaje);
                }
            }
        }

        /// <summary>
        /// ENLACE (ESCRITURA): Registra la acción en tu tabla 'envios_tracking'.
        /// </summary>
        private void RegistrarEnvio(string tipoDoc, string idRef, int idGrupo, string trimestre, string email, string estado, string destinatarioTipo) // <-- CAMBIO AQUÍ
        {
            string connectionString = new Conexion().GetConnection().ConnectionString;

            string queryInsert = @"
        INSERT INTO envios_tracking 
        (tipo_documento, id_referencia, id_grupo, trimestre, fecha_envio, 
         destinatario_tipo, destinatario_contacto, estado) 
        VALUES 
        (@tipoDoc, @idRef, @idGrupo, @trimestre, @fecha, 
         @tipoDest, @contacto, @estado)"; // <-- CAMBIO AQUÍ

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmdInsert = new MySqlCommand(queryInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@tipoDoc", tipoDoc);
                        cmdInsert.Parameters.AddWithValue("@idRef", idRef);
                        cmdInsert.Parameters.AddWithValue("@idGrupo", idGrupo);
                        cmdInsert.Parameters.AddWithValue("@trimestre", trimestre);
                        cmdInsert.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmdInsert.Parameters.AddWithValue("@contacto", email);
                        cmdInsert.Parameters.AddWithValue("@estado", estado);
                        cmdInsert.Parameters.AddWithValue("@tipoDest", destinatarioTipo); // <-- CAMBIO AQUÍ

                        conn.Open();
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar en envios_tracking: " + ex.Message);
            }
        }

        /// <summary>
        /// ENLACE (LECTURA): Obtiene el email del TUTOR de la BD.
        /// </summary>
        private string ObtenerEmailTutor(int idAlumno)
        {
            // ⚠️ ASUNCIÓN: Asumo que tienes una tabla 'tutores' con 'TutorID' y 'Correo_tutor'
            //             y que 'alumnos.TutorID' es la llave foránea.
            string email = null;
            string query = @"
        SELECT t.Correo
        FROM tutores t
        JOIN alumnos a ON t.TutorID = a.TutorID
        WHERE a.AlumnoID = @idAlumno";

            try
            {
                using (MySqlConnection conn = new Conexion().GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idAlumno", idAlumno);
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            email = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar email del tutor. Verifica que la tabla 'tutores' exista. " + ex.Message, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return email;
        }

        private void btnEnviarcorreoTutor_Click(object sender, EventArgs e)
        {
            // 1. Verificar que se haya generado un PDF personal
            if (string.IsNullOrWhiteSpace(this.rutaPdfPersonalGenerado))
            {
                MessageBox.Show("Primero debes generar la boleta personal (Botón 'Generar Boleta').", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Verificar que el archivo realmente exista
            if (!File.Exists(this.rutaPdfPersonalGenerado))
            {
                MessageBox.Show($"El archivo PDF no se encuentra:\n{this.rutaPdfPersonalGenerado}\n\nPor favor, genera la boleta de nuevo.", "Archivo no Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.rutaPdfPersonalGenerado = "";
                return;
            }

            // 3. Verificar que los datos de "memoria" estén cargados
            if (this.idAlumnoPdfPersonal == 0)
            {
                MessageBox.Show("No se ha seleccionado un alumno. Por favor, genera la boleta de nuevo.", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 4. Obtener datos de las variables de "memoria"
                string trimestre = this.trimestrePdfPersonal;
                string idReferencia = this.idAlumnoPdfPersonal.ToString();
                string nombreAlumno = this.nombreAlumnoPdfPersonal;
                int idGrupo = this.idGrupoPdfPersonal;

                // 5. Obtener el correo del TUTOR desde la BD
                string emailTutor = ObtenerEmailTutor(this.idAlumnoPdfPersonal);

                if (string.IsNullOrWhiteSpace(emailTutor))
                {
                    MessageBox.Show($"No se pudo encontrar un correo electrónico para el tutor del alumno '{nombreAlumno}'.\nRevisa la tabla 'tutores' en la base de datos.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 6. VERIFICACIÓN DE ENVÍO PREVIO (Reutilizamos la función)
                bool yaEnviado = VerificarEnvioPrevio("Boleta Personal", idReferencia, trimestre, emailTutor);

                if (yaEnviado)
                {
                    DialogResult respuesta = MessageBox.Show(
                        $"ADVERTENCIA: El sistema muestra que la boleta de {nombreAlumno} ({trimestre}) ya fue enviada a {emailTutor}.\n\n" +
                        "¿Estás seguro de que deseas enviarla de nuevo?",
                        "Posible Envío Duplicado",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (respuesta == DialogResult.No)
                    {
                        MessageBox.Show("Envío cancelado por el usuario.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // 7. Preparar y enviar el correo
                string asunto = $"Boleta de Calificaciones: {nombreAlumno} - {trimestre}";
                string cuerpo = $"Estimado tutor,\n\nSe adjunta la boleta de calificaciones del alumno {nombreAlumno} correspondiente al {trimestre}.\n\nSaludos cordiales,\nDirección.";

                this.Cursor = Cursors.WaitCursor;
                // Reutilizamos la misma función de envío
                EnviarCorreoConAdjunto(emailTutor, asunto, cuerpo, this.rutaPdfPersonalGenerado);
                this.Cursor = Cursors.Default;

                // 8. Registrar en la BD de tracking
                RegistrarEnvio("Boleta Personal", idReferencia, idGrupo, trimestre, emailTutor, "Enviado", "Tutor"); // <-- ¡Tipo "Tutor"!

                // 9. REGISTRAR EN BITÁCORA GENERAL
                BitacoraManager.RegistrarAccion($"Envío de Boleta Personal: Alumno {idReferencia} ({trimestre}) a {emailTutor}");

                MessageBox.Show($"Correo enviado exitosamente a: {emailTutor}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error al enviar el correo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                try
                {
                    RegistrarEnvio("Boleta Personal", this.idAlumnoPdfPersonal.ToString(), this.idGrupoPdfPersonal, this.trimestrePdfPersonal, "Error", "Fallido", "Tutor");
                }
                catch { }
            }
        }

    }
}
 
