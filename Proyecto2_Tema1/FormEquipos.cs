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
    public partial class FormEquipos : Form
    {
        public FormEquipos()
        {
            InitializeComponent();
            CargarCategorias();
            CargarEquipos();
        }

        // Carga los valores del enum Categoria en el ComboBox
        private void CargarCategorias()
        {
            cboCategoria.DataSource = Enum.GetValues(typeof(Categoria));
        }

        // Actualiza el DataGridView con la lista actual de equipos
        private void CargarEquipos()
        {
            dgvEquipos.DataSource = null;
            dgvEquipos.DataSource = GestorLiga.Equipos;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombreClub = txtClub.Text.Trim();

            if (nombreClub == "")
            {
                MessageBox.Show("El nombre del club no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Categoria categoria = (Categoria)cboCategoria.SelectedItem;

            GestorLiga.AltaEquipo(nombreClub, categoria);
            CargarEquipos();
            txtClub.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un equipo de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Equipo equipo = (Equipo)dgvEquipos.SelectedRows[0].DataBoundItem;

            if (GestorLiga.BajaEquipo(equipo.Id))
            {
                CargarEquipos();
                MessageBox.Show("Equipo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se puede eliminar un equipo con jugadores asignados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un equipo de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nuevoNombre = txtClub.Text.Trim();

            if (nuevoNombre == "")
            {
                MessageBox.Show("Ingrese el nuevo nombre del club.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Equipo equipo = (Equipo)dgvEquipos.SelectedRows[0].DataBoundItem;

            if (GestorLiga.ModificarEquipo(equipo.Id, nuevoNombre))
            {
                CargarEquipos();
                txtClub.Clear();
                MessageBox.Show("Equipo modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se puede modificar un equipo con jugadores asignados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
