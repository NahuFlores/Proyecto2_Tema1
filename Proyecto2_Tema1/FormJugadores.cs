using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2_Tema1
{
    public partial class FormJugadores : Form
    {
        public FormJugadores()
        {
            InitializeComponent();
            CargarJugadores();
            // Inicializar lista de equipos y actualizarla cuando cambie la edad
            CargarEquiposParaEdad();
            txtEdad.TextChanged += txtEdad_TextChanged;
        }

        // Actualiza el DataGridView con la lista actual de jugadores
        private void CargarJugadores()
        {
            dgvJugadores.DataSource = null;
            dgvJugadores.DataSource = GestorLiga.Jugadores;
        }

        // Carga en el combo los equipos compatibles con la edad indicada
        private void CargarEquiposParaEdad()
        {
            int edad;
            if (int.TryParse(txtEdad.Text.Trim(), out edad) && edad >= 6)
            {
                var compatibles = new List<Equipo>();
                foreach (var e in GestorLiga.Equipos)
                {
                    if (GestorLiga.EdadEsValida(edad, e.Categoria)) compatibles.Add(e);
                }
                cboEquipos.DataSource = null;
                cboEquipos.DataSource = compatibles;
            }
            else
            {
                cboEquipos.DataSource = null;
                cboEquipos.Items.Clear();
            }
        }

        private void txtEdad_TextChanged(object sender, EventArgs e)
        {
            CargarEquiposParaEdad();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string textoEdad = txtEdad.Text.Trim();

            if (dni == "" || nombre == "" || apellido == "" || textoEdad == "")
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (GestorLiga.DNIExiste(dni))
            {
                MessageBox.Show("Ya existe un jugador con ese DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int edad;
            bool edadValida = int.TryParse(textoEdad, out edad);

            if (!edadValida || edad < 6)
            {
                MessageBox.Show("La edad debe ser un número mayor o igual a 6.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool tieneSeguro = chkSeguro.Checked;
            bool estaAfiliado = chkAfiliado.Checked;

            try
            {
                GestorLiga.AltaJugador(dni, nombre, apellido, edad, tieneSeguro, estaAfiliado);

                // Si se seleccionó un equipo, intentar asignarlo (se valida edad internamente)
                if (cboEquipos != null && cboEquipos.SelectedItem != null)
                {
                    Equipo equipoSeleccionado = (Equipo)cboEquipos.SelectedItem;
                    bool asignado = GestorLiga.AsignarJugadorAEquipo(dni, equipoSeleccionado.Id);
                    if (!asignado)
                    {
                        MessageBox.Show("Jugador agregado pero no pudo asignarse al equipo seleccionado (requisito de edad o duplicado).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                CargarJugadores();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvJugadores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un jugador de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Jugador jugador = (Jugador)dgvJugadores.SelectedRows[0].DataBoundItem;

            if (GestorLiga.BajaJugador(jugador.DNI))
            {
                CargarJugadores();
                MessageBox.Show("Jugador eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LimpiarCampos()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            chkSeguro.Checked = false;
            chkAfiliado.Checked = false;
        }

        private void FormJugadores_Load(object sender, EventArgs e)
        {

        }
    }
}
