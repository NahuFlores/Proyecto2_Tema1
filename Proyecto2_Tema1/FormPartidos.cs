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
    public partial class FormPartidos : Form
    {
        public FormPartidos()
        {
            InitializeComponent();
            CargarCategorias();
        }

        // Carga los valores del enum Categoria en el ComboBox
        private void CargarCategorias()
        {
            cboCategoria.DataSource = Enum.GetValues(typeof(Categoria));
        }

        // Filtra y carga los equipos según la categoría seleccionada
        private void CargarEquiposPorCategoria()
        {
            Categoria categoriaSeleccionada = (Categoria)cboCategoria.SelectedItem;

            List<Equipo> equiposFiltrados = new List<Equipo>();
            foreach (Equipo e in GestorLiga.Equipos)
            {
                if (e.Categoria == categoriaSeleccionada)
                    equiposFiltrados.Add(e);
            }

            cboLocal.DataSource = null;
            cboLocal.DataSource = new List<Equipo>(equiposFiltrados);

            cboVisitante.DataSource = null;
            cboVisitante.DataSource = new List<Equipo>(equiposFiltrados);
        }

        // Carga los jugadores del equipo local en los CheckedListBox correspondientes
        private void CargarJugadoresLocal()
        {
            if (cboLocal.SelectedItem == null) return;

            Equipo equipoLocal = (Equipo)cboLocal.SelectedItem;
            List<Jugador> jugadores = GestorLiga.ObtenerJugadoresPorEquipo(equipoLocal.Id);

            clbTitularesLocal.Items.Clear();
            clbSuplentesLocal.Items.Clear();

            foreach (Jugador j in jugadores)
            {
                clbTitularesLocal.Items.Add(j);
                clbSuplentesLocal.Items.Add(j);
            }
        }

        // Carga los jugadores del equipo visitante en los CheckedListBox correspondientes
        private void CargarJugadoresVisitante()
        {
            if (cboVisitante.SelectedItem == null) return;

            Equipo equipoVisitante = (Equipo)cboVisitante.SelectedItem;
            List<Jugador> jugadores = GestorLiga.ObtenerJugadoresPorEquipo(equipoVisitante.Id);

            clbTitularesVisitante.Items.Clear();
            clbSuplentesVisitante.Items.Clear();

            foreach (Jugador j in jugadores)
            {
                clbTitularesVisitante.Items.Add(j);
                clbSuplentesVisitante.Items.Add(j);
            }
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEquiposPorCategoria();
        }

        private void cboLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarJugadoresLocal();
        }

        private void cboVisitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarJugadoresVisitante();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboLocal.SelectedItem == null || cboVisitante.SelectedItem == null)
            {
                MessageBox.Show("Seleccione ambos equipos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Equipo local = (Equipo)cboLocal.SelectedItem;
            Equipo visitante = (Equipo)cboVisitante.SelectedItem;

            if (local == visitante)
            {
                MessageBox.Show("Los equipos no pueden ser el mismo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar exactamente 5 titulares por equipo
            if (clbTitularesLocal.CheckedItems.Count != 5)
            {
                MessageBox.Show("Debe seleccionar exactamente 5 titulares para el equipo local.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clbTitularesVisitante.CheckedItems.Count != 5)
            {
                MessageBox.Show("Debe seleccionar exactamente 5 titulares para el equipo visitante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar máximo 3 suplentes por equipo
            if (clbSuplentesLocal.CheckedItems.Count > 3)
            {
                MessageBox.Show("El equipo local no puede tener más de 3 suplentes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clbSuplentesVisitante.CheckedItems.Count > 3)
            {
                MessageBox.Show("El equipo visitante no puede tener más de 3 suplentes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string horario = txtHorario.Text.Trim();
            string lugar = txtLugar.Text.Trim();

            if (horario == "" || lugar == "")
            {
                MessageBox.Show("El horario y el lugar son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int golesLocal, golesVisitante;
            bool golesLocalValidos = int.TryParse(txtGolesLocal.Text.Trim(), out golesLocal);
            bool golesVisitanteValidos = int.TryParse(txtGolesVisitante.Text.Trim(), out golesVisitante);

            if (!golesLocalValidos || !golesVisitanteValidos || golesLocal < 0 || golesVisitante < 0)
            {
                MessageBox.Show("Los goles deben ser números positivos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime fecha = dtpFecha.Value;

            Partido partido = GestorLiga.AltaPartido(local, visitante, fecha, horario, lugar);
            if (partido == null)
            {
                MessageBox.Show("No se pudo crear el partido. Verifique categorías de los equipos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir listas de jugadores seleccionados
            List<Jugador> titularesLocal = new List<Jugador>();
            List<Jugador> suplentesLocal = new List<Jugador>();
            List<Jugador> titularesVisitante = new List<Jugador>();
            List<Jugador> suplentesVisitante = new List<Jugador>();

            foreach (Jugador j in clbTitularesLocal.CheckedItems) titularesLocal.Add(j);
            foreach (Jugador j in clbSuplentesLocal.CheckedItems) suplentesLocal.Add(j);
            foreach (Jugador j in clbTitularesVisitante.CheckedItems) titularesVisitante.Add(j);
            foreach (Jugador j in clbSuplentesVisitante.CheckedItems) suplentesVisitante.Add(j);

            bool okAlineacion = GestorLiga.ConfigurarAlineacion(partido.Id, titularesLocal, suplentesLocal, titularesVisitante, suplentesVisitante);
            if (!okAlineacion)
            {
                MessageBox.Show("Alineación inválida. Verifique titulares/suplentes y pertenencia a los equipos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool okResultado = GestorLiga.RegistrarResultado(partido.Id, golesLocal, golesVisitante);
            if (!okResultado)
            {
                MessageBox.Show("No se pudo registrar el resultado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Partido registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    }
}
